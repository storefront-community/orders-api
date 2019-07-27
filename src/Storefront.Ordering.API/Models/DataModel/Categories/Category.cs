using System.Collections.Generic;
using Storefront.Ordering.API.Models.DataModel.Foods;
using Storefront.Ordering.API.Models.DataModel.OptionGroups;

namespace Storefront.Ordering.API.Models.DataModel.Categories
{
    public sealed class Category
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureFileId { get; set; }
        public ICollection<Food> Items { get; set; }
        public ICollection<OptionGroup> OptionGroups { get; set; }
    }
}
