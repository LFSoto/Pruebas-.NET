using AutomationPracticeDemo.Tests.Api.Client;
using AutomationPracticeDemo.Tests.Api.Dtos;
using NUnit.Framework;
using Reqnroll;

namespace AutomationPracticeDemo.Tests.StepDefinitions.FakeStore
{
    [Binding]
    public class ProductsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private FakeStoreClient _client = null!;
        private List<Product>? _products;
        private Product? _product;
        private bool _deleteSucceeded;

        public ProductsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have the API base ""(.*)""")]
        public void GivenIHaveTheAPIBase(string baseUrl)
        {
            _client = new FakeStoreClient(baseUrl);
            _scenarioContext.Set(_client, "apiClient");
        }

        // ?? Get all products ??

        [When(@"I query all products")]
        public async Task WhenIQueryAllProducts()
        {
            _products = await _client.GetAllProductsAsync();
        }

        [Then(@"the response should contain a list of products")]
        public void ThenTheResponseShouldContainAListOfProducts()
        {
            Assert.That(_products, Is.Not.Null.And.Not.Empty);
        }

        // ?? Get product by id ??

        [When(@"I query the product with id (.*)")]
        public async Task WhenIQueryTheProductWithId(int id)
        {
            _product = await _client.GetProductAsync(id);
        }

        [Then(@"the response should contain a product with id (.*)")]
        public void ThenTheResponseShouldContainAProductWithId(int id)
        {
            Assert.That(_product, Is.Not.Null);
            Assert.That(_product!.Id, Is.EqualTo(id));
        }

        // ?? Create a product ??

        [When(@"I create a product with title ""(.*)"" and price (.*)")]
        public async Task WhenICreateAProductWithTitleAndPrice(string title, double price)
        {
            var newProduct = new Product { Title = title, Price = price, Description = "test", Category = "test", Image = "https://example.com/img.png" };
            _product = await _client.CreateProductAsync(newProduct);
        }

        [Then(@"the created product should have title ""(.*)""")]
        public void ThenTheCreatedProductShouldHaveTitle(string title)
        {
            Assert.That(_product, Is.Not.Null);
            Assert.That(_product!.Title, Is.EqualTo(title));
        }

        // ?? Update a product ??

        [When(@"I update product with id (.*) to title ""(.*)"" and price (.*)")]
        public async Task WhenIUpdateProductWithIdToTitleAndPrice(int id, string title, double price)
        {
            var updated = new Product { Title = title, Price = price, Description = "updated", Category = "test", Image = "https://example.com/img.png" };
            _product = await _client.UpdateProductAsync(id, updated);
        }

        [Then(@"the updated product should have title ""(.*)""")]
        public void ThenTheUpdatedProductShouldHaveTitle(string title)
        {
            Assert.That(_product, Is.Not.Null);
            Assert.That(_product!.Title, Is.EqualTo(title));
        }

        // ?? Delete a product ??

        [When(@"I delete the product with id (.*)")]
        public async Task WhenIDeleteTheProductWithId(int id)
        {
            try
            {
                await _client.DeleteProductAsync(id);
                _deleteSucceeded = true;
            }
            catch
            {
                _deleteSucceeded = false;
            }
        }

        [Then(@"the product should be deleted successfully")]
        public void ThenTheProductShouldBeDeletedSuccessfully()
        {
            Assert.That(_deleteSucceeded, Is.True);
        }
    }
}
