using System.Collections.Generic;
using System.Linq;

namespace Storefront.Ordering.API.Models.TransferModel.Foods
{
    public sealed class ErrorFoodNotFoundJson : UnprocessableEntityJson
    {
        public ErrorFoodNotFoundJson() { }

        public ErrorFoodNotFoundJson(IEnumerable<long> foodIdsNotFound)
        {
            Error = "FOOD_NOT_FOUND";
            Ids = foodIdsNotFound.ToList();
        }

        public ICollection<long> Ids { get; set; }
    }
}
