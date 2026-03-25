using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class NewsletterPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public NewsletterPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Campo de email en el footer
        private IWebElement EmailInput => wait.Until(
            ExpectedConditions.ElementIsVisible(By.CssSelector("input#susbscribe_email"))
        );

        // Botón de suscripción
        private IWebElement SubscribeButton => driver.FindElement(By.CssSelector("button#subscribe"));

        // Mensaje de éxito
        private IWebElement SuccessMessage => wait.Until(
            ExpectedConditions.ElementIsVisible(By.CssSelector("div#success-subscribe"))
        );

        public void SubscribeToNewsletter(string email)
        {
            EmailInput.Clear();
            EmailInput.SendKeys(email);
            SubscribeButton.Click();
        }

        public string GetSuccessMessage()
        {
            return SuccessMessage.Text;
        }
    }
}