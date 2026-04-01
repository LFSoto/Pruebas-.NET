using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages.MainComponents
{
    public class menuPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public menuPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }
        // Declaración de los elementos de la página

        //Elementos del menu
        private IWebElement signupLoginOption => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("li a[href=\"/login\"]")));
        private IWebElement productOption => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("li a[href=\"/products\"]")));
        private IWebElement logoutOption => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("li a[href=\"/logout\"]")));
        private IWebElement contactUsOption => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("li a[href=\"/contact_us\"]")));
        private IWebElement homeOption => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("li a[href=\"/\"]")));

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

        public void ClickHomeOption()
        {
            homeOption.Click();
        }
    }
}
