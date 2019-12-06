using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Spending.Api.Services
{
    public class FormFileService : IFormFileService
    {
        public async Task<MemoryStream> CopyFileAsync(IFormFile file)
        {
            var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
