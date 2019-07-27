using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.Ordering.API.Models.DataModel.Stores
{
    public static class StoreMap
    {
        public static void Configure(this EntityTypeBuilder<Store> store)
        {
            store.ToTable("stores");

            store.HasKey(p => new
            {
                p.Id,
                p.TenantId
            })
            .HasName("pk_store");

            store.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            store.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

            store.Property(p => p.Active)
                .HasColumnName("active")
                .IsRequired();

            store.HasMany(p => p.Orders)
                .WithOne(p => p.Store)
                .HasForeignKey(p => new
                {
                    p.StoreId,
                    p.TenantId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_order__store");
        }
    }
}
