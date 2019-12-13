using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spending.Api.Models;
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
        public async Task<IActionResult> SavePatternAsync([FromBody]TransactionCategoryPattern transactionCategoryPattern, int  userId)
        {
            var isSuccess = await _transactionCategoryPatternService.SavePatternAsync(
                transactionCategoryPattern.Pattern, 
                transactionCategoryPattern.CategoryId, 
                userId);

            return Ok(isSuccess);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePatternAsync([FromBody]TransactionCategoryPattern transactionCategoryPattern, int userId)
        {
            var isSuccess = await _transactionCategoryPatternService.UpdatePatternAsync(
                 transactionCategoryPattern.Id,
                 transactionCategoryPattern.Pattern, 
                 transactionCategoryPattern.CategoryId, 
                 userId);

            return Ok(isSuccess);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePatternAsync(int transactionCategoryPatternId)
        {
            var isSuccess = await _transactionCategoryPatternService.DeletePatternAsync(transactionCategoryPatternId);

            return Ok(isSuccess);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatternsAsync(int userId)
        {
            var patterns = await _transactionCategoryPatternService.GetAllPatternsAsync(userId);

            return Ok(patterns);
        }
    }
}