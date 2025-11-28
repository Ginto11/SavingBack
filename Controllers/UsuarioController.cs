using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Models;
using SavingBack.Services;
using SavingBack.Utilities;

namespace SavingBack.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService usuarioService;
        private readonly Utilidad utilidadService;

        public UsuarioController(UsuarioService usuarioService, Utilidad utilidadService)
        {
            this.utilidadService = utilidadService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerTodos()
        {
            try
            {
                var usuarios = await usuarioService.BuscarTodos();

                return Ok(usuarios);
            }catch(Exception error)
            {
                return StatusCode(500, new { mensaje = error.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Nuevo(Usuario usuario)
        {
            try
            {
                usuario.Rol = "Cliente";
                usuario.Contrasena = utilidadService.Encriptar(usuario.Contrasena);

                await usuarioService.Insertar(usuario);

                return RespuestasService.Created();
            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }
    }   
}
