using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Spending.Api.Models;

namespace Spending.Api.Services
{
    public interface IStatementProcessor
    {
        Task ProcessAsync(StatementMetadata statementMetadata, IFormFileCollection files);
    }
}