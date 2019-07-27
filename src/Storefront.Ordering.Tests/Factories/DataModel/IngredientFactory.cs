using Storefront.Ordering.API.Models.DataModel.OptionGroups;
using Storefront.Ordering.API.Models.DataModel.Ingredients;

namespace Storefront.Ordering.Tests.Factories.DataModel
{
    public static class IngredientFactory
    {
        public static Ingredient ExtraBacon(this Ingredient ingredient, OptionGroup group)
        {
            ingredient.Name = "Extra bacon (2 slices)";
            ingredient.Price = 0.75M;
            ingredient.OptionGroup = group;

            return ingredient;
        }

        public static Ingredient DoubleOnions(this Ingredient ingredient, OptionGroup group)
        {
            ingredient.Name = "Double caramelized onions";
            ingredient.Price = 0.45M;
            ingredient.OptionGroup = group;

            return ingredient;
        }
    }
}
