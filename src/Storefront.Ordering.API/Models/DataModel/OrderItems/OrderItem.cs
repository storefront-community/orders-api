using System.Collections.Generic;
using Storefront.Ordering.API.Models.DataModel.Foods;
using Storefront.Ordering.API.Models.DataModel.OrderItemOptions;
using Storefront.Ordering.API.Models.DataModel.Orders;

namespace Storefront.Ordering.API.Models.DataModel.OrderItems
{
    public sealed class OrderItem
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public long OrderId { get; set; }
        public long FoodId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public Order Order { get; set; }
        public Food Item { get; set; }
        public ICollection<OrderItemOption> OrderItemOptions { get; set; }
    }
}
