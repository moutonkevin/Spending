using Microsoft.EntityFrameworkCore;
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

        //used by the Database.Migration
        public SpendingContext()
        {
        }

        //used by DI
        public SpendingContext(DbContextOptions<SpendingContext> options) : base(options)
        {
        }
    }
}