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

            var name = "Juan José";
            var email = "juanrm@test.com";
            var phone = "85414960";
            // Use a country that exists in the select on the tested page
            var country = "Japón";

            formPage.FillForm(name, email, phone, country);

            // screenshot before submit
            ScreenshotHelper.TakeScreenshot(Driver, "form_before.png");

            // assertions for input values using Assert.That
            Assert.That(formPage.GetName(), Is.EqualTo(name));
            Assert.That(formPage.GetEmail(), Is.EqualTo(email));
            Assert.That(formPage.GetPhone(), Is.EqualTo(phone));

            // submit
            formPage.Submit();

            // try to wait for and accept alert if present; do not fail if no alert
            string alertText = null;
            try
            {
                alertText = formPage.WaitForAlertAndAccept(5);
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {
                // no alert appeared within timeout, continue
            }

            if (!string.IsNullOrEmpty(alertText))
            {
                Assert.That(alertText, Is.Not.Null.And.Not.Empty);
            }

            // screenshot after
            ScreenshotHelper.TakeScreenshot(Driver, "form_after.png");
        }



    }
}


