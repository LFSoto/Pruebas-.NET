using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class FooterComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public FooterComponent(IWebDriver driver, TimeSpan timeout)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, timeout);
        }

        public IWebElement FindNewsletterInput()
        {
            try
            {
                var el = _driver.FindElements(By.CssSelector("footer input[type='email'], input#susbscribe_email, input[name='email'], input[placeholder*='email']")).FirstOrDefault();
                if (el != null) return el;
            }
            catch { }

            try
            {
                var footer = _driver.FindElements(By.TagName("footer")).FirstOrDefault();
                if (footer != null)
                    return footer.FindElements(By.XPath(".//input[@type='email' or contains(@placeholder,'email') or @name='email']")).FirstOrDefault();
            }
            catch { }

            return null;
        }

        public void Subscribe(string email)
        {
            var input = FindNewsletterInput();
            if (input == null) throw new NoSuchElementException("Newsletter input not found");
            input.Clear(); input.SendKeys(email);

            IWebElement submitBtn = null;
            try { submitBtn = input.FindElements(By.XPath("following::button[1] | following::input[@type='submit'][1]")).FirstOrDefault(); } catch { }
            if (submitBtn == null) submitBtn = _driver.FindElements(By.XPath("//button[contains(., 'Subscribe') or contains(., 'SUBSCRIBE') or contains(., 'Submit')]")).FirstOrDefault();
            if (submitBtn == null) throw new NoSuchElementException("Newsletter submit button not found");
            try { submitBtn.Click(); } catch { ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", submitBtn); }

            _wait.Until(d => d.FindElements(By.XPath("//*[contains(., 'You have been successfully subscribed') or contains(., 'successfully subscribed')]")).Count > 0);
        }
    }
}
