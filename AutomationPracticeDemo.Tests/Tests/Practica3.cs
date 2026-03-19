using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace AutomationPracticeDemo.Tests.Tests
{
	public class Practica3 
	{
		//Driver para la automatización de pruebas
		protected IWebDriver Driver;

		[SetUp]
		public void Setup()
		{
			//Configuración del driver de Chrome con opciones personalizadas para la automatización de pruebas
			var options = new ChromeOptions();
			options.AddArgument("--start-maximized");
			options.AddArgument("--disable-notifications");
			options.AddArgument("--disable-infobars");
			options.AddArgument("--headless=new"); //Se usa para ejecutar las pruebas sin levantar la interfaz
			options.AddArgument("--window-size=1920,1080");
			Driver = new ChromeDriver(options);
			Driver.Navigate().GoToUrl("https://automationexercise.com");
		}

		[TearDown]
		public void TearDown()
		{
			if (Driver != null)
			{
				Driver.Quit();
				Driver.Dispose();
			}
		}

		//Variables para los casos de prueba
		static readonly int random = new Random().Next(1, 1000);
		string emailRandom = "fran" + random + "@cenfotec.com";
		string name = "abcd1";
		string apellido = "Prueba";
		string password = "123abc**1";
		string email = "abcd1@abcd1.com";
		string rutaImagen = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resource\Paisaje.jpg"));

		[Test]
		public void Caso1_RegistroUsuarioNuevo()
		{
			//Se hace clic en el enlace "Signup / Login" para acceder a la página de registro
			var SignupLink = Driver.FindElement(By.CssSelector("a[href='/login']"));
			SignupLink.Click();

			//Se llena el campo de nombre 
			var NameInput = Driver.FindElement(By.CssSelector("input[data-qa='signup-name']"));
			NameInput.SendKeys(name);

			//Se llena el campo de correo electrónico con un valor aleatorio para evitar conflictos con usuarios existentes
			var EmailInput = Driver.FindElement(By.CssSelector("input[data-qa='signup-email']"));
			EmailInput.SendKeys(emailRandom);

			//Se hace clic en el botón SignUp para enviar el formulario de registro
			var SignupButton = Driver.FindElement(By.CssSelector("button[data-qa='signup-button']"));
			SignupButton.Click();

			//Se selecciona el genero en el formulario de registro
			var titleRadiobutton = Driver.FindElement(By.Id("id_gender2"));
			titleRadiobutton.Click();

			//Se llena el campo de contraseña en el formulario de registro
			var Password = Driver.FindElement(By.Id("password"));
			Password.SendKeys(password);

			//Se selecciona la fecha de nacimiento en el formulario de registro
			var Day = Driver.FindElement(By.Id("days"));
			Day.Click();
			SelectElement DaySelected = new SelectElement(Day);
			DaySelected.SelectByValue("1");

			//Se envia el dato del nombre
			var FirstName = Driver.FindElement(By.Id("first_name"));
			FirstName.SendKeys(name);

			//Se envia el dato del apellido
			var LastName = Driver.FindElement(By.Id("last_name"));
			LastName.SendKeys(apellido);

			//Se envia el dato de la dirección
			var Address = Driver.FindElement(By.Id("address1"));
			Address.SendKeys("Mi dirección");

			//Se envia el dato del país
			var Country = Driver.FindElement(By.Id("country"));
			Country.SendKeys("País");

			//Se envia el dato del estado
			var State = Driver.FindElement(By.Id("state"));
			State.SendKeys("Estado");

			//Se envia el dato de la ciudad
			var City = Driver.FindElement(By.Id("city"));
			City.SendKeys("Ciudad");

			//Se envia el dato del código postal
			var Zipcode = Driver.FindElement(By.Id("zipcode"));
			Zipcode.SendKeys("159753");

			//Se envia el dato del número de teléfono
			var MobileNumber = Driver.FindElement(By.Id("mobile_number"));
			MobileNumber.SendKeys("12345678");

			//Se toma una captura de pantalla del formulario de registro antes de enviarlo
			ScreenshotHelper.TakeScreenshot(Driver, "FormDone.png");

			//Se hace clic en el botón "Create Account" para enviar el formulario de registro
			var CreateAccuntButton = Driver.FindElement(By.CssSelector("button[data-qa='create-account']"));
			CreateAccuntButton.Click();

			//Se valida que el mensaje de éxito se muestre después de enviar el formulario de registro y se toma captura de evidencia
			var CreatedMessage = Driver.FindElement(By.CssSelector("h2[data-qa='account-created']"));
			ScreenshotHelper.TakeScreenshot(Driver, "accountCreated.png");
			Assert.That(CreatedMessage.Text, Is.EqualTo("ACCOUNT CREATED!"), "El mensaje de éxito debería mostrarse");

			//Se hace clic en el botón "Continue" para finalizar el proceso de registro
			var ContinueButton = Driver.FindElement(By.ClassName("btn-primary"));
			ContinueButton.Click();

			//Se valida que la opción "Logout" esté visible después de completar el registro y se toma captura de evidencia
			var logoutOption = Driver.FindElement(By.CssSelector("a[href='/logout']"));
			ScreenshotHelper.TakeScreenshot(Driver, "logoutOption.png");
			Assert.That(logoutOption.Text, Is.EqualTo("Logout"), "La opción Logout debería mostrarse");

		}


		[Test]
		public void Caso2_LoginUsuarioExistente()
		{
			//Se hace clic en el enlace "Signup / Login" para acceder a la página de inicio de sesión
			var SignupLink = Driver.FindElement(By.CssSelector("a[href='/login']"));
			SignupLink.Click();

			//Se llena el campo de correo electrónico 
			var emailAddress = Driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
			emailAddress.SendKeys(email);

			//Se llena el campo de contraseña
			var passwordField = Driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
			passwordField.SendKeys(password);

			//Se hace clic en el botón "Login" para enviar el formulario de inicio de sesión
			var loginButton = Driver.FindElement(By.CssSelector("button[data-qa='login-button']"));
			loginButton.Click();

			//Se valida que se muestra el nombre del usuario después de iniciar sesión
			var loginLabel = Driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]"));
			WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			wait.Until(drv => loginLabel.Displayed);
			ScreenshotHelper.TakeScreenshot(Driver, "loggedUser.png");
			Assert.That(loginLabel.Text, Is.EqualTo("Logged in as " + name), "El nombre de usuario debería mostrarse");

		}

		[Test]
		public void Caso3_AgregarProductosAlCarrito()
		{

			Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			//Se hace clic en el enlace "Signup / Login" para acceder a la página de inicio de sesión
			var SignupLink = Driver.FindElement(By.CssSelector("a[href='/login']"));
			SignupLink.Click();

			//Se llena el campo de correo electrónico 
			var emailAddress = Driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
			emailAddress.SendKeys(email);

			//Se llena el campo de contraseña
			var passwordField = Driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
			passwordField.SendKeys(password);

			//Se hace clic en el botón "Login" para enviar el formulario de inicio de sesión
			var loginButton = Driver.FindElement(By.CssSelector("button[data-qa='login-button']"));
			loginButton.Click();

			//Se hace clic en la opción "Products" para acceder a la página de productos
			var productsOption = Driver.FindElement(By.CssSelector("a[href='/products']"));
			productsOption.Click();

			//Se agrega el primer producto al carrito
			var addProduct1 = Driver.FindElement(By.XPath("//div[@class='productinfo text-center']//a[@data-product-id='1']"));
			WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			wait.Until(drv => addProduct1.Displayed);
			addProduct1.Click();

			//Se hace clic en el botón "Continue Shopping" para seguir comprando después de agregar el primer producto al carrito
			var continueShoppingButton = Driver.FindElement(By.XPath("//button[@class='btn btn-success close-modal btn-block']"));
			wait.Until(drv => continueShoppingButton.Displayed);
			continueShoppingButton.Click();

			//Se agrega el segundo producto al carrito
			var addProduct2 = Driver.FindElement(By.XPath("//div[@class='productinfo text-center']//a[@data-product-id='2']"));
			wait.Until(drv => addProduct2.Displayed);
			addProduct2.Click();

			//Se hace clic en el button 'View Cart' dentro del modal que aparece después de agregar al carrito
			var modalViewCart = Driver.FindElements(By.CssSelector("#cartModal a[href='/view_cart']")).FirstOrDefault();

			//Se consulta si el enlace "View Cart" está presente dentro del modal
			if (modalViewCart != null)
			{
				wait.Until(drv => modalViewCart.Displayed && modalViewCart.Enabled);
				modalViewCart.Click();
			}
			else
			{
				// Si no se encuentra el enlace dentro del modal, intentar el enlace del menú (fallback)
				var viewCartLink = Driver.FindElement(By.CssSelector("a[href='/view_cart']"));
				wait.Until(drv => viewCartLink.Displayed && viewCartLink.Enabled);
				viewCartLink.Click();
			}

			//Se hace clic en el botón "Proceed To Checkout" para ver los productos y el monto total del carrito
			var proceedToCheckoutButton = Driver.FindElement(By.XPath("//a[@class='btn btn-default check_out']"));
			wait.Until(drv => proceedToCheckoutButton.Displayed);
			proceedToCheckoutButton.Click();

			//Se realiza el  scroll hacia el campo para asegurar que estemos en la seccion correcta de la página
			var scrollDown = Driver.FindElement(By.XPath("//td[@class='cart_price']//p"));
			IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
			js.ExecuteScript("arguments[0].scrollIntoView(true);", scrollDown);

			//Se valida el primer producto agregado al carrito y se toma captura de evidencia
			var priceP1 = Driver.FindElement(By.XPath("//tr[@id='product-1']//td[@class='cart_price']//p"));
			ScreenshotHelper.TakeScreenshot(Driver, "firstProduct.png");
			Assert.That(priceP1.Text, Is.EqualTo("Rs. 500"), "El precio del primer producto debería mostrarse");
			
			//Se valida el segundo producto agregado al carrito y se toma captura de evidencia
			var priceP2 = Driver.FindElement(By.XPath("//tr[@id='product-2']//td[@class='cart_price']//p"));
			ScreenshotHelper.TakeScreenshot(Driver, "secondProduct.png");
			Assert.That(priceP2.Text, Is.EqualTo("Rs. 400"), "El precio del segundo producto debería mostrarse");

			//Se valida que el precio total del carrito sea correcto después de agregar los productos y se toma captura de evidencia
			//var total = Driver.FindElement(By.XPath("//p[@class='cart_total_price' and text()='Rs. 900']"));
			Match matchP1 = Regex.Match(priceP1.Text, @"\d+");
			int precio1 = 0, precio2 = 0;

			//Se extrae el valor numérico del precio del primer producto utilizando una expresión regular y se convierte a entero
			if (matchP1.Success)
				precio1 = int.Parse(matchP1.Value);

			//Se extrae el valor numérico del precio del primer producto utilizando una expresión regular y se convierte a entero
			Match matchP2 = Regex.Match(priceP2.Text, @"\d+");
			if (matchP2.Success)
				precio2 = int.Parse(matchP2.Value);
		
			var cantidad1 = Driver.FindElement(By.XPath("//tr[@id='product-1']//td[@class='cart_quantity']"));
			var cantidad2 = Driver.FindElement(By.XPath("//tr[@id='product-2']//td[@class='cart_quantity']"));
			var subtotal1 = precio1 * int.Parse(cantidad1.Text);
			var subtotal2 = precio2 * int.Parse(cantidad2.Text);
			var total =  subtotal1 + subtotal2;

			var totalCart = Driver.FindElement(By.XPath("//tr[last()]//td[last()]//p[@class='cart_total_price']"));

			ScreenshotHelper.TakeScreenshot(Driver, "totalCart.png");
			Assert.That(totalCart.Text, Is.EqualTo("Rs. " + total), "El precio total debería mostrarse");
			
		}

		[Test]
		public void Caso4_ContactUsForm()
		{
			//Se hace clic en el enlace "Contact Us" para acceder al formulario de contacto
			var contactUsLink = Driver.FindElement(By.CssSelector("a[href='/contact_us']"));
			contactUsLink.Click();

			//Se llena el campo del formulario de contacto con el nombre
			var nameField = Driver.FindElement(By.CssSelector("input[data-qa='name']"));
			nameField.SendKeys(name);

			//Se llena el campo del formulario de contacto con el email
			var emailField = Driver.FindElement(By.CssSelector("input[data-qa='email']"));
			emailField.SendKeys(email);

			//Se llena el campo del formulario de contacto con el subject
			var subjectField = Driver.FindElement(By.CssSelector("input[data-qa='subject']"));
			subjectField.SendKeys(name);

			//Se llena el campo del formulario de contacto con el mensaje
			var messageField = Driver.FindElement(By.Id("message"));
			messageField.SendKeys(name);

			//Se selecciona el archivo a subir 
			var archivo = Driver.FindElement(By.Name("upload_file"));
			//Console.WriteLine("archivo: " + archivo + "Ruta del archivo: " + rutaImagen);
			//WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			//wait.Until(drv => archivo.Displayed);
			archivo.SendKeys(rutaImagen);

			//Se da click en el botón de submit para enviar el formulario de contacto
			var submitButton = Driver.FindElement(By.CssSelector("input[data-qa='submit-button']"));
			submitButton.Click();

			//Se acpeta la alerta que aparece después de enviar el formulario de contacto
			var alertAccept = Driver.SwitchTo().Alert();
			alertAccept.Accept();

			//Se valida que el mensaje de éxito se muestre después de enviar el formulario de contacto y se toma captura de evidencia
			var successMessage = Driver.FindElement(By.XPath("//div[contains(text(),'Success! Your details have been submitted successfully.')]"));
			ScreenshotHelper.TakeScreenshot(Driver, "successContactUs.png");
			Assert.That(successMessage.Text, Is.EqualTo("Success! Your details have been submitted successfully."), "El mensaje de exito debería mostrarse");

		}

		[Test]
		public void Caso5_SuscripcionAlNewsletter()
		{
			//Se realiza el  scroll hacia el campo de suscripción para asegurar que estemos en la seccion correcta de la página
			var scrollDown  = Driver.FindElement(By.Id("susbscribe_email"));
			IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
			js.ExecuteScript("arguments[0].scrollIntoView(true);", scrollDown);

			//Se ingresa el correo electrónico en el campo 
			var emailField = Driver.FindElement(By.Id("susbscribe_email"));
			emailField.SendKeys(email);

			//Se hace clic en el botón de submit para enviar la suscripción
			var submitButton = Driver.FindElement(By.Id("subscribe"));
			submitButton.Click();

			//Se va;ida que el mensaje de éxito sea visible después de enviar la suscripción y se toma captua de evidencia
			var successMessage = Driver.FindElement(By.XPath("//div[contains(text(),'You have been successfully subscribed!')]"));
			ScreenshotHelper.TakeScreenshot(Driver, "suscribeMessage.png");
			Assert.That(successMessage.Text, Is.EqualTo("You have been successfully subscribed!"), "El mensaje de exito debería mostrarse");
		}
	}

}