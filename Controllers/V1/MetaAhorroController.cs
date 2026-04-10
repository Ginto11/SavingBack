using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SavingBack.Dtos;
using SavingBack.Models;
using SavingBack.Services;

namespace SavingBack.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/metas")]
    [ApiController]
    public class MetaAhorroController : ControllerBase
    {
        private readonly MetaAhorroService metaAhorroService;
        public MetaAhorroController(MetaAhorroService metaAhorroService) 
        {
            this.metaAhorroService = metaAhorroService;
        }

        [HttpGet]
        [Route("busqueda/{id}")]
        public async Task<ActionResult> BuscarMetaPorNombre([FromQuery] string nombre, int id)
        {
            try
            {

                if (nombre.IsNullOrEmpty())
                    return RespuestasService.ErrorModelo(this, "El campo Nombre es obligatorio.", 400);

                var metasBuscadas = await metaAhorroService.ObtenerMetaPorNombre(nombre, id);

                return RespuestasService.Ok(metasBuscadas);
            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Nueva(MetaAhorro meta)
        {
            try
            {
                meta.Estado = "Activa";

                await metaAhorroService.Insertar(meta);

                return RespuestasService.Created();

            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("usuario/{id}/activas")]
        public async Task<ActionResult> ObtenerMetasPorUsuario(int id)
        {
            try
            {
                var metas = await metaAhorroService.BuscarMetasActivasPorUsuarioId(id);

                return RespuestasService.Ok(metas);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("usuario/{id}/todas")]
        public async Task<ActionResult> ObtenerTodasLasMetasPorUsuarioId(int id)
        {
            try
            {
                var metas = await metaAhorroService.BuscarTodasLasMetasPorUsuarioId(id);

                return RespuestasService.Ok(metas);
            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> ObtenerMetaPorId(int id)
        {
            try
            {
                var meta = await metaAhorroService.ObtenerPorId(id);

                if (meta is null)
                    return RespuestasService.ErrorModelo(this, $"Meta con Id = ({id}), no encontrada.", 404);

                return RespuestasService.Ok(meta);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("progreso/usuario/{id}")]
        public async Task<ActionResult> ObtenerMetasActivasConProgresoPorUsuarioId(int id)
        {
            try
            {
                var metas = await metaAhorroService.BuscarMetasActivasConProgresoPorUsuarioId(id);

                return RespuestasService.Ok(metas);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpDelete]
        [Route("cancelar/{id}")]
        public async Task<ActionResult> Cancelar(int id)
        {
            try
            {
                var meta = await metaAhorroService.ObtenerPorId(id);

                if (meta is null)
                    return RespuestasService.ErrorModelo(this, $"Meta con Id = ({id}), no encontrada", 404);
                
                if (meta.MontoActual > 0)
                    return RespuestasService.ErrorModelo(this, "La meta actual no se puede eliminar, ya que cuenta con ahorros.", 409);


                meta.Estado = "Cancelada";

                await metaAhorroService.Actualizar(meta);

                return RespuestasService.Ok("Meta cancelada exitosamente.");
            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> ActualizarMeta(int id, ActualizarMetaDto meta)
        {
            try
            {

                var metaEncontrada = await metaAhorroService.ObtenerPorId(id);

                if (metaEncontrada is null)
                    return RespuestasService.ErrorModelo(this, $"Meta con Id = ({id}), no encontrada", 404);

                metaEncontrada.MontoObjetivo = meta.MontoObjetivo;
                metaEncontrada.Nombre = meta.Nombre;

                await metaAhorroService.Actualizar(metaEncontrada);

                return RespuestasService.Ok("Meta actualizada correctamente");

            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("estado/usuario/{id}")]
        public async Task<ActionResult> ObtenerMetasCumplidasPorUsuarioId(int id, [FromQuery] string estado)
        {
            try
            {
                var metasCumplidas = await metaAhorroService.MetasCumplidasPorUsuarioId(id, estado);

                return RespuestasService.Ok(metasCumplidas);

            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }


    }
}
