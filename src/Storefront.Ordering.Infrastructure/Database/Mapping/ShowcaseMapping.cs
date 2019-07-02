using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storefront.Ordering.Domain.Entities;

namespace Storefront.Ordering.Infrastructure.Database.Mapping
{
    public static class ShowcaseMapping
    {
        public static void Configure<TEntity>(this ReferenceOwnershipBuilder<TEntity, Showcase> showcase)
            where TEntity : class
        {
            showcase.Property(p => p.Name).HasColumnName("name").HasMaxLength(80);
            showcase.Property(p => p.Description).HasColumnName("description").HasMaxLength(255);

            showcase.Ignore(p => p.Photo);
        }
    }
}
