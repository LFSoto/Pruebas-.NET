using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement BtnLogin => _driver.FindElement(By.XPath("//a[@href='/login']"));
        private IWebElement Email => _driver.FindElement(By.XPath("//input[@data-qa='login-email']"));
        private IWebElement Password => _driver.FindElement(By.XPath("//input[@data-qa='login-password']"));
        private IWebElement BtnLogin2 => _driver.FindElement(By.XPath("//button[@data-qa='login-button']"));
        private IWebElement logoutOption => _driver.FindElement(By.CssSelector("li a[href=\"/logout\"]"));

        private IWebElement messageIncorrectPassword => _driver.FindElement(By.CssSelector("div.login-form p"));

       
 

        public void ClickLogin()
        {
            BtnLogin.Click();
        }
        public void ingresarLogin(string email, string password)

        {
            
            Email.SendKeys(email);
            Password.SendKeys(password);
            

        }
        public void ClickLogin2()
        {
            BtnLogin2.Click();
        }

        public string GetMessageIncorrectPassword()
        {
            if (messageIncorrectPassword.Displayed == false)
            {
                throw new NoSuchElementException("El elemento no está visible en la página.");
            }
            return messageIncorrectPassword.Text;
        }
        public string validatedUserLogout()
        {
            if (logoutOption.Displayed == false)
            {
                throw new NoSuchElementException("El elemento Logout no está visible en la página.");
            }
            return logoutOption.Text;
        }
    }
    
}
