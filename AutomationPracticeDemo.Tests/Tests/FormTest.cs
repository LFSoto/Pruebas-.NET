using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormTests : TestBase
    {
		[Test]
        public void Should_FillAndSubmitForm()
        {

			Driver.Navigate().GoToUrl("https://testautomationpractice.blogspot.com/");
			var formPage = new FormPage(Driver);
            formPage.FillForm("Juan Perez", "juan@test.com", "88888888", "Costa Rica");
            formPage.Submit();

            ScreenshotHelper.TakeScreenshot(Driver, "form_test.png");
            Assert.Pass("Formulario llenado y enviado.");
        }

		[Test]
		public void Should_FillandSubmitSection1()
		{
			Driver.Navigate().GoToUrl("https://testautomationpractice.blogspot.com/");
			var formPage = new FormPage(Driver);
			formPage.FillSection1("Francinni");
			ScreenshotHelper.TakeScreenshot(Driver, "section1_textField1.png");

			// Validar que el texto entrado sea el correcto
			Assert.That(formPage.TextEntered(), Is.EqualTo("Francinni"), "El campo debería tener texto");

			formPage.SubmitButtonSection1();
			ScreenshotHelper.TakeScreenshot(Driver, "section1_submitButton.png");

			// Validar que el borde sea negro
			Assert.That(formPage.ButtonBorderSection1(), Does.Contain("rgb(0, 0, 0)"), "El botón debería tener borde negro después del clic");

		}

		[Test]
		public void Should_SelectCheckBoxMonday()
		{
			Driver.Navigate().GoToUrl("https://testautomationpractice.blogspot.com/");
			var formPage = new FormPage(Driver);
			formPage.SelectCheckBoxMonday();
			
			ScreenshotHelper.TakeScreenshot(Driver, "selectedCheckboxMonday_test.png");

			// Validar que el checkbox Monday está seleccionado
			Assert.That(formPage.IsCheckBoxMondaySelected, "El checkbox Monday debería estar marcado");
		}


		[Test]
		public void Should_SelectGender()
		{

			Driver.Navigate().GoToUrl("https://testautomationpractice.blogspot.com/");
			var formPage = new FormPage(Driver);
			formPage.SelectGender();
			
			ScreenshotHelper.TakeScreenshot(Driver, "selectedRadioButtonGender_test.png");

			// Validar que el radiobutton Female está seleccionado
			//validar
			Assert.That(formPage.IsFemaleRadioButtonSelected, "El radiobutton Female debería estar marcado");
		}

		[Test]
		public void Should_SelectDatepicker()
		{
			Driver.Navigate().GoToUrl("https://testautomationpractice.blogspot.com/");
			var formPage = new FormPage(Driver);
			formPage.OpenDatePicker1();
			formPage.SelectDay();

			ScreenshotHelper.TakeScreenshot(Driver, "selectedDatepicker_test.png");

			// Validar que la fecha está seleccionada
			Assert.That(formPage.DateSelected, Is.EqualTo("03/02/2026"), "La fecha debería estar seleccionada");
		}


	}
}
