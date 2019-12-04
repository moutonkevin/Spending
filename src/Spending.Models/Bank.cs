using System.Collections.Generic;

namespace Spending.Models
{
    public static class Banks
    {
        public static readonly IDictionary<int, string> Mapping = new Dictionary<int, string>
        {
            {1, "Commonwealth Bank"},
            {2, "American Express"},
            {3, "CityBank"},
            {4, "Westpac"},
            {5, "Qantas"}
        };
    }
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
