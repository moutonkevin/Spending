using System;
using System.Collections.Generic;
using System.Linq;
using Spending.Database.Entities;

namespace Spending.Api.Services.Parsers.Csv
{
    public class QantasMoneyCsvParserService : ParserCsvBaseService, IParserService
    {
        public IList<Transaction> GetTransactions(string content)
        {
            var lines = GetIndividualTransactions(content, "\n");
            var transactions = new List<Transaction>();

            //skip header
            foreach (var line in RemoveHeader(lines))
            {
                var columns = GetIndividualTransactionColumns(line, ",").ToList();

                try
                {
                    var date = ParseDate(Sanitize(columns[0]), "yyyy-MM-dd");
                    var description = Sanitize(columns[1]);
                    var amount = ParseAmount(Sanitize(columns[2]));
                    var transactionType = (int)GetTransactionTypeEnum(amount, description);

                    transactions.Add(new Transaction
                    {
                        Date = date,
                        Amount = amount,
                        Description = description,
                        TransactionTypeId = transactionType
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
