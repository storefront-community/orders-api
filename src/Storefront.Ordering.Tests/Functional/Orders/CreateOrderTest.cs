using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Storefront.Ordering.API.Models.DataModel.Orders;
using Storefront.Ordering.API.Models.TransferModel;
using Storefront.Ordering.API.Models.TransferModel.Foods;
using Storefront.Ordering.API.Models.TransferModel.Orders;
using Storefront.Ordering.Tests.Factories.TransferModel;
using Storefront.Ordering.Tests.Fakes;
using Storefront.Ordering.Tests.Feed;
using Xunit;

namespace Storefront.Ordering.Tests.Functional.Orders
{
    public sealed class CreateOrderTest
    {
        private readonly ApiServer _server;

        public CreateOrderTest()
        {
            _server = new ApiServer();
        }

        [Fact]
        public async Task ShouldCreateSuccessfully()
        {
            var restaurant = new Restaurant();
            await restaurant.Populate(_server.Database);

            var path = "/";
            var jsonRequest = new CreateOrderJson()
                .Store(restaurant.Store)
                .Customer(name: "Mary", mobile: "555 123 123")
                .AddItem(restaurant.Burgers.Cheeseburger, quantity: 1);

            var token = new ApiToken(_server.JwtOptions);
            var client = new ApiClient(_server, token);
            var response = await client.PostJsonAsync(path, jsonRequest);
            var order = await _server.Database.Orders.SingleAsync();
            var cheeseburger = await _server.Database.OrderItems.SingleAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(restaurant.Store.Id, order.Store.Id);
            Assert.Equal("Mary", order.Name);
            Assert.Equal("555 123 123", order.Mobile);
            Assert.Equal(3.30M, order.Total);
            Assert.Equal(1, cheeseburger.Quantity);
            Assert.Equal(3.30M, cheeseburger.Price);
        }

        [Fact]
        public async Task ShouldCreateWithIngredientSuccessfully()
        {
            var restaurant = new Restaurant();
            await restaurant.Populate(_server.Database);

            var path = "/";
            var jsonRequest = new CreateOrderJson()
                .Store(restaurant.Store)
                .Customer(name: "Mary", mobile: "555 123 123")
                .AddFood(restaurant.Burgers.Cheeseburger, quantity: 1, ingredients: new[]
                {
                    restaurant.Burgers.Toppings.ExtraBacon
                });

            var token = new ApiToken(_server.JwtOptions);
            var client = new ApiClient(_server, token);
            var response = await client.PostJsonAsync(path, jsonRequest);
            var order = await _server.Database.Orders.IncludeOrderItemsAndOptions().SingleAsync();
            var cheeseburger = order.OrderItems.Single();
            var extraBacon = cheeseburger.OrderItemOptions.Single();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(restaurant.Store.Id, order.Store.Id);
            Assert.Equal("Mary", order.Name);
            Assert.Equal(4.05M, order.Total);
            Assert.Equal(1, cheeseburger.Quantity);
            Assert.Equal(3.30M, cheeseburger.Price);
            Assert.Equal(0.75M, extraBacon.Price);
            Assert.Single(order.OrderItems);
        }

        [Fact]
        public async Task ShouldHaveAtLeastOneItem()
        {
            var restaurant = new Restaurant();
            await restaurant.Populate(_server.Database);

            var path = "/";
            var jsonRequest = new CreateOrderJson()
                .Store(restaurant.Store)
                .Customer(name: "Mary", mobile: "555 123 123")
                .NoFood();

            var token = new ApiToken(_server.JwtOptions);
            var client = new ApiClient(_server, token);
            var response = await client.PostJsonAsync(path, jsonRequest);
            var jsonResponse = await client.ReadJsonAsync<BadRequestJson>(response);
            var expectedError = "The field Items must be a string or array type with a minimum length of '1'.";

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(jsonResponse.Errors, error => error == expectedError);
            Assert.Single(jsonResponse.Errors);
        }

        [Fact]
        public async Task ShouldNotCreateWithInexistentItem()
        {
            var restaurant = new Restaurant();
            await restaurant.Populate(_server.Database);

            var path = "/";
            var jsonRequest = new CreateOrderJson()
                .Store(restaurant.Store)
                .Customer(name: "Mary", mobile: "555 123 123")
                .AddUnknownFood();

            var token = new ApiToken(_server.JwtOptions);
            var client = new ApiClient(_server, token);
            var response = await client.PostJsonAsync(path, jsonRequest);
            var jsonResponse = await client.ReadJsonAsync<ErrorFoodNotFoundJson>(response);

            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
            Assert.Equal("FOOD_NOT_FOUND", jsonResponse.Error);
            Assert.Contains(jsonResponse.Ids, id => id == 9999);
        }
    }
}
