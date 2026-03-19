using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Pages.Components
{
    public class footerPage
    {

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public footerPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        private IWebElement emailSuscription => _driver.FindElement(By.Id("susbscribe_email"));
        private IWebElement btnSuscription => _driver.FindElement(By.Id("subscribe"));
        private IWebElement message => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("success-subscribe")));

        public void InputEmailSuscription(string email)
        {
            emailSuscription.SendKeys(email);
        }
        public void ClickSubscribeButton()
        { 
            btnSuscription.Click();
        }

        public string GetMessageSuscription()
        {
            //var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            //var message = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("success-subscribe")));
            return message.Text;
        }
    }
}
