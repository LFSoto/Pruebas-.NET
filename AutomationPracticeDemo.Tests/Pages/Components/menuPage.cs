using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Pages.Components
{
    public class menuPage
    {
        private readonly IWebDriver _driver;

        private readonly WebDriverWait _wait;

        public menuPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }
        private IWebElement signupLogin => _driver.FindElement(By.CssSelector("a[href='/login']"));
        private IWebElement LoggedIn => _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(., 'Logged in as')]")));
        private IWebElement contactUs => _driver.FindElement(By.CssSelector("a[href='/contact_us']"));

        public void ClickSignupLogin()
        {
            signupLogin.Click();
        }

        //Este método verifica si el elemento "Logged in as" está visible en la página. Si no lo está, lanza una excepción indicando que el usuario no está loggeado. Si el elemento está visible, devuelve el texto del mismo.
        public string IsLoggedVisible() 
        {
            try
            {
                // 1. Definimos una espera de 10 segundos
                //var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

                // 2. Esperamos hasta que el elemento sea visible
                // Esto reemplaza al 'if (loggedIn.Displayed)'
                //var element = wait.Until(d => d.FindElement(loggedIn));
                return LoggedIn.Text;
            }
            catch (Exception)
            {
                return "Elemento no encontrado";
            }
        }

        //Este método direcciona a la página de contacto
        public void ClickContactUs()
        {
            contactUs.Click();
        }
    }
}