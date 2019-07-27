using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storefront.Ordering.API.Authorization;
using Storefront.Ordering.API.Filters;
using Storefront.Ordering.API.Models.DataModel;
using Storefront.Ordering.API.Models.IntegrationModel.FileStorage;
using Storefront.Ordering.Tests.Fakes;

namespace Storefront.Ordering.Tests
{
    public sealed class Startup
    {
        public readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ModelValidationFilter());
            })
            .AddApplicationPart(typeof(Storefront.Ordering.API.Startup).Assembly);

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseInMemoryDatabase("db_test");
            });

            services.AddDefaultCorsPolicy();

            services.AddJwtAuthentication(_configuration.GetSection("Auth"));

            services.AddTransient<IFileStorage, MemoryFileStorage>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
