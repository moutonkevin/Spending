using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spending.Api.Models;
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
        [Route("GetUncategorizedTransactions")]
        public async Task<IActionResult> GetUncategorizedTransactions(int userId)
        {
            var uncategorizedTransactions = await _transactionService.GetUncategorizedTransactions(userId);

            return Ok(uncategorizedTransactions.ToList());
        }

        [HttpGet]
        [Route("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions([FromQuery]GetTransactionsRequest request)
        {
            var transactions = await _transactionService.GetAllTransactions(
                request.UserId, 
                request.BankId, 
                request.AccountId, 
                request.CategoryId, 
                request.Description);

            return Ok(transactions);
        }
    }
}