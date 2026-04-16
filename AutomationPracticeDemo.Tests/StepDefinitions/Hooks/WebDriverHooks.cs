using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationPracticeDemo.Tests.StepDefinitions.Hooks
{
    [Binding]
    public sealed class WebDriverHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public WebDriverHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            // Skip browser setup for API scenarios
            if (_scenarioContext.ScenarioInfo.Tags.Contains("api"))
                return;

            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");
            options.AddArgument("headless");

            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://automationexercise.com/");
            _scenarioContext.Set<IWebDriver>(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.TryGetValue<IWebDriver>(out var driver))
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
