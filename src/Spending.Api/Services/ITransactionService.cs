using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Spending.Api.Models;

namespace Spending.Api.Services
{
    public interface ITransactionService
    {
        Task SaveAsync(StatementMetadata statementMetadata, IFormFileCollection files);
    }
}