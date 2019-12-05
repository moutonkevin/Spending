namespace Spending.Api.Models
{
    public class StatementMetadata
    {
        public int UserId { get; set; }
        public int BankId { get; set; }
        public int AccountId { get; set; }
        public string StatementFileType { get; set; }
    }
}
