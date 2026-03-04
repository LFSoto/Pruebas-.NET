using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        //Obtiene el radio button de género masculino
        private IWebElement GenderRadioButton => _driver.FindElement(By.Id("male")); //Ejemplo para género masculino");

        //Selecciona un drowpdown para seleccionar el país
        private IWebElement CountryDropdown1 => _driver.FindElement(By.Id("country"));

        //Seleccionar Date Picker 1
        private IWebElement DatePicker1 => _driver.FindElement(By.Id("datepicker"));

        //Seleccionar Simple Alert
        private IWebElement SimpleAlertButton => _driver.FindElement(By.Id("alertBtn"));
        private IWebElement SubmitButton => _driver.FindElement(By.ClassName("submit-btn"));

        public void FillForm(string name, string email, string phone, string country)
        {
            NameInput.SendKeys(name);
            EmailInput.SendKeys(email);
            PhoneInput.SendKeys(phone);
            CountryDropdown.SendKeys(country);
        }

        //Método para seleccionar el género
        public void SelectGenderMale()
        {
            // Selecciona el radio button de género masculino si no está ya seleccionado
            if (!GenderRadioButton.Selected)
            {
                GenderRadioButton.Click();
            }
        }

        //Método para seleccionar el país
        public void SelectCountry()
        {
            CountryDropdown1.Click();
            var select = new SelectElement(CountryDropdown1);
            select.SelectByText("Brazil");
        }

        //Método para seleccionar el Date Picker 1
        public void SelectDatePicker1()
        {
            DatePicker1.Clear();
            DatePicker1.SendKeys("03/15/2026");
            DatePicker1.SendKeys(Keys.Enter);
        }

        //Método para seleccionar una simple alert - ALERTS
        public void SelectSimpleAlert(int milliseconds = 2000) // 👈 tiempo visible
        {
            SimpleAlertButton.Click();

            // Espera a que la alerta esté presente
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(drv =>
        {
            try
            {
                drv.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        });

        // Pausa visual para "ver" la alerta abierta
        Task.Delay(milliseconds).Wait();

        // Acepta la alerta
        _driver.SwitchTo().Alert().Accept();

        // TOMAMOS LA CAPTURA AQUÍ (Ahora que la alerta se cerró)
        AutomationPracticeDemo.Tests.Utils.ScreenshotHelper.TakeScreenshot(_driver, "alert.png");
        }



    public void Submit()
        {
            SubmitButton.Click();
        }
    }
}
//Aquí no hay Asserts, solo interacción con elementos de la página.