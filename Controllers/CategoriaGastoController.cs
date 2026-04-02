using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Services;

namespace SavingBack.Controllers
{
    [Authorize]
    [Route("api/categoria-gasto")]
    [ApiController]
    public class CategoriaGastoController : ControllerBase
    {
        private readonly CategoriaGastoService categoriaGastoService;

        public CategoriaGastoController(CategoriaGastoService categoriaGastoService)
        {
            this.categoriaGastoService = categoriaGastoService;
        }

        [HttpGet]
        public async Task<ActionResult> ListarCategoriasGastos()
        {
            try
            {
                var categorias = await categoriaGastoService.ObtenerCategoriasGastos();

                return RespuestasService.Ok(categorias);
            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }
        
    }
}
