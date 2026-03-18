using System.Text.RegularExpressions;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;

        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
        }

		//Webelements con los que se interactúa 
		private IWebElement AddProduct1 => _driver.FindElement(By.XPath("//div[@class='productinfo text-center']//a[@data-product-id='1']"));
		private IWebElement ContinueShoppingButton => _driver.FindElement(By.XPath("//button[@class='btn btn-success close-modal btn-block']"));
		private IWebElement AddProduct2 => _driver.FindElement(By.XPath("//div[@class='productinfo text-center']//a[@data-product-id='2']"));
		private IWebElement ViewCartLink => _driver.FindElement(By.CssSelector("a[href='/view_cart']"));
		private IWebElement ProceedToCheckoutButton => _driver.FindElement(By.XPath("//a[@class='btn btn-default check_out']"));
		private IWebElement ScrollDown => _driver.FindElement(By.XPath("//td[@class='cart_price']//p"));
		private IWebElement PriceP1 => _driver.FindElement(By.XPath("//tr[@id='product-1']//td[@class='cart_price']//p"));
		private IWebElement PriceP2 => _driver.FindElement(By.XPath("//tr[@id='product-2']//td[@class='cart_price']//p"));
		private IWebElement Cantidad1 => _driver.FindElement(By.XPath("//tr[@id='product-1']//td[@class='cart_quantity']"));
		private IWebElement Cantidad2 => _driver.FindElement(By.XPath("//tr[@id='product-2']//td[@class='cart_quantity']"));
		private IWebElement TotalCart => _driver.FindElement(By.XPath("//tr[last()]//td[last()]//p[@class='cart_total_price']"));

		//Métodos necesarios para interactuar con los elementos de la págin
		public void Select_FirstProduct()
		{
			//Se agrega el primer producto al carrito
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
			wait.Until(drv => AddProduct1.Displayed);
			AddProduct1.Click();
		}

		public void Click_ContinueShopping()
		{
			//Se hace clic en el botón "Continue Shopping" para seguir comprando después de agregar el primer producto al carrito
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
			wait.Until(drv => ContinueShoppingButton.Displayed);
			ContinueShoppingButton.Click();
		}

		public void Select_SecondProduct()
		{
			//Se agrega el segundo producto al carrito
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
			wait.Until(drv => AddProduct2.Displayed);
			AddProduct2.Click();
		}

		public void Click_ViewCart()
		{
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
			//Se hace clic en el button 'View Cart' dentro del modal que aparece después de agregar al carrito
			var modalViewCart = _driver.FindElements(By.CssSelector("#cartModal a[href='/view_cart']")).FirstOrDefault();
			//Se consulta si el enlace "View Cart" está presente dentro del modal
			if (modalViewCart != null)
			{
				wait.Until(drv => modalViewCart.Displayed && modalViewCart.Enabled);
				modalViewCart.Click();
			}
			else
			{
				// Si no se encuentra el enlace dentro del modal, intentar el enlace del menú (fallback)
				wait.Until(drv => ViewCartLink.Displayed && ViewCartLink.Enabled);
				ViewCartLink.Click();
			}
		}

		public void Click_ProceedToCheckout()
		{
			//Se hace clic en el botón "Proceed To Checkout" para ver los productos y el monto total del carrito
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
			wait.Until(drv => ProceedToCheckoutButton.Displayed);
			ProceedToCheckoutButton.Click();
		}

		public void Scroll_Down()
		{
			//Se realiza el  scroll hacia el campo para asegurar que estemos en la seccion correcta de la página

			IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
			js.ExecuteScript("arguments[0].scrollIntoView(true);", ScrollDown);
		}

		public string Return_Price1()
		{
			return PriceP1.Text;
		}

		public string Return_Price2()
		{
			return PriceP2.Text;
		}

		public string Return_Total()
		{
			return TotalCart.Text;
		}

		public int Return_CalculatedTotal()
		{
			//Se valida que el precio total del carrito sea correcto después de agregar los productos y se toma captura de evidencia
			Match matchP1 = Regex.Match(PriceP1.Text, @"\d+");
			int precio1 = 0, precio2 = 0;

			//Se extrae el valor numérico del precio del primer producto utilizando una expresión regular y se convierte a entero
			if (matchP1.Success)
				precio1 = int.Parse(matchP1.Value);

			//Se extrae el valor numérico del precio del primer producto utilizando una expresión regular y se convierte a entero
			Match matchP2 = Regex.Match(PriceP2.Text, @"\d+");
			if (matchP2.Success)
				precio2 = int.Parse(matchP2.Value);

			var subtotal1 = precio1 * int.Parse(Cantidad1.Text);
			var subtotal2 = precio2 * int.Parse(Cantidad2.Text);
			var total = subtotal1 + subtotal2;
			return total;
		}



		/*
		public void Select_FirstProduct()
		{
			//Se valida el primer producto agregado al carrito y se toma captura de evidencia
			ScreenshotHelper.TakeScreenshot(Driver, "firstProduct.png");
			Assert.That(priceP1.Text, Is.EqualTo("Rs. 500"), "El precio del primer producto debería mostrarse");

		}
		public void Select_FirstProduct()
		{
			//Se valida el segundo producto agregado al carrito y se toma captura de evidencia
			ScreenshotHelper.TakeScreenshot(Driver, "secondProduct.png");
			Assert.That(priceP2.Text, Is.EqualTo("Rs. 400"), "El precio del segundo producto debería mostrarse");
		}
		public void Select_FirstProduct()
		{
			//Se valida que el precio total del carrito sea correcto después de agregar los productos y se toma captura de evidencia
			//var total = Driver.FindElement(By.XPath("//p[@class='cart_total_price' and text()='Rs. 900']"));
			Match matchP1 = Regex.Match(priceP1.Text, @"\d+");
			int precio1 = 0, precio2 = 0;
		}
			//Se extrae el valor numérico del precio del primer producto utilizando una expresión regular y se convierte a entero
			if (matchP1.Success)
				precio1 = int.Parse(matchP1.Value);

		//Se extrae el valor numérico del precio del primer producto utilizando una expresión regular y se convierte a entero
		Match matchP2 = Regex.Match(priceP2.Text, @"\d+");
			if (matchP2.Success)
				precio2 = int.Parse(matchP2.Value);

		private IWebElement cantidad1 => _driver.FindElement(By.XPath("//tr[@id='product-1']//td[@class='cart_quantity']"));
		var subtotal1 = precio1 * int.Parse(cantidad1.Text);
		var subtotal2 = precio2 * int.Parse(cantidad2.Text);
		var total = subtotal1 + subtotal2;

		private IWebElement totalCart => _driver.FindElement(By.XPath("//tr[last()]//td[last()]//p[@class='cart_total_price']"));

		ScreenshotHelper.TakeScreenshot(Driver, "totalCart.png");
			Assert.That(totalCart.Text, Is.EqualTo("Rs. " + total), "El precio total debería mostrarse");


       	// Retorna el valor ingresado al campo de texto de la sección 1
		/*public string TextEntered() {
			var textBox1 = TextBox1.GetAttribute("value") ?? string.Empty;
		/return textBox1; 
		}*/

	}
}
