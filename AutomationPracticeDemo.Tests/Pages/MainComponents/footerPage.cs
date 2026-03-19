using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Pages.MainComponents
{
    public class footerPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public footerPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        // Elementos del newsletter subscription
        private IWebElement subscribreElementInput => _driver.FindElement(By.Id("susbscribe_email"));
        private IWebElement suscribeButton => _driver.FindElement(By.Id("subscribe"));
        private IWebElement susbscribreMessage => _driver.FindElement(By.CssSelector("div[class=\"alert-success alert\"]"));

        private IWebElement WaitVisible(By by) => _wait.Until(d =>
        {
            try
            {
                var el = d.FindElement(by);
                return el.Displayed ? el : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }
        });

        // Métodos para la suscripción al newsletter
        public void FillSuscribeInput(string email)
        {
            var input = WaitVisible(By.Id("susbscribe_email"));
            input.Clear();
            input.SendKeys(email);
        }

        public string GetSuscribeMessage()
        {
            return WaitVisible(By.CssSelector("div[class=\"alert-success alert\"]")).Text;
        }

        public void SumitSuscibeButton()
        {
            var button = WaitVisible(By.Id("subscribe"));

            // Scroll so it is in view (helps in headless)
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", button);

            try
            {
                // Wait until Selenium thinks the button is clickable
                _wait.Until(_ => button.Enabled && button.Displayed);
                button.Click();
            }
            catch (ElementClickInterceptedException)
            {
                // Ads/iframes sometimes intercept. JS click bypasses that.
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", button);
            }
        }
    }
}
