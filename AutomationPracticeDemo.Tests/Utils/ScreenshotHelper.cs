using AutomationPracticeDemoTest.Utils;
using OpenQA.Selenium;

namespace AutomationPracticeDemoTests.Utils;

/// <summary>
/// Utilidades para tomar y guardar capturas de pantalla en pruebas automatizadas.
/// </summary>
public static class ScreenshotHelper
{
    /// <summary>
    /// Toma una captura de pantalla del navegador y la guarda en la carpeta `Reportes/Images` del proyecto.
    /// </summary>
    /// <param name="driver">Instancia de `IWebDriver` usada para capturar la pantalla.</param>
    /// <param name="fileName">Nombre del archivo destino. Si no incluye extensión se usará `.png`.</param>
    public static void TakeScreenshot(IWebDriver driver, string fileName, string folderName = "")
    {
        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

        // Ensure only filename is used and has an extension
        var safeFileName = Path.GetFileName(fileName) ?? "screenshot.png";
        if (string.IsNullOrWhiteSpace(Path.GetExtension(safeFileName)))
            safeFileName = Path.ChangeExtension(safeFileName, ".png");

        // Build images directory under project: <projectRoot>/Reporte/Images
        var projectPath = AutomationUtils.GetPathFromProject();

        var imagesDir = Path.Combine(projectPath, "Reportes", "Images");
        if (!string.IsNullOrWhiteSpace(folderName))
        {
            imagesDir = Path.Combine(imagesDir, folderName.Trim());
        }
        
        Directory.CreateDirectory(imagesDir);

        var fullPath = Path.Combine(imagesDir, safeFileName);
        screenshot.SaveAsFile(fullPath);
    }
}
