using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.Ordering.API.Models.DataModel.OptionGroups
{
    public static class OptionGroupMap
    {
        public static void Configure(this EntityTypeBuilder<OptionGroup> optionGroup)
        {
            optionGroup.ToTable("option_group");

            optionGroup.HasKey(p => new
            {
                p.Id,
                p.TenantId
            })
            .HasName("pk_option_group");

            optionGroup.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            optionGroup.Property(p => p.CategoryId)
                .HasColumnName("category_id")
                .IsRequired();

            optionGroup.Property(p => p.Title)
                .HasColumnName("title")
                .HasMaxLength(50);

            optionGroup.Property(p => p.MinOptions)
                .HasColumnName("min_options")
                .HasMaxLength(2)
                .IsRequired();

            optionGroup.Property(p => p.MaxOptions)
                .HasColumnName("max_options")
                .HasMaxLength(2);

            optionGroup.Property(p => p.IsMultichoice)
                .HasColumnName("is_multichoice")
                .IsRequired();

            optionGroup.HasMany(p => p.Ingredients)
                .WithOne(p => p.OptionGroup)
                .HasForeignKey(p => new
                {
                    p.OptionGroupId,
                    p.TenantId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_option_group__ingredient");
        }
    }
}
