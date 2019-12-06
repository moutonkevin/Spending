using System.Collections.Generic;
using System.Text.RegularExpressions;
using Spending.Database.Entities;

namespace Spending.Api.Services.Parsers.Pdf
{
    public class AmexPdfParserService : ParserPdfBaseService, IParserService
    {
        private new List<string> ExtractDataFromTransaction(string content)
        {
            var matches = Regex.Matches(content, @"\([^)]*\)");
            var extractedData = new List<string>();

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    extractedData.Add(match.Groups[0].Value.Replace("(", "").Replace(")", ""));
                }
            }
            
            return extractedData;
        }

        private List<string> KeepOnlyFromTransactions(List<string> linesData)
        {
            for (var index = 0; index < linesData.Count; index++)
            {
                if (linesData[index].Equals("MR") &&
                    index + 1 < linesData.Count && linesData[index + 1].Equals("KEVIN") &&
                    index + 2 < linesData.Count && linesData[index + 2].Equals("MOUTON"))
                {
                    return linesData.GetRange(index + 3, linesData.Count - index - 3 - 1);
                }
            }
            return linesData;
        }

        private List<string> DiscardNoise(List<string> linesData)
        {
            var discardIndexFrom = 0;
            var discardIndexTo = 0;

            for (var index = 0; index < linesData.Count; index++)
            {
                if (linesData[index].Equals("You") &&
                    index + 1 < linesData.Count && linesData[index + 1].Equals("can") &&
                    index + 2 < linesData.Count && linesData[index + 2].Equals("pay"))
                {
                    discardIndexFrom = index;
                }

                if (linesData[index].Equals("Total") &&
                    index + 1 < linesData.Count && linesData[index + 1].Equals("New") &&
                    index + 2 < linesData.Count && linesData[index + 2].Equals("Payments"))
                {
                    discardIndexTo = index + 4;
                }

                if (discardIndexFrom != default && discardIndexTo != default)
                {
                    linesData.RemoveRange(discardIndexFrom, discardIndexTo);
                    index = index - discardIndexTo + discardIndexFrom;
                    discardIndexFrom = 0;
                    discardIndexTo = 0;
                }

                if (linesData[index].Equals("ONLINE") &&
                    index + 1 < linesData.Count && linesData[index + 1].Equals("PAYMENT") &&
                    index + 2 < linesData.Count && linesData[index + 2].Equals("RECEIVED"))
                {
                    discardIndexFrom = index;
                }

                if (!linesData[index].Equals("DATE") &&
                    index + 1 < linesData.Count && linesData[index + 1].Equals("TRANSACTION") &&
                    index + 2 < linesData.Count && linesData[index + 2].Equals("DETAILS"))
                {
                    discardIndexTo = index + 4;
                }

                if (discardIndexFrom != default && discardIndexTo != default)
                {
                    linesData.RemoveRange(discardIndexFrom, discardIndexTo);
                    index = index - discardIndexTo + discardIndexFrom;
                    discardIndexFrom = 0;
                    discardIndexTo = 0;
                }
            }
            return linesData;
        }

        public IList<Transaction> GetTransactions(string content)
        {
            var linesData = ExtractDataFromTransaction(content);
            var linesDataFromTransaction = KeepOnlyFromTransactions(linesData);
            var linesDataWithoutNoise = DiscardNoise(linesDataFromTransaction);
            
            return null;
        }
    }
}
