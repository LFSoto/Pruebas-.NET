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
    public  class ProductsTest : TestBase
    {

        private ProductsPage products;
        private CartPage cartPage;

        private const int ProductosAgregar = 2;

        [SetUp]
        public void SetUp()
        {
            products = new ProductsPage(Driver, Wait);
            cartPage = new CartPage(Driver, Wait);
        }


        [Test]
        public void AgregarProductosAlCarritoYValidarTotal()
        {
            // 1. Navegar a Products
            products.ClickProductsLink();
            //LimpiarAnuncios();

            // 2. Agregar productos aleatorios
            products.AddRandomProductsToCart(ProductosAgregar);

            // 3. Ir al carrito
            products.GoToCart();
            //LimpiarAnuncios();

            // 4. Validar cantidad de productos
            cartPage.ValidateProductCount(ProductosAgregar);

            // 5. Calcular y validar total
            double total = cartPage.CalculateTotalFromRows();
            cartPage.ValidateTotalGreaterThanZero(total);

            // 6. Scroll + Screenshot
            cartPage.ScrollToFirstRow();
            ScreenshotHelper.TakeScreenshot(Driver, "carrito_final_validado.png");
        }
    }
}