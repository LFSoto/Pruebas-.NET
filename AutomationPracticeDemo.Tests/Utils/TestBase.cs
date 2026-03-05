using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI; //Nuevo
using AutomationPracticeDemo.Tests.Utils; //Nuevo
using System; //Nuevo


namespace AutomationPracticeDemo.Tests.Base
{
    public class TestBase
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait; //Nuevo

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();

            options.AddArgument("--start-maximized");

            // Configuración del navegador
            options.AddArgument("--start-maximized");       // Abrir maximizado
            options.AddArgument("--disable-notifications"); // Desactivar notificaciones
            options.AddArgument("--disable-infobars");      // Quitar barra de información
            options.AddArgument("--headless=new");          // Ejecutar en modo headless (sin ventana gráfica)
            options.AddArgument("--window-size=1920,1080"); // Definir tamaño de ventana en headless

            Driver = new ChromeDriver(options);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            Driver.Navigate().GoToUrl("https://automationexercise.com/");
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (Driver != null)
                {
                    // Guardar screenshot usando ScreenshotHelper
                    ScreenshotHelper.TakeScreenshot(Driver, TestContext.CurrentContext.Test.Name);
                    TestContext.WriteLine("Screenshot guardado correctamente.");
                }
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error al guardar screenshot: " + ex.Message);
            }
            finally
            {
                if (Driver != null)
                {
                    Driver.Quit();
                    Driver.Dispose();
                }
            }
        }
    }
}
