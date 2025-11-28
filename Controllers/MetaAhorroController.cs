using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Dtos;
using SavingBack.Models;
using SavingBack.Services;

namespace SavingBack.Controllers
{
    [Route("api/metas")]
    [ApiController]
    public class MetaAhorroController : ControllerBase
    {
        private readonly MetaAhorroService metaAhorroService;
        public MetaAhorroController(MetaAhorroService metaAhorroService) 
        {
            this.metaAhorroService = metaAhorroService;
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
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("usuario/{id}")]
        public async Task<ActionResult> ObtenerMetasPorUsuario(int id)
        {
            try
            {
                var metas = await metaAhorroService.BuscarMetasActivasPorUsuarioId(id);

                return RespuestasService.Ok(metas);

            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
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
                return RespuestasService.ServerError(error.Message);
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
                    return RespuestasService.NotFound($"Meta con Id = ({id}), no encontrada.");

                return RespuestasService.Ok(meta);

            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
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
                return RespuestasService.ServerError(error.Message);
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
                    return RespuestasService.NotFound($"Meta con Id = ({id}), no encontrada");

                meta.Estado = "Cancelada";

                await metaAhorroService.Actualizar(meta);

                return RespuestasService.Ok("Meta cancelada exitosamente.");
            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
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
                    return RespuestasService.NotFound($"Meta con Id = ({id}), no encontrada");

                metaEncontrada.MontoObjetivo = meta.MontoObjetivo;
                metaEncontrada.Nombre = meta.Nombre;

                await metaAhorroService.Actualizar(metaEncontrada);

                return RespuestasService.Ok("Meta actualizada correctamente");

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }


    }
}
