using System.Collections.Generic;
using Storefront.Ordering.API.Models.DataModel.OptionGroups;
using Storefront.Ordering.API.Models.DataModel.OrderItemOptions;

namespace Storefront.Ordering.API.Models.DataModel.Ingredients
{
    public sealed class Ingredient
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public long OptionGroupId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public OptionGroup OptionGroup { get; set; }
        public ICollection<OrderItemOption> OrderItemOptions { get; set; }
    }
}
