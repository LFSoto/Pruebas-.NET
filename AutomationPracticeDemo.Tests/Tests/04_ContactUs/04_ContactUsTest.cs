using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Tests._04_ContactUs.Asserts;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests._04_ContactUs
{
	public class _04_ContactUsTest: TestBase
	{

		[Test, TestCaseSource(typeof(ContactUsDataSource), nameof(ContactUsDataSource.ContactUs))]
		public void Caso4_ContactUs(string name, string email, string subject, string message)
		{
			//Variables para la ruta de la imagen a adjuntar en el formulario de contacto
			string rutaImagen = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resource\Paisaje.jpg"));

			var menuPage = new MenuPage(Driver);
			var contactUsPage = new ContactUsPage(Driver);

			//Se navega a la página de contacto a través del enlace en el menú
			menuPage.Click_ContactUsLink();

			//Se completa el formulario de contacto con los datos proporcionados y se adjunta una imagen, luego se envía el formulario
			contactUsPage.Fill_ContactForm(name, email, subject, message, rutaImagen);
			ScreenshotHelper.TakeScreenshot(Driver, "fileAttached.png");
			contactUsPage.Click_SubmitButton();
			contactUsPage.Click_AlertAccept();
			
			//Se valida que el mensaje de éxito se muestre después de enviar el formulario de contacto y se toma captura de evidencia
			ScreenshotHelper.TakeScreenshot(Driver, "successContactUs.png");
			Assert.That(contactUsPage.Check_SuccessMessage, Is.EqualTo("Success! Your details have been submitted successfully."), "El mensaje de exito debería mostrarse");
		}
	}

}
