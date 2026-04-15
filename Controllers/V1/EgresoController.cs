using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Dtos;
using SavingBack.Models;
using SavingBack.Services;

namespace SavingBack.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/egresos")]
    public class EgresoController : ControllerBase
    {
        private readonly EgresoService egresoService;
        private readonly IngresoService ingresoService;

        public EgresoController(EgresoService egresoService, IngresoService ingresoService)
        {
            this.egresoService = egresoService;
            this.ingresoService = ingresoService;
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarEgreso(EgresoDto egreso)
        {
            try
            {

                var totalIngreso = await ingresoService.BuscarTotalIngresoEnTipo(egreso.Tipo, egreso.UsuarioId);

                var totalEgreso = await egresoService.BuscarTotalEgresoEnTipo(egreso.Tipo, egreso.UsuarioId);

                if (((totalIngreso - totalEgreso) - egreso.Monto) < 0)
                    return RespuestasService.ErrorModelo(this, $"No puede retirar mas de lo que tiene en ({egreso.Tipo})", 409);
                    
                await egresoService.Insertar(egreso);

                return RespuestasService.Created();

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("totales/usuario/{id}")]
        public async Task<ActionResult> ObtenerTotales(int id)
        {
            try
            {
                var resultado = await egresoService.ObtenerTiposTotalesEgresos(id);

                return RespuestasService.Ok(resultado);
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("usuario/{id}")]
        public async Task<ActionResult> ListarEgresos(int id)
        {
            try
            {
                var lista = await egresoService.ListaDeEgresos(id);

                return RespuestasService.Ok(lista);
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> EliminarEgreso(int id, string tipo, int usuarioId)
        {
            try
            {
                var egreso = await egresoService.BuscarPorId(id);
                var totalIngresos = await ingresoService.BuscarTotalIngresoEnTipo(tipo, usuarioId);
                var totalEgresos = await egresoService.BuscarTotalEgresoEnTipo(tipo, usuarioId);
                var totalValor = totalIngresos - totalEgresos;

                if (egreso is null)
                    return RespuestasService.ErrorModelo(this, $"Egreso con Id ({id}), no encontrado.", 404);

                if ((totalValor - egreso.Monto) < 0)
                    return RespuestasService.ErrorModelo(this, "No puedes eliminar el siguiente registro, quedarias en negativo.", 409);

                await egresoService.Eliminar(egreso);

                return RespuestasService.Ok("Registro eliminado exitosamente.");

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }
    }
}
