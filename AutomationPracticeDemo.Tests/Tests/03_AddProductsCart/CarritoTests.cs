using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.Pages;
using OpenQA.Selenium;


namespace AutomationPracticeDemo.Tests.Tests
{
    public class CarritoTests : Base.TestBase
    {
        [Test]
        public void AgregarProductosAlCarrito()
        {
            var productPage = new ProductPage(Driver, Wait);
            productPage.GoToProducts();
            productPage.AddRandomProductsToCart(2);
            productPage.GoToCart();

            var filas = Driver.FindElements(By.XPath("//tr[contains(@id,'product-')]"));
            Assert.That(filas.Count, Is.EqualTo(2), "El carrito debería contener 2 productos.");
        }
    }
}

