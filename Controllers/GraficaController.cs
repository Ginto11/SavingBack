using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Services;

namespace SavingBack.Controllers
{
    [Route("api/grafica")]
    [ApiController]
    public class GraficaController : ControllerBase
    {
        private readonly GraficaService graficaService;

        public GraficaController(GraficaService graficaService)
        {
            this.graficaService = graficaService;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> ObtenerData(int id)
        {
            try
            {
                var ahorros = await graficaService.ObtenerAhorroCompletoPorDia(id);

                return RespuestasService.Ok(ahorros);
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }
    }
}
