using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.Ordering.API.Models.DataModel.Categories
{
    public static class CategoryMap
    {
        public static void Configure(this EntityTypeBuilder<Category> category)
        {
            category.ToTable("category");

            category.HasKey(p => new
            {
                p.Id,
                p.TenantId
            })
            .HasName("pk_category");

            category.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            category.Property(p => p.TenantId)
                .HasColumnName("tenant_id");

            category.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

            category.Property(p => p.Description)
                .HasColumnName("description")
                .HasMaxLength(255);

            category.Property(p => p.PictureFileId)
                .HasColumnName("picture_file_id")
                .HasMaxLength(50);

            category.HasMany(p => p.Items)
                .WithOne(p => p.Category)
                .HasForeignKey(p => new
                {
                    p.CategoryId,
                    p.TenantId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_food__category");

            category.HasMany(p => p.OptionGroups)
                .WithOne(p => p.Category)
                .HasForeignKey(p => new{
                    p.CategoryId,
                    p.TenantId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_option_group__category");
        }
    }
}
