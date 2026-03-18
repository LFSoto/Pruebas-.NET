using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver Driver;

        public LoginPage(IWebDriver driver)
        {
            Driver = driver;
        }

        // Campos de usuario y contraseña
        public void EnterUsername(string username)
        {
            var emailField = Driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
            emailField.Clear();
            emailField.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            var passwordField = Driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
            passwordField.Clear();
            passwordField.SendKeys(password);
        }

        // ✅ Método corregido para hacer click en el botón de login
        public void ClickLogin()
        {
            var loginButton = Driver.FindElement(By.CssSelector("button[data-qa='login-button']"));

            // Espera a que el botón sea visible
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("button[data-qa='login-button']")));

            // Scroll al botón
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", loginButton);

            // Click forzado con JS
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", loginButton);
        }
    }
}

