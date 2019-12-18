using System.Collections.Generic;
using Spending.Database.Entities;

namespace Spending.Database
{
    public static class Constants
    {
        public static class Banks
        {
            public static IReadOnlyList<Bank> List = new List<Bank>
            {
                new Bank {Id = 1, Name = "Commonwealth Bank"},
                new Bank {Id = 2, Name = "American Express"},
                new Bank {Id = 3, Name = "CityBank"},
                new Bank {Id = 4, Name = "Westpac"},
                new Bank {Id = 5, Name = "Qantas"}
            };
        }

        public static class Accounts
        {
            public static IReadOnlyList<Account> List = new List<Account>
            {
                new Account { Id = 1, Name = "Smart Access", BankId = 1 },
                new Account { Id = 2, Name = "Goal Saver", BankId = 1 },
                new Account { Id = 3, Name = "NetBank Saver", BankId = 1 },
                new Account { Id = 4, Name = "Amex Credit Card", BankId = 2 },
                new Account { Id = 5, Name = "CityBank Credit Card", BankId = 3 },
                new Account { Id = 6, Name = "Westpac Credit Card", BankId = 4 },
                new Account { Id = 7, Name = "Qantas Credit Card", BankId = 5 }
            };
        }

        public static class Categories
        {
            public static IReadOnlyList<Category> List = new List<Category>
            {
                new Category { Id = 1, Name = "Rent" },
                new Category { Id = 2, Name = "Groceries" },
                new Category { Id = 3, Name = "Health Insurance" },
                new Category { Id = 4, Name = "Mobile Phone" },
                new Category { Id = 5, Name = "Electricity" },
                new Category { Id = 6, Name = "Gas" },
                new Category { Id = 7, Name = "Entertainment" },
                new Category { Id = 8, Name = "Internet" },
                new Category { Id = 9, Name = "Shopping" },
                new Category { Id = 10, Name = "Holiday" },
                new Category { Id = 11, Name = "Gym" },
                new Category { Id = 12, Name = "Transfer Between Accounts" },
                new Category { Id = 13, Name = "Salary" },
                new Category { Id = 14, Name = "Transport" },
                new Category { Id = 15, Name = "Eating out" },
                new Category { Id = 16, Name = "Haircut" },
                new Category { Id = 17, Name = "Fees" }
            };
    }

        public static class TransactionTypes
        {
            public static IReadOnlyList<TransactionType> List = new List<TransactionType>
            {
                new TransactionType { Id = 1, Name = "Debit" },
                new TransactionType { Id = 2, Name = "Credit" },
                new TransactionType { Id = 3, Name = "TransferBetweenAccounts" }
            };
        }

        public static class Users
        {
            public static IReadOnlyList<User> List = new List<User>
            {
                new User { Id = 1, Name = "Kevin" },
                new User { Id = 2, Name = "Mika" }
            };
        }
    }
}
