using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spending.Api.Services;
using Spending.Api.Services.Parser;
using Spending.Models;

namespace Spending.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IFormFileService, FormFileService>();
            services.AddScoped<ITextExtractorService, TextSharpPdfTextExtractorService>();
            services.AddScoped<IStatementService, StatementService>();
            services.AddScoped<CommonwealthBankParserService>();
            services.AddScoped<AmexParserService>();

            services.AddScoped<Func<string, IParserService>>(serviceProvider => key =>
            {
                if (key.Equals(Banks.Mapping[1]))
                {
                    return serviceProvider.GetService<CommonwealthBankParserService>();
                }
                else if (key.Equals(Banks.Mapping[2]))
                {
                    return serviceProvider.GetService<AmexParserService>();
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = int.MaxValue; //not recommended value
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
