using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Spending.Database.Context;
using Spending.Database.Entities;

namespace Spending.Api.DataAccess
{
    public class TransactionCategoryDatabaseDataAccess : ITransactionCategoryDatabaseDataAccess
    {
        private readonly ILogger<TransactionCategoryDatabaseDataAccess> _logger;
        private readonly SpendingContext _spendingContext;

        public TransactionCategoryDatabaseDataAccess(ILogger<TransactionCategoryDatabaseDataAccess> logger, SpendingContext spendingContext)
        {
            _logger = logger;
            _spendingContext = spendingContext;
        }

        public async Task<bool> SaveTransactionCategories(IEnumerable<Transaction> transactions, int categoryId)
        {
            try
            {
                var transactionCategories = transactions.Select(t => new TransactionCategory
                {
                    TransactionId = t.Id,
                    CategoryId = categoryId
                });

                await _spendingContext.TransactionCategory.AddRangeAsync(transactionCategories);
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
