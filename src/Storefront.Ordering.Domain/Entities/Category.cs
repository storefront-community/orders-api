using System.Collections.Generic;

namespace Storefront.Ordering.Domain.Entities
{
    public sealed class Category
    {
        private Category() { }

        public Category(Showcase showcase)
        {
            Showcase = showcase;
        }

        public long Id { get; private set; }
        public string PhotoId { get; private set; }
        public Showcase Showcase { get; private set; }
        public IEnumerable<Item> Items { get; private set; }
    }
}
