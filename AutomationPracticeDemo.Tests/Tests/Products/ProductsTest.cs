using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.Components;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests.Products
{
    [TestFixture]
    public  class ProductsTest : TestBase
    {

        private ProductsPage _products;
        private CartPage _cartPage;

        private const int ProductosAgregar = 2;

        [SetUp]
        public void SetUpPages()
        {
            _products = new ProductsPage(Driver);
            _cartPage = new CartPage(Driver);
        }


        [Test]
        public void AgregarProductosAlCarritoYValidarTotal()
        {
            // 1. Navegar a Products
            _products.ClickProductsLink();
            //LimpiarAnuncios();

            // 2. Agregar productos aleatorios
            _products.AddRandomProductsToCart(ProductosAgregar);

            // 3. Ir al carrito
            _products.GoToCart();
            //LimpiarAnuncios();

            // 4. Validar cantidad de productos
            _cartPage.ValidateProductCount(ProductosAgregar);

            // 5. Calcular y validar total
            double total = _cartPage.CalculateTotalFromRows();
            _cartPage.ValidateTotalGreaterThanZero(total);

            // 6. Scroll + Screenshot
            _cartPage.ScrollToFirstRow();
            ScreenshotHelper.TakeScreenshot(Driver, "carrito_final_validado.png");
        }
    }
}