using System.Collections.Generic;
using System.Threading.Tasks;
using Spending.Api.Models;

namespace Spending.Api.Services
{
    public interface ITransactionCategoryPatternService
    {
        Task<bool> SavePatternAsync(string descriptionContent, int categoryId, int userId);
        Task<IList<TransactionCategoryPattern>> GetAllPatternsAsync(int userId);
    }
}