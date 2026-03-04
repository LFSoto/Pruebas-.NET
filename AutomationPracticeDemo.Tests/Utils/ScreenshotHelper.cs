using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Utils
{
    public static class ScreenshotHelper
    {    /// <summary>
         /// Toma una captura de pantalla del navegador y la guarda en la carpeta `Reportes/Images` del proyecto.
         /// </summary>
         /// <param name="driver">Instancia de `IWebDriver` usada para capturar la pantalla.</param>
         /// <param name="fileName">Nombre del archivo destino. Si no incluye extensión se usará `.png`.</param>
        public static void TakeScreenshot(IWebDriver driver, string fileName)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            // Ensure only filename is used and has an extension
            var safeFileName = Path.GetFileName(fileName) ?? "screenshot.png";
            if (string.IsNullOrWhiteSpace(Path.GetExtension(safeFileName)))
                safeFileName = Path.ChangeExtension(safeFileName, ".png");

            // Build images directory under project: <projectRoot>/Reporte/Images
            var projectPath = GetPathFromProject();
            var imagesDir = Path.Combine(projectPath, "Reportes", "Images");
            Directory.CreateDirectory(imagesDir);

            var fullPath = Path.Combine(imagesDir, safeFileName);
            screenshot.SaveAsFile(fullPath);
        }


        /// <summary>
        /// Busca el directorio raíz del proyecto ascendiendo desde <c>AppContext.BaseDirectory</c>
        /// hasta encontrar un archivo <c>*.csproj</c>.
        /// </summary>
        /// <returns>Ruta completa del directorio del proyecto o <c>AppContext.BaseDirectory</c> si no se encuentra.</returns>
        public static string GetPathFromProject()
        {
            // Start from the test assembly base directory and walk up until a .csproj is found
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                if (dir.GetFiles("*.csproj", SearchOption.TopDirectoryOnly).Any())
                    return dir.FullName;
                dir = dir.Parent;
            }

            // Fallback to the base directory if no project file was found
            return AppContext.BaseDirectory;
        }
    }
}
