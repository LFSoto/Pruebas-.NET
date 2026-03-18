using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class SignUpPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public SignUpPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        // --- New User Signup ---
        // --- New User Signup ---
        private IWebElement nameSignupInput =>
            _wait.Until(d => d.FindElement(By.CssSelector("input[data-qa='signup-name']")));

        private IWebElement emailSignupInput =>
            _wait.Until(d => d.FindElement(By.CssSelector("input[data-qa='signup-email']")));

        private IWebElement signupButton =>
            _wait.Until(d => d.FindElement(By.CssSelector("button[data-qa='signup-button']")));

        // --- Account Information ---
        private IWebElement genderMr => _driver.FindElement(By.Id("id_gender1"));
        private IWebElement genderMrs => _driver.FindElement(By.Id("id_gender2"));
        private IWebElement passwordInput => _driver.FindElement(By.Id("password"));
        private IWebElement daysDropdown => _driver.FindElement(By.Id("days"));
        private IWebElement monthsDropdown => _driver.FindElement(By.Id("months"));
        private IWebElement yearsDropdown => _driver.FindElement(By.Id("years"));
        private IWebElement newsletterCheckbox => _driver.FindElement(By.Id("newsletter"));

        // --- Address Information ---
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
        private IWebElement createAccountButton => _driver.FindElement(By.CssSelector("button[data-qa='create-account']"));
        private IWebElement accountCreatedMessage =>
             _wait.Until(d => d.FindElement(By.XPath("//b[text()='Account Created!']")));

        private IWebElement continueButton =>
             _wait.Until(d => d.FindElement(By.CssSelector("a[data-qa='continue-button']")));

        // --- Métodos ---
        public void FillSignup(string name, string email)
        {
            nameSignupInput.SendKeys(name);
            emailSignupInput.SendKeys(email);
            signupButton.Click();
        }

        public void FillAccountInformation(string title, string name, string password, string day, string month, string year)
        {
            if (title.ToLower() == "mr")
                genderMr.Click();
            else
                genderMrs.Click();

            passwordInput.SendKeys(password);

            new SelectElement(daysDropdown).SelectByValue(day);
            new SelectElement(monthsDropdown).SelectByText(month);
            new SelectElement(yearsDropdown).SelectByValue(year);

            newsletterCheckbox.Click();
        }

        public void FillAddressInformation(string firstName, string lastName, string company, string address1, string address2,
                                           string country, string state, string city, string zipcode, string mobile)
        {
            firstNameInput.SendKeys(firstName);
            lastNameInput.SendKeys(lastName);
            companyInput.SendKeys(company);
            address1Input.SendKeys(address1);
            address2Input.SendKeys(address2);
            new SelectElement(countryDropdown).SelectByText(country);
            stateInput.SendKeys(state);
            cityInput.SendKeys(city);
            zipcodeInput.SendKeys(zipcode);
            mobileInput.SendKeys(mobile);
        }

        public void SubmitCreateAccount()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", createAccountButton);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", createAccountButton);
        }

        public bool IsAccountCreated()
        {
            return accountCreatedMessage.Displayed;
        }

        public void ClickContinue()
        {
            continueButton.Click();
        }

    }
}
