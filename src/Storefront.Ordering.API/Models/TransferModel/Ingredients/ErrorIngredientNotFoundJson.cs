using System.Collections.Generic;
using System.Linq;

namespace Storefront.Ordering.API.Models.TransferModel.Ingredients
{
    public sealed class ErrorIngredientNotFoundJson : UnprocessableEntityJson
    {
        public ErrorIngredientNotFoundJson(IEnumerable<long> foodIdsNotFound)
        {
            Error = "OPTIONAL_NOT_FOUND";
            Ids = foodIdsNotFound.ToList();
        }

        public ICollection<long> Ids { get; set; }
    }
}
