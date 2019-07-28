using Storefront.Ordering.API.Models.DataModel.Categories;

namespace Storefront.Ordering.Tests.Factories.DataModel
{
    public static class CategoryFactory
    {
        public static Category Burgers(this Category category)
        {
            category.Name = "Hamburgers";
            category.Description = "Great burger and very delicious fries.";

            return category;
        }
    }
}
