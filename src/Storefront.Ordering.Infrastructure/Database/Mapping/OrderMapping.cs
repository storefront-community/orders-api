using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storefront.Ordering.Domain.Entities;

namespace Storefront.Ordering.Infrastructure.Database.Mapping
{
    public sealed class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> order)
        {
            order.ToTable("orders", ApiDbContext.Schema);

            order.HasKey(p => p.Id).HasName("pk_order");

            order.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
            order.Property(p => p.OpenAt).HasColumnName("open_at").IsRequired();
            order.Property(p => p.CanceledAt).HasColumnName("canceled_at");
            order.Property(p => p.StartedAt).HasColumnName("started_at");
            order.Property(p => p.CompletedAt).HasColumnName("completed_at");
            order.Property(p => p.DeliveredAt).HasColumnName("delivered_at");

            order.OwnsOne(p => p.Contact).Configure();

            order.HasMany(p => p.OrderItems)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_order_item__order");
        }
    }
}
