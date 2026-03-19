using AutomationPracticeDemoTest.Tests.ContactUs.Assets;
using AutomationPracticeDemoTests;
using AutomationPracticeDemoTests.Pages;
using AutomationPracticeDemoTests.Utils;

namespace AutomationPracticeDemoTest.Tests.ContactUs;

[TestFixture]
public class ContactUsTest : BaseTest
{
    private string folderName = "ContactUsTest";

    [Test, Category("ContactUs"), TestCaseSource(typeof(MessageDataSource), nameof(MessageDataSource.MessageInformation))]
    public void ValidarFormularioContactUs(MessageData data)
    {
        var name = data.Name;
        var email = data.Email;
        var subject = data.Subject;
        var message = data.Message;

        var homePage = new HomePage(Driver);
        var contactUsPage = homePage.TopMenu.GoToContactUsPage();
        contactUsPage.FillContactUsForm(name, email, subject, message, "camiseta_01.jpg");
        ScreenshotHelper.TakeScreenshot(Driver, $"Llenado_Formulario_{email}"
            .Replace("@test.com", "").Trim(), folderName);
        contactUsPage.ClickSubmitButton();
        contactUsPage.AcceptAlert();
        Assert.That(contactUsPage.GetAlertSuccessMessage(), Is.EqualTo("Success! Your details have been submitted successfully."),
            "El mensaje de éxito no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, $"Registro_formulario_contactus_{email}".Replace("@test.com", "").Trim(), folderName);
    }
}
