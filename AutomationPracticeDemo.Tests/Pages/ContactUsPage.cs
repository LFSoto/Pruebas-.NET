using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class ContactUsPage 
    {

        private readonly IWebDriver _driver;
        public ContactUsPage(IWebDriver driver)
        {
            _driver = driver;
        }


        private IWebElement BtnContactUs => _driver.FindElement(By.XPath("//a[@href='/contact_us']"));
        private IWebElement NameInput => _driver.FindElement(By.XPath("//input[@data-qa='name']"));

        private IWebElement EmailInput => _driver.FindElement(By.XPath("//input[@data-qa='email']"));
        
        private IWebElement SubjectInput => _driver.FindElement(By.XPath("//input[@data-qa='subject']"));

        private IWebElement MessageInput => _driver.FindElement(By.XPath("//textarea[@data-qa='message']"));

        private IWebElement UploadFileInput => _driver.FindElement(By.XPath("//input[@name='upload_file']"));

        private IWebElement SubmitButton => _driver.FindElement(By.XPath("//input[@data-qa='submit-button']"));

        private IWebElement SuccessMessage => _driver.FindElement(By.CssSelector(".status.alert.alert-success"));

        public void btnContactUs()
        {
            BtnContactUs.Click();
        }

        public void FillContactForm(string name, string email, string subject, string message)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => NameInput.Displayed && EmailInput.Displayed && SubjectInput.Displayed && MessageInput.Displayed);
            NameInput.SendKeys(name);
            EmailInput.SendKeys(email);
            SubjectInput.SendKeys(subject);
            MessageInput.SendKeys(message);
            
        }
        public void UploadFile(string filePath)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => UploadFileInput.Displayed);
            UploadFileInput.SendKeys(filePath);
          
        }

        public void SubmitForm()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => SubmitButton.Enabled && SubmitButton.Displayed);
            SubmitButton.Click();
            _driver.SwitchTo().Alert().Accept();
        }
        public string GetSuccessMessage()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => SuccessMessage.Displayed);
            return SuccessMessage.Text;
        }

        
    }
}
