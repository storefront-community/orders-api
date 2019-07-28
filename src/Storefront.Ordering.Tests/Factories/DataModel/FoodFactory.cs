using Storefront.Ordering.API.Models.DataModel.Categories;
using Storefront.Ordering.API.Models.DataModel.Foods;

namespace Storefront.Ordering.Tests.Factories.DataModel
{
    public static class FoodFactory
    {
        public static Food Cheeseburger(this Food food, Category category)
        {
            food.Name = "Cheeseburger";
            food.Description = "100g of meat combined with a delicious slice of cheese.";
            food.Price = 3.30M;
            food.IsAvailable = true;
            food.Category = category;

            return food;
        }
    }
}
