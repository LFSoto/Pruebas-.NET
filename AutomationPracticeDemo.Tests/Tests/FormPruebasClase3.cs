using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Drawing;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormPruebasClase3
    {
        protected IWebDriver Driver;
        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");
            Driver = new ChromeDriver(options);
            Driver.Navigate().GoToUrl("https://automationexercise.com/");
        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
            }
        }
        [Test]
        public void ValidarRegistroNuevoUsuario()
        {
            var singUpBotton = Driver.FindElement(By.XPath("//a[text()=' Signup / Login']"));
            singUpBotton.Click();
            var inputName = Driver.FindElement(By.XPath("//input[@data-qa='signup-name']"));
            inputName.SendKeys("Maria");
            var inputEmail = Driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));
            inputEmail.SendKeys("aria6@qa.com");
            var ingresarBotton = Driver.FindElement(By.XPath("//button[text()='Signup']"));
            ingresarBotton.Click();
            var contrasena = Driver.FindElement(By.Id("password"));
            contrasena.SendKeys("123456");
            var primerNombre = Driver.FindElement(By.Id("first_name"));
            primerNombre.SendKeys("Maria");
            var apellido = Driver.FindElement(By.Id("last_name"));
            apellido.SendKeys("Mora");
            var direccion = Driver.FindElement(By.Id("address1"));
            direccion.SendKeys("calle 21");
            var pais = Driver.FindElement(By.Id("country"));
            pais.SendKeys("Israel");
            var estado = Driver.FindElement(By.Id("state"));
            estado.SendKeys("Israel");
            var cuidad = Driver.FindElement(By.Id("city"));
            cuidad.SendKeys("Tangamandapio");
            var codigoZip = Driver.FindElement(By.Id("zipcode"));
            codigoZip.SendKeys("21212");
            var numeroTelefono = Driver.FindElement(By.Id("mobile_number"));
            numeroTelefono.SendKeys("88888888");
            var crearCuenta = Driver.FindElement(By.XPath("//button[text()='Create Account']"));
            crearCuenta.Click();
            var mensajeCreacion = Driver.FindElement(By.XPath("//h2[@data-qa='account-created']"));
            var mensaje =mensajeCreacion.Text;
            Assert.That(mensaje, Is.EqualTo("ACCOUNT CREATED!"));
            var continuar = Driver.FindElement(By.XPath("//a[@data-qa='continue-button']"));
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(continuar));
            continuar.Click();
            var validarlogout = Driver.FindElement(By.XPath("//a[text()=' Logout']"));
            wait.Until(ExpectedConditions.ElementToBeClickable(validarlogout));
            var login = validarlogout.Text;
            Assert.That(login, Is.EqualTo("Logout"));
            var eliminarCuenta = Driver.FindElement(By.XPath("//a[text()=' Delete Account']"));
            eliminarCuenta.Click();
            var continuarEliminacion = Driver.FindElement(By.XPath("//a[@data-qa='continue-button']"));
            wait.Until(ExpectedConditions.ElementToBeClickable(continuarEliminacion));
            continuarEliminacion.Click();
        }

        [Test]
        public void ValidarLogin ()
        {
            var singUpBotton = Driver.FindElement(By.XPath("//a[text()=' Signup / Login']"));
            singUpBotton.Click();
            var inputEmail = Driver.FindElement(By.XPath("//input[@data-qa='login-email']"));
            inputEmail.SendKeys("aria@qa.com");
            var inputContrasena = Driver.FindElement(By.XPath("//input[@data-qa='login-password']"));
            inputContrasena.SendKeys("123456");
            var ingresarBotton = Driver.FindElement(By.XPath("//button[text()='Login']"));
            ingresarBotton.Click();
            var esperaLogin = new WebDriverWait(Driver, TimeSpan.FromSeconds(15)).Until(a=> a.FindElement(By.XPath("//a[contains(text(),'Logged in as')]")));
            var validarlogout = Driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]"));
            var login = validarlogout.Text;
            Assert.That(login, Is.EqualTo("Logged in as Maria"));
        }
        [Test]
        public void AgregarCarrito ()
        {
            var productoBotton = Driver.FindElement(By.XPath("//a[text()=' Products']"));
            productoBotton.Click();
            //features_items
            IList<IWebElement> listaProductos = Driver.FindElements(By.CssSelector(".features_items .col-sm-4"));
            int producto1 = new Random().Next(0, 33);
            int producto2 = new Random().Next(0, 33);
            List<string> nombreProductosAgregados = new List<string>();
            nombreProductosAgregados.Add(agregarAlCarrito(listaProductos, producto1));
            ContinuarComprando();
            nombreProductosAgregados.Add(agregarAlCarrito(listaProductos, producto2));
            VerCarrito();
            var tabla = Driver.FindElement(By.Id("cart_info_table"));
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(tabla));
            var registros = tabla.FindElements(By.CssSelector("tbody tr"));

            if (registros.Count == 2)
            {
                foreach (var producto in registros)
                {
                    var nombre = producto.FindElement(By.CssSelector(".cart_description h4")).Text;
                    if (nombreProductosAgregados.Any(s => s.Equals(nombre))){ 
                        var cantidad = producto.FindElement(By.ClassName("cart_quantity")).Text;
                        var precio = producto.FindElement(By.ClassName("cart_price")).Text.Replace("Rs. ", "");
                        var total = producto.FindElement(By.ClassName("cart_total")).Text.Replace("Rs. ", "");
                        var calculo = Convert.ToInt32(cantidad) * Convert.ToInt32(precio);
                        Assert.That(calculo.ToString(), Is.EqualTo(total));
                    } else {
                        Assert.Fail("No se encuentra el producto en el carrito");
                    }
                }
                Assert.Pass("Se encontraron los productos y calculo de total correctos.");
            } else
                Assert.Fail("No contiene los productos agregados.");

        }

        private string agregarAlCarrito(IList<IWebElement> listaProductos, int i)
        {
            var nombreProducto = listaProductos[i].FindElement(By.CssSelector("div.productinfo p")).Text;
            IWebElement productoBotton = listaProductos[i].FindElement(By.XPath(".//a[text()='Add to cart']"));
            // Hacer scroll hasta el botón
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", productoBotton);
            // Forzar el clic con JavaScript para evitar overlays
            js.ExecuteScript("arguments[0].click();", productoBotton);
            return nombreProducto;
        }

        private void ContinuarComprando()
        {
            var continuarShopping = Driver.FindElement(By.CssSelector("#cartModal button"));
            // Espera a que el modal de confirmación aparezca para dar click
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(continuarShopping));
            continuarShopping.Click();
        }
        private void VerCarrito()
        {
            var verCarrito = Driver.FindElement(By.CssSelector("#cartModal a[href*='/view_cart']"));
            // Espera a que el modal de confirmación aparezca para dar click
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(verCarrito));
            verCarrito.Click();
        }
        [Test]
        public void FormularioContacto()
        {
            var contactoBoton = Driver.FindElement(By.XPath("//a[text()=' Contact us']"));
            contactoBoton.Click();
            var nombre = Driver.FindElement(By.XPath("//input[@data-qa='name']"));
            nombre.SendKeys("Maria");
            var email = Driver.FindElement(By.XPath("//input[@data-qa='email']"));
            email.SendKeys("maria@qa.com");
            var asunto = Driver.FindElement(By.XPath("//input[@data-qa='subject']"));
            asunto.SendKeys("Ayuda");
            var mensaje = Driver.FindElement(By.XPath("//textarea[@data-qa='message']"));
            mensaje.SendKeys("Ayuda no entiendo como se usa.");
            var rutaFolder = Path.GetFullPath(@"..\..\..\ArchivoAdjunto\226531.jpg");
            var archivo = Driver.FindElement(By.XPath("//input[@name='upload_file']"));
            archivo.SendKeys(rutaFolder);
            var submintBoton = Driver.FindElement(By.XPath("//input[@data-qa='submit-button']"));
            submintBoton.Click();
            var alert = Driver.SwitchTo().Alert();
            alert.Accept();
            var mensajeExito = Driver.FindElement(By.CssSelector(".status.alert.alert-success"));
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(mensajeExito));
            var validarMensaje = mensajeExito.Text;
            Assert.That(validarMensaje, Is.EqualTo("Success! Your details have been submitted successfully."));
        }
        [Test]
        public void Suscripcion()
        {
            var ingresarEmail = Driver.FindElement(By.Id("susbscribe_email"));
            ingresarEmail.SendKeys("aria@qa.com");
            var botonEnviar = Driver.FindElement(By.Id("subscribe"));
            botonEnviar.Click();
            var mensajeExito = Driver.FindElement(By.CssSelector(".alert-success.alert"));
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(mensajeExito));
            var validarMensaje = mensajeExito.Text;
            Assert.That(validarMensaje, Is.EqualTo("You have been successfully subscribed!"));
        }
    }
}

