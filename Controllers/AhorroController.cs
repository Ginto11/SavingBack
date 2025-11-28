using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Dtos;
using SavingBack.Models;
using SavingBack.Services;

namespace SavingBack.Controllers
{
    [Route("api/ahorros")]
    [ApiController]
    public class AhorroController : ControllerBase
    {
        private readonly AhorroService ahorroService;
        private readonly MetaAhorroService metaAhorroService;
        private readonly CorreoService correoService;
        private readonly UsuarioService usuarioService;
        private readonly IHostEnvironment environment;

        public AhorroController(IHostEnvironment environment, AhorroService ahorroService, MetaAhorroService metaAhorroService, CorreoService correoService, UsuarioService usuarioService)
        {   
            this.metaAhorroService = metaAhorroService;
            this.ahorroService = ahorroService;
            this.correoService = correoService;
            this.usuarioService = usuarioService;
            this.environment = environment;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Agregar(CrearAhorroDto ahorroDto)
        {
            try
            {
                var rol = User.FindFirst("Rol")?.Value;
                var usuario = await usuarioService.BuscarPorId(ahorroDto.UsuarioId);

                var ahorro = new Ahorro
                {
                    UsuarioId = ahorroDto.UsuarioId,
                    Descripcion = ahorroDto.Descripcion,
                    MetaAhorroId = ahorroDto.MetaAhorroId,
                    Monto = ahorroDto.Monto
                };

                var meta = await metaAhorroService.ObtenerPorId(ahorroDto.MetaAhorroId);

                if (meta is null)
                    return RespuestasService.NotFound($"Meta con Id = ({ahorroDto.MetaAhorroId}), no encontrada.");

                if ((ahorroDto.Monto + meta.MontoActual) > meta.MontoObjetivo)
                    return RespuestasService.Conflict($"Por favor ingrese el valor exacto para cumplir la meta.");

                if(meta.MontoActual is null)
                {
                    meta.MontoActual = ahorro.Monto;
                } 
                else
                {
                    meta.MontoActual += ahorro.Monto;
                }

                if(meta.MontoActual >= meta.MontoObjetivo)
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
                    NombreMetaAhorro = meta.Nombre
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
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("usuario/{id}")]
        public async Task<ActionResult> TodosPorUsuarioId(int id)
        {
            try
            {

                var ahorros = await ahorroService.ObtenerTodosPorUsuarioId(id);


                return RespuestasService.Ok(ahorros);

            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
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
                return RespuestasService.ServerError(error.Message);
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
                return RespuestasService.ServerError(error.Message);
            }
        }
    }
}
