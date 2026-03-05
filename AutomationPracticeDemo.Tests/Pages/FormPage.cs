using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class FormPage
    {
        private readonly IWebDriver _driver;

        public FormPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement Signup => _driver.FindElement(By.LinkText("Signup / Login"));
        private IWebElement Name => _driver.FindElement(By.Name("name"));
        private IWebElement Email => _driver.FindElement(By.CssSelector("input[data-qa='signup-email']"));
        private IWebElement SignupButton => _driver.FindElement(By.CssSelector("button[data-qa='signup-button']"));
        private IWebElement Title => _driver.FindElement(By.Id("id_gender1"));
        private IWebElement Password => _driver.FindElement(By.Id("password"));
        private IWebElement FirstName => _driver.FindElement(By.Id("first_name"));
        private IWebElement LastName => _driver.FindElement(By.Id("last_name"));
        private IWebElement Company => _driver.FindElement(By.Id("company"));
        private IWebElement Address => _driver.FindElement(By.Id("address1"));
        private IWebElement Country => _driver.FindElement(By.Id("country"));
        private IWebElement State => _driver.FindElement(By.Id("state"));
        private IWebElement City => _driver.FindElement(By.Id("city"));
        private IWebElement ZipCode => _driver.FindElement(By.CssSelector("input[data-qa='zipcode']"));
        private IWebElement MobileNumber => _driver.FindElement(By.CssSelector("input[data-qa='mobile_number']"));
        private IWebElement btnCreateAccount => _driver.FindElement(By.CssSelector("button[data-qa='create-account']"));
        private IWebElement Message => _driver.FindElement(By.CssSelector("h2[data-qa='account-created']"));
        private IWebElement continueButton => _driver.FindElement(By.CssSelector("a[data-qa='continue-button']"));

        //HACER CLICK EN SIGNUP
        public void ClickSignup()
        {
            Signup.Click();
        }

        //COMPLETAR NOMBRE
        public void NewUser(string name, string email)
        {
            Name.SendKeys(name);
            Email.SendKeys(email);

        }

        //HACER CLICK EN SIGNUP
        public void ClickSignupButton()
        {
            SignupButton.Click();
        }

        //SELECCIONAR TITLE
        public void SelectTitle()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement titleRadio = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("id_gender1")));
            Title.Click();
        }

        //COMPLETAR CONTRASEÑA
        public void Password1(string password1)
        {
            Password.SendKeys(password1);
        }

        //COMPLETAR INFORMACIÓN
        public void CompleteInformation(string firstName, string lastName, string company, string address, string country, string state, string city, string zipCode, string MobileNumber1)
        {
            FirstName.SendKeys(firstName);
            LastName.SendKeys(lastName);
            Company.SendKeys(company);
            Address.SendKeys(address);
            Country.SendKeys(country);
            State.SendKeys(state);
            City.SendKeys(city);
            ZipCode.SendKeys(zipCode);
            MobileNumber.SendKeys(MobileNumber1);
        }

        //VALIDAR BOTÓN CREAR USUARIO
        public void ValidateCreateUserButton()
        {
            btnCreateAccount.Click();
        }

        //VALIDAR MENSAJE DE CUENTA CREADA
        public string ValidateMessage()
        {
            return Message.Text;

        }

        //VALIDAR CLIC BTN CONTINUE
         public void ValidateContinueButton()
        {
            continueButton.Click();
        }
        
        //VALIDAR QUE USUARIO ESTÁ LOGUEADO
        public bool IsUserLoggedIn()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                // Esperamos a que el elemento con el texto "Logged in as" sea visible
                var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(., 'Logged in as')]")));
                return element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false; // Si pasan 10 segundos y no aparece, devuelve false
            }
        }

    }
}
