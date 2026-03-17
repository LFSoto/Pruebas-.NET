using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Reflection;
using System.Xml.Linq;
using static NUnit.Framework.Internal.OSPlatform;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class AgregarCarritoPage
    {
        private readonly IWebDriver _driver;

        public AgregarCarritoPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement ProductoBotton => _driver.FindElement(By.XPath("//a[text()=' Products']"));
        private IList<IWebElement> ListaProductos => _driver.FindElements(By.CssSelector(".features_items .col-sm-4"));
        private IWebElement Tabla => _driver.FindElement(By.Id("cart_info_table"));
        private IList<IWebElement> Registros => Tabla.FindElements(By.CssSelector("tbody tr"));
        private IWebElement ContinuarBoton => _driver.FindElement(By.CssSelector("#cartModal button"));
        private IWebElement VerCarritoBoton => _driver.FindElement(By.CssSelector("#cartModal a[href*='/view_cart']"));
        private IWebElement ModalConfirmacion => _driver.FindElement(By.CssSelector("#cartModal"));

        private List<string>ProductosAgregados = new List<string>();
   
        public void ValidarRegistroTabla()
        {
            if (Registros.Count == 2)  {
            foreach (var producto in Registros)
            {
                var nombre = producto.FindElement(By.CssSelector(".cart_description h4")).Text;
                if (ProductosAgregados.Any(s => s.Equals(nombre)))
                {
                    var cantidad = producto.FindElement(By.ClassName("cart_quantity")).Text;
                    var precio = producto.FindElement(By.ClassName("cart_price")).Text.Replace("Rs. ", "");
                    var total = producto.FindElement(By.ClassName("cart_total")).Text.Replace("Rs. ", "");
                    var calculo = Convert.ToInt32(cantidad) * Convert.ToInt32(precio);

                    if(calculo.ToString() != total)
                        throw new Exception($"El total calculado '{calculo}' no coincide con el total mostrado en el carrito '{total}'.");
                }
                else
                {
                    throw new NoSuchElementException($"No se encuentra el producto {nombre} en el carrito");
                }
            }
            } else {
                throw new Exception($"No contiene los productos agregados.");
            }
        }
        public void agregarAlCarrito(int indice)
        {
            var nombreProducto = ListaProductos[indice].FindElement(By.CssSelector("div.productinfo p")).Text;
            IWebElement productoBotton = ListaProductos[indice].FindElement(By.XPath(".//a[text()='Add to cart']"));
            // Hacer scroll hasta el botón
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", productoBotton);
            // Forzar el clic con JavaScript para evitar overlays
            js.ExecuteScript("arguments[0].click();", productoBotton);
            ProductosAgregados.Add(nombreProducto);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(ModalConfirmacion));
        }

        public void ContinuarComprando()
        {
            // Espera a que el modal de confirmación aparezca para dar click
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(ContinuarBoton));
            ContinuarBoton.Click();
        }
        public void VerCarrito()
        {
            // Espera a que el modal de confirmación aparezca para dar click
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(VerCarritoBoton));
            VerCarritoBoton.Click();
        }
        public void ProductoBoton_Click()
        {
            ProductoBotton.Click();
        }
    }
}
