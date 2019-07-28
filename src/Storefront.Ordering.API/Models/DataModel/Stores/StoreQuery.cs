using System.Linq;

namespace Storefront.Ordering.API.Models.DataModel.Stores
{
    public static class StoreQuery
    {
        public static IQueryable<Store> WhereId(this IQueryable<Store> stores, long id)
        {
            return stores.Where(store => store.Id == id);
        }
    }
}
