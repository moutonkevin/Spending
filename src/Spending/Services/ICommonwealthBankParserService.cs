using Spending.Models;
using System.Collections.Generic;

namespace Spending.Api.Services
{
    public interface IParserService
    {
        IList<Transaction> GetTransactions(string content);
    }
}