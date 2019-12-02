using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Spending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatementController : ControllerBase
    {
        private readonly ILogger<StatementController> _logger;

        public StatementController(ILogger<StatementController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task Upload()
        {
            if (HttpContext.Request.Form.Files.Any())
            {
                foreach (var file in HttpContext.Request.Form.Files)
                {
                    var ms = new MemoryStream();

                    await file.CopyToAsync(ms);
                    var fileContentString = Encoding.ASCII.GetString(ms.ToArray());
                }
            }
        }
    }
}