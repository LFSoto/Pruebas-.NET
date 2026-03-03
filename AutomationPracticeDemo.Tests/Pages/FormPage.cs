using OpenQA.Selenium;
using System.Reflection;
using System.Xml.Linq;

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
        private IWebElement GenderInput => _driver.FindElement(By.Name("gender"));
        private IList<IWebElement> DaysCheckBox => _driver.FindElements(By.XPath("//input[@type='checkbox' and @class='form-check-input']"));
        private IWebElement StartDateInput => _driver.FindElement(By.Id("start-date"));
        private IWebElement EndDateInput => _driver.FindElement(By.Id("end-date"));
        private IWebElement SubmitDateButton => _driver.FindElement(By.CssSelector(".date-picker-box .submit-btn"));
        private IWebElement PromptButton => _driver.FindElement(By.Id("promptBtn"));
        public void FillForm(string name, string email, string phone, string country)
        {
            NameInput.SendKeys(name);
            EmailInput.SendKeys(email);
            PhoneInput.SendKeys(phone);
            CountryDropdown.SendKeys(country);
        }
        public void FillGender(string gander)
        {
            GenderInput.SendKeys(gander);
            GenderInput.Click();
        }
        public void Submit()
        {
            SubmitButton.Click();
        }
        public void FillDays(string day)
        {
            foreach (IWebElement item in DaysCheckBox)
            {
                if(item.GetAttribute("value") == day) { 
                    item.SendKeys(day);
                    item.Click();
                }
            }
        }
        public void FillRangeDates(string start, string end)
        {
            StartDateInput.Clear();
            StartDateInput.SendKeys(start);
            EndDateInput.Clear();
            EndDateInput.SendKeys(end);
        }
        public void SubmitDates()
        {
            SubmitDateButton.Click();
        }
        public void OpenAlertPrompt()
        {
            PromptButton.Click();
        }
    }
}
