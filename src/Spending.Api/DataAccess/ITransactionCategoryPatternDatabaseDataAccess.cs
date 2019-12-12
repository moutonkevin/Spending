using System.Collections.Generic;
using System.Threading.Tasks;
using Spending.Database.Entities;

namespace Spending.Api.DataAccess
{
    public interface ITransactionCategoryPatternDatabaseDataAccess
    {
        Task<IEnumerable<Transaction>> GetUncategorizedTransactions(int userId);
        Task<bool> SaveTransactionCategoryPattern(Transaction transaction, string descriptionContent, int categoryId, int userId);
    }
}