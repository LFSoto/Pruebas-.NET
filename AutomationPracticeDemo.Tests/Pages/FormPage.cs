using OpenQA.Selenium;

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
        private IWebElement maleRadioButton => _driver.FindElement(By.Id("male"));
        private IWebElement femaleRadioButton => _driver.FindElement(By.Id("female"));
        private IWebElement section1Input => _driver.FindElement(By.Id("input1"));
        private IWebElement section1Button => _driver.FindElement(By.Id("btn1"));
        private IWebElement datePicker1 => _driver.FindElement(By.Id("datepicker"));
        private IWebElement simpleAlert => _driver.FindElement(By.Id("alertBtn"));

        public void FillForm(string name, string email, string phone, string country)
        {
            NameInput.SendKeys(name);
            EmailInput.SendKeys(email);
            PhoneInput.SendKeys(phone);
            CountryDropdown.SendKeys(country);
        }

        public void Submit()
        {
            SubmitButton.Click();
        }

        public void SelectGender(string gender)
        {
            if (gender == "Female")
            {
                femaleRadioButton.Click();
            }
            else
            {
                maleRadioButton.Click();
            }
        }
        public void FillSection1(string text)
        {
            section1Input.SendKeys(text);
            section1Button.Click();
        }

        public string GetSection1Text()
        {
            return section1Input.GetAttribute("value");    
        }

        public void AddDate(string date)
        {
            datePicker1.SendKeys(date);
        }
        public void SelectTodayDate()
        {
            DateTime date = DateTime.Now;
            datePicker1.Click();
            string dateSelector = "#ui-datepicker-div td[data-month='" + Convert.ToString(date.Month - 1) + "'][data-year='" + date.Year.ToString() + "'] a[data-date='" + date.Day.ToString() + "']";
            var today = _driver.FindElement(By.CssSelector(dateSelector));
            today.Click();
        }
        public void ClearDate()
        {
            datePicker1.Clear();
        }
        public string TriggerAlert()
        {
            simpleAlert.Click();
            var alert = _driver.SwitchTo().Alert();
            return alert?.Text ?? string.Empty;
        }

        public void AcceptAlert()
        {
            _driver.SwitchTo().Alert().Accept();
        }
    }
}
