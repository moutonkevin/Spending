using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Spending.Database.Entities;

namespace Spending.Database.Context
{
    public class SpendingContext : DbContext
    {
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<TransactionCategory> TransactionCategory { get; set; }
        public DbSet<TransactionDescriptionUserCategory> TransactionDescriptionUserCategory { get; set; }

        public SpendingContext()
        {
        }

        public SpendingContext(DbContextOptions<SpendingContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //        .AddEnvironmentVariables();

        //    //builder.AddUserSecrets<Startup>();

        //    var configuration = builder.Build();

        //    var spendingDatabaseConnectionString = configuration["ConnectionStrings:SpendingDatabase"];

        //    Console.WriteLine(spendingDatabaseConnectionString);

        //    optionsBuilder.UseSqlServer(spendingDatabaseConnectionString);
        //}

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
                    new Account { Id = 1, Name = "Smart Access", BankId = 1},
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
                    new Category { Id = 7, Name = "Netflix" },
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
        }
    }
}