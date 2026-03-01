using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class FormPage
    {
        private readonly IWebDriver _driver;

        public FormPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement NameInput => _driver.FindElement(By.Id("name"));
        private IWebElement EmailInput => _driver.FindElement(By.Id("email"));
        private IWebElement PhoneInput => _driver.FindElement(By.Id("phone"));
        private IWebElement CountryDropdown => _driver.FindElement(By.Id("country"));
        private IWebElement SubmitButton => _driver.FindElement(By.ClassName("submit-btn"));

        public void FillForm(string name, string email, string phone, string country)
        {
            NameInput.Clear();
            NameInput.SendKeys(name);

            EmailInput.Clear();
            EmailInput.SendKeys(email);

            PhoneInput.Clear();
            PhoneInput.SendKeys(phone);

            SelectCountryByText(country);
        }

        public void SelectCountryByText(string country)
        {
            var select = new SelectElement(CountryDropdown);
            try
            {
                select.SelectByText(country);
            }
            catch (NoSuchElementException)
            {
                // If text not found, try selecting by value (fallback)
                select.SelectByValue(country);
            }
        }

        public string GetName() => NameInput.GetAttribute("value");
        public string GetEmail() => EmailInput.GetAttribute("value");
        public string GetPhone() => PhoneInput.GetAttribute("value");

        public void Submit()
        {
            SubmitButton.Click();
        }

        public string WaitForAlertAndAccept(int timeoutSeconds = 5)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d =>
            {
                try
                {
                    d.SwitchTo().Alert();
                    return true;
                }
                catch
                {
                    return false;
                }
            });

            var alert = _driver.SwitchTo().Alert();
            var text = alert.Text;
            alert.Accept();
            return text;
        }
    }
}


