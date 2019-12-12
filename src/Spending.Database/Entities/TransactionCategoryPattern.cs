namespace Spending.Database.Entities
{
    public class TransactionCategoryPattern
    {
        public int Id { get; set; }
        public string Pattern { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
