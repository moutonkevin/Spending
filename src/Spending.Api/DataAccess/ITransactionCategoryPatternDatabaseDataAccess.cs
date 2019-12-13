using System.Collections.Generic;
using System.Threading.Tasks;
using Spending.Database.Entities;

namespace Spending.Api.DataAccess
{
    public interface ITransactionCategoryPatternDatabaseDataAccess
    {
        Task<bool> SavePatternAsync(string pattern, int categoryId, int userId);
        Task<bool> UpdatePatternAsync(int id, string pattern, int categoryId, int userId);
        Task<bool> DeletePatternAsync(int id);
        Task<IList<TransactionCategoryPattern>> GetAllPatternsAsync(int userId);
    }
}