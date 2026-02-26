using NUnit.Framework;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormTests : TestBase
    {
        [Test]
        public void Should_FillAndSubmitForm()
        {
            var formPage = new FormPage(Driver);
            formPage.FillForm("Juan Perez", "juan@test.com", "88888888", "Costa Rica");
            formPage.Submit();

            ScreenshotHelper.TakeScreenshot(Driver, "form_test.png");
            Assert.Pass("Formulario llenado y enviado.");
            //Assert.Pass("Formulario llenado y enviado.");
        }
    }
}
