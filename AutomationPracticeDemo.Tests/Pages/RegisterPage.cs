using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class RegisterPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public RegisterPage(IWebDriver driver, TimeSpan timeout)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, timeout);
        }

        public void GoTo()
        {
            _driver.Navigate().GoToUrl("https://automationexercise.com/");
            _wait.Until(d => d.FindElement(By.XPath("//a[contains(., 'Signup / Login')]") ));
        }

        public void Register(RegisterData data)
        {
            // Start signup
            var signupLink = _wait.Until(d => d.FindElement(By.XPath("//a[contains(., 'Signup / Login')]") ));
            signupLink.Click();

            _wait.Until(d => d.FindElement(By.Name("name")));
            var nameInput = _driver.FindElement(By.Name("name"));
            var emailInput = _driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));

            // If email provided is empty, generate one
            var emailToUse = string.IsNullOrEmpty(data.email) ? $"reg{DateTime.UtcNow.Ticks}@example.com" : data.email;

            nameInput.Clear(); nameInput.SendKeys(data.name ?? "automation_user");
            emailInput.Clear(); emailInput.SendKeys(emailToUse);
            _driver.FindElement(By.XPath("//button[@data-qa='signup-button']")).Click();

            // Fill account details
            _wait.Until(d => d.FindElement(By.Id("id_gender1")));
            try { _driver.FindElement(By.Id("id_gender1")).Click(); } catch { }

            _driver.FindElement(By.Id("password")).SendKeys(data.password ?? "Password123!");

            // DOB default
            try
            {
                new SelectElement(_driver.FindElement(By.Id("days"))).SelectByValue("10");
                new SelectElement(_driver.FindElement(By.Id("months"))).SelectByValue("5");
                new SelectElement(_driver.FindElement(By.Id("years"))).SelectByValue("1990");
            }
            catch { }

            // newsletter and optin
            try
            {
                var newsletter = _driver.FindElement(By.Id("newsletter")); if (!newsletter.Selected) newsletter.Click();
            }
            catch { }
            try
            {
                var optin = _driver.FindElement(By.Id("optin")); if (!optin.Selected) optin.Click();
            }
            catch { }

            // Personal info
            try { _driver.FindElement(By.Id("first_name")).SendKeys(data.first_name ?? "Test"); } catch { }
            try { _driver.FindElement(By.Id("last_name")).SendKeys(data.last_name ?? "User"); } catch { }
            try { _driver.FindElement(By.Id("company")).SendKeys(data.company ?? "MyCompany"); } catch { }
            try { _driver.FindElement(By.Id("address1")).SendKeys(data.address1 ?? "123 Main St"); } catch { }
            try { _driver.FindElement(By.Id("address2")).SendKeys(data.address2 ?? "Apt 1"); } catch { }

            // Country selection with fallback
            try
            {
                var countrySelect = new SelectElement(_driver.FindElement(By.Id("country")));
                if (!string.IsNullOrEmpty(data.country)) countrySelect.SelectByText(data.country);
            }
            catch
            {
                try { var countryEl = _driver.FindElement(By.Id("country")); countryEl.Clear(); if (!string.IsNullOrEmpty(data.country)) countryEl.SendKeys(data.country); } catch { }
            }

            try { _driver.FindElement(By.Id("state")).SendKeys(data.state ?? "CA"); } catch { }
            try { _driver.FindElement(By.Id("city")).SendKeys(data.city ?? "San Francisco"); } catch { }
            try { _driver.FindElement(By.Id("zipcode")).SendKeys(data.zipcode ?? "94101"); } catch { }
            try { _driver.FindElement(By.Id("mobile_number")).SendKeys(data.mobile_number ?? "+11234567890"); } catch { }

            // Create account
            try
            {
                var createBtn = _driver.FindElement(By.CssSelector("button[data-qa='create-account']"));
                try { createBtn.Click(); } catch { ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", createBtn); }
            }
            catch { }

            _wait.Until(d => d.FindElements(By.XPath("//h2[contains(., 'Account Created')]")).Count > 0);
        }
    }
}
