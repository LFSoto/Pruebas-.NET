using System;
using AutomationPracticeDemo.Tests.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class RegisterPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Método seguro para encontrar elementos
        private IWebElement FindElement(By locator)
        {
            driver.SwitchTo().DefaultContent(); // salir de iframes/publicidad
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        // --- Account Information ---
        private IWebElement TitleMr => FindElement(By.Id("id_gender1"));
        private IWebElement TitleMrs => FindElement(By.Id("id_gender2"));
        private IWebElement Name => FindElement(By.Id("name"));
        private IWebElement Email => FindElement(By.Id("email"));
        private IWebElement Password => FindElement(By.Id("password"));
        private IWebElement DateOfBirthDay => FindElement(By.Id("days"));
        private IWebElement DateOfBirthMonth => FindElement(By.Id("months"));
        private IWebElement DateOfBirthYear => FindElement(By.Id("years"));

        // --- Address Information ---
        private IWebElement FirstName => FindElement(By.Id("first_name"));
        private IWebElement LastName => FindElement(By.Id("last_name"));
        private IWebElement Company => FindElement(By.Id("company"));
        private IWebElement Address => FindElement(By.Id("address1"));
        private IWebElement Address2 => FindElement(By.Id("address2"));
        private IWebElement Country => FindElement(By.Id("country"));
        private IWebElement State => FindElement(By.Id("state"));
        private IWebElement City => FindElement(By.Id("city"));
        private IWebElement Zipcode => FindElement(By.Id("zipcode"));
        private IWebElement MobileNumber => FindElement(By.Id("mobile_number"));

        // --- Botón de registro ---
        private IWebElement SubmitButton => FindElement(By.CssSelector("button[data-qa='create-account']"));


        // --- Métodos ---
        public void CloseAdsIfPresent()
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                var adFrames = driver.FindElements(By.CssSelector("iframe[id^='aswift']"));
                foreach (var frame in adFrames)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", frame);
                }
            }
            catch (Exception)
            {
                // Ignora si no hay iframes de publicidad
            }
        }
           
            

        public void SelectTitle(string gender)
        {
            driver.SwitchTo().DefaultContent();

            if (gender == "Mrs")
                TitleMrs.Click();
            else
                TitleMr.Click();
        }

        public void FillAccountInfo(AccountInformation acc)
        {
            SelectTitle(acc.Title);

            //Name.SendKeys(acc.Name);
            //Email.SendKeys(acc.Email);
            Password.SendKeys(acc.Password);

            new SelectElement(DateOfBirthDay).SelectByValue(acc.Day);
            new SelectElement(DateOfBirthMonth).SelectByValue(acc.Month);
            new SelectElement(DateOfBirthYear).SelectByValue(acc.Year);
        }

        public void FillAddressInfo(AddressInformation addr)
        {
            FirstName.SendKeys(addr.FirstName);
            LastName.SendKeys(addr.LastName);
            Company.SendKeys(addr.Company);
            Address.SendKeys(addr.Address);
            Address2.SendKeys(addr.Address2);
            new SelectElement(Country).SelectByText(addr.Country);
            State.SendKeys(addr.State);
            City.SendKeys(addr.City);
            Zipcode.SendKeys(addr.Zipcode);
            MobileNumber.SendKeys(addr.MobileNumber);
        }

        public void Submit()
        {
            driver.SwitchTo().DefaultContent();
            var button = SubmitButton;
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", button);

        }
    }
}