using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.Ordering.API.Models.DataModel.Foods
{
    public static class FoodMap
    {
        public static void Configure(this EntityTypeBuilder<Food> food)
        {
            food.ToTable("foods");

            food.HasKey(p => new
            {
                p.Id,
                p.TenantId
            })
            .HasName("pk_food");

            food.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            food.Property(p => p.TenantId)
                .HasColumnName("tenant_id");

            food.Property(p => p.CategoryId)
                .HasColumnName("category_id")
                .IsRequired();

            food.Property(p => p.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(8,2)")
                .IsRequired();

            food.Property(p => p.IsAvailable)
                .HasColumnName("is_available")
                .IsRequired();

            food.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

            food.Property(p => p.Description)
                .HasColumnName("description")
                .HasMaxLength(255);

            food.Property(p => p.PictureFileId)
                .HasColumnName("picture_file_id")
                .HasMaxLength(50);

            food.HasMany(p => p.OrderItems)
                .WithOne(p => p.Item)
                .HasForeignKey(p => new
                {
                    p.FoodId,
                    p.TenantId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_order_food__food");
        }
    }
}
