using System.IO;

namespace Spending.Api.Services.Extractors
{
    public interface ITextExtractorService
    {
        string GetContent(MemoryStream memoryStream);
    }
}