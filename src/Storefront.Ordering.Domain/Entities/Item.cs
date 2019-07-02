using System.Collections.Generic;

namespace Storefront.Ordering.Domain.Entities
{
    public sealed class Item
    {
        private Item() { }

        public Item(Showcase showcase, Category category, decimal price)
        {
            Showcase = showcase;
            Category = category;
            Price = price;
            IsAvailable = true;
        }

        public long Id { get; private set; }
        public long CategoryId { get; private set; }
        public string PhotoId { get; private set; }
        public decimal Price { get; private set; }
        public bool IsAvailable { get; private set; }
        public Category Category { get; private set; }
        public Showcase Showcase { get; private set; }
        public IEnumerable<OrderItem> OrderItems { get; private set; }

        public void DoAvailable()
        {
            IsAvailable = true;
        }

        public void DoNotAvailable()
        {
            IsAvailable = false;
        }
    }
}
