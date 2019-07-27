using Microsoft.EntityFrameworkCore;
using Storefront.Ordering.API.Models.DataModel.Categories;
using Storefront.Ordering.API.Models.DataModel.Foods;
using Storefront.Ordering.API.Models.DataModel.OptionGroups;
using Storefront.Ordering.API.Models.DataModel.Ingredients;
using Storefront.Ordering.API.Models.DataModel.OrderItemOptions;
using Storefront.Ordering.API.Models.DataModel.OrderItems;
using Storefront.Ordering.API.Models.DataModel.Orders;
using Storefront.Ordering.API.Models.DataModel.Stores;

namespace Storefront.Ordering.API.Models.DataModel
{
    public sealed class ApiDbContext : DbContext
    {
        public const string Schema = "ordering";

        public ApiDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<OptionGroup> OptionGroups { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemOption> OrderItemOptions { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<Category>().Configure();
            modelBuilder.Entity<Food>().Configure();
            modelBuilder.Entity<OptionGroup>().Configure();
            modelBuilder.Entity<Ingredient>().Configure();
            modelBuilder.Entity<OrderItem>().Configure();
            modelBuilder.Entity<OrderItemOption>().Configure();
            modelBuilder.Entity<Order>().Configure();
            modelBuilder.Entity<Store>().Configure();
        }

        [DbFunction("public.ci_ai")]
        public static string Normalize(string text) => text;
    }
}
