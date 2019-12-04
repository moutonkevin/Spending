using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Spending.Api.Models;

namespace Spending.Api.Services
{
    public interface IStatementService
    {
        Task ProcessAsync(StatementMetadata statementMetadata, IFormFileCollection files);
    }
}