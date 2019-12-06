using System.IO;
using System.Text;

namespace Spending.Api.Services.Extractors
{
    public class CsvTextExtractorService : ITextExtractorService
    {
        public string GetContent(MemoryStream memoryStream)
        {
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
