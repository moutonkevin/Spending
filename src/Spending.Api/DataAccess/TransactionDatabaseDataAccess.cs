using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Transaction>> GetTransactionsWithoutCategoryAsync(int userId)
        {
            return null;
        }
    }
}
