using System;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;



namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormTests : Base.TestBase
    {
        //--------------------- Registro de un Nuevo Usuario --------------------------------
        [Test]
        public void RegistroUsuarioNuevo()
        {
            // Generar email aleatorio único
            int random = new Random().Next(1, 1000);
            string emailRandom = "Practica%Clase3" + random + "@ucenfotec.com";

            Driver.FindElement(By.LinkText("Signup / Login")).Click();

            Driver.FindElement(By.Name("name")).SendKeys("DayanaTest");
            Driver.FindElement(By.CssSelector("input[data-qa='signup-email']"))
                  .SendKeys(emailRandom);
            Driver.FindElement(By.CssSelector("button[data-qa='signup-button']")).Click();

            Driver.FindElement(By.Id("id_gender2")).Click();
            Driver.FindElement(By.Id("password")).SendKeys("Password123");
            new SelectElement(Driver.FindElement(By.Id("days"))).SelectByValue("10");
            new SelectElement(Driver.FindElement(By.Id("months"))).SelectByValue("5");
            new SelectElement(Driver.FindElement(By.Id("years"))).SelectByValue("1995");
            Driver.FindElement(By.Id("first_name")).SendKeys("Dayana");
            Driver.FindElement(By.Id("last_name")).SendKeys("Test");
            Driver.FindElement(By.Id("address1")).SendKeys("San José, Costa Rica");
            new SelectElement(Driver.FindElement(By.Id("country"))).SelectByText("Canada");
            Driver.FindElement(By.Id("state")).SendKeys("SJ");
            Driver.FindElement(By.Id("city")).SendKeys("San José");
            Driver.FindElement(By.Id("zipcode")).SendKeys("10101");
            Driver.FindElement(By.Id("mobile_number")).SendKeys("123456789");

            // 📸 Captura de pantalla justo después de ingresar todos los datos
            ScreenshotHelper.TakeScreenshot(Driver, "RegistroDatosIngresados");

            Driver.FindElement(By.CssSelector("button[data-qa='create-account']")).Click();
            Assert.IsTrue(Driver.PageSource.Contains("Account Created!"));

            ScreenshotHelper.TakeScreenshot(Driver, "Cuenta Creada");

            Driver.FindElement(By.CssSelector("a[data-qa='continue-button']")).Click();

        }



        //--------------------- Login con usuario existente --------------------------------

        [Test]
        public void LoginUsuarioExistente()
        {
            // Paso 1: Ir a la página de login
            Driver.FindElement(By.LinkText("Signup / Login")).Click();

            // Paso 2: Ingresar correo
            Driver.FindElement(By.CssSelector("input[data-qa='login-email']"))
                  .SendKeys("dayana.porras@email.com");

            // Paso 3: Ingresar contraseña
            Driver.FindElement(By.CssSelector("input[data-qa='login-password']"))
                  .SendKeys("dayanaprueba123");

            // 📸 Captura de pantalla justo después de ingresar los datos
            ScreenshotHelper.TakeScreenshot(Driver, "LoginDatosIngresados");

            // Paso 4: Click en botón Login
            Driver.FindElement(By.CssSelector("button[data-qa='login-button']")).Click();

        }


        //--------------------- Agregar productos al carrito y verificar total --------------------------------

        [Test]
        public void AgregarProductosAlCarrito()
        {
            // Paso 1: Ir a Products
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                By.CssSelector("a[href='/products']"))).Click();

            // Paso 2: Obtener todos los botones Add to cart
            var botones = Wait.Until(d => d.FindElements(By.XPath("//a[text()='Add to cart']")));

            Random random = new Random();
            int index1 = random.Next(botones.Count);
            int index2 = random.Next(botones.Count);
            while (index2 == index1) index2 = random.Next(botones.Count);

            // ---- Agregar primer producto ----
            var primerProducto = botones[index1];
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", primerProducto);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", primerProducto);

            // Esperar modal y capturar mensaje de Add to cart
            var modal1 = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("cartModal")));
            ScreenshotHelper.TakeScreenshot(Driver, "Producto1_Click");

            // Click Continue Shopping
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                By.XPath("//button[text()='Continue Shopping']"))).Click();

            // ---- Agregar segundo producto ----
            botones = Driver.FindElements(By.XPath("//a[text()='Add to cart']"));
            var segundoProducto = botones[index2];
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", segundoProducto);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", segundoProducto);

            // Esperar modal y capturar mensaje de Add to cart
            var modal2 = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("cartModal")));
            ScreenshotHelper.TakeScreenshot(Driver, "Producto2_Click");

            // Click View Cart en modal
            var viewCartBtn = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                By.CssSelector("#cartModal a[href='/view_cart']")));
            viewCartBtn.Click();

            // ---- Validar que hay 2 productos ----
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("cart_info_table")));
            var filas = Driver.FindElements(By.XPath("//tr[contains(@id,'product-')]"));
            Assert.That(filas.Count, Is.EqualTo(2), "El carrito debería contener 2 productos.");

            // ---- Validar precios y totales ----
            var precio1Texto = Driver.FindElement(By.XPath("(//td[@class='cart_price']//p)[1]")).Text;
            var precio2Texto = Driver.FindElement(By.XPath("(//td[@class='cart_price']//p)[2]")).Text;
            int precio1 = int.Parse(precio1Texto.Replace("Rs. ", ""));
            int precio2 = int.Parse(precio2Texto.Replace("Rs. ", ""));

            var total1Texto = Driver.FindElement(By.XPath("(//td[@class='cart_total']//p)[1]")).Text;
            var total2Texto = Driver.FindElement(By.XPath("(//td[@class='cart_total']//p)[2]")).Text;
            int total1 = int.Parse(total1Texto.Replace("Rs. ", ""));
            int total2 = int.Parse(total2Texto.Replace("Rs. ", ""));

            Assert.That(total1, Is.EqualTo(precio1), "El total del primer producto es correcto.");
            Assert.That(total2, Is.EqualTo(precio2), "El total del segundo producto es correcto.");
        }


        //--------------------- Contact Us form --------------------------------
        [Test]
        public void ContactUsForm()
        {
            // Ir a Contact Us
            Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href='/contact_us']")));
            Driver.FindElement(By.CssSelector("a[href='/contact_us']")).Click();

            // Completar formulario
            Driver.FindElement(By.Name("name")).SendKeys("Dayana Test");
            Driver.FindElement(By.Name("email")).SendKeys("dayana.test@mail.com");
            Driver.FindElement(By.Name("subject")).SendKeys("Consulta de prueba");
            Driver.FindElement(By.Id("message")).SendKeys("Este es un mensaje de prueba.");

            // Adjuntar archivo
            string rutaImagen = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resource\Paisaje123.jpg"));
            Driver.FindElement(By.Name("upload_file")).SendKeys(rutaImagen);

            //Tomar captura **después de subir la imagen**
            ScreenshotHelper.TakeScreenshot(Driver, "ContactUs_DatosIngresado_ImagenCargada");

            //Enviar formulario
            Driver.FindElement(By.Name("submit")).Click();

            // Manejar alert si aparece
            try
            {
                IAlert alert = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
                alert.Accept(); // Presiona OK
            }
            catch (WebDriverTimeoutException)
            {
                // No hay alerta, continuar
            }

            // Esperar DOM listo y eliminar iframes si existen
            Wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").ToString() == "complete");
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript(
                    "document.querySelectorAll('iframe').forEach(a => a.remove());"
                );
            }
            catch { }

            // Esperar mensaje de éxito
            var successElement = Wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.XPath("//div[contains(@class,'status') and contains(text(),'Success')]")
                )
            );

            // Validar mensaje
            string successMessage = successElement.Text.Trim();
            Assert.That(successMessage, Does.Contain("Success! Your details have been submitted successfully."));
        }


        //--------------------- Suscripción al newsletter --------------------------------
        [Test]
        public void SuscripcionNewsletter()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("susbscribe_email")));
            Driver.FindElement(By.Id("susbscribe_email")).SendKeys("dayana.newsletter@mail.com");

            //Captura justo después de ingresar el correo
            ScreenshotHelper.TakeScreenshot(Driver, "NewsletterEmailEntered");

            Driver.FindElement(By.Id("subscribe")).Click();

            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("success-subscribe")));
            var message = Driver.FindElement(By.Id("success-subscribe")).Text;
            Assert.That(message, Is.EqualTo("You have been successfully subscribed!"));

        }
    }
}
