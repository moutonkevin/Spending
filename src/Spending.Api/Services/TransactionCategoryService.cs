using System.Threading.Tasks;
using Spending.Api.DataAccess;
using Spending.Database.Entities;

namespace Spending.Api.Services
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private readonly ITransactionCategoryDatabaseDataAccess _dataAccess;

        public TransactionCategoryService(ITransactionCategoryDatabaseDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task CategorizeAndSaveTransactionAsync(Transaction transaction, string descriptionContent, int categoryId, int userId)
        {
            await _dataAccess.SaveTransactionCategory(transaction, descriptionContent, categoryId, userId);
        }
    }
}
