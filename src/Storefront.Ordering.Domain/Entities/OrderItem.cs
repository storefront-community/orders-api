namespace Storefront.Ordering.Domain.Entities
{
    public sealed class OrderItem
    {
        private OrderItem() { }

        public OrderItem(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public long OrderId { get; private set; }
        public long ItemId { get; private set; }
        public int Quantity { get; private set; }
        public Order Order { get; private set; }
        public Item Item { get; private set; }
    }
}
