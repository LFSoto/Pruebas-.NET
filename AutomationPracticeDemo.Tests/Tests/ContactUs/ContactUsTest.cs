using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.Components;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests.ContactUs
{
    public class ContactUsTest : TestBase
    {
        [Test]
        public void ContactUsMessage()
        {
            var menuPage = new menuPage(Driver);
            var ContactUs = new ContactUsPage(Driver);

            // Navegación a la página de contacto
            menuPage.ClickContactUs();

            // Verifica a través del mensaje que está en la página de contacto

            string messageContactUs = ContactUs.MessageContactUs();
            Assert.That(messageContactUs, Is.EqualTo("CONTACT US"), "El título de la página de contacto no coincide");
            ScreenshotHelper.TakeScreenshot(Driver, "MessageContactUs_Test.png");
            

            //Llenado del formulario de login
            ContactUs.FillContactUsForm("Rick", "Rick@pruebas.com", "Prueba", "Este es un mensaje de prueba");
            ContactUs.ClickSubmitButton();

            string messageAlert = ContactUs.GetAlertMessage();
            Assert.That(messageAlert, Is.EqualTo("Press OK to proceed!"), "El mensaje de la alerta es incorrecto.");
            ScreenshotHelper.TakeScreenshot(Driver, "AlertAccept_Test.png");

            Assert.That(ContactUs.GetSuccessMessage, Is.EqualTo("Success! Your details have been submitted successfully."), "El mensaje de la página no coincide");
        }
    }
}
