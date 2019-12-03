using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Spending.Api.Models;

namespace Spending.Api.Services
{
    public class CommonwealthBankParserService : IParserService
    {
        private string ExtractDataFromTransaction(string content)
        {
            var matches = Regex.Match(content, @"\([^)]*\)");

            if (matches.Success)
            {
                return matches.Groups[0].Value.Replace("(", "").Replace(")", "");
            }
            return null;
        }

        private IList<string> ExtractRawSections(string content)
        {
            return content.Split(new[]
            {
                "ET\r",
                "BT\r"
            }, StringSplitOptions.RemoveEmptyEntries);
        }

        private IList<string> ExtractRawLines(string section)
        {
            return section.Split(new[]
            {
                "TJ\r",
                "Do\r",
                "BT\r",
                "ET\r"
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
                        !columnName.Equals("CR") && 
                        !columnName.StartsWith("$") && 
                        !columnName.Equals("\\"))
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
                    transaction.DateTime = new DateTime(DateTime.Now.Year, date.Month, date.Day);
                }

                var amountString = unfilteredTransaction.LastOrDefault();
                if (amountString != null && decimal.TryParse(amountString, out var amount))
                {
                    transaction.Amount = amount;
                }

                if (unfilteredTransaction.Count >= 3)
                {
                    transaction.Description = string.Join(' ', unfilteredTransaction.Skip(1).Take(unfilteredTransaction.Count - 2));
                }

                if (transaction.Amount != default && 
                    transaction.Description != default &&
                    transaction.DateTime != default)
                {
                    identifiedTransactions.Add(transaction);
                }
            }

            return identifiedTransactions;
        }

        public void GetTransactions(string content)
        {
            var rawSections = ExtractRawSections(content);
            //var rawTransactions = ExtractRawTransactions(rawSections.ToList());
            var transactions = ExtractTransactions(rawSections.ToList());
            var identifiedTransactions = IdentifyTransactions(transactions);
        }
    }
}
