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
    public class TransactionCategoryPatternDatabaseDataAccess : ITransactionCategoryPatternDatabaseDataAccess
    {
        private readonly ILogger<TransactionCategoryPatternDatabaseDataAccess> _logger;
        private readonly SpendingContext _spendingContext;

        public TransactionCategoryPatternDatabaseDataAccess(ILogger<TransactionCategoryPatternDatabaseDataAccess> logger, SpendingContext spendingContext)
        {
            _logger = logger;
            _spendingContext = spendingContext;
        }

        public async Task<bool> SavePatternAsync(string pattern, int categoryId, int userId)
        {
            try
            {
                await _spendingContext.TransactionCategoryPattern.AddAsync(new TransactionCategoryPattern
                {
                    Pattern = pattern,
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

        public async Task<bool> UpdatePatternAsync(int id, string pattern, int categoryId, int userId)
        {
            try
            {
                _spendingContext.TransactionCategoryPattern.Update(new TransactionCategoryPattern
                {
                    Id = id,
                    Pattern = pattern,
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

        public async Task<bool> DeletePatternAsync(int id)
        {
            try
            {
                _spendingContext.TransactionCategoryPattern.Remove(new TransactionCategoryPattern
                {
                    Id = id
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

        public async Task<IList<TransactionCategoryPattern>> GetAllPatternsAsync(int userId)
        {
            try
            {
                var patterns = await _spendingContext.TransactionCategoryPattern
                    .Include(pattern => pattern.Category)
                    .Where(pattern => pattern.UserId == userId)
                    .ToListAsync();

                return patterns;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
