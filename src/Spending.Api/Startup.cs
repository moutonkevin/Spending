using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spending.Api.DataAccess;
using Spending.Api.Services;
using Spending.Api.Services.Extractors;
using Spending.Api.Services.Parsers;
using Spending.Api.Services.Parsers.Csv;
using Spending.Api.Services.Parsers.Pdf;
using Spending.Database.Context;

namespace Spending.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureSystemServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = int.MaxValue;
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSystemServices(services);

            services.AddScoped<IFormFileService, FormFileService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionCategoryPatternService, TransactionCategoryPatternService>();

            #region Extractors

            services.AddScoped<TextSharpPdfTextExtractorService>();
            services.AddScoped<CsvTextExtractorService>();

            services.AddScoped<Func<int, ITextExtractorService>>(serviceProvider => fileTypeId =>
            {
                if (fileTypeId == Constants.PdFile.Id)
                {
                    return serviceProvider.GetService<TextSharpPdfTextExtractorService>();
                }
                else if (fileTypeId == Constants.CsvFile.Id)
                {
                    return serviceProvider.GetService<CsvTextExtractorService>();
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            });

            #endregion

            #region Parsers

            services.AddScoped<CommonwealthBankPdfParserService>();
            services.AddScoped<CommonwealthBankCsvParserService>();
            services.AddScoped<AmexPdfParserService>();
            services.AddScoped<AmexCsvParserService>();
            services.AddScoped<QantasMoneyCsvParserService>();
            
            services.AddScoped<Func<string, int, IParserService>>(serviceProvider => (bankKey, fileTypeId) =>
            {
                if (bankKey.Equals(Database.Constants.Banks.List.FirstOrDefault(b => b.Id == 1)))
                {
                    if (fileTypeId == Constants.PdFile.Id)
                    {
                        return serviceProvider.GetService<CommonwealthBankPdfParserService>();
                    }
                    else if (fileTypeId == Constants.CsvFile.Id)
                    {
                        return serviceProvider.GetService<CommonwealthBankCsvParserService>();
                    }
                    else
                    {
                        throw new KeyNotFoundException(fileTypeId.ToString());
                    }
                }
                else if (bankKey.Equals(Database.Constants.Banks.List.FirstOrDefault(b => b.Id == 2)))
                {
                    if (fileTypeId == Constants.PdFile.Id)
                    {
                        return serviceProvider.GetService<AmexPdfParserService>();
                    }
                    else if (fileTypeId == Constants.CsvFile.Id)
                    {
                        return serviceProvider.GetService<AmexCsvParserService>();
                    }
                    else
                    {
                        throw new KeyNotFoundException(fileTypeId.ToString());
                    }
                }
                else if (bankKey.Equals(Database.Constants.Banks.List.FirstOrDefault(b => b.Id == 5)))
                {
                    if (fileTypeId == Constants.PdFile.Id)
                    {
                        throw new KeyNotFoundException(fileTypeId.ToString());
                    }
                    else if (fileTypeId == Constants.CsvFile.Id)
                    {
                        return serviceProvider.GetService<QantasMoneyCsvParserService>();
                    }
                    else
                    {
                        throw new KeyNotFoundException(fileTypeId.ToString());
                    }
                }
                else
                {
                    throw new KeyNotFoundException(bankKey);
                }
            });

            #endregion

            services.AddScoped<ITransactionDataAccess, TransactionDatabaseDataAccess>();
            services.AddScoped<ITransactionCategoryPatternDatabaseDataAccess, TransactionCategoryPatternDatabaseDataAccess>();
            services.AddScoped<ITransactionCategoryDatabaseDataAccess, TransactionCategoryDatabaseDataAccess>();

            services
                .AddDbContext<SpendingContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("SpendingDatabase"));
                    options.EnableSensitiveDataLogging();
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
