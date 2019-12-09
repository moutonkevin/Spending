using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Spending.Database.Context;
using Spending.Database.Entities;

namespace Spending.DatabaseMigration.Context
{
    public class SpendingContextMigration : SpendingContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            //builder.AddUserSecrets<Startup>();

            var configuration = builder.Build();

            var spendingDatabaseConnectionString = configuration["ConnectionStrings:SpendingDatabase"];

            Console.WriteLine(spendingDatabaseConnectionString);

            optionsBuilder.UseSqlServer(spendingDatabaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Bank>()
                .HasData(
                    new Bank { Id = 1, Name = "Commonwealth Bank" },
                    new Bank { Id = 2, Name = "American Express" },
                    new Bank { Id = 3, Name = "CityBank" },
                    new Bank { Id = 4, Name = "Westpac" },
                    new Bank { Id = 5, Name = "Qantas" }
                );

            modelBuilder
                .Entity<Account>()
                .HasData(
                    new Account { Id = 1, Name = "Smart Access", BankId = 1 },
                    new Account { Id = 2, Name = "Goal Saver", BankId = 1 },
                    new Account { Id = 3, Name = "NetBank Saver", BankId = 1 },
                    new Account { Id = 4, Name = "Amex Credit Card", BankId = 2 },
                    new Account { Id = 5, Name = "CityBank Credit Card", BankId = 3 },
                    new Account { Id = 6, Name = "Westpac Credit Card", BankId = 4 },
                    new Account { Id = 7, Name = "Qantas Credit Card", BankId = 5 }
                );

            modelBuilder
                .Entity<Category>()
                .HasData(
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
                    new Category { Id = 13, Name = "Salary" }
                );

            modelBuilder
                .Entity<TransactionType>()
                .HasData(
                    new TransactionType { Id = 1, Name = "Debit" },
                    new TransactionType { Id = 2, Name = "Credit" },
                    new TransactionType { Id = 3, Name = "TransferBetweenAccounts" }
                );

            modelBuilder
                .Entity<User>()
                .HasData(
                    new User { Id = 1, Name = "Kevin" },
                    new User { Id = 2, Name = "Mika" }
                );

            modelBuilder
                .Entity<Transaction>()
                .Property(s => s.Amount)
                .HasColumnType("decimal(19, 2)");

            modelBuilder
                .Entity<Transaction>()
                .HasIndex(transaction => new
                {
                    transaction.Amount,
                    transaction.TransactionTypeId, 
                    transaction.Date,
                    transaction.Description,
                    transaction.UserId
                }).IsUnique();

            //modelBuilder
            //    .Entity<TransactionDescriptionUserCategory>()
            //    .HasData(
            //#region Rent
            //        new TransactionDescriptionUserCategory { Id = 1, UserId = 1, CategoryId = 1, Description = "Peterbus Rent" },
            //        new TransactionDescriptionUserCategory { Id = 2, UserId = 1, CategoryId = 1, Description = "EXCLUSIVE RESIDE 343499 RENT" },
            //        new TransactionDescriptionUserCategory { Id = 3, UserId = 1, CategoryId = 1, Description = "MIKAEL BESSE NetBank rent and insurance" },
            //#endregion
            //#region Groceries
            //        new TransactionDescriptionUserCategory { Id = 1, UserId = 1, CategoryId = 2, Description = "COLES " },
            //        new TransactionDescriptionUserCategory { Id = 1, UserId = 1, CategoryId = 2, Description = "MIRACLE SUPERMARKETS" },
            //        new TransactionDescriptionUserCategory { Id = 1, UserId = 1, CategoryId = 2, Description = "WW TOWN HALL" },
            //#endregion
            //#region Health insurance
            //    new TransactionDescriptionUserCategory { Id = 1, UserId = 1, CategoryId = 3, Description = "NIB HEALTH FUNDS" },
            //    new TransactionDescriptionUserCategory { Id = 1, UserId = 1, CategoryId = 3, Description = "MIKAEL BESSE NetBank rent and insurance" },
            //#endregion
            //#region Phone
            //    new TransactionDescriptionUserCategory { Id = 1, UserId = 1, CategoryId = 4, Description = "OPTUS BILLING" },
            //#endregion
            //#region Entertainment
            //    new TransactionDescriptionUserCategory { Id = 1, UserId = 1, CategoryId = 3, Description = "OPTUS BILLING" }
            //#endregion
            //    );
        }
    }
}
