using AutomationPracticeDemo.Tests.Pages;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class FromTestclase3
    {
        protected IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");

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
        public void registro()
        {
            Driver.FindElement(By.XPath("//a[@href='/login']")).Click();
            Driver.FindElement(By.XPath("//input[@data-qa='signup-name']")).SendKeys("Gustavo");
            int random = new Random().Next(1, 1000);
            string emailRandom = "PracticaClase3" + random + "@cenfotec.com";
            Driver.FindElement(By.XPath("//input[@data-qa='signup-email']")).SendKeys(emailRandom);
            Driver.FindElement(By.XPath("//button[@data-qa='signup-button']")).Click();
            Driver.FindElement(By.XPath("//input[@data-qa='password']")).SendKeys("12345678");
            Driver.FindElement(By.XPath("//input[@data-qa='first_name']")).SendKeys("Gustavo");
            Driver.FindElement(By.XPath("//input[@data-qa='last_name']")).SendKeys("Montero");
            Driver.FindElement(By.XPath("//input[@data-qa='address']")).SendKeys("coyol Alajuela");
            var countrySelect = new SelectElement(Driver.FindElement(By.Id("country")));
            countrySelect.SelectByText("Canada");
            Driver.FindElement(By.XPath("//input[@data-qa='state']")).SendKeys("Alajuela");
            Driver.FindElement(By.XPath("//input[@data-qa='city']")).SendKeys("Coyol");
            Driver.FindElement(By.XPath("//input[@data-qa='zipcode']")).SendKeys("20109");
            Driver.FindElement(By.XPath("//input[@data-qa='mobile_number']")).SendKeys("88651273");
            Driver.FindElement(By.XPath("//button[@data-qa='create-account']")).Click();
            var mensaje = Driver.FindElement(By.XPath("//b[normalize-space(text())='Account Created!']"));
            Assert.That(mensaje.Displayed, Is.True, "El mensaje 'Account Created!' no está visible en la pantalla.");
            Driver.FindElement(By.XPath("//a[normalize-space(text())='Continue']")).Click();
            var logoutButton = Driver.FindElement(By.XPath("//a[@href='/logout']"));
            Assert.That(logoutButton.Displayed, Is.True, "El Boton existe");
        }
        [Test]
        public void userExist()
        {
            Driver.FindElement(By.XPath("//a[@href='/login']")).Click();
            Driver.FindElement(By.XPath("//input[@data-qa='login-email']")).SendKeys("gu3@bcr.com");
            Driver.FindElement(By.XPath("//input[@data-qa='login-password']")).SendKeys("12345678");
            Driver.FindElement(By.XPath("//button[@data-qa='login-button']")).Click();
            var UserM = Driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')] | //b[normalize-space(text())='Gustavo']"));
            Assert.That(UserM.Displayed, Is.True, "El mensaje Logged in as Gustavo");
        }
        [Test]
        public void ProductCar()
        {
            Driver.FindElement(By.XPath("//a[@href='/products']")).Click();
             IWebElement addToCartBtn = Driver.FindElement(By.XPath("//p[text()='Blue Top']/following-sibling::a[@class='btn btn-default add-to-cart']")); 
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", addToCartBtn);
            addToCartBtn.Click();
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement continueShoppingBtn = wait.Until(drv =>
            {
                try
                {
                    var el = drv.FindElement(By.XPath("//button[text()='Continue Shopping']"));
                    return (el != null && el.Displayed && el.Enabled) ? el : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
            continueShoppingBtn.Click();
            IWebElement addToCartBtn2 = Driver.FindElement(By.XPath("//p[text()='Winter Top']/following-sibling::a[@class='btn btn-default add-to-cart']"));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", addToCartBtn2);
            addToCartBtn2.Click();
            var wait2 = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement Viewcart = wait2.Until(drv =>
            {
                try
                {
                    var el = drv.FindElement(By.XPath("//u[normalize-space(text())='View Cart']"));
                    return (el != null && el.Displayed && el.Enabled) ? el : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
            Viewcart.Click();
            var car1 = Driver.FindElement(By.XPath("//a[contains(text(),'Blue Top')]"));
            Assert.That(car1.Displayed, Is.True, "Existe en el carrito");
            var car2 = Driver.FindElement(By.XPath("//a[contains(text(),'Winter Top')]"));
            Assert.That(car2.Displayed, Is.True, "Existe en el carrito");
            var articulo1 = Driver.FindElement(By.XPath("//td[@class='cart_description']/h4/a[text()='Blue Top']/ancestor::tr"));
            string precioTexto = articulo1.FindElement(By.XPath(".//td[@class='cart_price']/p")).Text;
            string cantidadTexto = articulo1.FindElement(By.XPath(".//td[@class='cart_quantity']/button")).Text;
            string totalTexto = articulo1.FindElement(By.XPath(".//td[@class='cart_total']/p")).Text;
            decimal precio = decimal.Parse(precioTexto.Replace("Rs.", "").Trim());
            int cantidad = int.Parse(cantidadTexto.Trim());
            decimal total = decimal.Parse(totalTexto.Replace("Rs.", "").Trim());
            decimal esperado = precio * cantidad;
            Assert.That(total, Is.EqualTo(esperado), "Precio correcto primer articulo");
            var articulo2 = Driver.FindElement(By.XPath("//td[@class='cart_description']/h4/a[text()='Blue Top']/ancestor::tr"));
            string precioTexto2 = articulo2.FindElement(By.XPath(".//td[@class='cart_price']/p")).Text;
            string cantidadTexto2 = articulo2.FindElement(By.XPath(".//td[@class='cart_quantity']/button")).Text;
            string totalTexto2 = articulo2.FindElement(By.XPath(".//td[@class='cart_total']/p")).Text;
            decimal precio2 = decimal.Parse(precioTexto2.Replace("Rs.", "").Trim());
            int cantidad2 = int.Parse(cantidadTexto2.Trim());
            decimal total2 = decimal.Parse(totalTexto2.Replace("Rs.", "").Trim());
           decimal esperado2 = precio2 * cantidad2;
            Assert.That(total2, Is.EqualTo(esperado2), "Precio correcto segundo articulo");

        }
        [Test]
        public void ContactUsform()
        {
            Driver.FindElement(By.XPath("//a[@href='/contact_us']")).Click();
            Driver.FindElement(By.XPath("//input[@data-qa='name']")).SendKeys("Gustavo");
            Driver.FindElement(By.XPath("//input[@data-qa='email']")).SendKeys("Gustavo@email.com");
            Driver.FindElement(By.XPath("//input[@data-qa='subject']")).SendKeys("Curso");
            Driver.FindElement(By.XPath("//textarea[@data-qa='message']")).SendKeys("Estoy interesado en el curso de automatizacion");
            var rutaFolder = Path.GetFullPath(@"..\..\..\adjunto\prueba.png");
            var archivo = Driver.FindElement(By.XPath("//input[@name='upload_file']"));
            archivo.SendKeys(rutaFolder);
            Driver.FindElement(By.XPath("//input[@data-qa='submit-button']")).Click();
            Driver.SwitchTo().Alert().Accept();
            var successMessage = Driver.FindElement(By.CssSelector(".status.alert.alert-success")).Text;
            Assert.That(successMessage, Is.EqualTo("Success! Your details have been submitted successfully."));




        }
        [Test]
        public void Suscripcion()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            IWebElement susbscribe = Driver.FindElement(By.Id("susbscribe_email"));
            susbscribe.SendKeys("Gustavo@emial.com");
            IWebElement susbscribeBtn = Driver.FindElement(By.Id("subscribe"));
            susbscribeBtn.Click();
            IWebElement susbscribeMessage = Driver.FindElement(By.Id("success-subscribe"));
            Assert.That(susbscribeMessage.Text, Is.EqualTo("You have been successfully subscribed!"));



        }


    }
}
