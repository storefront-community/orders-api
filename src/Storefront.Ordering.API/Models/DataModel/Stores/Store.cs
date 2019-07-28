using System.Collections.Generic;
using Storefront.Ordering.API.Models.DataModel.Orders;

namespace Storefront.Ordering.API.Models.DataModel.Stores
{
    public sealed class Store
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
