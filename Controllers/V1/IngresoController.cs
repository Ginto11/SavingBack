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
    [Route("api/v{version:apiVersion}/ingresos")]
    public class IngresoController : ControllerBase
    {

        private readonly IngresoService ingresoService;
        private readonly EgresoService egresoService;

        public IngresoController(IngresoService ingresoService, EgresoService egresoService)
        {
            this.ingresoService = ingresoService;
            this.egresoService = egresoService;
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarIngreso(IngresoDto ingreso)
        {
            try
            {
                await ingresoService.Insertar(ingreso);

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
                var resultado = await ingresoService.ObtenerTiposTotalesIngresos(id);

                return RespuestasService.Ok(resultado);
            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("usuario/{id}")]
        public async Task<ActionResult> ListarIngresos(int id)
        {
            try {
                var lista = await ingresoService.ListaDeIngresos(id);

                return RespuestasService.Ok(lista);
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> EliminarIngreso(int id, string tipo, int usuarioId)
        {
            try
            {
                var ingreso = await ingresoService.BuscarPorId(id);
                var totalIngresos = await ingresoService.BuscarTotalIngresoEnTipo(tipo, usuarioId);
                var totalEgresos = await egresoService.BuscarTotalEgresoEnTipo(tipo, usuarioId);
                var totalValor = totalIngresos - totalEgresos;

                if (ingreso is null)
                    return RespuestasService.ErrorModelo(this, $"Ingreso con Id ({id}), no encontrado.", 404);

                if ((totalValor - ingreso.Monto) < 0)
                    return RespuestasService.ErrorModelo(this, "No puedes eliminar el siguiente registro, quedarias en negativo.", 409);

                await ingresoService.Eliminar(ingreso);

                return RespuestasService.Ok("Registro eliminado exitosamente.");

            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

    }
}
