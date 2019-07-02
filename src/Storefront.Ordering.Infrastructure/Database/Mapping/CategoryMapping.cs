using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storefront.Ordering.Domain.Entities;

namespace Storefront.Ordering.Infrastructure.Database.Mapping
{
    public sealed class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category.ToTable("categories", ApiDbContext.Schema);

            category.HasKey(p => p.Id).HasName("pk_category");

            category.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
            category.Property(p => p.PhotoId).HasColumnName("photo_id").HasMaxLength(50);

            category.OwnsOne(p => p.Showcase).Configure();

            category.HasMany(p => p.Items)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_item__category");
        }
    }
}
