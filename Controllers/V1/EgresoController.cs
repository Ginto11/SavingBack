using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Dtos;
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
    }
}
