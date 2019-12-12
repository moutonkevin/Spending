using System;
using System.Collections.Generic;
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
using Spending.Database.Entities;

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
            services.AddScoped<ITransactionCategoryService, TransactionCategoryService>();

            #region Extractors

            services.AddScoped<TextSharpPdfTextExtractorService>();
            services.AddScoped<CsvTextExtractorService>();

            services.AddScoped<Func<string, ITextExtractorService>>(serviceProvider => key =>
            {
                if (key.Equals(Constants.Pdf))
                {
                    return serviceProvider.GetService<TextSharpPdfTextExtractorService>();
                }
                else if (key.Equals(Constants.Csv))
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
            
            services.AddScoped<Func<string, string, IParserService>>(serviceProvider => (bankKey, fileTypeKey) =>
            {
                if (bankKey.Equals(Banks.Mapping[1]))
                {
                    if (fileTypeKey.Equals(Constants.Pdf))
                    {
                        return serviceProvider.GetService<CommonwealthBankPdfParserService>();
                    }
                    else if (fileTypeKey.Equals(Constants.Csv))
                    {
                        return serviceProvider.GetService<CommonwealthBankCsvParserService>();
                    }
                    else
                    {
                        throw new KeyNotFoundException(fileTypeKey);
                    }
                }
                else if (bankKey.Equals(Banks.Mapping[2]))
                {
                    if (fileTypeKey.Equals(Constants.Pdf))
                    {
                        return serviceProvider.GetService<AmexPdfParserService>();
                    }
                    else if (fileTypeKey.Equals(Constants.Csv))
                    {
                        return serviceProvider.GetService<AmexCsvParserService>();
                    }
                    else
                    {
                        throw new KeyNotFoundException(fileTypeKey);
                    }
                }
                else if (bankKey.Equals(Banks.Mapping[5]))
                {
                    if (fileTypeKey.Equals(Constants.Pdf))
                    {
                        throw new KeyNotFoundException(fileTypeKey);
                    }
                    else if (fileTypeKey.Equals(Constants.Csv))
                    {
                        return serviceProvider.GetService<QantasMoneyCsvParserService>();
                    }
                    else
                    {
                        throw new KeyNotFoundException(fileTypeKey);
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

            services
                .AddDbContext<SpendingContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("SpendingDatabase"));
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
