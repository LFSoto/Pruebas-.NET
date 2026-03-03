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
        private IWebElement DatePicker1Input2 => _driver.FindElement(By.Id("txtDate"));
        private IWebElement DatePicker3Start => _driver.FindElement(By.Id("start-date"));
        private IWebElement DatePicker3End => _driver.FindElement(By.Id("end-date"));

        private IWebElement Mensajepantalla => _driver.FindElement(By.Id("result"));

        private IWebElement btnconfirmation => _driver.FindElement(By.Id("confirmBtn"));
        private IWebElement btnPrompt => _driver.FindElement(By.Id("promptBtn"));

        private IWebElement mensajepantallademo => _driver.FindElement(By.Id("demo"));
        



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
        public bool IsGenderSelected()
        {
            return GenderInput.Selected;
        }
        public void days()
        {
            CheckboxMondayInput.Click();
            CheckboxTuesdayInput.Click();
            CheckboxwednesdaydInput.Click();
        }

        public bool daysSelectMonday()
        {
            return CheckboxMondayInput.Selected;
        }
        public bool daysSelectTuesday()
        {
            return CheckboxTuesdayInput.Selected;
        }
        public bool daysSelectWednesday()
        {
            return CheckboxwednesdaydInput.Selected;
        }
        public String country()
        {
            SelectElement select = new SelectElement(CountryDropdown);
            select.SelectByText("Japan");
            return select.SelectedOption.Text;
        }
      
        public String Colors()
        {
            SelectElement select = new SelectElement(ColorsInput);
            select.SelectByText("Red");
            return select.SelectedOption.Text;
        }
        public String Animals()
        {
            SelectElement select = new SelectElement(SortedListInput);
            select.SelectByText("Dog");
            return select.SelectedOption.Text;
        }
        public void ColorsSelect()
        {
            SelectElement select = new SelectElement(ColorsInput);
            select.SelectByText("Red");

        }
        public void AnimalsSelect()
        {
            SelectElement select = new SelectElement(SortedListInput);
            select.SelectByText("Dog");

        }

        public void Datapicker1()
        {
            DatePicker1Input.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            wait.Until(d => d.FindElement(By.CssSelector("#ui-datepicker-div")).Displayed);


            while (true)
            {
                string header = _driver.FindElement(By.CssSelector("#ui-datepicker-div .ui-datepicker-title")).Text;
                if (header.Contains("March") && header.Contains("2026"))
                    break;
                _driver.FindElement(By.CssSelector("#ui-datepicker-div .ui-datepicker-next")).Click();

                wait.Until(d => d.FindElement(By.CssSelector("#ui-datepicker-div .ui-datepicker-title")).Displayed);
            }


            _driver.FindElement(By.XPath("//div[@id='ui-datepicker-div']//table[@class='ui-datepicker-calendar']//a[text()='10']")).Click();
        }
        public void Datapicker2()
        {

            DatePicker1Input2.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));


            // Seleccionar año con SelectElement
            IWebElement yearDropdown = wait.Until(d => d.FindElement(By.ClassName("ui-datepicker-year")));
            var selectYear = new SelectElement(yearDropdown);
            selectYear.SelectByText("2026");

            // Seleccionar mes con SelectElement
            IWebElement monthDropdown = wait.Until(d => d.FindElement(By.ClassName("ui-datepicker-month")));
            var selectMonth = new SelectElement(monthDropdown);
            selectMonth.SelectByText("Mar"); // O "March", según cómo aparezca en el HTML

            // Seleccionar día
            IWebElement dayCell = wait.Until(d => d.FindElement(By.XPath("//a[text()='25']")));
            dayCell.Click();



        }



        public string Datapicker3(string fechainicio, string fechafinal)
        {

            DatePicker3Start.SendKeys(fechainicio);
            DatePicker3End.SendKeys(fechafinal);

            DateTime fechaInicio1 = DateTime.ParseExact(fechainicio, "dd/MM/yyyy", null);
            DateTime fechafin1 = DateTime.ParseExact(fechafinal, "dd/MM/yyyy", null);

            int dias = (fechafin1 - fechaInicio1).Days;

            return dias.ToString();



        }
        public string GetMensajePantalla()
        {
            return Mensajepantalla.Text;

        }
        public void alertConfirmation()
        {
            btnconfirmation.Click();
        }
        public void alertPrompt()
        {
            btnPrompt.Click();
        }

        public string GetMensajePantallaPrompt()
        {
            return mensajepantallademo.Text;

        }
    }
}
