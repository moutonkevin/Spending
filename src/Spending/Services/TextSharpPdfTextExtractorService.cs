using System;
using System.IO;
using System.Text;
using iText.Kernel.Pdf;

namespace Spending.Api.Services
{
    public class TextSharpPdfTextExtractorService : ITextExtractorService
    {
        public string GetContent(MemoryStream memoryStream)
        {
            var pdfTextBuilder = new StringBuilder();

            try
            {
                var pdfReader = new PdfReader(memoryStream);
                var pdf = new PdfDocument(pdfReader);

                for (var pageNumber = 1; pageNumber <= pdf.GetNumberOfPages(); pageNumber++)
                {
                    var content = pdf.GetPage(pageNumber).GetContentBytes();
                    var contentString = Encoding.UTF8.GetString(content, 0, content.Length);

                    pdfTextBuilder.AppendLine(contentString);
                }
            }
            catch (Exception _)
            {
                return null;
            }

            return pdfTextBuilder.ToString();
        }
    }
}
