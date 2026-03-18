using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace AutomationPracticeDemo.Tests.Pages
{
    public class ProductPage
    {
        private readonly IWebDriver Driver;
        private readonly WebDriverWait Wait;

        public ProductPage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        public void GoToProducts()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/products']"))).Click();
        }

        public void AddRandomProductsToCart(int count)
        {
            var botones = Wait.Until(d => d.FindElements(By.XPath("//a[text()='Add to cart']")));
            var random = new Random();
            var indices = Enumerable.Range(0, botones.Count).OrderBy(x => random.Next()).Take(count).ToList();

            int numeroProducto = 1;

            foreach (var i in indices)
            {
                var producto = botones[i];

                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", producto);
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", producto);

                // Esperar que aparezca el modal del carrito
                Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("cartModal")));

                // 📸 Captura cuando se agrega el producto
                ScreenshotHelper.TakeScreenshot(Driver, $"Producto_Agregado_{numeroProducto}");

                // Continuar comprando
                Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Continue Shopping']"))).Click();

                numeroProducto++;
            }
        }

        public void GoToCart()
        {
            Driver.FindElement(By.CssSelector("a[href='/view_cart']")).Click();
        }
    }
}

