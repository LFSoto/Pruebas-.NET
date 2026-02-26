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

        private IWebElement Alertsimplebutton => _driver.FindElement(By.Id("alertBtn"));
        private IWebElement AddressInput => _driver.FindElement(By.Id("textarea"));
        private IWebElement GenderInput => _driver.FindElement(By.Id("female"));
        private IWebElement CheckboxMondayInput => _driver.FindElement(By.Id("monday"));
        private IWebElement CheckboxTuesdayInput => _driver.FindElement(By.Id("tuesday"));
        private IWebElement CheckboxwednesdaydInput => _driver.FindElement(By.Id("wednesday"));

        private IWebElement ColorsInput => _driver.FindElement(By.Id("colors"));

        private IWebElement SortedListInput => _driver.FindElement(By.Id("animals"));

        private IWebElement DatePicker1Input => _driver.FindElement(By.Id("datepicker"));

        public void FillForm(string name, string email, string phone, string address)

        {
            NameInput.SendKeys(name);
            EmailInput.SendKeys(email);
            PhoneInput.SendKeys(phone);
            AddressInput.SendKeys(address);
            

        }

        public void Submit()
        {
            SubmitButton.Click();
        }

        public void alertsimple()
        {
            Alertsimplebutton.Click();
        }
        public void Gender()
        {
            GenderInput.Click();
        }
        public void days()
        {
            CheckboxMondayInput.Click();
            CheckboxTuesdayInput.Click();
            CheckboxwednesdaydInput.Click();
        }
        public void country()
        {
            SelectElement select = new SelectElement(CountryDropdown);
            select.SelectByText("Japan");

        }
        public void Colors()
        {
            SelectElement select = new SelectElement(ColorsInput);
            select.SelectByText("Red");

        }
        public void Animals()
        {
            SelectElement select = new SelectElement(SortedListInput);
            select.SelectByText("Dog");

        }

        public void Datapicker1()
        {
            DatePicker1Input.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            // Esperar a que el datepicker (generalmente renderizado en #ui-datepicker-div) sea visible
            wait.Until(d => d.FindElement(By.CssSelector("#ui-datepicker-div")).Displayed);

            // Navegar hasta febrero 2025 usando el contenedor global del datepicker
            while (true)
            {
                string header = _driver.FindElement(By.CssSelector("#ui-datepicker-div .ui-datepicker-title")).Text;
                if (header.Contains("February") && header.Contains("2027"))
                    break;
                _driver.FindElement(By.CssSelector("#ui-datepicker-div .ui-datepicker-next")).Click();
                // esperar a que el encabezado se actualice
                wait.Until(d => d.FindElement(By.CssSelector("#ui-datepicker-div .ui-datepicker-title")).Displayed);
            }

            // Seleccionar el día 10 dentro del contenedor global
            _driver.FindElement(By.XPath("//div[@id='ui-datepicker-div']//table[@class='ui-datepicker-calendar']//a[text()='10']")).Click();
        }
        public void Datapicker2()
        {
        } public void Datapicker3()
        {
        }
    }
}
