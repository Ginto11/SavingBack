using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Dtos;
using SavingBack.Models;
using SavingBack.Services;

namespace SavingBack.Controllers
{
    [Route("api/ingresos")]
    [ApiController]
    public class IngresoController : ControllerBase
    {

        private readonly IngresoService ingresoService;

        public IngresoController(IngresoService ingresoService)
        {
            this.ingresoService = ingresoService;
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

    }
}
