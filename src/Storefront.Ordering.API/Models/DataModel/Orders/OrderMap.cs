using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.Ordering.API.Models.DataModel.Orders
{
    public static class OrderMap
    {
        public static void Configure(this EntityTypeBuilder<Order> order)
        {
            order.ToTable("orders");

            order.HasKey(p => new
            {
                p.Id,
                p.TenantId
            })
            .HasName("pk_order");

            order.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            order.Property(p => p.StoreId)
                .HasColumnName("store_id")
                .IsRequired();

            order.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(50);

            order.Property(p => p.Mobile)
                .HasColumnName("mobile")
                .HasMaxLength(20);

            order.Property(p => p.Email)
                .HasColumnName("email")
                .HasMaxLength(80);

            order.Property(p => p.OpenAt)
                .HasColumnName("open_at")
                .IsRequired();

            order.Property(p => p.CanceledAt)
                .HasColumnName("canceled_at");

            order.Property(p => p.StartedAt)
                .HasColumnName("started_at");

            order.Property(p => p.CompletedAt)
                .HasColumnName("completed_at");

            order.Property(p => p.ClosedAt)
                .HasColumnName("delivered_at");

            order.Property(p => p.Total)
                .HasColumnName("total")
                .HasColumnType("decimal(8,2)")
                .IsRequired();

            order.HasMany(p => p.OrderItems)
                .WithOne(p => p.Order)
                .HasForeignKey(p => new
                {
                    p.OrderId,
                    p.TenantId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_order_food__order");
        }
    }
}
