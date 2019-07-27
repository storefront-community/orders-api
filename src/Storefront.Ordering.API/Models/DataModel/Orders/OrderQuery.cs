using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Storefront.Ordering.API.Models.DataModel.Orders
{
    public static class OrderQuery
    {
        public static IQueryable<Order> IncludeOrderItemsAndOptions(this IQueryable<Order> orders)
        {
            return orders.Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.OrderItemOptions);
        }
    }
}
