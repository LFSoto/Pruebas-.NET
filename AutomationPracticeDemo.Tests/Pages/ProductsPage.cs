using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;


namespace AutomationPracticeDemo.Tests.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // Declaración de los elementos de la página
        // Localizadores relativos a los productos
        private IWebElement productsTitle => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div.features_items >h2.title.text-center")));
        private IWebElement firstProductType => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div:nth-child(3) div.productinfo.text-center > p")));
        private IWebElement firstProductPrice => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div:nth-child(3) div.productinfo.text-center h2")));
        private IWebElement addFirstCartButton => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("div.productinfo.text-center a[data-product-id=\"1\"]")));
        private IWebElement secondProductType => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(" div:nth-child(4)  div.productinfo.text-center p")));
        private IWebElement secondProductPrice => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div:nth-child(4) div.productinfo.text-center h2")));
        private IWebElement addSecondCartButton => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("div.productinfo.text-center a[data-product-id=\"2\"]")));
        private IWebElement modaAddedlTitle => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#cartModal h4")));
        private IWebElement modaAddedlMessage => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#cartModal p")));
        private IWebElement viewCartButton => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("#cartModal a")));
        private IWebElement continueShoppingButton => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("#cartModal button[data-dismiss=\"modal\"]")));
        private IWebElement tableCartInfo => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("cart_info_table")));

        //Validar que estamos en la página de productos
        public string ValidatedProductsPage()
        {
            return productsTitle.Text;

        }
        //Obtener el tipo y precio del primer producto
        public (string, string) GetFirstProductDetails()
        {
            return (firstProductType.Text, firstProductPrice.Text);
        }

        //Agregar el primer producto al carrito
        public void AddFirstProductToCart()
        {

            // Manejar posible excepción de click interceptado en caso de que el botón no sea clickeable directamente
            try
            {
                addFirstCartButton.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", addFirstCartButton);
            }

        }

        //Validar que el producto se ha agregado al carrito
        public (string, string) ValidateProductAddedModal()
        {
            //Implimentar una espera explícita para asegurar que el text del modal esté visible
            return (modaAddedlTitle.Text, modaAddedlMessage.Text);
        }
        //Continuar comprando
        public void ContinueShopping()
        {
            continueShoppingButton.Click();
        }

        //Obtener el tipo y precio del segundo producto 
        public (string, string) GetSecondProductDetails()
        {
            return (secondProductType.Text, secondProductPrice.Text);
        }

        //Agregar el segundo producto al carrito
        public void AddSecondProductToCart()
        {
            try
            {
                addSecondCartButton.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", addSecondCartButton);
            }
        }
        //Trasladarse el carrito de compras
        public void SummitViewCart()
        {
            viewCartButton.Click();
        }

        // Validar productos en el carrito con el precio correcto
        public void ValidateProductInCart(string productType)
        {
            string foundProductType = null, foundProductPrice = null, foundQuantity = null, foundTotal = null;

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            IList<IWebElement> tableRows = tableCartInfo.FindElements(By.TagName("tr"));
            for (int x = 1; x < tableRows.Count; x++)
            {
                IWebElement row = tableRows[x];
                // Encuentra todas las celdas dentro de la fila
                IList<IWebElement> tableColumns = row.FindElements(By.TagName("td"));

                for (int y = 0; y < tableColumns.Count; y++)
                {
                    var columnClass = tableColumns[y].GetAttribute("class");
                    
                    if (columnClass != null && columnClass.Equals("cart_description"))
                    {
                        var columnText = tableColumns[y].FindElement(By.CssSelector("h4 > a")).Text;
                        if (columnText.Equals(productType))
                        {
                        foundProductType = columnText;
                        foundProductPrice = tableColumns[2].FindElement(By.CssSelector("td.cart_price > p")).Text;
                        foundQuantity = tableColumns[3].FindElement(By.CssSelector("td.cart_quantity > button")).Text;
                        foundTotal = tableColumns[4].FindElement(By.CssSelector(" td.cart_total > p")).Text;
                    }
                }
                }
            }

            // Calcular el total basado en el precio y la cantidad
            int calculatedTotal = 0;
            if (!string.IsNullOrEmpty(foundProductPrice)&& !string.IsNullOrEmpty(foundQuantity))
            {
                calculatedTotal = int.Parse(extractNum(foundProductPrice))* int.Parse(foundQuantity);

            }
            // Validar que el total calculado coincida con el total mostrado en el carrito
            if (foundTotal != null && calculatedTotal != int.Parse(extractNum(foundTotal)))
            {
                throw new Exception($"El total calculado '{calculatedTotal}' no coincide con el total mostrado en el carrito '{extractNum(foundTotal)}'.");
            }

            //Validar que se haya encontrado el producto
            if (foundProductType == null || foundProductPrice == null)
            {
                throw new NoSuchElementException($"El producto con tipo '{productType}' no se encontró en el carrito.");
            }
        }

        private string extractNum(string numText)
        {
            // Usar una expresión regular para extraer solo los dígitos de monto
            var match = Regex.Match(numText, @"\d+");
            return match.Success ? match.Value : "0";
        }
    }
}
