using System;

namespace Spending.Api.Models
{
    public class Transaction
    {
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
