using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Tests.Suscripcion.Asserts;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Drawing;

namespace AutomationPracticeDemo.Tests.Suscripcion
{
    [TestFixture]
    public class SuscripcionTest : TestBase
    {
        [Test, Category("Suscripcion"), TestCaseSource(typeof(SuscripcionDataSource), nameof(SuscripcionDataSource.TodosRegistros))]
        public void Suscripcion(SuscripcionData suscipcion)
        {
            var formPage = new SuscripcionPage(Driver);
            //se llena el campo correo
            formPage.LlenarSuscripcion(suscipcion.Correo);
            ScreenshotHelper.TakeScreenshot(Driver, "Suscripcion_1.png");
            //se le da click al boton enviar
            formPage.Enviar();
            //se valida el mensaje exitoso
            Assert.That(formPage.ValidarMensajeExitoso, Is.EqualTo("You have been successfully subscribed!"));
            ScreenshotHelper.TakeScreenshot(Driver, "Suscripcion_2.png");
        }

    }
}

