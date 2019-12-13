using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spending.Api.DataAccess;
using Spending.Api.Models;

namespace Spending.Api.Services
{
    public class TransactionCategoryPatternService : ITransactionCategoryPatternService
    {
        private readonly ITransactionCategoryPatternDatabaseDataAccess _dataAccess;

        public TransactionCategoryPatternService(ITransactionCategoryPatternDatabaseDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<bool> SavePatternAsync(string pattern, int categoryId, int userId)
        {
            return await _dataAccess.SavePatternAsync(pattern, categoryId, userId);
        }

        public async Task<bool> UpdatePatternAsync(int id, string pattern, int categoryId, int userId)
        {
            return await _dataAccess.UpdatePatternAsync(id, pattern, categoryId, userId);
        }

        public async Task<bool> DeletePatternAsync(int id)
        {
            return await _dataAccess.DeletePatternAsync(id);
        }

        public async Task<IList<TransactionCategoryPattern>> GetAllPatternsAsync(int userId)
        {
            var patterns = await _dataAccess.GetAllPatternsAsync(userId);

            return patterns.Select(s => new TransactionCategoryPattern
            {
                UserId = s.UserId,
                Id = s.Id,
                CategoryId = s.CategoryId,
                Pattern = s.Pattern,
                CategoryDisplayName = s.Category.Name
            }).ToList();
        }
    }
}
