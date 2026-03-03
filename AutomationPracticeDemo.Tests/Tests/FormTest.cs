using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using NUnit.Framework;
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
            Assert.That(formPage.IsGenderSelected(), Is.True, "El female debería estar marcado.");
            Thread.Sleep(3000);
            ScreenshotHelper.TakeScreenshot(Driver, "gender.png");

            formPage.days();
            Assert.That(formPage.daysSelectMonday, Is.True, "Monday debería estar marcado.");
            Assert.That(formPage.daysSelectTuesday, Is.True, "Tuesday debería estar marcado.");
            Assert.That(formPage.daysSelectWednesday, Is.True, "Wednesday debería estar marcado.");
            Thread.Sleep(3000);
            ScreenshotHelper.TakeScreenshot(Driver, "days.png");

            
            Assert.That(formPage.country(), Is.EqualTo("Japan"));
            Thread.Sleep(3000);
            ScreenshotHelper.TakeScreenshot(Driver, "country.png");

           
            Assert.That(formPage.Colors(), Is.EqualTo("Red"));
            Thread.Sleep(3000); 
            ScreenshotHelper.TakeScreenshot(Driver, "colors.png");  

            
            Assert.That(formPage.Animals(), Is.EqualTo("Dog"));
            Thread.Sleep(3000); 
            ScreenshotHelper.TakeScreenshot(Driver, "animals.png");


            formPage.Datapicker1();
            Thread.Sleep(3000);
            ScreenshotHelper.TakeScreenshot(Driver, "datepicker1.png");

            formPage.Datapicker2();
            Thread.Sleep(3000);
            ScreenshotHelper.TakeScreenshot(Driver, "datepicker2.png");

           String diaspantalla = formPage.Datapicker3("01/03/2026","30/03/2026");  
            Thread.Sleep(3000);
            ScreenshotHelper.TakeScreenshot(Driver, "datepicker3.png");


            formPage.Submit();
            Thread.Sleep(3000);
            Assert.That(formPage.GetMensajePantalla, Is.EqualTo("You selected a range of " + diaspantalla + " days."));


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
        [Test]
        public void Should_AlertÇonfirmation()
        {
            var formPage = new FormPage(Driver);
            formPage.alertConfirmation();
            Thread.Sleep(3000);
            IAlert alert = Driver.SwitchTo().Alert();
            alert.Accept();
        }
        [Test]
        public void Should_AlertPrompt()
        {
            var formPage = new FormPage(Driver);
            formPage.alertPrompt();
            IAlert prompt = Driver.SwitchTo().Alert();
            string textoIngresado = "Gustavo";
            prompt.SendKeys(textoIngresado);
            Thread.Sleep(3000);
            prompt.Accept();
            Assert.That(formPage.GetMensajePantallaPrompt, Is.EqualTo("Hello " + textoIngresado + "! How are you today?"));

        }

    }
}
