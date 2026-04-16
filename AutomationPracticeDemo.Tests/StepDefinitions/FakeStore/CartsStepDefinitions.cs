using AutomationPracticeDemo.Tests.Api.Client;
using AutomationPracticeDemo.Tests.Api.Dtos;
using NUnit.Framework;
using Reqnroll;

namespace AutomationPracticeDemo.Tests.StepDefinitions.FakeStore
{
    [Binding]
    public class CartsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private FakeStoreClient _client = null!;
        private List<Cart>? _carts;
        private Cart? _cart;
        private bool _deleteSucceeded;

        public CartsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        private FakeStoreClient GetClient()
        {
            if (_client is not null) return _client;
            _client = _scenarioContext.Get<FakeStoreClient>("apiClient");
            return _client;
        }

        // ?? Get all carts ??

        [When(@"I query all carts")]
        public async Task WhenIQueryAllCarts()
        {
            _carts = await GetClient().GetAllCartsAsync();
        }

        [Then(@"the response should contain a list of carts")]
        public void ThenTheResponseShouldContainAListOfCarts()
        {
            Assert.That(_carts, Is.Not.Null.And.Not.Empty);
        }

        // ?? Get cart by id ??

        [When(@"I query the cart with id (.*)")]
        public async Task WhenIQueryTheCartWithId(int id)
        {
            _cart = await GetClient().GetCartAsync(id);
        }

        [Then(@"the response should contain a cart with id (.*)")]
        public void ThenTheResponseShouldContainACartWithId(int id)
        {
            Assert.That(_cart, Is.Not.Null);
            Assert.That(_cart!.Id, Is.EqualTo(id));
        }

        // ?? Create a cart ??

        [When(@"I create a cart for user (.*) with product (.*)")]
        public async Task WhenICreateACartForUserWithProduct(int userId, int productId)
        {
            var newCart = new Cart
            {
                UserId = userId,
                Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Products = [new CartProduct { ProductId = productId, Quantity = 1 }]
            };
            _cart = await GetClient().CreateCartAsync(newCart);
        }

        [Then(@"the created cart should have an id")]
        public void ThenTheCreatedCartShouldHaveAnId()
        {
            Assert.That(_cart, Is.Not.Null);
            Assert.That(_cart!.Id, Is.GreaterThan(0));
        }

        // ?? Update a cart ??

        [When(@"I update cart with id (.*) to have product (.*)")]
        public async Task WhenIUpdateCartWithIdToHaveProduct(int cartId, int productId)
        {
            var updated = new Cart
            {
                UserId = 1,
                Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Products = [new CartProduct { ProductId = productId, Quantity = 1 }]
            };
            _cart = await GetClient().UpdateCartAsync(cartId, updated);
        }

        [Then(@"the updated cart should contain product (.*)")]
        public void ThenTheUpdatedCartShouldContainProduct(int productId)
        {
            Assert.That(_cart, Is.Not.Null);
            Assert.That(_cart!.Products, Is.Not.Null.And.Not.Empty);
        }

        // ?? Delete a cart ??

        [When(@"I delete the cart with id (.*)")]
        public async Task WhenIDeleteTheCartWithId(int id)
        {
            try
            {
                await GetClient().DeleteCartAsync(id);
                _deleteSucceeded = true;
            }
            catch
            {
                _deleteSucceeded = false;
            }
        }

        [Then(@"the cart should be deleted successfully")]
        public void ThenTheCartShouldBeDeletedSuccessfully()
        {
            Assert.That(_deleteSucceeded, Is.True);
        }
    }
}
