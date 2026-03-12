using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class SignupPage
	{
        private readonly IWebDriver _driver;

        public SignupPage(IWebDriver driver)
        {
            _driver = driver;
        }

		//Webelements con los que se interactúa 
		private IWebElement NameInput => _driver.FindElement(By.CssSelector("input[data-qa='signup-name']"));
		private IWebElement EmailInput => _driver.FindElement(By.CssSelector("input[data-qa='signup-email']"));
		private IWebElement SignupButton => _driver.FindElement(By.CssSelector("button[data-qa='signup-button']"));
		private IWebElement titleRadiobutton => _driver.FindElement(By.Id("id_gender2"));
		private IWebElement Password => _driver.FindElement(By.Id("password"));
		private IWebElement Day => _driver.FindElement(By.Id("days"));
		private IWebElement FirstName => _driver.FindElement(By.Id("first_name"));
		private IWebElement LastName => _driver.FindElement(By.Id("last_name"));
		private IWebElement Address => _driver.FindElement(By.Id("address1"));
		private IWebElement Country => _driver.FindElement(By.Id("country"));
		private IWebElement State => _driver.FindElement(By.Id("state"));
		private IWebElement City => _driver.FindElement(By.Id("city"));
		private IWebElement Zipcode => _driver.FindElement(By.Id("zipcode"));
		private IWebElement MobileNumber => _driver.FindElement(By.Id("mobile_number"));
		private IWebElement CreateAccuntButton => _driver.FindElement(By.CssSelector("button[data-qa='create-account']"));

		private IWebElement CreatedMessage => _driver.FindElement(By.CssSelector("h2[data-qa='account-created']"));
		private IWebElement ContinueButton => _driver.FindElement(By.ClassName("btn-primary"));

		//Métodos necesarios para interactuar con los elementos de la página
		

		//Se llena el campo de nombre
		public void Fill_NameInput(String name)
		{
			 
			NameInput.SendKeys(name);
		}

		//Se llena el campo de correo electrónico con un valor aleatorio para evitar conflictos con usuarios existente
		public void Fill_EmailInput(String emailRandom)
		{
			EmailInput.SendKeys(emailRandom);
		}

		//Se hace clic en el botón SignUp para enviar el formulario de registro
		public void Click_SignupButton()
		{
			SignupButton.Click();
		}

		//Se selecciona el genero en el formulario de registro
		public void Fill_titleRadiobutton()
		{
			titleRadiobutton.Click();
		}

		//Se llena el campo de contraseña en el formulario de registro
		public void Fill_SignupLink(String password)
		{
			Password.SendKeys(password);
		}

		//Se selecciona la fecha de nacimiento en el formulario de registro
		public void Click_DaySelected()
		{
			Day.Click();
			SelectElement DaySelected = new SelectElement(Day);
			DaySelected.SelectByValue("1");
		}

		//Se envia el dato del nombre
		public void Fill_FirstName(String name)
		{
			FirstName.SendKeys(name);
		}

		//Se envia el dato del apellido
		public void Fill_LastName(String apellido)
		{
			LastName.SendKeys(apellido);
		}

		//Se envia el dato de la dirección
		public void Fill_Address(String direccion)
		{
			Address.SendKeys(direccion);
		}

		//Se envia el dato del país
		public void Fill_Country(String pais)
		{
			Country.SendKeys(pais);
		}

		//Se envia el dato del estado
		public void Fill_State(String estado)
		{
			State.SendKeys(estado);
		}

		//Se envia el dato de la ciudad
		public void Fill_City(String ciudad)
		{
			City.SendKeys(ciudad);
		}

		//Se envia el dato del código postal
		public void Fill_Zipcode(String zipCode)
		{
			Zipcode.SendKeys(zipCode);
		}

		//Se envia el dato del número de teléfono
		public void Fill_MobileNumber(String telefono)
		{
			MobileNumber.SendKeys(telefono);
		}

		//Se hace clic en el botón "Create Account" para enviar el formulario de registro
		public void Click_CreateAccuntButton()
		{
			CreateAccuntButton.Click();
		}
		public void Click_ContinueButton()
		{
			//Se hace clic en el botón "Continue" para finalizar el proceso de registro
			ContinueButton.Click();
		}

		// Retorna el valor del texto del mensaje de cuenta creada
		public string SuccessMessage()
		{
			return CreatedMessage.Text;
		}

	

		/*
		[Test]
		public void Should_FillForm()
		{
			var signupPage = new SignupPage(Driver);
			signupPage.FillForm("Juan Perez", "juan@test.com", "88888888", "Costa Rica");
			
			//Se toma una captura de pantalla del formulario de registro antes de enviarlo
			ScreenshotHelper.TakeScreenshot(Driver, "FormDone.png");
			signupPage.Submit();
			
			//Se valida que el mensaje de éxito se muestre después de enviar el formulario de registro y se toma captura de evidencia
			ScreenshotHelper.TakeScreenshot(Driver, "accountCreated.png");
			Assert.That(SuccessMessage().Text, Is.EqualTo("ACCOUNT CREATED!"), "El mensaje de éxito debería mostrarse");

			//Se valida que la opción "Logout" esté visible después de completar el registro y se toma captura de evidencia
			ScreenshotHelper.TakeScreenshot(Driver, "logoutOption.png");
			Assert.That(LogoutText().Text, Is.EqualTo("Logout"), "La opción Logout debería mostrarse");
		}
		*/
	}
}
