using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SavingBack.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{versio:apiVersion}/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Home()
        {
            return Ok("Hola desde el home");
        }
    }
}
