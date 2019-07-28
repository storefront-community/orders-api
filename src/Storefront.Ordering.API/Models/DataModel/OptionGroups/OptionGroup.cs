using System.Collections.Generic;
using Storefront.Ordering.API.Models.DataModel.Categories;
using Storefront.Ordering.API.Models.DataModel.Ingredients;

namespace Storefront.Ordering.API.Models.DataModel.OptionGroups
{
    public sealed class OptionGroup
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public long CategoryId { get; set; }
        public string Title { get; set; }
        public int MinOptions { get; set; }
        public int? MaxOptions { get; set; }
        public bool IsMultichoice { get; set; }
        public Category Category { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
