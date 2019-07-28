using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Storefront.Ordering.API.Models.DataModel.Foods
{
    public static class FoodQuery
    {
        public static IQueryable<Food> IncludeCategoryAndIngredients(this IQueryable<Food> foods)
        {
            return foods.Include(food => food.Category)
                .ThenInclude(category => category.OptionGroups)
                    .ThenInclude(optionGroup => optionGroup.Ingredients);
        }

        public static IQueryable<Food> WhereIdIn(this IQueryable<Food> foods, IEnumerable<long> ids)
        {
            return foods.Where(food => ids.Contains(food.Id));
        }
    }
}
