namespace Spending.Models
{
    public enum TransactionTypeEnum
    {
        Debit = 1,
        Credit = 2,
        TransferBetweenAccounts = 3
    }

    public class TransactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
