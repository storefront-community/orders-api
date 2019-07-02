using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storefront.Ordering.API.Filters;
using Storefront.Ordering.Domain.Repositories;
using Storefront.Ordering.Infrastructure.AmazonS3;
using Storefront.Ordering.Infrastructure.Database;

namespace Storefront.Ordering.API
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
            services.Configure<AmazonS3Options>(Configuration.GetSection("AmazonS3"));

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseNpgsql(Configuration["ConnectionString:PostgreSQL"], pgsql =>
                {
                    pgsql.MigrationsHistoryTable(tableName: "__migration_history", schema: ApiDbContext.Schema);
                });
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new ModelValidationAttribute());
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddTransient<IPhotoRepository, AmazonS3Api>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseMvc();
        }
    }
}
