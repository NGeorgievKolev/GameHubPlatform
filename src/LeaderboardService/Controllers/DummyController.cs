
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DummyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("pong from LeaderboardService");
    }
}
