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

            // CI-friendly defaults (GitHub Actions uses Linux containers/VMs)
            options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");

            // Optional for local runs; harmless in CI
            options.AddArgument("--start-maximized");

            Driver = new ChromeDriver(options);

            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);

            // Prefer explicit waits in page objects to avoid flaky implicit-wait interactions
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

            Driver.Navigate().GoToUrl("https://automationexercise.com/signup");
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
