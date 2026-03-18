using AutomationPracticeDemo.Tests.Utils;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.MainComponents;
using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Tests._03_AddproductsToCart
{
    public class AddProductsToCartTest : TestBase
    {
        /// <summary>
        /// Ejercicio 3: Agregar productos al carrito y verificar total
        /// </summary
        [Test]
        public void AddProductToCartTest()
        {
            var menuPage = new menuPage(Driver);
            var productsPage = new ProductsPage(Driver);

            // Navegación a la página de productos
            menuPage.ClickProductOption();
            Assert.That(productsPage.ValidatedProductsPage, Is.EqualTo("ALL PRODUCTS"));
            ScreenshotHelper.TakeScreenshot(Driver, "ProductsPage_test.png");

            //Obtener detalles del primer producto y validarlos
            var (firstProductType, firstProductPrice) = productsPage.GetFirstProductDetails();
            Assert.Multiple(() =>
            {
                Assert.That(firstProductType, Is.EqualTo("Blue Top"));
                Assert.That(firstProductPrice, Is.EqualTo("Rs. 500"));
            });

            //Agregar el primer producto al carrito
            productsPage.AddFirstProductToCart();

            //Validar modal de producto agregado
            var (modalTitle, modalMessage) = productsPage.ValidateProductAddedModal();
            Assert.Multiple(() =>
            {
                Assert.That(modalTitle, Is.EqualTo("Added!"));
                Assert.That(modalMessage, Is.EqualTo("Your product has been added to cart."));
            });
            ScreenshotHelper.TakeScreenshot(Driver, "ProductAddedModal_test.png");
            productsPage.ContinueShopping();

            //Obtener detalles del segundo producto y validarlos
            var (secondProductType, secondProductPrice) = productsPage.GetSecondProductDetails();
            Assert.Multiple(() =>
            {
                Assert.That(secondProductType, Is.EqualTo("Men Tshirt"));
                Assert.That(secondProductPrice, Is.EqualTo("Rs. 400"));
            });

            //Agregar el segundo producto al carrito
            productsPage.AddSecondProductToCart();

            //visualizar el carrito
            productsPage.SummitViewCart();
            ScreenshotHelper.TakeScreenshot(Driver, "ViewCart_test.png");

            //Validar que los productos están en el carrito con el precio correcto y el total es correcto
            productsPage.ValidateProductInCart(firstProductType);
            productsPage.ValidateProductInCart(secondProductType);
        }
    }
}
