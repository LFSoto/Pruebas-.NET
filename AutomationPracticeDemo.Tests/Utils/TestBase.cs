using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");

            Driver = new ChromeDriver(options);

            // Espera implícita corta (preferir waits explícitos en los PageObjects)
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            // Entrar siempre por la home para que el menú (logout, login, etc.) exista de forma consistente
            Driver.Navigate().GoToUrl("https://automationexercise.com/");
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
