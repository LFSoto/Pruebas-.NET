using NUnit.Framework;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests;

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
        Assert.That(Driver.Url, Does.Contain("success"), "La URL no contiene 'success' después de enviar el formulario.");
    }

    [Test]
    public void Should_TestDayCheckbox()
    {
        var formPage = new FormPage(Driver);
        formPage.DoCheckOnAllDaysOfTheWeek();
        ScreenshotHelper.TakeScreenshot(Driver, "TestDayCheckbox.png");
        Assert.That(formPage.IsAllDayOfTheWeekChecked(), Is.True, "No todos los días de la semana están marcados.");
    }


    [Test]
    public void Should_TestCountryDropbdown()
    {
        var formPage = new FormPage(Driver);
        var countryToSelect = "France";
        formPage.SelectCountry(countryToSelect);
        ScreenshotHelper.TakeScreenshot(Driver, "TestCountryDropdown.png");
        Assert.That(formPage.IsCountrySelected(countryToSelect), Is.True, $"El país '{countryToSelect}' no está seleccionado en el dropdown.");
    }

    [Test]
    public void Should_TestDatePicker()
    {
        var formPage = new FormPage(Driver);
        // formato MM/dd/yyyy
        var dateToSelect1 = "12/31/2022";
        // formato dd/MM/yyyy 
        var dateToSelect2 = "31/12/2019";

        formPage.SelectDateInDatePickerOne(dateToSelect1);
        // Se valida que la fecha seleccionada en el DatePicker 1 sea la misma que se intentó seleccionar
        Assert.That(formPage.GetSelectedDateInDatePickerOne(),
            Is.EqualTo(dateToSelect1), $"La fecha seleccionada en el DatePicker 1 no es '{dateToSelect1}'.");

        formPage.SelectDateInDatePickerTwo(dateToSelect2);
        // Se valida que la fecha seleccionada en el DatePicker 2 sea la misma que se intentó seleccionar
        Assert.That(formPage.GetSelectedDateInDatePickerTwo(),
            Is.EqualTo(dateToSelect2), $"La fecha seleccionada en el DatePicker 2 no es '{dateToSelect2}'.");
    }

    [Test]
    public void Should_TestAlertMessage()
    {
        var textoEsperado = "I am an alert box!";
        var formPage = new FormPage(Driver);
        formPage.ClickOnSimpleAlerLabel();
        Assert.That(formPage.GetSimpleAlertText(), Is.EqualTo(textoEsperado), "El texto de la alerta no es el esperado.");
        formPage.AcceptSimpleAlert();
        Assert.That(formPage.IsAlertPresent(), Is.False, "La alerta no fue aceptada correctamente.");
    }
}
