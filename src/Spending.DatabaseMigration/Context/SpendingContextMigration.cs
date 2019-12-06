using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Spending.Database.Context;

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
    }
}
