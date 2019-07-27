using System.Threading.Tasks;
using Storefront.Ordering.API.Models.DataModel;
using Storefront.Ordering.API.Models.DataModel.Stores;
using Storefront.Ordering.Tests.Factories.DataModel;

namespace Storefront.Ordering.Tests.Feed
{
    public sealed class Restaurant
    {
        public Restaurant()
        {
            Store = new Store().BurgerRestaurant();

            Burgers = new BurgerMenu();
            Drinks = new DrinksMenu();
        }

        public Store Store { get; }
        public BurgerMenu Burgers { get; }
        public DrinksMenu Drinks { get; }

        public async Task Populate(ApiDbContext database)
        {
            database.Stores.Add(Store);
            database.Categories.Add(Burgers.Category);
            database.Foods.Add(Burgers.Cheeseburger);
            database.OptionGroups.Add(Burgers.Toppings.MoreFlavor);
            database.Ingredients.Add(Burgers.Toppings.ExtraBacon);
            database.Ingredients.Add(Burgers.Toppings.DoubleOnion);

            await database.SaveChangesAsync();
        }
    }
}
