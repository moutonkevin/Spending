using System.Collections.Generic;
using System.Threading.Tasks;
using Spending.Database.Entities;

namespace Spending.Api.DataAccess
{
    public interface ITransactionDataAccess
    {
        Task<bool> SaveAsync(IEnumerable<Transaction> transactions);
        Task<IEnumerable<Transaction>> GetUncategorizedTransactions(int userId);
        Task<IEnumerable<Transaction>> GetAllTransactions(int userId, int? bankId, int? accountId, int? categoryId, string description);
        Task<IEnumerable<Transaction>> GetTransactionsSatisfyingPattern(int userId, int patternId);
    }
}
