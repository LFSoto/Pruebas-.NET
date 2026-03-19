using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class ContactPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ContactPage(IWebDriver driver, TimeSpan timeout)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, timeout);
        }

        public void GoTo()
        {
            _driver.Navigate().GoToUrl("https://automationexercise.com/contact_us");
            _wait.Until(d => d.FindElements(By.Name("name")).Count > 0);
        }

        public void Fill(ContactData data, string attachmentPath = null)
        {
            var nameEl = _driver.FindElement(By.Name("name"));
            var emailEl = _driver.FindElement(By.Name("email"));
            IWebElement subjectEl = null;
            try { subjectEl = _driver.FindElement(By.Name("subject")); } catch { }
            var messageEl = _driver.FindElement(By.Name("message"));

            nameEl.Clear(); nameEl.SendKeys(data.name);
            emailEl.Clear(); emailEl.SendKeys(data.email);
            if (subjectEl != null && !string.IsNullOrEmpty(data.subject)) { subjectEl.Clear(); subjectEl.SendKeys(data.subject); }
            messageEl.Clear(); messageEl.SendKeys(data.message);

            if (!string.IsNullOrEmpty(attachmentPath))
            {
                try
                {
                    var fileInput = _driver.FindElement(By.CssSelector("input[type='file']"));
                    fileInput.SendKeys(attachmentPath);
                }
                catch { }
            }
        }

        /// <summary>
        /// Envía el formulario Contact Us y espera el mensaje de éxito.
        /// Si la espera falla por timeout, intenta cerrar un posible alert del navegador
        /// (accept/dismiss) que esté bloqueando la página y reintenta una espera corta
        /// para detectar el mensaje de éxito antes de propagar el error.
        /// </summary>
        public void Submit()
        {
            var submitBtn = _driver.FindElements(By.XPath("//input[@type='submit'] | //button[contains(., 'Submit')] | //button[contains(., 'Submit')]")).FirstOrDefault();
            if (submitBtn != null) { try { submitBtn.Click(); } catch { ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", submitBtn); } }

            // Espera robusta: intenta detectar el mensaje de éxito. Si aparece un alert lo cierra y reintenta la espera.
            try
            {
                _wait.Until(d => d.FindElements(By.XPath("//*[contains(., 'Success! Your details have been submitted successfully') or contains(., 'Success!')]")).Count > 0);
            }
            catch (WebDriverTimeoutException)
            {
                // Si hubo timeout, puede deberse a un alert que bloqueó la interacción; intentar dismiss y esperar de nuevo corto tiempo
                try
                {
                    var alert = _driver.SwitchTo().Alert();
                    try { alert.Accept(); } catch { try { alert.Dismiss(); } catch { } }
                    TestContext.WriteLine("Dismissed alert during Contact submit.");

                    // reintentar una espera corta
                    var shortWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                    shortWait.Until(d => d.FindElements(By.XPath("//*[contains(., 'Success! Your details have been submitted successfully') or contains(., 'Success!')]")).Count > 0);
                }
                catch (NoAlertPresentException)
                {
                    // No alert; simplemente relanzar la excepción original para que el test falle con el timeout
                    throw;
                }
            }
        }

        public string GetSuccessMessage()
        {
            var el = _driver.FindElements(By.XPath("//*[contains(., 'Success! Your details have been submitted successfully') or contains(., 'Success!')]")).FirstOrDefault();
            return el?.Text;
        }
    }
}
