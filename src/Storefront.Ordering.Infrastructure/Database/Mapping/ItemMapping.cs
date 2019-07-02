using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storefront.Ordering.Domain.Entities;

namespace Storefront.Ordering.Infrastructure.Database.Mapping
{
    public sealed class ItemMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> item)
        {
            item.ToTable("items", ApiDbContext.Schema);

            item.HasKey(p => p.Id).HasName("pk_item");

            item.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
            item.Property(p => p.CategoryId).HasColumnName("category_id");
            item.Property(p => p.PhotoId).HasColumnName("photo_id").HasMaxLength(50);
            item.Property(p => p.Price).HasColumnName("price").HasColumnType("decimal(8,2)").IsRequired();
            item.Property(p => p.IsAvailable).HasColumnName("is_available");

            item.OwnsOne(p => p.Showcase).Configure();

            item.HasMany(p => p.OrderItems)
                .WithOne(p => p.Item)
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_order_item__item");
        }
    }
}
