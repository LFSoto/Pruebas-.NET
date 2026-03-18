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
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        // Elementos del menu
        // Nota: selectors más tolerantes (evitan depender de un único header)
        private IWebElement signupLoginOption => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/login']")));
        private IWebElement productOption => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/products']")));
        private IWebElement contactUsOption => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/contact_us']")));

        private IWebElement logoutOption => _wait.Until(d =>
        {
            try
            {
                var el = d.FindElement(By.CssSelector("a[href='/logout']"));
                return el.Displayed ? el : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        });

        public void ClickSignUpLogin() => signupLoginOption.Click();

        public void ClickProductOption() => productOption.Click();

        public void ClickContactUsOption() => contactUsOption.Click();

        public string validatedUserLogout()
        {
            // Espera a que el logout esté visible (indica sesión iniciada)
            return logoutOption.Text;
        }
    }
}
