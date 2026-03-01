using OpenQA.Selenium;
using System.IO;

namespace AutomationPracticeDemo.Tests.Utils
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(IWebDriver driver, string fileName)
        {
            var screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
            if (!Directory.Exists(screenshotsDir))
            {
                Directory.CreateDirectory(screenshotsDir);
            }

            var filePath = Path.Combine(screenshotsDir, fileName);

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            // Use byte array to avoid depending on ScreenshotImageFormat symbol
            File.WriteAllBytes(filePath, screenshot.AsByteArray);
        }
    }
}


