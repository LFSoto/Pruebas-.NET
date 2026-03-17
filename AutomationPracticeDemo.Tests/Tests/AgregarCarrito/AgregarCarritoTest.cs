using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Drawing;

namespace AutomationPracticeDemo.Tests.Carrito
{
    [TestFixture]
    public class AgregarCarritoTest : TestBase
    {
        [Test]
        public void AgregarCarrito()
        {
            //Ingresar a la pantalla productos
            var formPage = new AgregarCarritoPage(Driver);
            formPage.ProductoBoton_Click();
            ScreenshotHelper.TakeScreenshot(Driver, "AgregarCarrito_1.png");
            //se selecciona productos aleatorios
            int producto1 = new Random().Next(0, 33);
            int producto2 = new Random().Next(0, 33);
            //se agrega al carrito el producto 1
            formPage.agregarAlCarrito(producto1);
            ScreenshotHelper.TakeScreenshot(Driver, "AgregarCarrito_2.png");
            //se continua comprando productos
            formPage.ContinuarComprando();
            //se agregar el segundo producto
            formPage.agregarAlCarrito(producto2);
            ScreenshotHelper.TakeScreenshot(Driver, "AgregarCarrito_3.png");
            //se ingresa a la pantalla carrito
            formPage.VerCarrito();
            //se valida los productos y totales de la tabla del carrito
            formPage.ValidarRegistroTabla();
            ScreenshotHelper.TakeScreenshot(Driver, "AgregarCarrito_4.png");
        }
    }
}

