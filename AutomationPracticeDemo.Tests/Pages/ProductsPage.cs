using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Pages
{
    public  class ProductsPage
    {

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        private IWebElement productsLink =>
            _wait.Until(ExpectedConditions.ElementToBeClickable(
                By.CssSelector("a[href='/products']")));

        private IWebElement continueShoppingButton =>
            _wait.Until(ExpectedConditions.ElementToBeClickable(
                By.CssSelector("button.close-modal")));

        private IWebElement ViewCartLink =>
            _wait.Until(ExpectedConditions.ElementToBeClickable(
                By.CssSelector("a[href='/view_cart']")));

        private ReadOnlyCollection<IWebElement> addToCartButtons => _driver.FindElements(By.CssSelector(".features_items .add-to-cart"));
        private bool IsModalClosed => _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("cartModal")));

        //private IWebElement productsLink => _driver.FindElement(By.CssSelector("a[href='/products']"));

        //private IWebElement continueShoppingButton => _driver.FindElement(By.CssSelector("button.close-modal"));

        //private IWebElement ViewCartLink => _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href='/view_cart']")));


        //Método para hacer clic en el enlace de productos
        public void ClickProductsLink()
        {
            productsLink.Click();
        }

        public void AddRandomProductsToCart(int numberOfProducts)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            
            var usedIndices = new List<int>();
            var random = new Random();

            for (int i = 0; i < numberOfProducts; i++)
            {
                var allProducts = _driver.FindElements(
            By.CssSelector(".features_items .add-to-cart"));
                //limpiarAnuncios();

                int randomIndex;
                do
                {
                    randomIndex = random.Next(allProducts.Count);
                } while (usedIndices.Contains(randomIndex));

                usedIndices.Add(randomIndex);

                var product = allProducts[randomIndex];

                js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", product);
                js.ExecuteScript("arguments[0].click();", product);

                //limpiarAnuncios();
                ClickContinueShopping(js);

                Console.WriteLine($"Producto aleatorio #{i + 1} agregado con éxito.");
            }
        }

        private void ClickContinueShopping(IJavaScriptExecutor js)
        {
            js.ExecuteScript("arguments[0].click();", continueShoppingButton);
        }

        public void GoToCart()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

            // 1. Forzar cierre del modal por JS
            js.ExecuteScript(@"
        var modal = document.getElementById('cartModal');
        if (modal) {
            modal.classList.remove('show');
            modal.style.display = 'none';
        }
        var backdrop = document.querySelector('.modal-backdrop');
        if (backdrop) backdrop.remove();
        document.body.classList.remove('modal-open');
    ");

            // 2. Esperar explícitamente que el backdrop desaparezca del DOM
            _wait.Until(d =>
                d.FindElements(By.CssSelector(".modal-backdrop")).Count == 0);

            // 3. Esperar explícitamente que el body NO tenga la clase modal-open
            _wait.Until(d =>
                !d.FindElement(By.TagName("body"))
                  .GetAttribute("class")
                  .Contains("modal-open"));

            // 4. Esperar que el cartLink sea clickeable y hacer clic con JS
            var cartLink = _wait.Until(
                ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/view_cart']")));

            js.ExecuteScript("arguments[0].click();", cartLink);
        }
    }
}