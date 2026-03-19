using System;
using AutomationPracticeDemo.Tests.Utils;
using System;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Tests.contacUs4.Asserts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutomationPracticeDemo.Tests.Tests.contacUs4
{
    public class ConstactUsTest : TestBase
    {
        [Test, TestCaseSource(typeof(MessageDataSource), nameof(MessageDataSource.MessageInformation))]
        public void ContactUs(string name, string email, string subject, string message)
        {
            var contactUsPage = new Pages.ContactUsPage(Driver);
            var rutaImagen = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resource", "Paisaje.jpg"));
            contactUsPage.btnContactUs();
            contactUsPage.FillContactForm(name, email, subject, message);
            if (File.Exists(rutaImagen))
            {
                contactUsPage.UploadFile(rutaImagen);
            }
            ScreenshotHelper.TakeScreenshot(Driver, "ContactUs.png");
            contactUsPage.SubmitForm();
            Assert.That(contactUsPage.GetSuccessMessage(), Is.EqualTo("Success! Your details have been submitted successfully."));
        }
    }

}