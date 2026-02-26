using NUnit.Framework;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V137.BluetoothEmulation;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormTests : TestBase
    {
        [Test]
        public void Should_FillAndSubmitForm()
        {
            var formPage = new FormPage(Driver);
            formPage.FillForm("Gustavo Montero Gonzalez", "gumontero@test.com", "88651273", "Coyol, Alajuela");
            formPage.Gender();
            Thread.Sleep(3000);
            ScreenshotHelper.TakeScreenshot(Driver, "gender.png");

            formPage.days();
            Thread.Sleep(3000);
            ScreenshotHelper.TakeScreenshot(Driver, "days.png");

            formPage.country();
            Thread.Sleep(3000);
            ScreenshotHelper.TakeScreenshot(Driver, "country.png");

            formPage.Colors();
             Thread.Sleep(3000); 
            ScreenshotHelper.TakeScreenshot(Driver, "colors.png");  

            formPage.Animals();
                Thread.Sleep(3000); 
            ScreenshotHelper.TakeScreenshot(Driver, "animals.png");


            formPage.Datapicker1();
            Thread.Sleep(3000);
            ScreenshotHelper.TakeScreenshot(Driver, "datepicker1.png");
           

            formPage.Submit();

            ScreenshotHelper.TakeScreenshot(Driver, "form_test.png");
            Assert.Pass("Formulario llenado y enviado.");
        }

        [Test]
        public void Should_Alertsimple()
        {
            var formPage = new FormPage(Driver);

            formPage.alertsimple();
           

            IAlert alert = Driver.SwitchTo().Alert();


            string alertText = alert.Text;

            Assert.That(alertText, Is.EqualTo("I am an alert box!"));
            
            Thread.Sleep(3000);
            alert.Accept();
            ScreenshotHelper.TakeScreenshot(Driver, "Alertsimple.png");


        }
    }
}
