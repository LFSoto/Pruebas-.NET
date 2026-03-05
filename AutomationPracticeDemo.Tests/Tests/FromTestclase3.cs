using AutomationPracticeDemo.Tests.Pages;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            Driver.FindElement(By.XPath("//div[@class='productinfo text-center']//p[text()='Blue Top']/following-sibling::a[contains(@class,'add-to-cart')]")).Click();
            Driver.FindElement(By.XPath("//button[normalize-space(text())='Continue Shopping']")).Click();
            Driver.FindElement(By.XPath("//div[@class='productinfo text-center']//p[text()='Winter Top']/following-sibling::a[contains(@class,'add-to-cart')]")).Click();
            Driver.FindElement(By.XPath("//u[normalize-space(text())='View Cart']")).Click();
            var car1 = Driver.FindElement(By.XPath("//a[contains(text(),'Blue Top')]"));
            Assert.That(car1.Displayed, Is.True, "Existe en el carrito");
            var car2 = Driver.FindElement(By.XPath("//a[contains(text(),'Winter Top')]"));
            Assert.That(car2.Displayed, Is.True, "Existe en el carrito");


            var filaBlueTop = driver.FindElement(By.XPath("//td[@class='cart_description']/h4/a[text()='Blue Top']/ancestor::tr"));

            string precioTexto = filaBlueTop.FindElement(By.XPath(".//td[@class='cart_price']/p")).Text;
            string cantidadTexto = filaBlueTop.FindElement(By.XPath(".//td[@class='cart_quantity']/button")).Text;
            string totalTexto = filaBlueTop.FindElement(By.XPath(".//td[@class='cart_total']/p")).Text;

            decimal precio = decimal.Parse(precioTexto.Replace("Rs.", "").Trim());
            int cantidad = int.Parse(cantidadTexto.Trim());
            decimal total = decimal.Parse(totalTexto.Replace("Rs.", "").Trim());

            decimal esperado = precio * cantidad;

            // Assert con NUnit estilo "That"
            Assert.That(total, Is.EqualTo(esperado),
                $"El total mostrado ({total}) no coincide con el esperado ({esperado}) para Blue Top.");



        }



    }
}
