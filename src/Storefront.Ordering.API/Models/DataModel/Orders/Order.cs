using System;
using System.Collections.Generic;
using Storefront.Ordering.API.Models.DataModel.OrderItems;
using Storefront.Ordering.API.Models.DataModel.Stores;

namespace Storefront.Ordering.API.Models.DataModel.Orders
{
    public sealed class Order
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public long StoreId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime OpenAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public decimal Total { get; set; }
        public Store Store { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
