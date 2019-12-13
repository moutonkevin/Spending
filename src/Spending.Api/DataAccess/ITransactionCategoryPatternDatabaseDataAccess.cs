﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Spending.Database.Entities;

namespace Spending.Api.DataAccess
{
    public interface ITransactionCategoryPatternDatabaseDataAccess
    {
        Task<bool> SavePatternAsync(string descriptionContent, int categoryId, int userId);
        Task<IList<TransactionCategoryPattern>> GetAllPatternsAsync(int userId);
    }
}