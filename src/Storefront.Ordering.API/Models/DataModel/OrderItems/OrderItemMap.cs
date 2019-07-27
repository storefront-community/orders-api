using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.Ordering.API.Models.DataModel.OrderItems
{
    public static class OrderItemMap
    {
        public static void Configure(this EntityTypeBuilder<OrderItem> orderItem)
        {
            orderItem.ToTable("order_foods");

            orderItem.HasKey(p => new
            {
                p.Id,
                p.TenantId
            })
            .HasName("pk_order_food");

            orderItem.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            orderItem.Property(p => p.TenantId)
                .HasColumnName("tenant_id");

            orderItem.Property(p => p.OrderId)
                .HasColumnName("order_id");

            orderItem.Property(p => p.FoodId)
                .HasColumnName("food_id");

            orderItem.Property(p => p.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(6,2)")
                .IsRequired();

            orderItem.Property(p => p.Quantity)
                .HasColumnName("quantity")
                .HasMaxLength(2)
                .IsRequired();

            orderItem.HasMany(p => p.OrderItemOptions)
                .WithOne(p => p.OrderItem)
                .HasForeignKey(p => new
                {
                    p.OrderItemId,
                    p.TenantId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_order_food_ingredient__order_food");
        }
    }
}
