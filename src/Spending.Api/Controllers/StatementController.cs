using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spending.Api.Models;
using Spending.Api.Services;

namespace Spending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatementController : ControllerBase
    {
        private readonly ILogger<StatementController> _logger;
        private readonly ITransactionService _transactionService;

        public StatementController(
            ILogger<StatementController> logger,
            ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task ProcessDocuments()
        {
            if (HttpContext.Request.Form.Files.Any())
            {
                var files = HttpContext.Request.Form.Files;
                var statementMetadata = JsonConvert.DeserializeObject<StatementMetadata>(HttpContext.Request.Form
                    .ToArray()
                    .Where(form => form.Key.Equals("statementMetadata"))
                    .Select(s => s.Value)
                    .ToList()[0]);

                await _transactionService.SaveAsync(statementMetadata, files);
            }
        }
    }
}