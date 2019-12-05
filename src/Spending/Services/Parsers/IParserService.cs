using System.Collections.Generic;
using Spending.Models;

namespace Spending.Api.Services.Parsers
{
    public interface IParserService
    {
        IList<Transaction> GetTransactions(string content);
    }
}