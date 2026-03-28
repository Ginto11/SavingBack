using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Dtos;
using SavingBack.Services;

namespace SavingBack.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
                    return RespuestasService.ErrorModelo(this, "Credenciales incorrectas");

                var token = authService.GenerarToken(usuario);

                return RespuestasService.LoginExitoso(usuario, token);
                
            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message);
            }
        }

        [HttpGet]
        [Route("validar_token")]
        public IActionResult ValidarToken([FromHeader(Name = "Authorization")] string? bearerToken)
        {
            try
            {
                if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
                    return RespuestasService.ErrorModelo(this, "Token no enviado o mal formado.");

                var tokenJWT = bearerToken.Substring("Bearer ".Length);

                var validacion = authService.ValidarJWT(tokenJWT);
                if (validacion == null)
                {
                    return RespuestasService.ErrorModelo(this, "Token expirado, inicie sesión nuevamente.");
                }

                return RespuestasService.TokenValido();

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message);
            }
        }
        

    }
}
