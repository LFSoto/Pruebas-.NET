using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        // Elementos del Login
        private IWebElement titleLoginAccount => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div.login-form h2")));
        private IWebElement emailLoginInput => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("input[data-qa=\"login-email\"]")));
        private IWebElement passwordLoginInput => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("input[data-qa=\"login-password\"]")));
        private IWebElement loginButton => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("button[data-qa=\"login-button\"]")));

        /// <summary>
        /// Métodos para interactuar con los elementos del Login
        /// 

        //Metodo para obtener el título de Login 
        public string GetTitleLoginAccount()
        {
            return titleLoginAccount.Text;
        }

        //Metodo para llenar el formulario de Login
        public void FillLogin(string email, string password)
        {
            emailLoginInput.Clear();
            emailLoginInput.SendKeys(email);

            passwordLoginInput.Clear();
            passwordLoginInput.SendKeys(password);
        }

        //Metodo para enviar el formulario de Login
        public void SubmitLogin()
        {
            loginButton.Click();
        }

        //Metodo para obtener el mensaje de contraseña incorrecta
        public string GetMessageIncorrectPassword()
        {
            // El sitio puede renderizar el mensaje en distintos contenedores.
            // Probamos varios selectores comunes y esperamos a que exista y tenga texto.
            By[] candidates =
            {
                By.CssSelector("div.login-form p"),
                By.CssSelector("form[action='/login'] p"),
                By.CssSelector("section#form p"),
                By.XPath("//p[contains(.,'Your email or password is incorrect')]"),
            };

            return _wait.Until(d =>
            {
                foreach (var by in candidates)
                {
                    try
                    {
                        var el = d.FindElement(by);
                        if (el.Displayed)
                        {
                            var text = (el.Text ?? string.Empty).Trim();
                            if (!string.IsNullOrWhiteSpace(text))
                                return text;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        // try next
                    }
                }

                return null;
            }) ?? string.Empty;
        }

        public bool IsLogoutVisible()
        {
            try
            {
                return _driver.FindElement(By.CssSelector("a[href='/logout']")).Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
