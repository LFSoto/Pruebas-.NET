using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace AutomationPracticeDemo.Tests.Utils
{
    public class TestBase
    {
        protected IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            // Try to use chromedriver from Drivers folder in output if present
            var driversDir = Path.Combine(AppContext.BaseDirectory, "Drivers");
            ChromeDriverService service;
            if (Directory.Exists(driversDir))
            {
                service = ChromeDriverService.CreateDefaultService(driversDir);
            }
            else
            {
                // fallback to default service (PATH or Selenium.WebDriver.ChromeDriver NuGet)
                service = ChromeDriverService.CreateDefaultService();
            }

            Driver = new ChromeDriver(service, options);
            Driver.Navigate().GoToUrl("https://testautomationpractice.blogspot.com/");
        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
            }
        }
    }
}
