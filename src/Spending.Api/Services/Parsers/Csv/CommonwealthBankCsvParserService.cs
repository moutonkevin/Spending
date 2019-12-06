using System;
using System.Collections.Generic;
using System.Globalization;
using Spending.Database.Entities;

namespace Spending.Api.Services.Parsers.Csv
{
    public class CommonwealthBankCsvParserService : IParserService
    {
        public IList<Transaction> GetTransactions(string content)
        {
            var lines = content.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            var transactions = new List<Transaction>();

            foreach (var line in lines)
            {
                var columns = line.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    var date = columns[0];
                    var amount = columns[1].Replace("\"", "");
                    var description = columns[2];

                    transactions.Add(new Transaction
                    {
                        Date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Amount = decimal.Parse(amount, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint),
                        Description = description,
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Transaction could not be parsed: {line}");
                }
            }

            return transactions;
        }
    }
}
