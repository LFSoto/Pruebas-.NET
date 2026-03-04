using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationPracticeDemo.Tests.Utils;

public class TestBase
{
    protected IWebDriver Driver;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        Driver = new ChromeDriver(options);
        Driver.Navigate().GoToUrl("https://testautomationpractice.blogspot.com/");
    }

    [TearDown]
    public void TearDown()
    {
        CloseBrowser();
    }

    public void CloseBrowser()
    {
        if (Driver != null)
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }

    /// <summary>
    /// Genera una fecha aleatoria y la devuelve como cadena con el formato "dd/MM/yyyy".
    /// Ejemplo: "03/10/2026".
    /// </summary>
    /// <returns>Fecha aleatoria formateada como "dd/MM/yyyy".</returns>
    public static string GenerateRandomDateString(string formatoFecha)
    {
        // Genera una fecha aleatoria entre hoy y 10 años en el futuro
        var daysOffset = Random.Shared.Next(0, 10 * 365);
        var randomDate = DateTime.Today.AddDays(daysOffset);
        return randomDate.ToString(formatoFecha);
    }
}
