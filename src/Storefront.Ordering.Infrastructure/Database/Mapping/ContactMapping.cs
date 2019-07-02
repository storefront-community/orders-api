using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storefront.Ordering.Domain.Entities;

namespace Storefront.Ordering.Infrastructure.Database.Mapping
{
    public static class ContactMapping
    {
        public static void Configure<TEntity>(this ReferenceOwnershipBuilder<TEntity, Contact> showcase)
            where TEntity : class
        {
            showcase.Property(p => p.Name).HasColumnName("name").HasMaxLength(50);
            showcase.Property(p => p.Mobile).HasColumnName("mobile").HasMaxLength(20);
            showcase.Property(p => p.Email).HasColumnName("email").HasMaxLength(80);
        }
    }
}
