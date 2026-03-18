using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace AutomationPracticeDemo.Tests.Pages
{
    public class ContactUsPage
    {
        private readonly IWebDriver Driver;
        private readonly WebDriverWait Wait;

        public ContactUsPage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        public void GoTo()
        {
            Driver.FindElement(By.CssSelector("a[href='/contact_us']")).Click();
        }

        public void FillForm(string name, string email, string subject, string message, string filePath)
        {
            Driver.FindElement(By.Name("name")).SendKeys(name);
            Driver.FindElement(By.Name("email")).SendKeys(email);
            Driver.FindElement(By.Name("subject")).SendKeys(subject);
            Driver.FindElement(By.Id("message")).SendKeys(message);
            Driver.FindElement(By.Name("upload_file")).SendKeys(filePath);

            // 📸 Captura justo después de llenar todos los datos
            ScreenshotHelper.TakeScreenshot(Driver, "ContactUs_DatosIngresados");

        }

        public void Submit()
        {
            Driver.FindElement(By.Name("submit")).Click();
            try
            {
                IAlert alert = Wait.Until(ExpectedConditions.AlertIsPresent());
                alert.Accept();
            }
            catch { }
        }

        public bool IsSuccess()
        {
            var successElement = Wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//div[contains(@class,'status') and contains(text(),'Success')]")));
            return successElement.Text.Contains("Success!");
        }
    }
}

