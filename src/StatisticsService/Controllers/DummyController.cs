using Microsoft.AspNetCore.Mvc;

namespace StatisticsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DummyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("pong from StatisticsService");
    }
}
