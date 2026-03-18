using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LoginPage(IWebDriver driver, TimeSpan timeout)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, timeout);
        }

        // Navega a la página de login y espera a que el campo de email esté presente.
        public void GoTo()
        {
            _driver.Navigate().GoToUrl("https://automationexercise.com/login");
            _wait.Until(d => d.FindElement(By.CssSelector("input[data-qa='login-email']")));
        }

        // Realiza el proceso de login con las credenciales suministradas y espera el indicador de sesión "Logged in as".
        public void Login(string email, string password)
        {
            var emailInput = _driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
            var pwdInput = _driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
            emailInput.Clear();
            emailInput.SendKeys(email);
            pwdInput.Clear();
            pwdInput.SendKeys(password);

            var btn = _driver.FindElement(By.CssSelector("button[data-qa='login-button']"));
            try { btn.Click(); } catch { ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", btn); }

            _wait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Logged in as')]")).Count > 0);
        }

        // Intenta iniciar sesión con credenciales esperadas como inválidas.
        // Devuelve el primer mensaje de error detectado en la página o null si no se encuentra ninguno.
        public string LoginExpectingFailure(string email, string password)
        {
            var shortWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            var emailInput = _driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
            var pwdInput = _driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
            emailInput.Clear();
            emailInput.SendKeys(email);
            pwdInput.Clear();
            pwdInput.SendKeys(password);

            var btn = _driver.FindElement(By.CssSelector("button[data-qa='login-button']"));
            try { btn.Click(); } catch { ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", btn); }

            var errorXPaths = new[]
            {
                "//p[contains(., 'Your email or password is incorrect')]",
                "//div[contains(@class,'alert') and contains(., 'incorrect')]",
                "//div[contains(@class,'error')]",
                "//span[contains(., 'incorrect')]",
                "//p[contains(., 'Incorrect')]",
                "//div[contains(@class,'alert-danger')]",
                "//div[contains(@class,'validation-summary-errors')]",
                "//div[contains(@class,'form-error')]",
                "//div[contains(@class,'result') and contains(., 'incorrect')]",
            };

            try
            {
                shortWait.Until(drv => drv.FindElements(By.XPath("//a[contains(., 'Logged in as')]")).Count > 0
                                        || errorXPaths.Any(xp => drv.FindElements(By.XPath(xp)).Count > 0));
            }
            catch { }

            foreach (var xp in errorXPaths)
            {
                try
                {
                    var els = _driver.FindElements(By.XPath(xp));
                    var first = els.FirstOrDefault();
                    if (first != null && !string.IsNullOrWhiteSpace(first.Text))
                        return first.Text.Trim();
                }
                catch { }
            }

            try
            {
                var alertEl = _driver.FindElements(By.CssSelector("[role='alert']")).FirstOrDefault();
                if (alertEl != null && !string.IsNullOrWhiteSpace(alertEl.Text))
                    return alertEl.Text.Trim();
            }
            catch { }

            return null;
        }
    }
}
