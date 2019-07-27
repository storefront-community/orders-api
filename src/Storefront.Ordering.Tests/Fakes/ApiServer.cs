using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Storefront.Ordering.API.Authorization;
using Storefront.Ordering.API.Models.DataModel;

namespace Storefront.Ordering.Tests.Fakes
{
    public sealed class ApiServer : TestServer
    {
        public ApiServer() : base(new Program().CreateWebHostBuilder()) { }

        public ApiDbContext Database => Host.Services.GetService<ApiDbContext>();
        public JwtOptions JwtOptions => Host.Services.GetService<IOptions<JwtOptions>>().Value;
    }
}
