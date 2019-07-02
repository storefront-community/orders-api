using System;
using System.Collections.Generic;

namespace Storefront.Ordering.Domain.Entities
{
    public sealed class Order
    {
        private Order() { }

        public Order(Contact contact)
        {
            Contact = contact;
            OrderItems = new HashSet<OrderItem>();
        }

        public long Id { get; private set; }
        public Contact Contact { get; private set; }
        public DateTime OpenAt { get; private set; }
        public DateTime? CanceledAt { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DateTime? DeliveredAt { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; }

        public void Cancel()
        {
            CanceledAt = CanceledAt ?? DateTime.UtcNow;
        }

        public void Start()
        {
            StartedAt = StartedAt ?? DateTime.UtcNow;
        }

        public void Complete()
        {
            CompletedAt = CompletedAt ?? DateTime.UtcNow;
        }

        public void Deliver()
        {
            DeliveredAt = DeliveredAt ?? DateTime.UtcNow;
        }
    }
}
