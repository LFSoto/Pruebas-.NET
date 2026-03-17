using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Tests.Contacto.Asserts;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Drawing;

namespace AutomationPracticeDemo.Tests.Contacto
{
    [TestFixture]
    public class ContactoTest : TestBase
    {
        [Test, Category("Contacto"), TestCaseSource(typeof(ContactoDataSource), nameof(ContactoDataSource.TodosRegistros))]
        public void FormularioContacto(ContactoData contacto)
        {
            var formPage = new ContactoPage(Driver);
            //se le da clcik al boton contacto
            formPage.ContactoClick();
            ScreenshotHelper.TakeScreenshot(Driver, "Contacto_1.png");
            //se llena el formulario
            formPage.LlenarContacto(contacto.Nombre, contacto.Correo, contacto.Asunto, 
                contacto.Mensaje, Path.GetFullPath(@contacto.Archivo));
            ScreenshotHelper.TakeScreenshot(Driver, "Contacto_2.png");
            //se le da click al boton submint
            formPage.SubmintClick();
            //se valida el mensaje de alerta
            Assert.That(formPage.MensajeAlerta, Is.EqualTo("Press OK to proceed!"));
            //se acepta la alerta
            formPage.AceptarAlerta();
            ScreenshotHelper.TakeScreenshot(Driver, "Contacto_3.png");
            //se valida el mensaje exitoso
            Assert.That(formPage.ValidarMensajeExitoso, Is.EqualTo("Success! Your details have been submitted successfully."));
        }
    }
}

