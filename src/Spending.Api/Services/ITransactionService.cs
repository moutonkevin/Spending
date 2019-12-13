using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Spending.Api.Models;

namespace Spending.Api.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Spending.Database.Entities.Transaction>> SaveAsync(StatementMetadata statementMetadata, IFormFileCollection files);
        Task<IEnumerable<Spending.Api.Models.Transaction>> GetUncategorizedTransactions(int userId);
    }
}