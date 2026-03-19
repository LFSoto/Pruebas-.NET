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
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public LoginPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }
        private IWebElement emailLogin => _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[data-qa='login-email']")));
        private IWebElement passwordLogin => _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[data-qa='login-password']")));
        private IWebElement loginButton => _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[data-qa='login-button']")));
        private IWebElement message => _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[.='Your email or password is incorrect!']")));

        public void InputLogin(string email, string password)
        {
            emailLogin.SendKeys(email);
            passwordLogin.SendKeys(password);
        }

        public void ClickLoginButton()
        {
            loginButton.Click();
        }
        public string MessageInvalidLogin()
        {
            //var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            //var message = _driver.FindElement(By.XPath("//p[.='Your email or password is incorrect!']"));
            return message.Text;
        }


    }
}