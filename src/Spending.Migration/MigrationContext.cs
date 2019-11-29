﻿using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Spending.Models;

namespace Spending.Migration
{
    public class MigrationContext : DbContext
    {
        public DbSet<Bank> Bank { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.AddUserSecrets<Startup>();

            var configuration = builder.Build();

            var spendingDatabaseConnectionString = configuration["ConnectionStrings:SpendingDatabase"];

            optionsBuilder.UseSqlServer(spendingDatabaseConnectionString);
        }
    }
}
