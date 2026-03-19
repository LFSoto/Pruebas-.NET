using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.MainComponents;
using AutomationPracticeDemo.Tests.Tests._04_ContactUs.Asserts;
using AutomationPracticeDemo.Tests.Tests.Login.Asserts;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Tests._04_ContactUs
{
    [TestFixture]
    public class ContactUs : TestBase
    {
        /// <summary>
        /// Ejercicio 4: Contact Us form
        /// </summary
        /// 
        [Test, Category("ContactUs"), TestCaseSource(typeof(MessageDataSource), nameof(MessageDataSource.MessageInformation))]
        public void ContactUsTest(string name, string email, string subject, string message)
        {
            //Declaración de variables
            var menuPage = new menuPage(Driver);
            var contactUsPage = new ContactUSPage(Driver);

            //Ruta del archivo a subir
            string rutaImagen = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resource\Paisaje.jpg"));

            // Navegación a la página de Contact Us
            menuPage.ClickContactUsOption();
            Assert.That(contactUsPage.GetTitleContactUSPage, Is.EqualTo("CONTACT US"));

            //Llenado del formulario de Contact Us
            contactUsPage.FillContactForm(name, email, subject, message);

            //Subir un archivo (opcional)
            contactUsPage.UploadFile(rutaImagen);
            ScreenshotHelper.TakeScreenshot(Driver, "ContactUs_test.png");
            contactUsPage.SubmitContactForm();

            //Validar mensaje de éxito
            Assert.That(contactUsPage.GetAlertMessage(), Is.EqualTo("Press OK to proceed!"));
            contactUsPage.AcceptAlert();

            //Validar mensaje de éxito
            Assert.That(contactUsPage.GetSuccessMessage(), Is.EqualTo("Success! Your details have been submitted successfully."));
        }
    }
}
