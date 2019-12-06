﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Spending.Database.Entities;

namespace Spending.Api.DataAccess
{
    public interface ITransactionDataAccess
    {
        Task<bool> SaveAsync(IEnumerable<Transaction> transactions);
    }
}
