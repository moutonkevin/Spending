using System;
using System.Collections.Generic;
using System.Linq;
using Spending.Models;

namespace Spending.Api.Services.Parsers.Csv
{
    public class QantasMoneyCsvParserService : ParserCsvBaseService, IParserService
    {
        public IList<Transaction> GetTransactions(string content)
        {
            var lines = GetIndividualTransactions(content, "\n");
            var transactions = new List<Transaction>();

            //skip header
            foreach (var line in lines.Skip(1))
            {
                var columns = GetIndividualTransactionColumns(line, ",").ToList();

                try
                {
                    var date = Sanitize(columns[0]);
                    var description = Sanitize(columns[1]);
                    var amount = Sanitize(columns[2]);

                    transactions.Add(new Transaction
                    {
                        Date = ParseDate(date, "yyyy-MM-dd"),
                        Amount = ParseAmount(amount),
                        Description = description
                    });
                }
                catch (Exception _)
                {
                    Console.WriteLine($"Transaction could not be parsed: {line}");
                }
            }

            return transactions;
        }
    }
}
