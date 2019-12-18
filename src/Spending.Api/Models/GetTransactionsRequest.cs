namespace Spending.Api.Models
{
    public class GetTransactionsRequest
    {
        public int UserId { get; set; }
        public int? BankId { get; set; }
        public int? AccountId { get; set; }
        public int? CategoryId { get; set; }
        public string Description { get; set; }
    }
}
