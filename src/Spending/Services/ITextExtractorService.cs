using System.IO;

namespace Spending.Api.Services
{
    public interface ITextExtractorService
    {
        string GetContent(MemoryStream memoryStream);
    }
}