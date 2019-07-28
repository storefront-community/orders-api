using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Storefront.Ordering.Tests
{
    public class Program
    {
        private readonly IConfiguration configuration;

        public Program()
        {
            BuildPath = Path.Combine("bin", "Debug", "netcoreapp2.2");
            ProjectPath = AppContext.BaseDirectory.Replace(BuildPath, string.Empty);
            ContentPath = ProjectPath.Replace("tests", "api");

            configuration = new ConfigurationBuilder()
                .SetBasePath(ProjectPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();
        }

        public string BuildPath { get; }
        public string ProjectPath { get; }
        public string ContentPath { get; }

        public IWebHostBuilder CreateWebHostBuilder() => new WebHostBuilder()
            .UseStartup<Startup>()
            .UseEnvironment("Testing")
            .ConfigureAppConfiguration((builder, config) =>
            {
                builder.HostingEnvironment.ContentRootPath = ProjectPath;
                builder.HostingEnvironment.WebRootPath = ContentPath;
                builder.HostingEnvironment.ContentRootFileProvider = new PhysicalFileProvider(ProjectPath);
                builder.HostingEnvironment.WebRootFileProvider = new PhysicalFileProvider(ContentPath);

                config.AddConfiguration(configuration);
            });
    }
}
