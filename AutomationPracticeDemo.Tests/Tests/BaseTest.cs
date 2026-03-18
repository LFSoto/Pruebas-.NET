using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationPracticeDemo.Tests;

public class BaseTest
{
    protected IWebDriver Driver;

    [SetUp]
    protected void SetUpDriver()
    {
        var options = new ChromeOptions();
        //options.AddArgument("--start-maximized");
        options.AddArgument("--headless=new");
        options.AddArgument("--window-size=1920,1080");
        options.AddArgument("--remote-allow-origins=*");
        Driver = new ChromeDriver(options);
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        Driver.Navigate().GoToUrl("https://automationexercise.com/");
    }

    [TearDown]
    protected void TearDownDriver()
    {
        AddScreenshotIfTestFailed();
        CloseBrowser();
    }

    private void CloseBrowser()
    {
        if (Driver != null)
        {
            try
            {
                Driver.Quit();
            }
            catch { }
            try
            {
                Driver.Dispose();
            }
            catch { }
            Driver = null!;
        }
    }

    private void AddScreenshotIfTestFailed()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var fileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            ScreenshotHelper.TakeScreenshot(Driver, fileName);
        }
    }
}
