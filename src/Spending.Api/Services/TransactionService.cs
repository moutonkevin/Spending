using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Spending.Api.DataAccess;
using Spending.Api.Models;
using Spending.Api.Services.Extractors;
using Spending.Api.Services.Parsers;
using Spending.Database.Entities;

namespace Spending.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ILogger<TransactionService> _logger;
        private readonly IFormFileService _formFileService;
        private readonly Func<string, string, IParserService> _parserServiceResolver;
        private readonly Func<string, ITextExtractorService> _textExtractorResolver;
        private readonly ITransactionDataAccess _transactionDataAccess;

        public TransactionService(
            ILogger<TransactionService> logger,
            IFormFileService formFileService,
            Func<string, string, IParserService> parserServiceResolver,
            Func<string, ITextExtractorService> textExtractorResolver,
            ITransactionDataAccess transactionDataAccess)
        {
            _logger = logger;
            _formFileService = formFileService;
            _parserServiceResolver = parserServiceResolver;
            _textExtractorResolver = textExtractorResolver;
            _transactionDataAccess = transactionDataAccess;
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

        private void GroupSimilarTransactions(IList<Transaction> transactions)
        {
            var groupedTransactions = transactions.GroupBy(x => new {x.Amount, x.TransactionTypeId, x.Date, x.Description, x.UserId}).ToList();
            var similarTransactionsGrouped = groupedTransactions.Where(groupedTransaction => groupedTransaction.Count() > 1).ToList();
            var similarTransactionsMerged = similarTransactionsGrouped.Select(s => new Transaction
            {
                Description = s.FirstOrDefault().Description,
                Date = s.FirstOrDefault().Date,
                AccountId = s.FirstOrDefault().AccountId,
                UserId = s.FirstOrDefault().UserId,
                TransactionTypeId = s.FirstOrDefault().TransactionTypeId,
                Amount = s.Sum(ss => ss.Amount)
            }).ToList();

            //remove the duplicated
            var transactionsToRemove = similarTransactionsGrouped.SelectMany(s => s.ToList()).ToList();
            foreach (var transactionToRemove in transactionsToRemove)
            {
                var res = transactions.Remove(transactionToRemove);
            }

            //add the grouped ones
            similarTransactionsMerged.ForEach(transactions.Add);
        }

        public async Task<IEnumerable<Transaction>> SaveAsync(StatementMetadata statementMetadata, IFormFileCollection files)
        {
            var parserService = GetParserService(statementMetadata.BankId, statementMetadata.StatementFileType);
            var extractorService = GetExtractorServiceForFileType(statementMetadata.StatementFileType);

            var combinedTransactions = new List<Transaction>();

            foreach (var file in files)
            {
                var fileStream = await _formFileService.CopyFileAsync(file);
                var fileContent = extractorService.GetContent(fileStream);
                var transactions = parserService.GetTransactions(fileContent);

                ConsolidateTransactions(statementMetadata, transactions);
                GroupSimilarTransactions(transactions);

                await _transactionDataAccess.SaveAsync(transactions);

                combinedTransactions.AddRange(transactions);
            }

            return combinedTransactions;
        }

        public async Task<IEnumerable<Transaction>> GetUncategorizedTransactions(int userId)
        {
            return await _transactionDataAccess.GetUncategorizedTransactions(userId);
        }
    }
}
