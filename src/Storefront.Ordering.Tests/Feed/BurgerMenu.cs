using Storefront.Ordering.API.Models.DataModel.Categories;
using Storefront.Ordering.API.Models.DataModel.Foods;
using Storefront.Ordering.Tests.Factories.DataModel;

namespace Storefront.Ordering.Tests.Feed
{
    public sealed class BurgerMenu
    {
        public BurgerMenu()
        {
            Category = new Category().Burgers();
            Cheeseburger = new Food().Cheeseburger(Category);
            Toppings = new BurgerToppings(Category);
        }

        public Category Category { get; }
        public Food Cheeseburger { get; }
        public BurgerToppings Toppings { get; }
    }
}
