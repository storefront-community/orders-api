using Storefront.Ordering.API.Models.DataModel.Categories;
using Storefront.Ordering.API.Models.DataModel.OptionGroups;

namespace Storefront.Ordering.Tests.Factories.DataModel
{
    public static class OptionGroupFactory
    {
        public static OptionGroup BurgerIngredients(this OptionGroup optionGroup, Category category)
        {
            optionGroup.Title = "More flavor to your burger?";
            optionGroup.MinOptions = 0;
            optionGroup.MaxOptions = null;
            optionGroup.IsMultichoice = true;
            optionGroup.Category = category;

            return optionGroup;
        }
    }
}
