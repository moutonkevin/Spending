using System.IO;
using Microsoft.Extensions.Configuration;

namespace Spending.Migration
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            
            builder.AddUserSecrets<Startup>();

            var configuration = builder.Build();

            var spendingDatabaseConnectionString = configuration["ConnectionStrings:SpendingDatabase"];
        }
    }
}
