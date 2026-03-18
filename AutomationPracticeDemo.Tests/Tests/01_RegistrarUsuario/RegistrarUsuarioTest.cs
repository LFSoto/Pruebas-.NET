using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Tests.RegistrarUsuario.Asserts;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests._01_RegistrarUsuario
{
	public class RegistrarUsuarioTest : TestBase
	{
		
		[Test, TestCaseSource(typeof(UsuarioDataSource), nameof(UsuarioDataSource.RegisterUser))]
		public void Caso1_RegistroUsuarioNuevo(string name, string password, string firstname, string lastName, string address, string country, string state, string city, string zipcode, string mobileNumber)
		{
			var menuPage = new MenuPage(Driver);
			var signupPage = new SignupPage(Driver);

			//Se hace clic en el enlace "Signup / Login" para acceder a la página de registro
			menuPage.Click_SignupLink();

			//Se genera un numero random para crear un email único cada vez que se ejecute el test, evitando así conflictos con registros anteriores
			int random = new Random().Next(1, 1000);
			string emailRandom = "francinni" + random + "@cenfotec.com";

			//Se completa el formulariode nombre y correo electrónico  
			signupPage.Fill_RegisterForm(name, emailRandom);

			//Se toma una captura de pantalla del formulario de registro antes de enviarlo
			ScreenshotHelper.TakeScreenshot(Driver, "FormDone.png");

			//Se hace clic en el botón "Signup" para ir el formulario de registro
			signupPage.Click_SignupButton();

			//Se completa el formulario de registro con la información del usuario y se hace clic en el botón "Create Account"
			signupPage.Fill_AccountInfo( name, password,  firstname,  lastName,  address,  country,  state,  city,  zipcode,  mobileNumber);
			signupPage.Click_CreateAccuntButton();

			//Se valida que el mensaje de éxito se muestre después de enviar el formulario de registro y se toma captura de evidencia
			ScreenshotHelper.TakeScreenshot(Driver, "accountCreated.png");
			Assert.That(signupPage.SuccessMessage(), Is.EqualTo("ACCOUNT CREATED!"), "El mensaje de éxito debería mostrarse");

			//Se hace clic en el botón "Continue" para completar el proceso de registro
			signupPage.Click_ContinueButton();

			//Se valida que la opción "Logout" esté visible después de completar el registro y se toma captura de evidencia
			ScreenshotHelper.TakeScreenshot(Driver, "loggedUser.png");
			Assert.That(menuPage.LogoutText, Is.EqualTo("Logout"), "La opción de Logout debería mostrarse");
		}

	}

}