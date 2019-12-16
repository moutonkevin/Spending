using System.Collections.Generic;
using System.Threading.Tasks;
using Spending.Database.Entities;

namespace Spending.Api.DataAccess
{
    public interface ITransactionCategoryDatabaseDataAccess
    {
        Task<bool> SaveTransactionCategories(IEnumerable<Transaction> transactions, int categoryId);
    }
}