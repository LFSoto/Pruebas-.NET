using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class WebSiteTest : TestBase
    {
        // Campo estático para generar un sufijo aleatorio reutilizable
        private static readonly int random = new Random().Next(1, 1000);

        [Test]
        public void Should_RegisterNewUser_FullFlow()
        {
            // Navegar al sitio indicado (TestBase puede haber navegado a otra URL en Setup)
            Driver.Navigate().GoToUrl("https://automationexercise.com/");

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));

            // 2. Click en Signup / Login
            var signupLogin = wait.Until(d => d.FindElement(By.XPath("//a[contains(., 'Signup / Login')]")));
            signupLogin.Click();

            // 3. Completar nombre y correo.
            wait.Until(d => d.FindElement(By.Name("name")));
            var nameInput = Driver.FindElement(By.Name("name"));
            var emailInput = Driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));

            // Usar sufijo aleatorio para email y nombre
            string emailRandom = "PracticaClase3" + random + "@cenfotec.com";
            var userName = "user_" + random;
            var userEmail = emailRandom;

            nameInput.SendKeys(userName);
            emailInput.SendKeys(userEmail);

            Driver.FindElement(By.XPath("//button[@data-qa='signup-button']")).Click();

            // 4. Continuar con los datos del formulario (esperar a que cargue el formulario de creación)
            wait.Until(d => d.FindElement(By.Id("id_gender1")));

            // Seleccionar título
            Driver.FindElement(By.Id("id_gender1")).Click();

            // contraseña
            Driver.FindElement(By.Id("password")).SendKeys("Password123!");

            // fecha de nacimiento
            var day = new SelectElement(Driver.FindElement(By.Id("days")));
            day.SelectByValue("10");
            var month = new SelectElement(Driver.FindElement(By.Id("months")));
            month.SelectByValue("5");
            var year = new SelectElement(Driver.FindElement(By.Id("years")));
            year.SelectByValue("1990");

            // checkbox suscripciones
            var newsletter = Driver.FindElement(By.Id("newsletter"));
            if (!newsletter.Selected) newsletter.Click();
            var optin = Driver.FindElement(By.Id("optin"));
            if (!optin.Selected) optin.Click();

            // completar detalles personales
            Driver.FindElement(By.Id("first_name")).SendKeys("Test");
            Driver.FindElement(By.Id("last_name")).SendKeys("User");
            Driver.FindElement(By.Id("company")).SendKeys("MyCompany");
            Driver.FindElement(By.Id("address1")).SendKeys("123 Main St");
            Driver.FindElement(By.Id("address2")).SendKeys("Apt 1");

            // Seleccionar pais usando Select by visible text o enviar texto
            try
            {
                var countrySelect = new SelectElement(Driver.FindElement(By.Id("country")));
                countrySelect.SelectByText("United States");
            }
            catch
            {
                // fallback: sendkeys
                var countryEl = Driver.FindElement(By.Id("country"));
                countryEl.Clear();
                countryEl.SendKeys("United States");
            }

            Driver.FindElement(By.Id("state")).SendKeys("CA");
            Driver.FindElement(By.Id("city")).SendKeys("San Francisco");
            Driver.FindElement(By.Id("zipcode")).SendKeys("94101");
            Driver.FindElement(By.Id("mobile_number")).SendKeys("+11234567890");

            // 5. Click en Create Account.
            Driver.FindElement(By.XPath("//button[@data-qa='create-account']")).Click();

            // 6. Validar mensaje: Account Created!
            wait.Until(d => d.FindElement(By.XPath("//h2[contains(., 'Account Created') or contains(., 'Account Created!')]")));
            var createdText = Driver.FindElement(By.XPath("//h2[contains(., 'Account Created') or contains(., 'Account Created!')]")).Text;
            Assert.That(createdText.ToLower(), Does.Contain("account created"));

            // 7. Click en Continue → validar que el usuario está logueado.
            var continueBtn = wait.Until(d => d.FindElement(By.XPath("//a[contains(., 'Continue')]")));
            // Click puede abrir un anuncio intermedio que cambie el foco; usar JS click como fallback
            try
            {
                continueBtn.Click();
            }
            catch
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", continueBtn);
            }

            // Esperar por el indicador "Logged in as"
            wait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Logged in as')]")).Count > 0);
            var loggedInEl = Driver.FindElement(By.XPath("//a[contains(., 'Logged in as')]")).Text;
            Assert.That(loggedInEl.ToLower(), Does.Contain("logged in as"));

            // Tomar captura
            ScreenshotHelper.TakeScreenshot(Driver, "automationexercise_registration.png");
        }
    }
}
