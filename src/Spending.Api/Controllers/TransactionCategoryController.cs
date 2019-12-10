using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spending.Api.Services;
using Spending.Database.Entities;

namespace Spending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCategoryController : ControllerBase
    {
        private readonly ITransactionCategoryService _transactionCategoryService;

        public TransactionCategoryController(ITransactionCategoryService transactionCategoryService)
        {
            _transactionCategoryService = transactionCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CategorizeTransaction([FromBody]Transaction transaction, string descriptionContent, int categoryId, int userId)
        {
            var isSuccess = await _transactionCategoryService.CategorizeAndSaveTransactionAsync(transaction, descriptionContent, categoryId, userId);

            return Ok(new
            {
                IsSuccess = isSuccess
            });
        }
    }
}