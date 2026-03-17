using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Tests.Registro.Asserts;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Drawing;

namespace AutomationPracticeDemo.Tests.Registro
{
    [TestFixture]
    public class RegistroTest: TestBase
    {
        [Test, Category("Registro"), TestCaseSource(typeof(RegistroDataSource), nameof(RegistroDataSource.TodosRegistros))]
        public void ValidarRegistroNuevoUsuario(RegistroData registro)
        {
            var pageForm = new RegistroPage(Driver);
            // se le da click al boton sing up/login
            pageForm.BotonSingUp_Click();
            ScreenshotHelper.TakeScreenshot(Driver, "Registro_1.png");
            // se llenar el formulario inicial
            pageForm.LlenarRegistroIngresar(registro.Nombre, registro.Correo);
            ScreenshotHelper.TakeScreenshot(Driver, "Registro_2.png");
            pageForm.EnviarIngresoRegistroBoton();
            // se llena el resto de información del formulario
            pageForm.LlenarRegistro(registro.Contrasena, registro.PrimerNombre, registro.Apellido, registro.Direccion, registro.Pais, registro.Estado, registro.Ciudad, registro.CodigoZip, registro.NumeroTelefono);
            ScreenshotHelper.TakeScreenshot(Driver, "Registro_3.png");
            // se le da click al botón crear 
            pageForm.CrearCuentaBoton();
            // se valida mensaje creación
            var mensajeCreacion = pageForm.ValidarMensajeCreacion();
            Assert.That(mensajeCreacion, Is.EqualTo("ACCOUNT CREATED!"));
            ScreenshotHelper.TakeScreenshot(Driver, "Registro_4.png");
            // se le dal boton continuar
            pageForm.ContinuatBoton();
            // se valida que haya ingresado (que exista el boton Logout)
            var validarlogout = pageForm.ValidarLogout();
            Assert.That(validarlogout, Is.EqualTo("Logout"));
            ScreenshotHelper.TakeScreenshot(Driver, "Registro_5.png");
            // se elimina cuenta para evitar basura de la prueba 
            pageForm.EliminarCuentaBoton();
            // se le da al boton continuar
            pageForm.ContinuarEliminacionBoton();
        }

    }
}

