using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Spending.Database;
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
                .HasData(Constants.Banks.List);

            modelBuilder
                .Entity<Account>()
                .HasData(Constants.Accounts.List);

            modelBuilder
                .Entity<Category>()
                .HasData(Constants.Categories.List);

            modelBuilder
                .Entity<TransactionType>()
                .HasData(Constants.TransactionTypes.List);

            modelBuilder
                .Entity<User>()
                .HasData(Constants.Users.List);

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
        }
    }
}
