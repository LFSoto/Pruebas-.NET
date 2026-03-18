using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class NewsletterPage
    {
        private readonly IWebDriver Driver;
        private readonly WebDriverWait Wait;

        public NewsletterPage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        public void Subscribe(string email)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("susbscribe_email")));
            Driver.FindElement(By.Id("susbscribe_email")).SendKeys(email);
            Driver.FindElement(By.Id("subscribe")).Click();
        }

        public bool IsSubscribed()
        {
            var message = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("success-subscribe"))).Text;
            return message == "You have been successfully subscribed!";
        }
    }
}

