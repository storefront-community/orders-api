using Storefront.Ordering.API.Models.DataModel.Categories;
using Storefront.Ordering.API.Models.DataModel.OptionGroups;
using Storefront.Ordering.API.Models.DataModel.Ingredients;
using Storefront.Ordering.Tests.Factories.DataModel;

namespace Storefront.Ordering.Tests.Feed
{
    public class BurgerToppings
    {
        public BurgerToppings(Category category)
        {
            MoreFlavor = new OptionGroup().BurgerIngredients(category);
            ExtraBacon = new Ingredient().ExtraBacon(MoreFlavor);
            DoubleOnion = new Ingredient().DoubleOnions(MoreFlavor);
        }

        public OptionGroup MoreFlavor { get; }
        public Ingredient ExtraBacon { get; }
        public Ingredient DoubleOnion { get; }
    }
}
