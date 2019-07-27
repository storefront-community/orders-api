using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Storefront.Ordering.API.Models.DataModel.OrderItemOptions;
using Storefront.Ordering.API.Models.DataModel.OrderItems;

namespace Storefront.Ordering.API.Models.TransferModel.Orders
{
    public sealed class AddOrderItemJson
    {
        [Required]
        public long? FoodId { get; set; }

        [Required, Range(1, 99)]
        public int? Quantity { get; set; }

        [Required]
        public ICollection<long> Ingredients { get; set; }

        public OrderItem Map(long tenantId)
        {
            return new OrderItem
            {
                TenantId = tenantId,
                FoodId = FoodId.Value,
                Quantity = Quantity.Value,
                OrderItemOptions = Ingredients
                    .Select(ingredientId => new OrderItemOption
                    {
                        IngredientId = ingredientId
                    })
                    .ToList()
            };
        }
    }
}
