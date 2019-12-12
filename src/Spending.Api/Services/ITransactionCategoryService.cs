using System.Collections.Generic;
using System.Threading.Tasks;
using Spending.Database.Entities;

namespace Spending.Api.Services
{
    public interface ITransactionCategoryService
    {
        Task<IEnumerable<Transaction>> GetUncategorizedTransactions(int userId);
        Task<bool> CategorizeAndSaveTransactionAsync(Transaction transaction, string descriptionContent, int categoryId, int userId);
    }
}