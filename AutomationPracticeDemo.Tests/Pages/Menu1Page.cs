using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
	public class Menu1Page
	{
		private readonly IWebDriver _driver;

		public Menu1Page(IWebDriver driver)
		{
			_driver = driver;
		}

		//Webelements con los que se interactúa 
		private IWebElement SignupLink => _driver.FindElement(By.CssSelector("a[href='/login']"));
		private IWebElement logoutOption => _driver.FindElement(By.CssSelector("a[href='/logout']"));
		private IWebElement contactUsLink => _driver.FindElement(By.CssSelector("a[href='/contact_us']"));



		//Métodos necesarios para interactuar con los elementos de la página
		public void Click_SignupLink()
		{
			SignupLink.Click();
		}

		// Retorna el valor del texto del mensaje de cuenta creada
		public string LogoutText()
		{
			return logoutOption.Text;
		}

		//Se hace clic en el enlace "Contact Us" para acceder al formulario de contacto
		public void Fill_NameInput(String name)
		{
			contactUsLink.Click();
		}

		//Se hace clic en la opción "Products" para acceder a la página de productos
		public void Click_ProductsOption()
		{
			ProductsOption.Click();
		}

		///////////suscripcion

		private IWebElement ScrollDown => _driver.FindElement(By.Id("susbscribe_email"));
		private IWebElement EmailField => _driver.FindElement(By.Id("susbscribe_email"));
		private IWebElement SubmitButton => _driver.FindElement(By.Id("subscribe"));
		private IWebElement SuccessMessage => _driver.FindElement(By.XPath("//div[contains(text(),'You have been successfully subscribed!')]"));

		private IWebElement ProductsOption => _driver.FindElement(By.CssSelector("a[href='/products']"));


		//Se realiza el  scroll hacia el campo de suscripción para asegurar que estemos en la seccion correcta de la página
		public void Move_ScrollDown()
		{
			IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
			js.ExecuteScript("arguments[0].scrollIntoView(true);", ScrollDown);
		}

		//Se ingresa el correo electrónico en el campo 
		public void Fill_EmailAddress(String email)
		{
			EmailField.SendKeys(email);
		}

		//Se hace clic en el botón de submit para enviar la suscripción
		public void Click_SubmitButton()
		{
			SubmitButton.Click();
		}

		// Retorna el mensaje de éxito que se muestra después de enviar el formulario de contacto
		public string Check_SuccessMessage()
		{
			return SuccessMessage.Text;
		}

		/*
		//Se valida que el mensaje de éxito sea visible después de enviar la suscripción y se toma captua de evidencia
		ScreenshotHelper.TakeScreenshot(Driver, "suscribeMessage.png");
		Assert.That(successMessage().Text, Is.EqualTo("You have been successfully subscribed!"), "El mensaje de exito debería mostrarse");
		*/
	}
}
