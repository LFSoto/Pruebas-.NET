using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AutomationPracticeDemo.Tests.Pages;

namespace AutomationPracticeDemo.Tests.Tests
{
    [TestFixture]
    public class CartTest
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-popup-blocking");

            _driver = new ChromeDriver(options);
            _driver.Manage().Cookies.DeleteAllCookies();
        }

        [Test]
        public void AgregarProductosYVerificarTotal()
        {
            _driver.Navigate().GoToUrl("https://automationexercise.com");

            var cartPage = new CartPage(_driver);

            cartPage.AddProductToCart("Blue Top");
            cartPage.AddProductToCart("Men Tshirt");

            cartPage.GoToCart(); // 👈 Navegar al carrito

            var total = cartPage.GetCartTotal();
            Assert.That(total, Does.Contain("Rs.")); // Ajusta según el precio real
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}