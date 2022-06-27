using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exchange_rate_hangfire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            // NOP:
            await Task.CompletedTask;

            return Ok("Hangfire is working...");
        }
    }
}
