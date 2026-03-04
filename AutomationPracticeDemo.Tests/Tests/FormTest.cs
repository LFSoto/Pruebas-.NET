using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormTests : TestBase
    {
        //Inicializamos FormPage
        private FormPage formPage;
        
        [SetUp]
        public void Inicializar()
        {
            Driver.Manage().Window.Maximize();
            formPage = new FormPage(Driver);
        }

        //Termina método inicializar


        [Test]
        public void Should_FillAndSubmitForm()
        {
           // var formPage = new FormPage(Driver);
            formPage.FillForm("Juan Perez", "juan@test.com", "88888888", "Costa Rica");
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0, 300);"); // Baja un poco para centrar el formulario
            ScreenshotHelper.TakeScreenshot(Driver, "form_test.png");
            formPage.Submit();
            
            Assert.Pass("Formulario llenado y enviado.");
        }

        [Test] //Método de prueba para seleccionar el género masculino - RADIO BUTTOM
        public void Should_SelectGenderMale()
        {
            //var formPage = new FormPage(Driver);
            formPage.SelectGenderMale();
            ScreenshotHelper.TakeScreenshot(Driver, "gender.png");
            //Thread.Sleep(10000);
            Assert.Pass("Género masculino seleccionado.");
        }

        [Test] //Método de seleccionar el país - DROPDOWN

        public void Should_SelectCountry()
        {
            //var formPage = new FormPage(Driver);
            formPage.SelectCountry();
            //Thread.Sleep(10000);
            ScreenshotHelper.TakeScreenshot(Driver, "country.png");
            Assert.Pass("País seleccionado.");
        }

        [Test] //Método de prueba para seleccionar una fecha en el DatePicker1 - DATEPICKER
        public void Should_DatePicker1() {

            formPage.SelectDatePicker1();
            ScreenshotHelper.TakeScreenshot(Driver, "datepicker1.png");
            Assert.Pass("Fecha seleccionada en DatePicker1.");
        }

        [Test] //Método de prueba para seleccionar una simple alert - ALERTS
        public void Should_SimpleAlert() {

            formPage.SelectSimpleAlert(3000);
            //Thread.Sleep(10000);
            //ScreenshotHelper.TakeScreenshot(Driver, "simpleAlert.png");
            Assert.Pass("Alerta simple seleccionada.");
        }

    }
}
//Aquí sucede la prueba usando los Assert para saber si la prueba pasó o no.