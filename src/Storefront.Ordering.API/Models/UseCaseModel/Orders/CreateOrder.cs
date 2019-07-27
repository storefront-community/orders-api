using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Storefront.Ordering.API.Models.DataModel;
using Storefront.Ordering.API.Models.DataModel.Foods;
using Storefront.Ordering.API.Models.DataModel.Ingredients;
using Storefront.Ordering.API.Models.DataModel.Orders;
using Storefront.Ordering.API.Models.DataModel.Stores;

namespace Storefront.Ordering.API.Models.UseCaseModel.Orders
{
    public sealed class CreateOrder
    {
        public CreateOrder(Order order)
        {
            Order = order;
        }

        public Order Order { get; private set; }
        public bool StoreNotFound { get; private set; }
        public IEnumerable<long> ItemIdsNotFound { get; private set; }
        public IEnumerable<long> IngredientIdsNotFound { get; private set; }

        public async Task<bool> SaveTo(ApiDbContext dbContext)
        {
            await FindStore(dbContext);
            if (StoreNotFound) return false;

            var foods = await FindItems(dbContext);
            if (ItemIdsNotFound.Any()) return false;

            var ingredients = FindIngredients(foods);
            if (IngredientIdsNotFound.Any()) return false;

            Calculate(foods, ingredients);

            dbContext.Orders.Add(Order);
            await dbContext.SaveChangesAsync();

            return true;
        }

        private void Calculate(IEnumerable<Food> foods, IEnumerable<Ingredient> ingredients)
        {
            foreach (var orderItem in Order.OrderItems)
            {
                var addedItem = foods.Single(food => food.Id == orderItem.FoodId);

                orderItem.Price = addedItem.Price;
                orderItem.Total = addedItem.Price;

                foreach (var orderItemOption in orderItem.OrderItemOptions)
                {
                    var addedIngredient = ingredients.Single(ingredient =>
                        ingredient.Id == orderItemOption.IngredientId);

                    orderItemOption.Price = addedIngredient.Price;
                    orderItem.Total += addedIngredient.Price;
                }

                orderItem.Total *= orderItem.Quantity;
            }

            Order.Total = Order.OrderItems
                .Sum(orderItem => orderItem.Total);
        }

        private async Task FindStore(ApiDbContext dbContext)
        {
            Order.Store = await dbContext.Stores
                .WhereId(Order.StoreId)
                .SingleOrDefaultAsync();

            StoreNotFound = Order.Store == null;
        }

        private async Task<IEnumerable<Food>> FindItems(ApiDbContext dbContext)
        {
            var foodIds = Order.OrderItems
                .Select(orderItem => orderItem.FoodId);

            var foods = await dbContext.Foods
                .IncludeCategoryAndIngredients()
                .WhereIdIn(foodIds)
                .ToListAsync();

            ItemIdsNotFound = foodIds
                .Where(foodId => !foods
                    .Any(food => food.Id == foodId))
                .ToList();

            return foods;
        }

        private IEnumerable<Ingredient> FindIngredients(IEnumerable<Food> foods)
        {
            var ingredients = foods
                .Select(food => food.Category)
                .SelectMany(category => category.OptionGroups)
                .SelectMany(optionGroup => optionGroup.Ingredients)
                .ToList();

            IngredientIdsNotFound = Order.OrderItems
                .SelectMany(orderItem => orderItem.OrderItemOptions)
                .Select(orderItemOption => orderItemOption.IngredientId)
                .Where(ingredientId => !ingredients
                    .Any(ingredient => ingredient.Id == ingredientId))
                .ToList();

            return ingredients;
        }
    }
}
