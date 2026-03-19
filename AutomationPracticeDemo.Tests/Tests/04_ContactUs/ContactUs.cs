using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.MainComponents;
using AutomationPracticeDemo.Tests.Tests._04_ContactUs.Asserts;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests._04_ContactUs
{
    [TestFixture]
    public class ContactUs : TestBase
    {
        [Test, Category("ContactUs"), TestCaseSource(typeof(MessageDataSource), nameof(MessageDataSource.MessageInformation))]
        public void ContactUsTest(string name, string email, string subject, string message)
        {
            var menuPage = new menuPage(Driver);
            var contactUsPage = new ContactUSPage(Driver);

            // Resolve file from repo root (works in Windows + Linux runners)
            var rutaImagen = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resource", "Paisaje.jpg"));

            menuPage.ClickContactUsOption();
            Assert.That(contactUsPage.GetTitleContactUSPage, Is.EqualTo("CONTACT US"));

            contactUsPage.FillContactForm(name, email, subject, message);

            // Upload is optional; avoid failing CI if the sample image isn't present.
            if (File.Exists(rutaImagen))
            {
                contactUsPage.UploadFile(rutaImagen);
            }

            ScreenshotHelper.TakeScreenshot(Driver, "ContactUs_test.png");
            contactUsPage.SubmitContactForm();

            Assert.That(contactUsPage.GetAlertMessage(), Is.EqualTo("Press OK to proceed!"));
            contactUsPage.AcceptAlert();

            Assert.That(contactUsPage.GetSuccessMessage(), Is.EqualTo("Success! Your details have been submitted successfully."));
        }
    }
}
