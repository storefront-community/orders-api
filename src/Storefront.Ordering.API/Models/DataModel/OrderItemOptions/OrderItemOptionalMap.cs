using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.Ordering.API.Models.DataModel.OrderItemOptions
{
    public static class OrderItemOptionMap
    {
        public static void Configure(this EntityTypeBuilder<OrderItemOption> orderItemOption)
        {
            orderItemOption.ToTable("order_food_ingredients");

            orderItemOption.HasKey(p => new
            {
                p.OrderItemId,
                p.TenantId,
                p.IngredientId
            })
            .HasName("pk_order_food_ingredient");

            orderItemOption.Property(p => p.OrderItemId)
                .HasColumnName("order_food_id");

            orderItemOption.Property(p => p.TenantId)
                .HasColumnName("tenant_id");

            orderItemOption.Property(p => p.IngredientId)
                .HasColumnName("opitional_id");

            orderItemOption.Property(p => p.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(6,2)")
                .IsRequired();
        }
    }
}
