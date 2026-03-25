using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class ContactUsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public ContactUsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Selectores
        private IWebElement NameInput => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[data-qa='name']")));
        private IWebElement EmailInput => driver.FindElement(By.CssSelector("input[data-qa='email']"));
        private IWebElement SubjectInput => driver.FindElement(By.CssSelector("input[data-qa='subject']"));
        private IWebElement MessageInput => driver.FindElement(By.CssSelector("textarea[data-qa='message']"));
        private IWebElement SubmitButton => driver.FindElement(By.CssSelector("input[data-qa='submit-button']"));
        private IWebElement SuccessMessage => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.status.alert.alert-success")));

        // Método para llenar el formulario
        public void FillContactForm(string name, string email, string subject, string message)
        {
            NameInput.SendKeys(name);
            EmailInput.SendKeys(email);
            SubjectInput.SendKeys(subject);
            MessageInput.SendKeys(message);
        }

        // Método para enviar el formulario
        public void SubmitForm()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", SubmitButton);

            try
            {
                // Esperar y aceptar el alert
                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
                alert.Accept();
            }
            catch (WebDriverTimeoutException)
            {
                // Si no aparece alert, continuar normalmente
            }
        }

        // Método para validar mensaje de éxito
        public string GetSuccessMessage()
        {
            return SuccessMessage.Text;
        }
    }
}