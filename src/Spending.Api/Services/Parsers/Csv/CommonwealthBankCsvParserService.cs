using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Spending.Database.Entities;

namespace Spending.Api.Services.Parsers.Csv
{
    public class CommonwealthBankCsvParserService : ParserCsvBaseService, IParserService
    {
        public IList<Transaction> GetTransactions(string content)
        {
            var lines = GetIndividualTransactions(content, Environment.NewLine);
            var transactions = new List<Transaction>();

            foreach (var line in lines)
            {
                var columns = GetIndividualTransactionColumns(line, ",").ToList();

                try
                {
                    var date = ParseDate(Sanitize(columns[0]), "dd/MM/yyyy").ToShortDateString();
                    var amount = ParseAmount(Sanitize(columns[1]));
                    var description = Sanitize(columns[2]);
                    var transactionType = (int)GetTransactionTypeEnum(amount, description);

                    transactions.Add(new Transaction
                    {
                        Date = date,
                        Amount = amount,
                        Description = description,
                        TransactionTypeId = transactionType
                    });
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Transaction could not be parsed: {line}");
                }
            }

            return transactions;
        }
    }
}
