using System.Collections.Generic;
using Storefront.Ordering.API.Models.DataModel.Categories;
using Storefront.Ordering.API.Models.DataModel.OrderItems;

namespace Storefront.Ordering.API.Models.DataModel.Foods
{
    public sealed class Food
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureFileId { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
