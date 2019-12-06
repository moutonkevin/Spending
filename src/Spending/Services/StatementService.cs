using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Spending.Api.Models;
using Spending.Api.Services.Extractors;
using Spending.Api.Services.Parsers;
using Spending.Database.Entities;

namespace Spending.Api.Services
{
    public class StatementService : IStatementService
    {
        private readonly ILogger<StatementService> _logger;
        private readonly IFormFileService _formFileService;
        private readonly Func<string, string, IParserService> _parserServiceResolver;
        private readonly Func<string, ITextExtractorService> _textExtractorResolver;

        public StatementService(
            ILogger<StatementService> logger,
            IFormFileService formFileService,
            Func<string, string, IParserService> parserServiceResolver,
            Func<string, ITextExtractorService> textExtractorResolver)
        {
            _logger = logger;
            _formFileService = formFileService;
            _parserServiceResolver = parserServiceResolver;
            _textExtractorResolver = textExtractorResolver;
        }

        private IParserService GetParserService(int bankId, string fileType)
        {
            var bankName = Banks.Mapping.ContainsKey(bankId) ? Banks.Mapping[bankId] : null;

            return _parserServiceResolver.Invoke(bankName, fileType);
        }

        private ITextExtractorService GetExtractorServiceForFileType(string fileType)
        {
            return _textExtractorResolver.Invoke(fileType);
        }

        private void ConsolidateTransactions(StatementMetadata statementMetadata, IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                transaction.AccountId = statementMetadata.AccountId;
                transaction.UserId = statementMetadata.UserId;
            }
        }

        public async Task ProcessAsync(StatementMetadata statementMetadata, IFormFileCollection files)
        {
            var parserService = GetParserService(statementMetadata.BankId, statementMetadata.StatementFileType);
            var extractorService = GetExtractorServiceForFileType(statementMetadata.StatementFileType);

            foreach (var file in files)
            {
                var fileStream = await _formFileService.CopyFileAsync(file);
                var fileContent = extractorService.GetContent(fileStream);
                var transactions = parserService.GetTransactions(fileContent);

                ConsolidateTransactions(statementMetadata, transactions);
            }
        }
    }
}
