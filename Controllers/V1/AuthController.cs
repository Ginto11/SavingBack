using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Dtos;
using SavingBack.Services;

namespace SavingBack.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;
        private readonly UsuarioService usuarioService;

        public AuthController(AuthService authService, UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
            this.authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(UsuarioLoginDto usuarioLogim)
        {
            try
            {

                var usuario = await usuarioService.BuscarPorContrasenaYUsuario(usuarioLogim);

                if (usuario is null)
                    return RespuestasService.ErrorModelo(this, "Credenciales incorrectas", 401);

                var token = authService.GenerarToken(usuario);

                return RespuestasService.LoginExitoso(usuario, token);
                
            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }


    }
}
