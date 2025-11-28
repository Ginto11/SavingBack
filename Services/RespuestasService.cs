using Microsoft.AspNetCore.Mvc;
using SavingBack.Dtos;
using SavingBack.Models;

namespace SavingBack.Services
{
    public static class RespuestasService
    {
        public static ActionResult Created()
        {
            return new ObjectResult( new { codigo = 201, mensaje = "Registro exitoso."}) { StatusCode = 201 };
        }

        public static ActionResult BadRequest()
        {
            return new ObjectResult(new { codigo = 400, mensaje = "Complete todos los campos. Por favor" }) { StatusCode = 400 };
        }

        public static ActionResult Conflict(string mensaje)
        {
            return new ObjectResult(new { codigo = 409, mensaje }) { StatusCode = 409 };
        } 

        public static ActionResult NoContent()
        {
            return new ObjectResult(new { codigo = 204 }) { StatusCode = 204 };
        }

        public static ActionResult ServerError(string mensaje)
        {
            return new ObjectResult(new { codigo = 500, mensaje }) { StatusCode = 500 };
        }

        public static ActionResult NotFound(string mensaje)
        {
            return new ObjectResult(new { codigo = 404, mensaje }) { StatusCode = 404 };
        }

        public static ActionResult Ok(Object objeto)
        {
            return new ObjectResult(new { codigo = 200, data = objeto }) { StatusCode = 200 };
        }

        public static ActionResult InvalidCredentials(string mensaje)
        {
            return new ObjectResult(new { codigo = 401, mensaje }) { StatusCode = 401 };
        }

        public static ActionResult LoginExitoso(Usuario usuario, string token)
        {
            return new ObjectResult(new { codigo = 200, 
                data = new UsuarioLogueadoDto {
                    Id = usuario.Id,
                    PrimerNombre = usuario.PrimerNombre, 
                    Token = token,
                    Rol = usuario.Rol!,
                }
            });
        }
    }
}
