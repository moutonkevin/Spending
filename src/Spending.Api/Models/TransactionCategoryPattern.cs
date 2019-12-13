namespace Spending.Api.Models
{
    public class TransactionCategoryPattern
    {
        public int Id { get; set; }
        public string Pattern { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryDisplayName { get; set; }
    }
}
