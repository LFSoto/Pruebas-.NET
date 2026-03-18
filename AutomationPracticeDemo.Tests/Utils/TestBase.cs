using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationPracticeDemo.Tests.Utils
{
    public class TestBase
    {
        protected IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
           /* var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            Driver = new ChromeDriver(options);
			//Driver.Navigate().GoToUrl("https://testautomationpractice.blogspot.com/");*/
			

			//Configuración del driver de Chrome con opciones personalizadas para la automatización de pruebas
			var options = new ChromeOptions();
			options.AddArgument("--start-maximized");
			options.AddArgument("--disable-notifications");
			options.AddArgument("--disable-infobars");
			options.AddArgument("--headless=new"); //Se usa para ejecutar las pruebas sin levantar la interfaz
			options.AddArgument("--window-size=1920,1080");
			Driver = new ChromeDriver(options);
			Driver.Navigate().GoToUrl("https://automationexercise.com");
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
    }
}
