using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutomationExercise.Tests.Utils;

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
                TestContext.WriteLine("No se encontró chromedriver en las carpetas buscadas. Usando constructor por defecto (PATH/NuGet).);
                Driver = new ChromeDriver(options);
            }

            Driver.Navigate().GoToUrl("https://automationexercise.com/");

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
        if (Driver != null)
        {
            Driver.Quit();
            Driver.Dispose();
        }
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
