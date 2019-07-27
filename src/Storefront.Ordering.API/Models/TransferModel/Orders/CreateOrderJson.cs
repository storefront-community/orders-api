using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Storefront.Ordering.API.Models.DataModel.Orders;

namespace Storefront.Ordering.API.Models.TransferModel.Orders
{
    public sealed class CreateOrderJson
    {
        [Required]
        public long? StoreId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; }

        [EmailAddress, MaxLength(80)]
        public string Email { get; set; }

        [Required, MinLength(1), MaxLength(99)]
        public ICollection<AddOrderItemJson> Items { get; set; }

        public Order Map(long tenantId)
        {
            return new Order
            {
                TenantId = tenantId,
                StoreId = StoreId.Value,
                Name = Name,
                Mobile = Mobile,
                Email = Email,
                OpenAt = DateTime.UtcNow,
                OrderItems = Items
                    .Select(food => food.Map(tenantId))
                    .ToList()
            };
        }
    }
}
