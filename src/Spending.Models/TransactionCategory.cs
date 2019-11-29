namespace Spending.Models
{
    public class TransactionCategory
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
