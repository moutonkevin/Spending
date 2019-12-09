using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Spending.Api.Models;
using Spending.Database.Entities;

namespace Spending.Api.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> SaveAsync(StatementMetadata statementMetadata, IFormFileCollection files);
    }
}