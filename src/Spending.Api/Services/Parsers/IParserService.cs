using System.Collections.Generic;
using Spending.Database.Entities;

namespace Spending.Api.Services.Parsers
{
    public interface IParserService
    {
        IList<Transaction> GetTransactions(string content);
    }
}