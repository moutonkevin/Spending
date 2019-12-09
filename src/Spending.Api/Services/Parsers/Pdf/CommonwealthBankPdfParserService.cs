using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Spending.Database.Entities;

namespace Spending.Api.Services.Parsers.Pdf
{
    public class CommonwealthBankPdfParserService : ParserPdfBaseService, IParserService
    {
        private IList<string> ExtractRawSections(string content)
        {
            return content.Split(new[]
            {
                "ET\r",
                "BT\r"
            }, StringSplitOptions.RemoveEmptyEntries);
        }

        private IList<IList<string>> ExtractTransactions(List<string> sections)
        {
            var transactions = new List<IList<string>>();

            foreach (var section in sections)
            {
                var lines = ExtractRawLines(section);
                var columns = new List<string>();

                foreach (var line in lines)
                {
                    var columnName = ExtractDataFromTransaction(line);
                    if (columnName != null && 
                        !columnName.Equals("CR"))
                    {
                        columns.Add(columnName);
                    }
                }

                if (columns.Count >= 3)
                    transactions.Add(columns);
            }
            return transactions;
        }

        private IList<Transaction> IdentifyTransactions(IList<IList<string>> transactions)
        {
            var identifiedTransactions = new List<Transaction>();

            foreach (var unfilteredTransaction in transactions)
            {
                var transaction = new Transaction();

                if (DateTime.TryParseExact(unfilteredTransaction[0], "dd MMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    transaction.Date = new DateTime(DateTime.Now.Year, date.Month, date.Day).ToShortDateString();
                    unfilteredTransaction.Remove(unfilteredTransaction[0]);
                }

                var debit = unfilteredTransaction.FirstOrDefault(f => f.Equals("$"));
                if (debit != null)
                {
                    transaction.TransactionTypeId = (int)TransactionTypeEnum.Credit;
                }
                else if (unfilteredTransaction.Any(a => a.Contains("Transfer to")) &&
                         unfilteredTransaction.Any(a => a.Contains("CommBank app")))
                {
                    transaction.TransactionTypeId = (int)TransactionTypeEnum.TransferBetweenAccounts;
                }
                else
                {
                    transaction.TransactionTypeId = (int)TransactionTypeEnum.Debit;
                }

                unfilteredTransaction.Remove("$");
                unfilteredTransaction.Remove("\\");

                var balance = unfilteredTransaction.FirstOrDefault(f => f.StartsWith("$") && f.Length > 1);
                if (balance != null)
                {
                    unfilteredTransaction.Remove(balance);
                }

                var amountString = unfilteredTransaction.LastOrDefault();
                if (amountString != null && decimal.TryParse(amountString, out var amount))
                {
                    transaction.Amount = amount;
                    unfilteredTransaction.Remove(amountString);
                }

                transaction.Description = string.Join(' ', unfilteredTransaction);

                if (transaction.Amount != default && 
                    transaction.Description != default &&
                    transaction.Date != default)
                {
                    identifiedTransactions.Add(transaction);
                }
            }

            return identifiedTransactions;
        }

        public IList<Transaction> GetTransactions(string content)
        {
            var rawSections = ExtractRawSections(content);
            var transactions = ExtractTransactions(rawSections.ToList());
            var identifiedTransactions = IdentifyTransactions(transactions);

            return identifiedTransactions;
        }
    }
}
