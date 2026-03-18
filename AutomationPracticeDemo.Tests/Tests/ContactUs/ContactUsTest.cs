
using AutomationPracticeDemo.Test.Tests.ContactUs.Assets;
using AutomationPracticeDemo.Tests;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;

namespace AutomationPracticeDemo.Test.Tests.ContactUs;

[TestFixture]
public class ContactUsTest : BaseTest
{
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
            .Replace("@test.com", "").Trim());
        contactUsPage.ClickSubmitButton();
        contactUsPage.AcceptAlert();
        Assert.That(contactUsPage.GetAlertSuccessMessage(), Is.EqualTo("Success! Your details have been submitted successfully."),
            "El mensaje de éxito no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, $"Registro_formulario_contactus_{email}".Replace("@test.com", "").Trim());
    }
}
