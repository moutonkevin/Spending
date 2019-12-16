using System.Collections.Generic;
using System.Linq;

namespace Spending.Api
{
    public static class Constants
    {
        public static IList<FileType> FileTypes = new List<FileType>
        {
            new FileType { Id = 1, Type = "pdf", MediaType = "application/pdf"},
            new FileType { Id = 2, Type = "csv", MediaType = "text/csv"}
        };

        public static FileType PdFile = FileTypes.FirstOrDefault(f => f.Id == 1);
        public static FileType CsvFile = FileTypes.FirstOrDefault(f => f.Id == 2);

        public class FileType
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public string MediaType { get; set; }
        }
    }
}
