using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Spending.Api.Services
{
    public interface IStatementProcessor
    {
        Task ProcessAsync(IFormFileCollection files);
    }
}