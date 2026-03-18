using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.Pages;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;



namespace AutomationPracticeDemo.Tests.Tests
{
    public class LoginTests
    {
        protected IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--disable-popup-blocking");
            Driver = new ChromeDriver(options);
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Navigate().GoToUrl("https://automationexercise.com/login");
            Assert.That(Driver.Url.Contains("/login"));
    
        }

        [TearDown]
        public void Teardown()
        {
            if (Driver != null)
            {
                Driver.Quit();    // Cierra la sesión del navegador
                Driver.Dispose(); // Libera los recursos del WebDriver
            }
        }

        [Test]
        [TestCaseSource(nameof(GetLoginData))]
        public void LoginTest(string user, string pass)
        {
            var loginPage = new LoginPage(Driver);
            loginPage.EnterUsername(user);
            loginPage.EnterPassword(pass);
            loginPage.ClickLogin();

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            var logoutLink = wait.Until(d => d.FindElement(By.CssSelector("a[href='/logout']")));
            Assert.That(logoutLink.Displayed, Is.True);
        }

        public static IEnumerable<TestCaseData> GetLoginData()
        {
            var json = File.ReadAllText("Data/LoginData.json");
            var data = JArray.Parse(json);

            foreach (var item in data)
            {
                yield return new TestCaseData(
                    item["username"].ToString(),
                    item["password"].ToString()
                );
            }
        }
    }
}
