using OpenQA.Selenium;
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
        public footerPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Elementos del newsletter subscription
        private IWebElement subscribreElementInput => _driver.FindElement(By.Id("susbscribe_email"));
        private IWebElement suscribeButton => _driver.FindElement(By.Id("subscribe"));
        private IWebElement susbscribreMessage => _driver.FindElement(By.CssSelector("div[class=\"alert-success alert\"]"));

        // Métodos para la suscripción al newsletter
        public void FillSuscribeInput(string email)
        {
            subscribreElementInput.SendKeys(email);
        }
        public string GetSuscribeMessage()
        {
            return susbscribreMessage.Text;
        }
        public void SumitSuscibeButton()
        {
            suscribeButton.Click();
        }
    }
}
