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
        private readonly ITransactionDataAccess _transactionDataAccess;
        private readonly ITransactionCategoryDatabaseDataAccess _transactionCategoryDatabaseDataAccess;

        public TransactionCategoryPatternService(ITransactionCategoryPatternDatabaseDataAccess dataAccess,
            ITransactionDataAccess transactionDataAccess,
            ITransactionCategoryDatabaseDataAccess transactionCategoryDatabaseDataAccess)
        {
            _dataAccess = dataAccess;
            _transactionDataAccess = transactionDataAccess;
            _transactionCategoryDatabaseDataAccess = transactionCategoryDatabaseDataAccess;
        }

        public async Task<bool> SavePatternAsync(string pattern, int categoryId, int userId)
        {
            var patternId = await _dataAccess.SavePatternAsync(pattern, categoryId, userId);
            if (patternId == null)
                return false;

            var transactions = await _transactionDataAccess.GetTransactionsSatisfyingPattern(userId, patternId.Value);
            var areCategoriesSaved = await _transactionCategoryDatabaseDataAccess.SaveTransactionCategories(transactions, categoryId);

            return areCategoriesSaved;
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
