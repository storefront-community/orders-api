using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.Ordering.API.Models.DataModel.Ingredients
{
    public static class IngredientMap
    {
        public static void Configure(this EntityTypeBuilder<Ingredient> ingredient)
        {
            ingredient.ToTable("ingredient");

            ingredient.HasKey(p => new
            {
                p.Id,
                p.TenantId
            })
            .HasName("pk_ingredient");

            ingredient.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            ingredient.Property(p => p.OptionGroupId)
                .HasColumnName("option_group_id")
                .IsRequired();

            ingredient.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

            ingredient.Property(p => p.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(8,2)")
                .IsRequired();

            ingredient.Property(p => p.IsAvailable)
                .HasColumnName("is_available")
                .IsRequired();

            ingredient.HasMany(p => p.OrderItemOptions)
                .WithOne(p => p.Opitional)
                .HasForeignKey(p => new
                {
                    p.IngredientId,
                    p.TenantId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_order_food_ingredient__ingredient");
        }
    }
}
