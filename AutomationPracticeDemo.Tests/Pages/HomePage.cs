using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public HomePage(IWebDriver driver, TimeSpan timeout)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, timeout);
        }

        public void GoTo()
        {
            _driver.Navigate().GoToUrl("https://automationexercise.com/");
            _wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void GoToSignup()
        {
            var el = _wait.Until(d => d.FindElement(By.XPath("//a[contains(., 'Signup / Login')]")));
            el.Click();
        }
    }
}
