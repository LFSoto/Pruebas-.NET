using NUnit.Framework;
using AutomationPracticeDemo.Tests.Api.Client;
using AutomationPracticeDemo.Tests.Api.Dtos;
using AutomationPracticeDemo.Tests.Reporting;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests.API.FakeStore
{
    public class FakeStoreApiTests : ReportedTestBase
    {
        private FakeStoreClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new FakeStoreClient("https://fakestoreapi.com");
        }

        [Test]
        public async Task GetAllProducts_ReturnsList()
        {
            LogInfo("Requesting GET /products");
            var products = await _client.GetAllProductsAsync();
            LogInfo($"Received {products.Count} products");
            Assert.That(products, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task GetProductById_ReturnsProduct()
        {
            LogInfo("Requesting GET /products/1");
            var prod = await _client.GetProductAsync(1);
            LogInfo($"Product title: {prod.Title}");
            Assert.That(prod, Is.Not.Null);
            Assert.That(prod.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task Login_ReturnsToken()
        {
            LogInfo("Requesting POST /auth/login");
            var lr = await _client.LoginAsync(new LoginRequest { Username = "johnd", Password = "m38rmF$" });
            LogInfo("Token received successfully");
            Assert.That(lr, Is.Not.Null);
            Assert.That(lr.Token, Is.Not.Null.And.Not.Empty);
        }
    }
}
