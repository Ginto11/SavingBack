using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Dtos;
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

        [HttpGet("{id}")]
        public async Task<ActionResult> ObtenerUsuario(int id)
        {
            try
            {
                var usuario = await usuarioService.BuscarPorId(id);

                if (usuario is null)
                    return RespuestasService.NotFound("Usuario no encontrado");

                return RespuestasService.Ok(usuario);

            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> ActualizarUsuario(int id, [FromForm] UsuarioDto usuario)
        {
            try
            {
                var usuarioExistente = await usuarioService.BuscarEntidadUsuarioPorId(id);
                if (usuarioExistente is null)
                    return RespuestasService.NotFound("Usuario no encontrado");

                usuarioExistente.PrimerNombre = usuario.PrimerNombre;
                usuarioExistente.PrimerApellido = usuario.PrimerApellido;
                usuarioExistente.Cedula = usuario.Cedula;
                usuarioExistente.Correo = usuario.Correo;
                usuarioExistente.FechaNacimiento = usuario.FechaNacimiento;
                usuarioExistente.ManejaGastos = usuario.ManejaGastos;

                if (usuario.NuevaFoto is not null)
                {
                    var nombreFoto = $"{Guid.NewGuid()} + {Path.GetExtension(usuario.NuevaFoto!.FileName)}";
                    var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Fotos", nombreFoto);
                    using var stream = new FileStream(ruta, FileMode.Create);
                    usuarioExistente.FotoPerfil = $"/Uploads/Fotos/{nombreFoto}";
                    await usuario.NuevaFoto!.CopyToAsync(stream);
                }

                await usuarioService.Actualizar(usuarioExistente);

                return RespuestasService.Ok("Usuario actualizado exitosamente");
            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }
    }   
}
