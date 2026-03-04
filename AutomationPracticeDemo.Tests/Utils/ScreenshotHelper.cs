using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Utils
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(IWebDriver driver, string fileName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var fullPath = Path.Combine(folderPath, $"{timestamp}_{fileName}");

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(fullPath);
        }
    }
}


/*namespace AutomationPracticeDemo.Tests.Utils
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
*/ //Anterior