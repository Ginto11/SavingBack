using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Dtos;
using SavingBack.Models;
using SavingBack.Services;

namespace SavingBack.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ahorros")]
    public class AhorroController : ControllerBase
    {
        private readonly AhorroService ahorroService;
        private readonly MetaAhorroService metaAhorroService;
        private readonly CorreoService correoService;
        private readonly UsuarioService usuarioService;
        private readonly IHostEnvironment environment;
        private readonly EgresoService egresoService;
        private readonly IngresoService ingresoService;

        public AhorroController(IngresoService ingresoService, EgresoService egresoService, IHostEnvironment environment, AhorroService ahorroService, MetaAhorroService metaAhorroService, CorreoService correoService, UsuarioService usuarioService)
        {
            this.ingresoService = ingresoService;
            this.egresoService = egresoService;
            this.metaAhorroService = metaAhorroService;
            this.ahorroService = ahorroService;
            this.correoService = correoService;
            this.usuarioService = usuarioService;
            this.environment = environment;
        }

        [HttpPost]
        public async Task<ActionResult> Agregar(CrearAhorroDto ahorroDto)
        {
            try
            {
                var rol = User.FindFirst("Rol")?.Value;
                var usuario = await usuarioService.BuscarPorId(ahorroDto.UsuarioId);
                var totalIngreso = await ingresoService.BuscarTotalIngresoEnTipo(ahorroDto.TipoAhorro, ahorroDto.UsuarioId);
                var totalEgreso = await egresoService.BuscarTotalEgresoEnTipo(ahorroDto.TipoAhorro, ahorroDto.UsuarioId);

                var ahorro = new Ahorro
                {
                    UsuarioId = ahorroDto.UsuarioId,
                    Descripcion = ahorroDto.Descripcion,
                    MetaAhorroId = ahorroDto.MetaAhorroId,
                    Monto = ahorroDto.Monto,
                    TipoAhorro = ahorroDto.TipoAhorro
                };

                var egreso = new EgresoDto
                {
                    Monto = Decimal.ToInt32(ahorroDto.Monto),
                    CategoriaGastoId = 13,
                    Tipo = ahorroDto.TipoAhorro,
                    UsuarioId = ahorroDto.UsuarioId
                };


                var meta = await metaAhorroService.ObtenerPorId(ahorroDto.MetaAhorroId);

                if (meta is null)
                    return RespuestasService.ErrorModelo(this, $"Meta con Id = ({ahorroDto.MetaAhorroId}), no encontrada.", 404);

                if ((ahorroDto.Monto + meta.MontoActual) > meta.MontoObjetivo)
                {
                    var diferencia = meta.MontoObjetivo - meta.MontoActual;
                    return RespuestasService.ErrorModelo(this, $"Por favor ingrese el valor exacto para cumplir la meta. Serian ${diferencia:N0} pesos.", 409);
                }

                if (ahorroDto.TipoAhorro == "")
                    return RespuestasService.ErrorModelo(this, "El campo Tipo Ahorro es requerido.", 400);

                if (((totalIngreso - totalEgreso) - ahorroDto.Monto) < 0)
                    return RespuestasService.ErrorModelo(this, $"Fondos insuficientes en ({ahorroDto.TipoAhorro})", 409);

                if (meta.MontoActual is null)
                {
                    meta.MontoActual = ahorro.Monto;
                    await egresoService.Insertar(egreso);
                } 
                else
                {
                    meta.MontoActual += ahorro.Monto;
                    await egresoService.Insertar(egreso);
                }

                if (meta.MontoActual >= meta.MontoObjetivo)
                {
                    meta.Estado = "Cumplida";
                }

                await ahorroService.Insertar(ahorro);
                await metaAhorroService.Actualizar(meta);

                var infoMensaje = new InfoMensaje
                {
                    NombreUsuariAhorro = usuario!.PrimerNombre,
                    DescripcionAhorro = ahorroDto.Descripcion!,
                    MontoAhorro = ahorroDto.Monto,
                    NombreMetaAhorro = meta.Nombre,
                    FechaAhorro = DateTime.Now
                };


                if (environment.IsProduction())
                {
                    if (rol == "Admin")
                    {
                        await correoService.MensajeAdministradores(infoMensaje);
                    }
                    else { await correoService.MensajeClientes(infoMensaje, usuario.Correo); }
                }

                return RespuestasService.Created();

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("usuario/{id}")]
        public async Task<ActionResult> TodosPorUsuarioId(int paginaActual, int tamanoPagina, int id)
        {
            try
            {
                if (paginaActual == 0)
                    return RespuestasService.ErrorModelo(this, "El numero de pagina debe ser mayor a 0", 409);


                var resultadoPagina = await ahorroService.ObtenerTodosLosAhorrosPaginadosPorUsuarioId(id, paginaActual, tamanoPagina);

                return RespuestasService.Ok(resultadoPagina);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("usuario/cantidades/{id}")]
        public async Task<ActionResult> ObtenerCantidadesTotales(int id)
        {
            try
            {

                var cantidades = await ahorroService.ObtenerCantidadesTotalesPorUsuarioId(id);


                return RespuestasService.Ok(cantidades);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("usuario/ultimos-movimientos/{id}")]
        public async Task<ActionResult> ObtenerUltimosMovimientos(int id)
        {
            try
            {

                var ultimos = await ahorroService.ObtenerUltimosMovimientosPorUsuarioId(id);


                return RespuestasService.Ok(ultimos);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> EliminarAhorro(int id)
        {
            try
            {
                var ahorroABorrar = await ahorroService.BuscarPorId(id);

                if (ahorroABorrar == null)
                    return RespuestasService.ErrorModelo(this, $"Ahorro no encontrado.", 404);

                var metaPorActualizar = await metaAhorroService.ObtenerPorId(ahorroABorrar.MetaAhorroId);


                if (metaPorActualizar == null)
                    return RespuestasService.ErrorModelo(this, $"Meta no encontrada.", 404);

                if (metaPorActualizar.Estado == "Cumplida")
                    return RespuestasService.ErrorModelo(this, $"No se puede eliminar un ahorro de una meta cumplida.", 409);

                metaPorActualizar.MontoActual -= ahorroABorrar.Monto;

                await ahorroService.Eliminar(ahorroABorrar);
                await metaAhorroService.Actualizar(metaPorActualizar);

                return RespuestasService.NoContent();

            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("descripcion/{id}")]
        public async Task<ActionResult> ObtenerAhorrosPorDescripcion(int paginaActual, int tamanoPagina, int id, string descripcion)
        {
            try
            {
                var ahorros = await ahorroService.ObtenerAhorrosPorDescripcionPaginadosPorUsuarioId(id, paginaActual, tamanoPagina, descripcion);

                return RespuestasService.Ok(ahorros);
            }
            catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }
    }
}
