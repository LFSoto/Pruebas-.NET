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

        // -------------------- TextBoxes --------------------
        private IWebElement NameInput => _driver.FindElement(By.Id("name"));
        private IWebElement EmailInput => _driver.FindElement(By.Id("email"));
        private IWebElement PhoneInput => _driver.FindElement(By.Id("phone"));

        // -------------------- DropDown --------------------
        private IWebElement CountryDropdown => _driver.FindElement(By.Id("country"));

        // -------------------- RadioButtons ** Nuevo --------------------

        private IWebElement MaleRadio => _driver.FindElement(By.Id("male"));
        private IWebElement FemaleRadio => _driver.FindElement(By.Id("female"));


        // -------------------- DatePicker ** Nuevo --------------------
        private IWebElement DatePicker => _driver.FindElement(By.Id("datepicker"));


        // -------------------- Button ** Nuevo --------------------
        private IWebElement SubmitButton => _driver.FindElement(By.ClassName("submit-btn"));


        // -------------------- Alert simple --------------------
        private IWebElement AlertButton => _driver.FindElement(By.Id("alertBtn"));

        // -------------------- Alert confirmación --------------------
        private IWebElement ConfirmButton => _driver.FindElement(By.Id("confirmBtn"));


        public void FillForm(string name, string email, string phone, string country)
        {
            NameInput.SendKeys(name);
            EmailInput.SendKeys(email);
            PhoneInput.SendKeys(phone);
            CountryDropdown.SendKeys(country);
        }

        public void SelectGenderMale(string gender)  // ** Nuevo
        {
            if (gender.ToLower() == "male")
                MaleRadio.Click();
            else
                FemaleRadio.Click();
        }


        public void SelectGenderFemale(string gender)  // ** Nuevo
        {
            if (gender.ToLower() == "female")
                MaleRadio.Click();
            else
                FemaleRadio.Click();
        }

        public void PickDate(string date)
        {
            DatePicker.SendKeys(date);
        }

        public string GetSelectedDate()
        {
            return DatePicker.GetAttribute("value");
        }


        public void Submit()
        {
            SubmitButton.Click();
        }

        // -------------------- Métodos para Alert simple --------------------
        public string GetAlertTextAndAccept()
        {
            AlertButton.Click();
            IAlert alert = _driver.SwitchTo().Alert();
            string alertText = alert.Text;
            alert.Accept(); // cerrar el alert
            return alertText;
        }


        // -------------------- Métodos para Confirm Alert --------------------
        public void TriggerConfirmAlert()
        {
            ConfirmButton.Click();
        }

        public string GetAlertText()
        {
            return _driver.SwitchTo().Alert().Text;
        }

        public void AcceptAlert()
        {
            _driver.SwitchTo().Alert().Accept();
        }

        public void DismissAlert()
        {
            _driver.SwitchTo().Alert().Dismiss();
        }

    }
}
