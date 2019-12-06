﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                await _spendingContext.Transaction.AddRangeAsync(transactions);
                await _spendingContext.SaveChangesAsync();

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Could not save transactions into DB");
                
                return false;
            }
        }
    }
}
