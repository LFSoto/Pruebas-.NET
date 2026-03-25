using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class CartPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Ocultar iframes de publicidad
        public void CloseAdsIfPresent()
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                var adFrames = driver.FindElements(By.CssSelector("iframe[id^='aswift']"));
                foreach (var frame in adFrames)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", frame);
                }
            }
            catch { }
        }

        // Selector del total en el carrito
        private IWebElement CartTotal => wait.Until(
         ExpectedConditions.ElementIsVisible(By.CssSelector("p.cart_total_price")));

        // Método para agregar producto al carrito
        public void AddProductToCart(string productName)
        {
            var productContainer = driver.FindElement(
                By.XPath($"//p[text()='{productName}']/ancestor::div[@class='productinfo text-center']")
            );

            var addButton = productContainer.FindElement(By.CssSelector("a.add-to-cart"));

            // Ocultar anuncios antes de hacer clic
            CloseAdsIfPresent();

            // Forzar clic con JavaScript para evitar interceptación
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", addButton);

            // Manejar modal de confirmación
            try
            {
                var continueButton = wait.Until(
                    ExpectedConditions.ElementToBeClickable(By.CssSelector("button.close-modal"))
                );
                continueButton.Click();
            }
            catch { }
        }

        // Método para navegar al carrito
        public void GoToCart()
        {
            var cartLink = wait.Until(
                ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/view_cart']"))
            );
            cartLink.Click();
        }

        // Método para obtener el total del carrito
        public string GetCartTotal()
        {
            return CartTotal.Text;
        }
    }
}