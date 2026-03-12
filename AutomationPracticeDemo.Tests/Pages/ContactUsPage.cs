using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class ContactUsPage
    {
        private readonly IWebDriver _driver;

        public ContactUsPage(IWebDriver driver)
        {
            _driver = driver;
        }

		//Webelements con los que se interactúa 
		private IWebElement NameField => _driver.FindElement(By.CssSelector("input[data-qa='name']"));
		private IWebElement EmailField => _driver.FindElement(By.CssSelector("input[data-qa='email']"));
		private IWebElement SubjectField => _driver.FindElement(By.CssSelector("input[data-qa='subject']"));
		private IWebElement MessageField => _driver.FindElement(By.Id("message"));
		private IWebElement Archivo => _driver.FindElement(By.Name("upload_file"));
		private IWebElement SubmitButton => _driver.FindElement(By.CssSelector("input[data-qa='submit-button']"));
		private IWebElement SuccessMessage => _driver.FindElement(By.XPath("//div[contains(text(),'Success! Your details have been submitted successfully.')]"));

		//Métodos necesarios para interactuar con los elementos de la página
				
		//Se llena el campo del formulario de contacto con el nombre
		public void Fill_NameInput(String name)
		{
			NameField.SendKeys(name);
		}

		//Se llena el campo del formulario de contacto con el email
		public void Fill_EmailField(String email)
		{
			EmailField.SendKeys(email);
		}

		//Se llena el campo del formulario de contacto con el subject
		public void Fill_SubjectField(String subject)
		{
			SubjectField.SendKeys(subject);

		}

		//Se llena el campo del formulario de contacto con el mensaje
		public void Fill_MessageField(String message)
		{
			MessageField.SendKeys(message);
		}

		//Se selecciona el archivo a subir 
		public void Fill_Archivo(String rutaImagen)
		{
			Archivo.SendKeys(rutaImagen);
		}

		//Se da click en el botón de submit para enviar el formulario de contacto
		public void Click_SubmitButton()
		{
			SubmitButton.Click();
		}

		//Se acpeta la alerta que aparece después de enviar el formulario de contacto
		public void Click_AlertAccept()
		{
			var alertAccept = _driver.SwitchTo().Alert();
			alertAccept.Accept();
		}

		// Retorna el mensaje de éxito que se muestra después de enviar el formulario de contacto
		public string Check_SuccessMessage()
		{
			return SuccessMessage.Text;
		}

		/*
		//Se valida que el mensaje de éxito se muestre después de enviar el formulario de contacto y se toma captura de evidencia
		ScreenshotHelper.TakeScreenshot(Driver, "successContactUs.png");
		Assert.That(Check_SuccessMessage().Text, Is.EqualTo("Success! Your details have been submitted successfully."), "El mensaje de exito debería mostrarse");private IWebElement NameInput => _driver.FindElement(By.Id("name"));
		*/

	}
}
