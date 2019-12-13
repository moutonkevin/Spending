using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spending.Api.Services;

namespace Spending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCategoryPatternController : ControllerBase
    {
        private readonly ITransactionCategoryPatternService _transactionCategoryPatternService;

        public TransactionCategoryPatternController(ITransactionCategoryPatternService transactionCategoryPatternService)
        {
            _transactionCategoryPatternService = transactionCategoryPatternService;
        }

        [HttpPost]
        public async Task<IActionResult> SavePatternAsync(string descriptionContent, int categoryId, int userId)
        {
            var isSuccess = await _transactionCategoryPatternService.SavePatternAsync(descriptionContent, categoryId, userId);

            return Ok(new
            {
                IsSuccess = isSuccess
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatternsAsync(int userId)
        {
            var patterns = await _transactionCategoryPatternService.GetAllPatternsAsync(userId);

            return Ok(patterns);
        }
    }
}