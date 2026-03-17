using AutomationPracticeDemo.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests.ProductCar3
{
    public class ProductCartest : TestBase
    {
        [Test]
        public void AddProductToCart()
        {
            var productPage = new Pages.ProductPage(Driver);
            productPage.ClickProduct();

            ScreenshotHelper.TakeScreenshot(Driver, "ProductsPage.png");
            productPage.AddToCart();
            productPage.AddToCart2();
            
            Assert.That(productPage.existBluetop(), Is.True, "Existe en el carrito");
            Assert.That(productPage.existWinterTop(), Is.True, "Existe en el carrito");

            ScreenshotHelper.TakeScreenshot(Driver, "Product.png");
            productPage.totalPrice1();
            productPage.totalPrice2();



        }
    }
  
}
