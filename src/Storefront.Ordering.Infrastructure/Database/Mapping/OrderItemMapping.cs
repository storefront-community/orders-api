using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storefront.Ordering.Domain.Entities;

namespace Storefront.Ordering.Infrastructure.Database.Mapping
{
    public sealed class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> orderItem)
        {
            orderItem.ToTable("order_items", ApiDbContext.Schema);

            orderItem.HasKey(p => new { p.OrderId, p.ItemId }).HasName("pk_order_item");

            orderItem.Property(p => p.OrderId).HasColumnName("order_id");
            orderItem.Property(p => p.ItemId).HasColumnName("item_id");
            orderItem.Property(p => p.Quantity).HasColumnName("quantity").IsRequired();
        }
    }
}
