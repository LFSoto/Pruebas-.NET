using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class SuscriptionPage
    {
        private readonly IWebDriver _driver;
        public SuscriptionPage(IWebDriver driver)
        {
            _driver = driver;
        }
        private IWebElement susbscribeInput => _driver.FindElement(By.Id("susbscribe_email"));

        private IWebElement susbscribeBtn => _driver.FindElement(By.Id("subscribe"));

        private IWebElement susbscribeMessage => _driver.FindElement(By.Id("success-subscribe"));


        public void Bajarscroll() {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }

        public void Fillemail(string email)
        {
            susbscribeInput.SendKeys(email);
        }
        public void btnclickSuscrition() { 
            susbscribeBtn.Click();
        }

        public string MessageSusbscribe()
        {
            if (susbscribeMessage.Displayed == false)
            {
                throw new NoSuchElementException("You have been successfully subscribed!' no está visible en la pantalla.");
            }

            return susbscribeMessage.Text;
        }

        /*
        IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
        js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            IWebElement susbscribe = Driver.FindElement(By.Id("susbscribe_email"));
        susbscribe.SendKeys("Gustavo@emial.com");
            IWebElement susbscribeBtn = Driver.FindElement(By.Id("subscribe"));
        susbscribeBtn.Click();
            IWebElement susbscribeMessage = Driver.FindElement(By.Id("success-subscribe"));
        Assert.That(susbscribeMessage.Text, Is.EqualTo("You have been successfully subscribed!"));
        */
    }
}
