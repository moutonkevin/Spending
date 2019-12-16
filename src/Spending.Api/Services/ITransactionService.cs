using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Spending.Api.Models;

namespace Spending.Api.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Spending.Database.Entities.Transaction>> SaveAsync(StatementMetadata statementMetadata, IFormFileCollection files);
        Task<IEnumerable<Transaction>> GetUncategorizedTransactions(int userId);
        Task<IEnumerable<Transaction>> GetAllTransactions(int userId);
    }
}