using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;


namespace AutomationPracticeDemo.Tests.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;
        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Declaración de los elementos de la página
        private IWebElement productsTitle => _driver.FindElement(By.CssSelector("div.features_items >h2.title.text-center"));
        private IWebElement firstProductType => _driver.FindElement(By.CssSelector("div:nth-child(3) div.productinfo.text-center > p"));//div:nth-child(3) div.product-overlay p
        private IWebElement firstProductPrice => _driver.FindElement(By.CssSelector("div:nth-child(3) div.productinfo.text-center h2"));
        private IWebElement addFirstCartButton => _driver.FindElement(By.CssSelector("div.productinfo.text-center a[data-product-id=\"1\"]"));//div.product-overlay a[data-product-id=\"1\"]"
        private IWebElement secondProductType => _driver.FindElement(By.CssSelector(" div:nth-child(4)  div.productinfo.text-center  p"));
        private IWebElement secondProductPrice => _driver.FindElement(By.CssSelector("div:nth-child(4) div.productinfo.text-center h2"));
        private IWebElement addSecondCartButton => _driver.FindElement(By.CssSelector("div.productinfo.text-center a[data-product-id=\"2\"]"));
        private IWebElement modaAddedlTitle => _driver.FindElement(By.CssSelector("#cartModal h4"));
        private IWebElement modaAddedlMessage => _driver.FindElement(By.CssSelector("#cartModal p"));
        private IWebElement viewCartButton => _driver.FindElement(By.CssSelector("#cartModal a"));
        private IWebElement continueShoppingButton => _driver.FindElement(By.CssSelector("#cartModal button[data-dismiss=\"modal\"]"));
        private IWebElement tableCartInfo => _driver.FindElement(By.Id("cart_info_table"));

        //Validar que estamos en la página de productos
        public string ValidatedProductsPage()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => productsTitle.Displayed);
            return productsTitle.Text;

        }
        //Obtener el tipo y precio del primer producto
        public (string, string) GetFirstProductDetails()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => firstProductType.Displayed && firstProductPrice.Displayed);
            return (firstProductType.Text, firstProductPrice.Text);
        }

        //Agregar el primer producto al carrito
        public void AddFirstProductToCart()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => addFirstCartButton.Enabled && addFirstCartButton.Displayed);

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
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => modaAddedlTitle.Displayed && modaAddedlMessage.Displayed);
            return (modaAddedlTitle.Text, modaAddedlMessage.Text);
        }
        //Continuar comprando
        public void ContinueShopping()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => continueShoppingButton.Enabled && continueShoppingButton.Displayed);
            continueShoppingButton.Click();
        }

        //Obtener el tipo y precio del segundo producto 
        public (string, string) GetSecondProductDetails()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => secondProductType.Displayed && secondProductPrice.Displayed);
            return (secondProductType.Text, secondProductPrice.Text);
        }

        //Agregar el segundo producto al carrito
        public void AddSecondProductToCart()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => addSecondCartButton.Enabled && addSecondCartButton.Displayed);
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
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => viewCartButton.Enabled && viewCartButton.Displayed);
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
