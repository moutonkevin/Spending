namespace Spending.Database.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public TransactionCategory TransactionCategory { get; set; }
    }
}
