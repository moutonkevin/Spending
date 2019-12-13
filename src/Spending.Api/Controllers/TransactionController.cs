using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spending.Api.Services;

namespace Spending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUncategorizedTransactions(int userId)
        {
            var uncategorizedTransactions = await _transactionService.GetUncategorizedTransactions(userId);

            return Ok(uncategorizedTransactions);
        }
    }
}