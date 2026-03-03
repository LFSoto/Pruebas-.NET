using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Support.UI;
using System.Drawing;

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
        }
        [Test]
        public void VerificarRadioButtom_Genero()
        {
            var formPage = new FormPage(Driver);
            formPage.FillGender("Male");

            ScreenshotHelper.TakeScreenshot(Driver, "form_RadioButtom.png");
            Assert.Pass("Seleccionado el genero correctamente");
        }
        [Test]
        public void VerificarCheckBox_Days()
        {
            var formPage = new FormPage(Driver);
            string[] dias = { "monday", "friday", "sunday" };
            foreach (var dia in dias){
                formPage.FillDays(dia);
            }

            ScreenshotHelper.TakeScreenshot(Driver, "form_CheckBoxDays.png");
            Assert.Pass($"Seleccionado los dias {string.Join(",", dias)} correctamente");
        }
        [Test]
        public void VerificarRangeDates()
        {
            var formPage = new FormPage(Driver);
            formPage.FillRangeDates(new DateTime(2026,02,03).ToString("dd-MM-yyyy"), new DateTime(2026, 03, 03).ToString("dd-MM-yyyy"));
            formPage.SubmitDates();

            ScreenshotHelper.TakeScreenshot(Driver, "form_RangeDate.png");
            Assert.Pass("Seleccionado el rango de fechas y submit.");
        }
        [Test]
        public void VerificarPromptAlert()
        {
            var formPage = new FormPage(Driver);
            formPage.OpenAlertPrompt();
            // Esperar un instante para que el alerta aparezca
            Thread.Sleep(200);
            ScreenshotHelper.TakeScreenshot(Driver, "form_PromptAlert.png",true);

            IAlert alert = Driver.SwitchTo().Alert();
            string actualAlertText = alert.Text;
            Assert.That(actualAlertText, Is.EqualTo("Please enter your name:"));
            alert.Accept();

        }
    }
}
