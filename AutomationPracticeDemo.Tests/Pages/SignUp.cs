using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class SignUp
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public SignUp(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        //Variables para los elementos de la página de registro
        private IWebElement nameSignupInput => _driver.FindElement(By.CssSelector("input[data-qa='signup-name']"));
        private IWebElement emailSignupInput => _driver.FindElement(By.CssSelector("input[data-qa='signup-email']"));
        private IWebElement signupButton => _driver.FindElement(By.CssSelector("button[data-qa='signup-button']"));

        //Variables para el signup
        private IWebElement titleSignup => _driver.FindElement(By.Id("id_gender1"));
        private IWebElement passwordSignup => _driver.FindElement(By.Id("password"));
        private IWebElement firstNameSignup => _driver.FindElement(By.Id("first_name"));
        private IWebElement lastNameSignup => _driver.FindElement(By.Id("last_name"));
        private IWebElement AddresSignup => _driver.FindElement(By.Id("address1"));
        private IWebElement countrySignup => _driver.FindElement(By.Id("country"));
        private IWebElement stateSignup => _driver.FindElement(By.Id("state"));
        private IWebElement citySignup => _driver.FindElement(By.Id("city"));
        private IWebElement zipCodeSignup => _driver.FindElement(By.Id("zipcode"));
        private IWebElement mobileSignup => _driver.FindElement(By.Id("mobile_number"));
        private IWebElement createAccountButton => _driver.FindElement(By.CssSelector("button[data-qa='create-account']"));

        private IWebElement messageCreateAccount => _driver.FindElement(By.XPath("//h2/b[text()='Account Created!']"));

        //Método que recibe el nombre y el email como parámetros.
        public void InputSignup(string name, string email)
        {
            nameSignupInput.SendKeys(name);
            emailSignupInput.SendKeys(email);
        }

        //Método que hace clic en el botón de registro.
        public void ClickSignupButton()
        {
            signupButton.Click();
        }

        //Método para verificar que el usuario ingreso los datos correctamente, y valida que esta en la página de registro
        public string MessageSignup()
        {
            _wait.Until(d => d.Url.Contains("signup"));

            // Aquí se busca el mensaje "Enter Account Information" para verificar que se ha llegado a la página de registro después de hacer clic en el botón de registro.
            var message = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2/b[text()='Enter Account Information']")));
            
            return message.Text;
        }

        //Método para seleccionar Mr. o Mrs.
        public void TitleSignup()
        {
            titleSignup.Click();
        }

        //Método para ingresar contraseña
        public void PasswordSignup(string password)
        {
            passwordSignup.Clear();
            passwordSignup.SendKeys(password);
        }

        //Método para primer nombre
        public void FirstNameSignup(string firstName)
        {
            firstNameSignup.Clear();
            firstNameSignup.SendKeys(firstName);
        }

        //Método para apellido
        public void LastNameSignup(string lastName)
        {
            lastNameSignup.Clear();
            lastNameSignup.SendKeys(lastName);
        }

        //Método para dirección
        public void AddressSignup(string address)
        {
            AddresSignup.Clear();
            AddresSignup.SendKeys(address);
        }

        //Método para seleccionar el país
        public void CountrySignup(string country)
        {
            var selectElement = new SelectElement(countrySignup);
            selectElement.SelectByText(country);
        }

        //Método para ingresar el estado
        public void StateSignup(string state)
        {
            stateSignup.Clear();
            stateSignup.SendKeys(state);
        }

        //Método para ingresar Ciudad
        public void CitySignup(string city)
        {
            citySignup.Clear();
            citySignup.SendKeys(city);

        }

        //Método para ingresar el código postal
        public void ZipCodeSignup(string zipCode)
        {
            zipCodeSignup.Clear();
            zipCodeSignup.SendKeys(zipCode);
        }

        //Método para ingresar Télefono
        public void MobileSignup(string mobile)
        {
            mobileSignup.Clear();
            mobileSignup.SendKeys(mobile);
        }

        //Método para hacer clic en el botón de crear cuenta
        public void CreateAccountButton()
        {
            createAccountButton.Click();
        }

        //Método para verificar que el mensaje de cuenta creada es visible y correcto
        public string MessageAccountCreated()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var message = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2/b[text()='Account Created!']")));
            return message.Text;
        }
    }
}
