using Storefront.Ordering.API.Models.DataModel.Ingredients;
using Storefront.Ordering.API.Models.DataModel.OrderItems;

namespace Storefront.Ordering.API.Models.DataModel.OrderItemOptions
{
    public sealed class OrderItemOption
    {
        public long OrderItemId { get; set; }
        public long TenantId { get; set; }
        public long IngredientId { get; set; }
        public decimal Price { get; set; }
        public Ingredient Opitional { get; set; }
        public OrderItem OrderItem { get; set; }
    }
}
