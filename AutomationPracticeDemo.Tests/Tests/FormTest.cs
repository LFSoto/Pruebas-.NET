using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;



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
            string emailRandom = "PracticaClase3" + random + "@cenfotec.com";

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

            Driver.FindElement(By.CssSelector("button[data-qa='create-account']")).Click();
            Assert.IsTrue(Driver.PageSource.Contains("Account Created!"));

            Driver.FindElement(By.CssSelector("a[data-qa='continue-button']")).Click();
        }



        //--------------------- Login con usuario existente --------------------------------

        [Test]
        public void LoginUsuarioExistente()
        {
            Driver.FindElement(By.LinkText("Signup / Login")).Click();

            Driver.FindElement(By.CssSelector("input[data-qa='login-email']"))
                  .SendKeys("dayana.porras@email.com");
            Driver.FindElement(By.CssSelector("input[data-qa='login-password']"))
                  .SendKeys("dayanaprueba123");
            Driver.FindElement(By.CssSelector("button[data-qa='login-button']")).Click();
        }

        //--------------------- Agregar productos al carrito y verificar total --------------------------------
        [Test]
        public void AgregarProductosAlCarrito()
        {

            // Paso 1: Ir a Products
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                By.CssSelector("a[href='/products']"))).Click();

            // Paso 2: Agregar primer producto
            var primerProducto = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                By.XPath("//a[text()='Add to cart']")));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", primerProducto);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", primerProducto);

            // Cerrar modal con Continue Shopping
            var continueBtn = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                By.XPath("//button[text()='Continue Shopping']")));
            continueBtn.Click();

            // Paso 3: Agregar segundo producto
            var segundoProducto = Driver.FindElements(By.XPath("//a[text()='Add to cart']"))[1];
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", segundoProducto);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", segundoProducto);

            // Paso 4: Ir al carrito
            var viewCartBtn = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                By.XPath("//a[text()='View Cart']")));
            viewCartBtn.Click();

            // Paso 5: Validar que haya al menos 2 productos
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("cart_info")));
            var filas = Driver.FindElements(By.XPath("//tr[contains(@id,'product-')]"));
            Assert.That(filas.Count, Is.EqualTo(2));

            // Paso 6: Validar totales
            var total1Texto = Driver.FindElement(By.XPath("(//td[@class='cart_total']//p)[1]")).Text;
            var total2Texto = Driver.FindElement(By.XPath("(//td[@class='cart_total']//p)[2]")).Text;

            int total1 = int.Parse(total1Texto.Replace("Rs. ", ""));
            int total2 = int.Parse(total2Texto.Replace("Rs. ", ""));
            int sumaEsperada = total1 + total2;

            Assert.That(total1, Is.GreaterThan(0));
            Assert.That(total2, Is.GreaterThan(0));
            Assert.That(sumaEsperada, Is.GreaterThan(0));
        }

        //--------------------- Contact Us form --------------------------------
        [Test]
        public void ContactUsForm()
        {
            Driver.Navigate().GoToUrl("https://automationexercise.com/");
            Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href='/contact_us']")));
            Driver.FindElement(By.CssSelector("a[href='/contact_us']")).Click();

            Driver.FindElement(By.Name("name")).SendKeys("Dayana Test");
            Driver.FindElement(By.Name("email")).SendKeys("dayana.test@mail.com");
            Driver.FindElement(By.Name("subject")).SendKeys("Consulta de prueba");
            Driver.FindElement(By.Id("message")).SendKeys("Este es un mensaje de prueba.");

            var uploadInput = Driver.FindElement(By.Name("upload_file"));
            uploadInput.SendKeys(@"C:\Users\Dayana\Documents\archivo_prueba.txt");

            Driver.FindElement(By.Name("submit")).Click();

            Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".status")));
            var successMessage = Driver.FindElement(By.CssSelector(".status")).Text;
            Assert.That(successMessage, Is.EqualTo("Success! Your details have been submitted successfully."));
        }

        //--------------------- Suscripción al newsletter --------------------------------
        [Test]
        public void SuscripcionNewsletter()
        {
            Driver.Navigate().GoToUrl("https://automationexercise.com/");
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("susbscribe_email")));
            Driver.FindElement(By.Id("susbscribe_email")).SendKeys("dayana.newsletter@mail.com");
            Driver.FindElement(By.Id("subscribe")).Click();

            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("success-subscribe")));
            var message = Driver.FindElement(By.Id("success-subscribe")).Text;
            Assert.That(message, Is.EqualTo("You have been successfully subscribed!"));
        }
    }
}
