using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spending.Database.Context;
using Spending.Database.Entities;

namespace Spending.Api.DataAccess
{
    public class TransactionDatabaseDataAccess : ITransactionDataAccess
    {
        private readonly ILogger<TransactionDatabaseDataAccess> _logger;
        private readonly SpendingContext _spendingContext;

        public TransactionDatabaseDataAccess(ILogger<TransactionDatabaseDataAccess> logger, SpendingContext spendingContext)
        {
            _logger = logger;
            _spendingContext = spendingContext;
        }

        public async Task<bool> SaveAsync(IEnumerable<Transaction> transactions)
        {
            try
            {
                for (int i = 0, batchSize = 100; i < transactions.Count(); i+= batchSize)
                {
                    await _spendingContext.UpsertRange(transactions.Skip(i).Take(batchSize))
                        .On(v => new { v.Amount, v.Description, v.Date, v.TransactionTypeId, v.UserId })
                        .NoUpdate()
                        .RunAsync();

                    await _spendingContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Could not save transactions into DB");
                
                return false;
            }
        }

        public async Task<IEnumerable<Transaction>> GetUncategorizedTransactions(int userId)
        {
            try
            {
                return await _spendingContext.Transaction
                    .FromSqlRaw("EXEC dbo.sp_getTransactionsWithoutCategory @userId", new SqlParameter("userId", userId))
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return default;
        }

        private IQueryable<Transaction> AddToWhereClauseIfNonDefault(IQueryable<Transaction> query, Expression<Func<Transaction, bool>> expression, int? value)
        {
            return value.HasValue && value.Value >= 0 ? query.Where(expression) : query;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions(int userId, int? bankId, int? accountId, int? categoryId)
        {
            try
            {
                var query = _spendingContext.Transaction
                    .Include(t => t.TransactionType)
                    .Include(t => t.Account)
                    .Include(i => i.TransactionCategory)
                    .ThenInclude(ii => ii.Category)
                    .Where(t => t.UserId == userId);

                query = AddToWhereClauseIfNonDefault(query, transaction => transaction.AccountId == accountId, accountId);
                query = AddToWhereClauseIfNonDefault(query, transaction => transaction.TransactionCategory.CategoryId == categoryId, categoryId);
                query = AddToWhereClauseIfNonDefault(query, transaction => transaction.Account.BankId == bankId, bankId);

                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return default;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsSatisfyingPattern(int userId, int patternId)
        {
            try
            {
                return await _spendingContext.Transaction
                    .FromSqlRaw("EXEC dbo.sp_getTransactionsSatisfyingCategoryPattern @userId, @transactionCategoryPatternId",
                        new SqlParameter("userId", userId),
                        new SqlParameter("transactionCategoryPatternId", patternId))
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return default;
        }
    }
}
