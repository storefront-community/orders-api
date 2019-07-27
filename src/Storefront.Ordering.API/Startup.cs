using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storefront.Ordering.API.Authorization;
using Storefront.Ordering.API.Filters;
using Storefront.Ordering.API.Models.DataModel;
using Storefront.Ordering.API.Models.IntegrationModel.FileStorage;
using Storefront.Ordering.API.Models.IntegrationModel.FileStorage.AmazonS3;

namespace Storefront.Ordering.API
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AmazonS3Options>(_configuration.GetSection("AmazonS3"));

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseNpgsql(_configuration["ConnectionString:PostgreSQL"], pgsql =>
                {
                    pgsql.MigrationsHistoryTable(tableName: "__migration_history", schema: ApiDbContext.Schema);
                });
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new ModelValidationFilter());
            });

            services.AddDefaultCorsPolicy();

            services.AddJwtAuthentication(_configuration.GetSection("Auth"));

            services.AddTransient<IFileStorage, AmazonS3Bucket>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
