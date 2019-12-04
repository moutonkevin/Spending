using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Spending.Api.Models;
using Spending.Api.Services.Parser;
using Spending.Models;

namespace Spending.Api.Services
{
    public class StatementService : IStatementService
    {
        private readonly ILogger<StatementService> _logger;
        private readonly IFormFileService _formFileService;
        private readonly ITextExtractorService _extractorService;
        private readonly Func<string, IParserService> _parserServiceResolver;

        public StatementService(
            ILogger<StatementService> logger,
            IFormFileService formFileService,
            ITextExtractorService extractorService,
            Func<string, IParserService> parserServiceResolver)
        {
            _logger = logger;
            _formFileService = formFileService;
            _extractorService = extractorService;
            _parserServiceResolver = parserServiceResolver;
        }

        private void ConsolidateTransactions(StatementMetadata statementMetadata, IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                transaction.AccountId = statementMetadata.AccountId;
                transaction.UserId = statementMetadata.UserId;
            }
        }

        private IParserService GetParserServiceForBank(int bankId)
        {
            var bankName = Banks.Mapping.ContainsKey(bankId) ? Banks.Mapping[bankId] : null;

            return _parserServiceResolver.Invoke(bankName);
        }

        public async Task ProcessAsync(StatementMetadata statementMetadata, IFormFileCollection files)
        {
            var parserService = GetParserServiceForBank(statementMetadata.BankId);

            foreach (var file in files)
            {
                var fileStream = await _formFileService.CopyFileAsync(file);
                var fileContent = _extractorService.GetContent(fileStream);
                var transactions = parserService.GetTransactions(fileContent);

                ConsolidateTransactions(statementMetadata, transactions);
            }
        }
    }
}
