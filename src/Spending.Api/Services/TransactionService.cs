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

namespace Spending.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ILogger<TransactionService> _logger;
        private readonly IFormFileService _formFileService;
        private readonly Func<int, int, IParserService> _parserServiceResolver;
        private readonly Func<int, ITextExtractorService> _textExtractorResolver;
        private readonly ITransactionDataAccess _transactionDataAccess;

        public TransactionService(
            ILogger<TransactionService> logger,
            IFormFileService formFileService,
            Func<int, int, IParserService> parserServiceResolver,
            Func<int, ITextExtractorService> textExtractorResolver,
            ITransactionDataAccess transactionDataAccess)
        {
            _logger = logger;
            _formFileService = formFileService;
            _parserServiceResolver = parserServiceResolver;
            _textExtractorResolver = textExtractorResolver;
            _transactionDataAccess = transactionDataAccess;
        }

        private IParserService GetParserService(int bankId, int fileTypeId)
        {
            return _parserServiceResolver.Invoke(bankId, fileTypeId);
        }

        private ITextExtractorService GetExtractorServiceForFileType(int fileTypeId)
        {
            return _textExtractorResolver.Invoke(fileTypeId);
        }

        private void ConsolidateTransactions(StatementMetadata statementMetadata, IEnumerable<Spending.Database.Entities.Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                transaction.AccountId = statementMetadata.AccountId;
                transaction.UserId = statementMetadata.UserId;
            }
        }

        private void GroupSimilarTransactions(IList<Spending.Database.Entities.Transaction> transactions)
        {
            var groupedTransactions = transactions.GroupBy(x => new {x.Amount, x.TransactionTypeId, x.Date, x.Description, x.UserId}).ToList();
            var similarTransactionsGrouped = groupedTransactions.Where(groupedTransaction => groupedTransaction.Count() > 1).ToList();
            var similarTransactionsMerged = similarTransactionsGrouped.Select(s => new Spending.Database.Entities.Transaction
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

        public async Task<IEnumerable<Spending.Database.Entities.Transaction>> SaveAsync(StatementMetadata statementMetadata, IFormFileCollection files)
        {
            var parserService = GetParserService(statementMetadata.BankId, statementMetadata.StatementFileTypeId);
            var extractorService = GetExtractorServiceForFileType(statementMetadata.StatementFileTypeId);

            var combinedTransactions = new List<Spending.Database.Entities.Transaction>();

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
            var uncategorizedTransactions = await _transactionDataAccess.GetUncategorizedTransactions(userId);

            return uncategorizedTransactions.Select(s => new Transaction
            {
                Description = s.Description,
                Id = s.Id,
                Amount = s.Amount,
                Date = s.Date,
                TransactionTypeId = s.TransactionTypeId,
                CategoryId = Database.Constants.Categories.List.First().Id
            }).OrderBy(o => o.Description);
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions(int userId, int? bankId, int? accountId, int? categoryId, string description)
        {
            var transactions = await _transactionDataAccess.GetAllTransactions(userId, bankId, accountId, categoryId, description);

            return transactions.Select(s => new Transaction
            {
                Description = s.Description,
                Id = s.Id,
                Amount = s.Amount,
                Date = s.Date,
                TransactionTypeName = s.TransactionType?.Name,
                CategoryName = s.TransactionCategory?.Category?.Name
            }).OrderBy(o => o.Description);
        }
    }
}
