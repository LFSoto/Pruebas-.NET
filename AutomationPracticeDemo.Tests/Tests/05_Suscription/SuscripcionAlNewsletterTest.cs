using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests._05_Suscription
{
	public class SuscripcionAlNewsletterTest : TestBase
	{

		[Test]
		public void Caso5_SuscripcionAlNewsletter()
		{
			var suscripcionAlNewsletterPage = new SuscripcionAlNewsletterPage(Driver);
			string email = "test@test.com";

			//Se desplaza hacia abajo para ubicar el formulario de suscripción al newsletter, se completa el campo de correo electrónico y se envía el formulario
			suscripcionAlNewsletterPage.Move_ScrollDown();
			suscripcionAlNewsletterPage.Fill_EmailAddress(email);
			suscripcionAlNewsletterPage.Click_SubmitButton();
			suscripcionAlNewsletterPage.Check_SuccessMessage();
			
			//Se valida que el mensaje de éxito sea visible después de enviar la suscripción y se toma captua de evidencia
			ScreenshotHelper.TakeScreenshot(Driver, "suscribeMessage.png");
			Assert.That(suscripcionAlNewsletterPage.Check_SuccessMessage, Is.EqualTo("You have been successfully subscribed!"), "El mensaje de exito debería mostrarse");
						
		}
	}

}
