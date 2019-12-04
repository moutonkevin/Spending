using System.Collections.Generic;
using Spending.Models;

namespace Spending.Api.Services.Parser
{
    public interface IParserService
    {
        IList<Transaction> GetTransactions(string content);
    }
}