using Microsoft.AspNetCore.Mvc;

namespace Spending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok("All good");
        }
    }
}