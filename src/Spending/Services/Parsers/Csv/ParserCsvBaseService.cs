using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Spending.Api.Services.Parsers.Csv
{
    public class ParserCsvBaseService
    {
        protected IEnumerable<string> GetIndividualTransactions(string content, params string[] separator)
        {
            return content.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        protected IEnumerable<string> GetIndividualTransactionColumns(string transaction, params string[] separator)
        {
            return transaction.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        protected string Sanitize(string content)
        {
            return content.Replace("\"", "").Trim();
        }

        protected decimal ParseAmount(string amount)
        {
            return decimal.Parse(amount, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint);
        }

        protected DateTime ParseDate(string date, string format)
        {
            return DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
        }

        protected IEnumerable<string> RemoveHeader(IEnumerable<string> transactions)
        {
            return transactions.Skip(1);
        }
    }
}
