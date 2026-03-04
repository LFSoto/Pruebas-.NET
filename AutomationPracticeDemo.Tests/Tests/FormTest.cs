using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System;

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

        [Test] // *** Nuevo
        public void Should_SelectGenderMale()
        {
            var formPage = new FormPage(Driver);
            formPage.SelectGenderMale("male");

            ScreenshotHelper.TakeScreenshot(Driver, "male_ radio_test.png");

            //Assert.IsTrue(formPage.IsGenderSelected("Masculino"), "El RadioButton no fue seleccionado.");
            Assert.Pass("RadioButton seleccionado correctamente.");
        }

        [Test] // *** Nuevo
        public void Should_SelectGenderFemale()
        {
            var formPage = new FormPage(Driver);
            formPage.SelectGenderMale("female");

            ScreenshotHelper.TakeScreenshot(Driver, "female_ radio_test.png");

            //Assert.IsTrue(formPage.IsGenderSelected("Masculino"), "El RadioButton no fue seleccionado.");
            Assert.Pass("RadioButton seleccionado correctamente.");
        }


        [Test]
        public void Should_SelectDate()
        {
            var formPage = new FormPage(Driver);

            // Elegir fecha en el date picker
            formPage.PickDate("02/28/2026");

            // Espera explícita hasta que el campo tenga el valor esperado
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => formPage.GetSelectedDate() == "02/28/2026");

            // Tomar captura como evidencia
            ScreenshotHelper.TakeScreenshot(Driver, "date_test.png");

            // Assert: validar que la fecha seleccionada es correcta
            Assert.AreEqual("02/28/2026", formPage.GetSelectedDate(),
                "La fecha seleccionada no es correcta.");
        }

        [Test]
        public void Should_HandleSimpleAlert()
        {
            var formPage = new FormPage(Driver);

            // Disparar el alert simple y obtener texto
            string alertMessage = formPage.GetAlertTextAndAccept();

            // Assert: validar texto del alert
            Assert.AreEqual("I am an alert box!", alertMessage,
                "El texto del alert simple no coincide.");

            // Screenshot después de cerrar el alert
            ScreenshotHelper.TakeScreenshot(Driver, "simple_alert.png");
        }

        [Test]
        public void Should_HandleConfirmAlert()
        {
            var formPage = new FormPage(Driver);

            // Disparar el confirm alert
            formPage.TriggerConfirmAlert();

            // Capturar texto del alert
            string alertMessage = formPage.GetAlertText();

            // Assert con el texto real del sitio
            Assert.AreEqual("Press a button!", alertMessage,
                "El texto de la alerta de confirmación no coincide.");

            // Aceptar el alert
            formPage.AcceptAlert();

            // Screenshot después de cerrar el alert
            ScreenshotHelper.TakeScreenshot(Driver, "confirm_alert.png");
        }



    }
}
