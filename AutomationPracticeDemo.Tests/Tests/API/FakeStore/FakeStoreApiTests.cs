using NUnit.Framework;
using AutomationPracticeDemo.Tests.Api.Client;
using AutomationPracticeDemo.Tests.Api.Dtos;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests.API.FakeStore
{
    public class FakeStoreApiTests
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
            var products = await _client.GetAllProductsAsync();
            Assert.That(products, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task GetProductById_ReturnsProduct()
        {
            var prod = await _client.GetProductAsync(1);
            Assert.That(prod, Is.Not.Null);
            Assert.That(prod.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task Login_ReturnsToken()
        {
            var lr = await _client.LoginAsync(new LoginRequest { Username = "johnd", Password = "m38rmF$" });
            Assert.That(lr, Is.Not.Null);
            Assert.That(lr.Token, Is.Not.Null.And.Not.Empty);
        }
    }
}
