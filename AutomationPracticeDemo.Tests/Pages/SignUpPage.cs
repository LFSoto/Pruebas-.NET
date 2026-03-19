using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class SignUpPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public SignUpPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        // Declaración de los elementos de la página
        // Elementos del SignUp
        private IWebElement titleNewUserSignup => _driver.FindElement(By.CssSelector("div.signup-form h2"));
        private IWebElement nameSignupInput => _driver.FindElement(By.CssSelector("input[data-qa=\"signup-name\"]"));
        private IWebElement emaiSignuplInput => _driver.FindElement(By.CssSelector("input[data-qa=\"signup-email\"]"));
        private IWebElement signupButton => _driver.FindElement(By.CssSelector("button[data-qa=\"signup-button\"]"));

        private IWebElement enterAccountInfoTitle => _driver.FindElement(By.CssSelector("div.login-form >h2"));
        private IWebElement nameInput => _driver.FindElement(By.Id("name"));
        private IWebElement emailInput => _driver.FindElement(By.Id("email"));
        private IList<IWebElement> genderRadioButtons => _driver.FindElements(By.CssSelector("div.radio-inline"));
        private IWebElement passwordInput => _driver.FindElement(By.Id("password"));
        private IWebElement daysDropdown => _driver.FindElement(By.Id("days"));
        private IWebElement monthsDropdown => _driver.FindElement(By.Id("months"));
        private IWebElement yearsDropdown => _driver.FindElement(By.Id("years"));
        private IWebElement newsletterCheckbox => _driver.FindElement(By.Id("newsletter"));
        private IWebElement firstNameInput => _driver.FindElement(By.Id("first_name"));
        private IWebElement lastNameInput => _driver.FindElement(By.Id("last_name"));
        private IWebElement companyInput => _driver.FindElement(By.Id("company"));
        private IWebElement address1Input => _driver.FindElement(By.Id("address1"));
        private IWebElement address2Input => _driver.FindElement(By.Id("address2"));
        private IWebElement countryDropdown => _driver.FindElement(By.Id("country"));
        private IWebElement stateInput => _driver.FindElement(By.Id("state"));
        private IWebElement cityInput => _driver.FindElement(By.Id("city"));
        private IWebElement zipcodeInput => _driver.FindElement(By.Id("zipcode"));
        private IWebElement mobileInput => _driver.FindElement(By.Id("mobile_number"));
        private IWebElement createAccountButton => _driver.FindElement(By.CssSelector("button[data-qa=\"create-account\"]"));
        private IWebElement accountCreatedTitle => _driver.FindElement(By.CssSelector("h2[data-qa=\"account-created\"] b"));
        private IList<IWebElement> accountCreatedMessage => _driver.FindElements(By.CssSelector("#form > div > div > div > p"));
        private IWebElement continueButton => _driver.FindElement(By.CssSelector("a[data-qa=\"continue-button\"]"));

        private static By EnterAccountInfoTitleBy => By.CssSelector("div.login-form >h2");

        private IWebElement WaitVisible(By by) => _wait.Until(d =>
        {
            try
            {
                var el = d.FindElement(by);
                return el.Displayed ? el : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }
        });

        // Métodos para interactuar con los elementos del SignUp
        public string GetTitleNewUserSignup()
        {
            if (titleNewUserSignup.Displayed == false)
            {
                throw new NoSuchElementException("El elemento no está visible en la página.");
            }
            return titleNewUserSignup.Text;
        }
        public void FillSignup(string name, string email)
        {
            nameSignupInput.SendKeys(name);
            emaiSignuplInput.SendKeys(email);
        }
        public void SubmitSignup()
        {
            signupButton.Click();
        }

        //Metodo para obetener el titulo de Enter Account Information
        public string GetTitleEnterAccountInfo()
        {
            // Retry a few times in case the DOM is re-rendered.
            for (var attempt = 0; attempt < 3; attempt++)
            {
                try
                {
                    var el = WaitVisible(EnterAccountInfoTitleBy);
                    return el.Text;
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(250);
                }
            }

            // Final attempt (will throw if not present)
            return WaitVisible(EnterAccountInfoTitleBy).Text;
        }

        //Metodo para llenar el formulario de Account Information
        public void FillAccountInformation(string name, string title, string password, string day, string month, string year)
        {
            nameInput.Clear();
            passwordInput.Clear();
            nameInput.SendKeys(name);

            if (title.ToLower() == "mr")
            {
                genderRadioButtons[0].Click();
            }
            else
            {
                genderRadioButtons[1].Click();
            }
            passwordInput.SendKeys(password);
            daysDropdown.SendKeys(day);
            monthsDropdown.SendKeys(month);
            yearsDropdown.SendKeys(year);
            newsletterCheckbox.Click();
        }

        //Metodo para obtener el texto del campo email
        public string ShouldValidatedTextEmailInput()
        {
            string? expectedEmail = emailInput.GetAttribute("value");
            if (string.IsNullOrEmpty(expectedEmail))
            {
                throw new NoSuchElementException("El campo de email está vacío.");
            }
            return expectedEmail;
        }

        //Metodo para llenar el formulario de Address Information
        public void FillAddressInformation(string firstName, string lastName, string company, string address1, string address2, string country, string state, string city, string zipcode, string mobile)
        {
            // Limpiar los campos antes de llenarlos
            firstNameInput.Clear();
            lastNameInput.Clear();
            companyInput.Clear();
            address1Input.Clear();
            address2Input.Clear();
            stateInput.Clear();
            cityInput.Clear();
            zipcodeInput.Clear();
            mobileInput.Clear();
            // Llenar los campos con la información proporcionada
            firstNameInput.SendKeys(firstName);
            lastNameInput.SendKeys(lastName);
            companyInput.SendKeys(company);
            address1Input.SendKeys(address1);
            address2Input.SendKeys(address2);
            countryDropdown.SendKeys(country);
            stateInput.SendKeys(state);
            cityInput.SendKeys(city);
            zipcodeInput.SendKeys(zipcode);
            mobileInput.SendKeys(mobile);
        }

        //Metodo para enviar el formulario de creación de cuenta
        public void SubmitCreateAccount()
        {
            createAccountButton.Click();
        }

        //Metodo para obtener el título y mensaje de cuenta creada
        public string GetAccountCreatedTitle()
        {
            // Wait for the account-created title to appear after submit.
            var el = WaitVisible(By.CssSelector("h2[data-qa=\"account-created\"] b"));
            return el.Text;
        }

        //Metodo para obtener el mensaje de cuenta creada
        public string GetAccountCreatedMessage()
        {
            string messageText = string.Empty;
            foreach (var element in accountCreatedMessage)
            {
                if (element.Displayed)
                {
                    messageText = messageText + " " + element.Text;
                }
            }
            return messageText;
        }

        public void SumitContinue()
        {
            continueButton.Click();
        }
    }
}
