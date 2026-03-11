using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Utils;

public class TestBase
{
    protected IWebDriver Driver;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--remote-allow-origins=*");

        var projectPath = GetPathFromProject();
        var candidates = new[]
        {
            Path.Combine(projectPath, "Drivers"),
            Path.Combine(AppContext.BaseDirectory, "Drivers")
        };

        try
        {
            var found = FindChromeDriverExecutable(candidates);

            if (found != null)
            {
                var (dir, fileName) = found.Value;
                TestContext.WriteLine($"Usando ChromeDriver desde: {Path.Combine(dir, fileName)}");
                var service = ChromeDriverService.CreateDefaultService(dir, fileName);
                Driver = new ChromeDriver(service, options);
            }
            else
            {
                TestContext.WriteLine("No se encontró chromedriver en las carpetas buscadas. Usando constructor por defecto (PATH/NuGet).");
                Driver = new ChromeDriver(options);
            }

            // No navegar por defecto; los tests manejan su propia navegación
            var wait = new WebDriverWait(new SystemClock(), Driver, TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(500));
            wait.Until(drv => ((IJavaScriptExecutor)drv).ExecuteScript("return document.readyState").Equals("complete"));
        }
        catch (Exception ex)
        {
            TestContext.WriteLine("Error al inicializar ChromeDriver o navegar a la página: " + ex);
            try { Driver?.Quit(); } catch { }
            try { Driver?.Dispose(); } catch { }
            throw;
        }
    }

    [TearDown]
    public void TearDown()
    {
        // Ensure we log out to leave a clean state for the next test
        try
        {
            EnsureLoggedOut();
        }
        catch (Exception ex)
        {
            TestContext.WriteLine("Error during EnsureLoggedOut: " + ex);
        }

        if (Driver != null)
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }

    /// <summary>
    /// Intenta cerrar la sesión si el enlace "Logout" está presente.
    /// Esta función es segura y captura cualquier excepción para no romper el TearDown.
    /// </summary>
    protected void EnsureLoggedOut()
    {
        if (Driver == null)
            return;

        try
        {
            // Buscar enlaces que puedan indicar sesión iniciada
            var possibleLogoutXpaths = new[]
            {
                "//a[contains(., 'Logout')]",
                "//a[contains(., 'Log Out')]",
                "//a[contains(., 'LOGOUT')]",
                "//a[contains(., 'Log out')]"
            };

            foreach (var xpath in possibleLogoutXpaths)
            {
                var els = Driver.FindElements(By.XPath(xpath));
                if (els != null && els.Count > 0)
                {
                    try
                    {
                        // click the first visible logout link
                        var el = els.FirstOrDefault(e => e.Displayed);
                        if (el != null)
                        {
                            try { el.Click(); }
                            catch { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", el); }

                            // wait briefly for logout to complete
                            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
                            wait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Signup / Login')]"))?.Count > 0);
                        }
                    }
                    catch (Exception inner)
                    {
                        TestContext.WriteLine($"Warning: fallo al intentar hacer logout con xpath '{xpath}': {inner.Message}");
                    }

                    // If we attempted logout, stop checking other xpaths
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            TestContext.WriteLine("EnsureLoggedOut caught exception: " + ex);
        }
    }

    /// <summary>
    /// Genera una fecha aleatoria y la devuelve como cadena con el formato especificado.
    /// </summary>
    public static string GenerateRandomDateString(string formatoFecha)
    {
        var daysOffset = Random.Shared.Next(0, 10 * 365);
        var randomDate = DateTime.Today.AddDays(daysOffset);
        return randomDate.ToString(formatoFecha);
    }

    private static string GetPathFromProject()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            if (dir.GetFiles("*.csproj", SearchOption.TopDirectoryOnly).Length > 0)
                return dir.FullName;
            dir = dir.Parent;
        }

        return AppContext.BaseDirectory;
    }

    private static (string dir, string fileName)? FindChromeDriverExecutable(string[] candidateDirectories)
    {
        foreach (var dir in candidateDirectories)
        {
            try
            {
                if (!Directory.Exists(dir))
                    continue;

                var files = Directory.GetFiles(dir, "chromedriver*", SearchOption.AllDirectories);
                var exe = files.FirstOrDefault(f => Path.GetExtension(f).Equals(".exe", StringComparison.OrdinalIgnoreCase));
                var any = exe ?? files.FirstOrDefault();

                if (!string.IsNullOrEmpty(any))
                {
                    var fileName = Path.GetFileName(any);
                    var fileDir = Path.GetDirectoryName(any) ?? dir;
                    return (fileDir, fileName);
                }
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error buscando chromedriver en '{dir}': {ex.Message}");
            }
        }

        return null;
    }
}
