using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using System.Drawing;
using System.Runtime.InteropServices;

namespace AutomationPracticeDemo.Tests.Utils
{
    public static class ScreenshotHelper
    {
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        public static void TakeScreenshot(IWebDriver driver, string fileName, bool alert = false)
        {
            var rutaFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\", "Evidencias");
            if (!Directory.Exists(rutaFolder))
                Directory.CreateDirectory(rutaFolder);

            var ruta = Path.Combine(rutaFolder, fileName);
            if (!alert) { 
                // Si no hay alerta, usar el método estándar de Selenium
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(ruta);
            } else {
                int width = GetSystemMetrics(0);  // SM_CXSCREEN
                int height = GetSystemMetrics(1); // SM_CYSCREEN

                Rectangle bounds = new Rectangle(0, 0, width, height);

                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                    }
                    bitmap.Save(ruta);
                }
            }
        }
    }
}
