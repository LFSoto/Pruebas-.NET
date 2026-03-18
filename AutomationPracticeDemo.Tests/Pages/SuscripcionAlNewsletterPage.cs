using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Pages
{
public class SuscripcionAlNewsletterPage
	{
		private readonly IWebDriver _driver;

		public SuscripcionAlNewsletterPage(IWebDriver driver)
		{
			_driver = driver;
		}

		///////////suscripcion

		private IWebElement ScrollDown => _driver.FindElement(By.Id("susbscribe_email"));
		private IWebElement EmailField => _driver.FindElement(By.Id("susbscribe_email"));
		private IWebElement SubmitButton => _driver.FindElement(By.Id("subscribe"));
		private IWebElement SuccessMessage => _driver.FindElement(By.XPath("//div[contains(text(),'You have been successfully subscribed!')]"));

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

	}
}
