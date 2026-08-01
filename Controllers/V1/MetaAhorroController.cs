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
        [Route("{metaAhorroId}")]
        public async Task<ActionResult> ObtenerMetaPorId(int metaAhorroId)
        {
            try
            {
                var meta = await metaAhorroService.ObtenerPorId(metaAhorroId);

                if (meta is null)
                    return RespuestasService.ErrorModelo(this, $"Meta con Id = ({metaAhorroId}), no encontrada.", 404);

                return RespuestasService.Ok(meta);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        

        [HttpDelete]
        [Route("cancelar/{metaAhorroId}")]
        public async Task<ActionResult> Cancelar(int metaAhorroId)
        {
            try
            {
                var meta = await metaAhorroService.ObtenerPorId(metaAhorroId);

                if (meta is null)
                    return RespuestasService.ErrorModelo(this, $"Meta con Id = ({metaAhorroId}), no encontrada", 404);
                
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
        [Route("{metaAhorroId}")]
        public async Task<ActionResult> ActualizarMeta(int metaAhorroId, ActualizarMetaDto meta)
        {
            try
            {
                if (meta.Nombre.IsNullOrEmpty())
                    return RespuestasService.ErrorModelo(this, "El campo Nombre es requerido.", 400);

                var metaEncontrada = await metaAhorroService.ObtenerPorId(metaAhorroId);

                if (metaEncontrada is null)
                    return RespuestasService.ErrorModelo(this, $"Meta con Id = ({metaAhorroId}), no encontrada", 404);

                if (metaEncontrada.MontoActual > meta.MontoObjetivo)
                    return RespuestasService.ErrorModelo(this, "El monto objetivo no puede ser menor de lo que ya llevas ahorrado.", 409);

                if (metaEncontrada.MontoActual == meta.MontoObjetivo)
                {
                    metaEncontrada.FechaCumplimiento = DateTime.Now;
                    metaEncontrada.Estado = "Cumplida";
                }


                metaEncontrada.MontoObjetivo = meta.MontoObjetivo;
                metaEncontrada.Nombre = meta.Nombre;

                await metaAhorroService.Actualizar(metaEncontrada);

                return RespuestasService.Ok("Meta actualizada correctamente");

            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

    }
}
