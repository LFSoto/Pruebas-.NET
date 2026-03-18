using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
	public class MenuPage
	{
		private readonly IWebDriver _driver;

		public MenuPage(IWebDriver driver)
		{
			_driver = driver;
		}

		//Webelements con los que se interactúa 
		private IWebElement SignupLink => _driver.FindElement(By.CssSelector("a[href='/login']"));
		private IWebElement LogoutOption => _driver.FindElement(By.CssSelector("a[href='/logout']"));
		private IWebElement ContactUsLink => _driver.FindElement(By.CssSelector("a[href='/contact_us']"));

		private IWebElement ProductsOption => _driver.FindElement(By.CssSelector("a[href='/products']"));

		//Métodos necesarios para interactuar con los elementos de la página
		public void Click_SignupLink()
		{
			SignupLink.Click();
		}

		// Retorna el valor del texto del mensaje de cuenta creada
		public string LogoutText()
		{
			return LogoutOption.Text;
		}

		//Se hace clic en el enlace "Contact Us" para acceder al formulario de contacto
		public void Click_ContactUsLink()
		{
			ContactUsLink.Click();
		}

		//Se hace clic en la opción "Products" para acceder a la página de productos
		public void Click_ProductsOption()
		{
			ProductsOption.Click();
		}

		
	}
}
