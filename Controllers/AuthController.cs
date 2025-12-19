using Microsoft.AspNetCore.Mvc;
using SavingBack.Dtos;
using SavingBack.Services;

namespace SavingBack.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController
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
                if (usuarioLogim.Usuario.Trim() == "" && usuarioLogim.Contrasena.Trim() == "")
                    return RespuestasService.BadRequest();

                var usuario = await usuarioService.BuscarPorContrasenaYUsuario(usuarioLogim);

                if (usuario is null)
                    return RespuestasService.InvalidCredentials("Credenciales incorrectas.");

                var token = authService.GenerarToken(usuario);

                return RespuestasService.LoginExitoso(usuario, token);
                
            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpPost]
        [Route("validar-token")]
        public ActionResult ValidarToken([FromHeader(Name = "Authorization")] string? bearerToken)
        {
            try
            {
                if (string.IsNullOrEmpty(bearerToken) || bearerToken.StartsWith("Bearer "))
                    return RespuestasService.TokenInvalido("Token no enviado o mal formado.");

                var tokenJWT = bearerToken.Substring("Bearer ".Length);

                var validacion = authService.ValidarJWT(tokenJWT);
                if (validacion == null)
                    return RespuestasService.TokenInvalido("Token expirado, inicie sesión nuevamente.");

                return RespuestasService.TokenValido();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
