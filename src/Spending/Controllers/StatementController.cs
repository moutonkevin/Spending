using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Spending.Api.Services;

namespace Spending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatementController : ControllerBase
    {
        private readonly ILogger<StatementController> _logger;
        private readonly IStatementProcessor _statementProcessor;

        public StatementController(
            ILogger<StatementController> logger,
            IStatementProcessor statementProcessor)
        {
            _logger = logger;
            _statementProcessor = statementProcessor;
        }

        [HttpPost]
        public async Task ProcessDocuments()
        {
            if (HttpContext.Request.Form.Files.Any())
            {
                await _statementProcessor.ProcessAsync(HttpContext.Request.Form.Files);
            }
        }
    }
}