namespace Storefront.Ordering.API.Models.DataModel.Orders
{
    public class OrderStatus
    {
        private readonly Order _order;

        public OrderStatus(Order order)
        {
            _order = order;
        }

        public override string ToString()
        {
            if (_order.ClosedAt != null) return "Closed";
            if (_order.CanceledAt != null) return "Canceled";
            if (_order.CompletedAt != null) return "Completed";
            if (_order.StartedAt != null) return "Started";

            return "Opened";
        }
    }
}
