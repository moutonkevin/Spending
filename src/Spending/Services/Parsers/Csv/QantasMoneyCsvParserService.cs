using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Spending.Models;

namespace Spending.Api.Services.Parsers.Csv
{
    public class QantasMoneyCsvParserService : IParserService
    {
        public IList<Transaction> GetTransactions(string content)
        {
            var lines = content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var transactions = new List<Transaction>();

            //skip header
            foreach (var line in lines.Skip(1))
            {
                var columns = line.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    var date = columns[0].Replace("\"", "").Trim();
                    var description = columns[1].Replace("\"", "").Trim();
                    var amount = columns[2].Trim();

                    transactions.Add(new Transaction
                    {
                        Date = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Amount = decimal.Parse(amount, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint),
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
