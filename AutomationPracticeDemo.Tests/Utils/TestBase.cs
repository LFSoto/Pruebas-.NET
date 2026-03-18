using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Utils
{
    public class TestBase
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        [SetUp]
        public void Setup()
        {

            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");

            Driver = new ChromeDriver(options);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            
            Driver.Navigate().GoToUrl("https://automationexercise.com/");

            Driver.Manage().Cookies.DeleteAllCookies();

            Driver.Navigate().Refresh();

        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                Driver.Manage().Cookies.DeleteAllCookies();
                Driver.Quit();
                Driver.Dispose();
            }
        }
    }
}
