using System;
using System.Collections.Generic;
using Spending.Models;

namespace Spending.Api.Services.Parser
{
    public class AmexParserService : IParserService
    {
        public IList<Transaction> GetTransactions(string content)
        {
            try
            {
                System.IO.File.WriteAllText("C:\\Users\\kevinm\\Downloads\\amexpdf.txt", content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
        }
    }
}
