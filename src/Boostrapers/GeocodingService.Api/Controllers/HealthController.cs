using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeocodingService.Api.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Health() => Ok();
    }
}
