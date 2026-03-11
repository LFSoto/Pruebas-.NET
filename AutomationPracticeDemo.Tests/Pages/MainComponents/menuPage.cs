using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Pages.MainComponents
{
    public class menuPage
    {
        private readonly IWebDriver _driver;
        public menuPage(IWebDriver driver)
        {
            _driver = driver;
        }
        // Declaración de los elementos de la página

        //Elementos del menu
        private IWebElement signupLoginOption => _driver.FindElement(By.CssSelector("li a[href=\"/login\"]"));
        private IWebElement productOption => _driver.FindElement(By.CssSelector("li a[href=\"/products\"]"));
        private IWebElement logoutOption => _driver.FindElement(By.CssSelector("li a[href=\"/logout\"]"));
        private IWebElement contactUsOption => _driver.FindElement(By.CssSelector("li a[href=\"/contact_us\"]"));


        // Declaración de metodos para interactuar con los elementos del menu de navegación
        public void ClickSignUpLogin()
        {
            signupLoginOption.Click();
        }
        public void ClickProductOption()
        {
            productOption.Click();
        }
        public void ClickContactUsOption()
        {
            contactUsOption.Click();
        }   
        public string validatedUserLogout()
        {
            if (logoutOption.Displayed == false)
            {
                throw new NoSuchElementException("El elemento Logout no está visible en la página.");
            }
            return logoutOption.Text;
        }

    }
}
