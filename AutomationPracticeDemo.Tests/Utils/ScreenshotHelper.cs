using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Utils
{
    public static class ScreenshotHelper
    {

        public static void TakeScreenshot(IWebDriver driver, string fileName)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(fileName);
        }
    }
}
