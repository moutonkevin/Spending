using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Spending.Api.Models;

namespace Spending.Api.Services
{
    public class StatementProcessor : IStatementProcessor
    {
        private readonly ILogger<StatementProcessor> _logger;
        private readonly IFormFileService _formFileService;
        private readonly ITextExtractorService _extractorService;
        private readonly IParserService _parserService;

        public StatementProcessor(
            ILogger<StatementProcessor> logger,
            IFormFileService formFileService,
            ITextExtractorService extractorService,
            IParserService parserService)
        {
            _logger = logger;
            _formFileService = formFileService;
            _extractorService = extractorService;
            _parserService = parserService;
        }

        public async Task ProcessAsync(StatementMetadata statementMetadata, IFormFileCollection files)
        {
            foreach (var file in files)
            {
                var fileStream = await _formFileService.CopyFileAsync(file);
                var fileContent = _extractorService.GetContent(fileStream);
                var transactions = _parserService.GetTransactions(fileContent);


            }
        }
    }
}
