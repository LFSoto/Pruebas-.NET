using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver Driver;

        public LoginPage(IWebDriver driver)
        {
            Driver = driver;
        }

        private IWebElement EmailInput => Driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
        private IWebElement PasswordInput => Driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
        private IWebElement LoginButton => Driver.FindElement(By.CssSelector("button[data-qa='login-button']"));

        public void GoTo()
        {
            Driver.FindElement(By.LinkText("Signup / Login")).Click();
        }

        public void Login(string email, string password)
        {
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            LoginButton.Click();
        }

        public bool IsLoggedIn()
        {
            return Driver.PageSource.Contains("Logged in as");
        }
    }

}
