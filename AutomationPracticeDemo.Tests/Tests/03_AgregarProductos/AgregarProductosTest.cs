using global::AutomationPracticeDemo.Tests.Pages;
using global::AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests._03_AgregarProductos
{
		public class AgregarProductosTest : TestBase
		{

		[Test]
		public void Caso3_AgregarProductos()
		{
			var menuPage = new MenuPage(Driver);
			var loginPage = new LoginPage(Driver);
			var productsPage = new ProductsPage(Driver);

			//Se hace clic en el enlace "Signup / Login" para acceder a la página de registro	
			menuPage.Click_SignupLink();

			//Se llenan los campos de inicio de sesión con las credenciales proporcionadas y se hace clic en el botón "Login"
			loginPage.Fill_LoginForm("abcd1@abcd1.com", "123abc**1");
			loginPage.Click_LoginButton();

			//Se da clic en la opción "Products" para acceder a la página de productos
			menuPage.Click_ProductsOption();

			//Se selecciona el primer producto y se hace clic en "Continue Shopping" para seguir navegando por los productos
			productsPage.Select_FirstProduct();
			productsPage.Click_ContinueShopping();

			//Se selecciona el segundo producto y se hace clic en "View Cart" para revisar los productos agregados al carrito
			productsPage.Select_SecondProduct();
			productsPage.Click_ViewCart();

			//Se hace clic en "Proceed To Checkout" para avanzar al proceso de pago y se desplaza hacia abajo para revisar los detalles del carrito
			productsPage.Click_ProceedToCheckout();
			productsPage.Scroll_Down();

			//Se toman capturas de pantalla para verificar que los precios de los productos y el total del carrito se muestren correctamente
			ScreenshotHelper.TakeScreenshot(Driver, "firstProduct.png");
			Assert.That(productsPage.Return_Price1, Is.EqualTo("Rs. 500"), "El precio del segundo producto debería mostrarse");

			ScreenshotHelper.TakeScreenshot(Driver, "secondProduct.png");
			Assert.That(productsPage.Return_Price2, Is.EqualTo("Rs. 400"), "El precio del segundo producto debería mostrarse");
				
			ScreenshotHelper.TakeScreenshot(Driver, "totalCart.png");
			Assert.That(productsPage.Return_Total, Is.EqualTo("Rs. " + productsPage.Return_CalculatedTotal()), "El precio total debería mostrarse");

			}

		}

	}
