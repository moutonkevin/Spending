using System;
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

        public async Task SaveTransactionCategory(Transaction transaction, string descriptionContent, int categoryId, int userId)
        {
            try
            {
                await _spendingContext.TransactionDescriptionUserCategory.AddAsync(new TransactionDescriptionUserCategory
                {
                    Description = descriptionContent,
                    CategoryId = categoryId,
                    UserId = userId
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
