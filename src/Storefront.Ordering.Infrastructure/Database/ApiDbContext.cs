using Microsoft.EntityFrameworkCore;
using Storefront.Ordering.Domain.Entities;
using Storefront.Ordering.Infrastructure.Database.Mapping;

namespace Storefront.Ordering.Infrastructure.Database
{
    public sealed class ApiDbContext : DbContext
    {
        public const string Schema = "ordering";

        public ApiDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new ItemMapping());
            modelBuilder.ApplyConfiguration(new OrderItemMapping());
            modelBuilder.ApplyConfiguration(new OrderMapping());
        }

        [DbFunction("public.ci_ai")]
        public static string Normalize(string text) => text;
    }
}
