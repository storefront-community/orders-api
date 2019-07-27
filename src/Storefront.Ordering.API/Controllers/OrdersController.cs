using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storefront.Ordering.API.Authorization;
using Storefront.Ordering.API.Models.DataModel;
using Storefront.Ordering.API.Models.TransferModel.Foods;
using Storefront.Ordering.API.Models.TransferModel.Ingredients;
using Storefront.Ordering.API.Models.TransferModel.Orders;
using Storefront.Ordering.API.Models.TransferModel.Stores;
using Storefront.Ordering.API.Models.UseCaseModel.Orders;

namespace Storefront.Ordering.API.Controllers
{
    [Route(""), Authorize]
    public sealed class OrdersController : Controller
    {
        private readonly ApiDbContext _dbContext;

        public OrdersController(ApiDbContext apiDbContext)
        {
            _dbContext = apiDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromHeader] string authorization, [FromBody] CreateOrderJson json)
        {
            var payload = new AuthPayload(authorization);
            var order = json.Map(payload.TenantId);
            var createOrder = new CreateOrder(order);

            if (!await createOrder.SaveTo(_dbContext))
            {
                if (createOrder.StoreNotFound)
                {
                    return new ErrorStoreNotFoundJson();
                }

                if (createOrder.ItemIdsNotFound.Any())
                {
                    return new ErrorFoodNotFoundJson(createOrder.ItemIdsNotFound);
                }

                if (createOrder.IngredientIdsNotFound.Any())
                {
                    return new ErrorIngredientNotFoundJson(createOrder.IngredientIdsNotFound);
                }
            }

            return new ReadOrderJson(createOrder.Order);
        }
    }
}
