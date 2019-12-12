using System.Collections.Generic;
using System.Threading.Tasks;
using Spending.Api.DataAccess;
using Spending.Database.Entities;

namespace Spending.Api.Services
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private readonly ITransactionCategoryPatternDatabaseDataAccess _dataAccess;

        public TransactionCategoryService(ITransactionCategoryPatternDatabaseDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Transaction>> GetUncategorizedTransactions(int userId)
        {
            return await _dataAccess.GetUncategorizedTransactions(userId);
        }

        public async Task<bool> CategorizeAndSaveTransactionAsync(Transaction transaction, string descriptionContent, int categoryId, int userId)
        {
            return await _dataAccess.SaveTransactionCategoryPattern(transaction, descriptionContent, categoryId, userId);
        }
    }
}
