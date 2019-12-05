using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Spending.Api.Services.Parsers.Pdf
{
    public class ParserPdfBaseService
    {
        protected string ExtractDataFromTransaction(string content)
        {
            var matches = Regex.Match(content, @"\([^)]*\)");

            if (matches.Success)
            {
                return matches.Groups[0].Value.Replace("(", "").Replace(")", "");
            }
            return null;
        }

        protected IList<string> ExtractRawLines(string section)
        {
            return section.Split(new[]
            {
                "TJ\r"
            }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
