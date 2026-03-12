using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class RegistroPage
    {
        private readonly IWebDriver _driver;
        public RegistroPage(IWebDriver driver)
        {
            _driver = driver;
        }


        private IWebElement BtnLogin => _driver.FindElement(By.XPath("//a[@href='/login']"));

        private IWebElement Nameinput => _driver.FindElement(By.XPath("//input[@data-qa='signup-name']"));

        private IWebElement Email => _driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));
        private IWebElement BtnSignup => _driver.FindElement(By.XPath("//button[@data-qa='signup-button']"));
        private IWebElement Password => _driver.FindElement(By.XPath("//input[@data-qa='password']"));
        private IWebElement FirstName => _driver.FindElement(By.XPath("//input[@data-qa='first_name']"));
        
        private IWebElement LastName => _driver.FindElement(By.XPath("//input[@data-qa='last_name']"));

        private IWebElement Address => _driver.FindElement(By.XPath("//input[@data-qa='address']"));

        private IWebElement countryD => _driver.FindElement(By.Id("country"));

        private IWebElement State => _driver.FindElement(By.XPath("//input[@data-qa='state']"));
            
        private IWebElement City => _driver.FindElement(By.XPath("//input[@data-qa='city']"));

        private IWebElement ZipCode => _driver.FindElement(By.XPath("//input[@data-qa='zipcode']"));
        private IWebElement MobileNumber => _driver.FindElement(By.XPath("//input[@data-qa='mobile_number']"));
        private IWebElement BtnCreateAccount => _driver.FindElement(By.XPath("//button[@data-qa='create-account']"));
       
        private IWebElement MessageAccountCreated => _driver.FindElement(By.XPath("//b[normalize-space(text())='Account Created!']"));
         
        private IWebElement BtnContinue => _driver.FindElement(By.XPath("//a[normalize-space(text())='Continue']"));

        private IWebElement LogoutOption => _driver.FindElement(By.XPath("//a[@href='/logout']"));

        private IList<IWebElement> genderRadioButtons => _driver.FindElements(By.CssSelector("div.radio-inline"));

        private IWebElement daysDropdown => _driver.FindElement(By.Id("days"));
        private IWebElement monthsDropdown => _driver.FindElement(By.Id("months"));
        private IWebElement yearsDropdown => _driver.FindElement(By.Id("years"));

        private IWebElement companyInput => _driver.FindElement(By.Id("company"));

        private IWebElement address2Input => _driver.FindElement(By.Id("address2"));

        private IWebElement nameForm => _driver.FindElement(By.Id("name"));

        public  void ClickLogin()
        {
            BtnLogin.Click();
        }
        public void filllogin(string Name, string email)
            {


            Nameinput.SendKeys(Name);
         
            Email.SendKeys(email);
           
        }
        public void ClickSignup()
        {
            BtnSignup.Click();
        }
        public void FillAccountInformation(string Name, string title, string password, string day, string mes, string year)
        {
            nameForm.Clear();
            nameForm.SendKeys(Name);
            if (title.ToLower() == "mr")
            {
                genderRadioButtons[0].Click();
            }
            else
            {
                genderRadioButtons[1].Click();
            }
            Password.SendKeys(password);
            daysDropdown.SendKeys(day);
            monthsDropdown.SendKeys(mes);
            yearsDropdown.SendKeys(year);
        }

        public void FillAddressInformation(string firstName, string lastName,string compania, string address,string address2, string country, string state, string city, string zipCode, string mobileNumber)
        {
            FirstName.SendKeys(firstName);
            LastName.SendKeys(lastName);
            companyInput.SendKeys(compania);
            Address.SendKeys(address);
            address2Input.SendKeys(address2);
            countryD.SendKeys(country);
            State.SendKeys(state);
            City.SendKeys(city);
            ZipCode.SendKeys(zipCode);
            MobileNumber.SendKeys(mobileNumber);
        }

        public void ClickCreateAccount()
        {
            BtnCreateAccount.Click();
        }
        public string messagingCreateAccount()
        {
            if (MessageAccountCreated.Displayed == false)
            {
                throw new NoSuchElementException("El mensaje 'Account Created!' no está visible en la pantalla.");
            }

            return MessageAccountCreated.Text;

        }
        public void ClickContinue()
        {
            BtnContinue.Click();
        }

            public string validatedUserLogout()
            {
                if (LogoutOption.Displayed == false)
                {
                    throw new NoSuchElementException("El elemento Logout no está visible en la página.");
                }
                return LogoutOption.Text;
        }




     

    }
}
