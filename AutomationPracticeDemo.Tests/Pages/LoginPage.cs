using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace AutomationPracticeDemo.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }
        
        // Elementos del Login
        private IWebElement titleLoginAccount => _driver.FindElement(By.CssSelector("div.login-form h2"));
        private IWebElement emailLoginInput => _driver.FindElement(By.CssSelector("input[data-qa=\"login-email\"]"));
        private IWebElement passwordLoginInput => _driver.FindElement(By.CssSelector("input[data-qa=\"login-password\"]"));
        private IWebElement loginButton => _driver.FindElement(By.CssSelector("button[data-qa=\"login-button\"]"));
        private IWebElement messageIncorrectPassword => _driver.FindElement(By.CssSelector("div.login-form p"));


        /// <summary>
        /// Métodos para interactuar con los elementos del Login
        /// 

        //Metodo para obtener el título de Login 
        public string GetTitleLoginAccount()
        {
            if (titleLoginAccount.Displayed == false)
            {
                throw new NoSuchElementException("El elemento no está visible en la página.");
            }
            return titleLoginAccount.Text;
        }

        //Metodo para llenar el formulario de Login
        public void FillLogin(string email, string password)
        {
            emailLoginInput.SendKeys(email);
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
            if (messageIncorrectPassword.Displayed == false)
            {
                throw new NoSuchElementException("El elemento no está visible en la página.");
            }
            return messageIncorrectPassword.Text;
        }
    }
}
