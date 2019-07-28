using Storefront.Ordering.API.Models.DataModel.Stores;

namespace Storefront.Ordering.Tests.Factories.DataModel
{
    public static class StoreFactory
    {
        public static Store BurgerRestaurant(this Store store)
        {
            store.Name = "Best Burger's Restaurant";

            return store;
        }
    }
}
