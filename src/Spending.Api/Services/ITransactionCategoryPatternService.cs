using System.Collections.Generic;
using System.Threading.Tasks;
using Spending.Api.Models;

namespace Spending.Api.Services
{
    public interface ITransactionCategoryPatternService
    {
        Task<bool> SavePatternAsync(string pattern, int categoryId, int userId);
        Task<bool> UpdatePatternAsync(int id, string pattern, int categoryId, int userId);
        Task<bool> DeletePatternAsync(int id);
        Task<IList<TransactionCategoryPattern>> GetAllPatternsAsync(int userId);
    }
}