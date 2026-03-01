using NUnit.Framework;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormTests : TestBase
    {
        [Test]
        public void Should_FillAndSubmitForm()
        {
            var formPage = new FormPage(Driver);

            var name = "Juan Perez";
            var email = "juan@test.com";
            var phone = "88888888";
            var country = "Costa Rica";

            formPage.FillForm(name, email, phone, country);

            // screenshot before submit
            ScreenshotHelper.TakeScreenshot(Driver, "form_before.png");

            // assertions for input values using Assert.That
            Assert.That(formPage.GetName(), Is.EqualTo(name));
            Assert.That(formPage.GetEmail(), Is.EqualTo(email));
            Assert.That(formPage.GetPhone(), Is.EqualTo(phone));

            // submit
            formPage.Submit();

            // wait for and accept alert
            var alertText = formPage.WaitForAlertAndAccept(5);
            Assert.That(alertText, Is.Not.Null.And.Not.Empty);

            // screenshot after
            ScreenshotHelper.TakeScreenshot(Driver, "form_after.png");
        }



    }
}


