using System.Collections.Generic;
using System.Linq;
using Storefront.Ordering.API.Models.DataModel.Foods;
using Storefront.Ordering.API.Models.DataModel.Ingredients;
using Storefront.Ordering.API.Models.DataModel.Stores;
using Storefront.Ordering.API.Models.TransferModel.Orders;

namespace Storefront.Ordering.Tests.Factories.TransferModel
{
    public static class CreateOrderJsonExtensions
    {
        public static CreateOrderJson Store(this CreateOrderJson json, Store store)
        {
            json.StoreId = store.Id;

            return json;
        }

        public static CreateOrderJson Customer(this CreateOrderJson json, string name, string mobile)
        {
            json.Name = name;
            json.Mobile = mobile;

            return json;
        }

        public static CreateOrderJson NoFood(this CreateOrderJson json)
        {
            json.Items = new List<AddOrderItemJson>();

            return json;
        }

        public static CreateOrderJson AddUnknownFood(this CreateOrderJson json)
        {
            return json.AddFood(new Food { Id = 9999 }, quantity: 1, new Ingredient[0]);
        }

        public static CreateOrderJson AddItem(this CreateOrderJson json, Food food, int quantity)
        {
            return json.AddFood(food, quantity, new Ingredient[0]);
        }

        public static CreateOrderJson AddFood(this CreateOrderJson json, Food food, int quantity, Ingredient[] ingredients)
        {
            json.Items = json.Items ?? new List<AddOrderItemJson>();

            json.Items.Add(new AddOrderItemJson
            {
                FoodId = food.Id,
                Quantity = quantity,
                Ingredients = ingredients
                    .Select(ingredient => ingredient.Id)
                    .ToList()
            });

            return json;
        }
    }
}
