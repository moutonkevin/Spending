using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Spending.Api.Services
{
    public interface IFormFileService
    {
        Task<MemoryStream> CopyFileAsync(IFormFile file);
    }
}
