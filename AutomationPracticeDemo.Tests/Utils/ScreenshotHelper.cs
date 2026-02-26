using OpenQA.Selenium;
using System;
using System.IO;

namespace AutomationPracticeDemo.Tests.Utils
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(IWebDriver driver, string fileName)
        {
            var projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            var screenshotsDir = Path.Combine(projectRoot, "screenshots");
            Directory.CreateDirectory(screenshotsDir);

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            var fullPath = Path.Combine(screenshotsDir, $"{timestamp}_{fileName}.png");

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(fullPath);
        }
    }
}
