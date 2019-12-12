using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spending.Database.Context;
using Spending.Database.Entities;

namespace Spending.Api.DataAccess
{
    public class TransactionCategoryPatternDatabaseDataAccess : ITransactionCategoryPatternDatabaseDataAccess
    {
        private readonly ILogger<TransactionCategoryPatternDatabaseDataAccess> _logger;
        private readonly SpendingContext _spendingContext;

        public TransactionCategoryPatternDatabaseDataAccess(ILogger<TransactionCategoryPatternDatabaseDataAccess> logger, SpendingContext spendingContext)
        {
            _logger = logger;
            _spendingContext = spendingContext;
        }

        public async Task<IEnumerable<Transaction>> GetUncategorizedTransactions(int userId)
        {
            try
            {
                return _spendingContext.Transaction
                    .FromSqlRaw("EXEC dbo.sp_getTransactionsWithoutCategory @userId", new SqlParameter("userId", userId))
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return default;
        }

        public async Task<bool> SaveTransactionCategoryPattern(Transaction transaction, string descriptionContent, int categoryId, int userId)
        {
            try
            {
                await _spendingContext.TransactionCategoryPattern.AddAsync(new TransactionCategoryPattern
                {
                    Pattern = descriptionContent,
                    CategoryId = categoryId,
                    UserId = userId
                });
                await _spendingContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
