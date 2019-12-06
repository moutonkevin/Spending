namespace Spending.Database.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
    }
}
