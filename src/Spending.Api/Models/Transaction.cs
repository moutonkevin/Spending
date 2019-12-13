namespace Spending.Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public int TransactionTypeId { get; set; }
        public int CategoryId { get; set; }
    }
}
