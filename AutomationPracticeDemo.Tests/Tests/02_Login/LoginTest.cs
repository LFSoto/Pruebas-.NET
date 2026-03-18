using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using AutomationPracticeDemo.Tests.Tests.Login.Asserts;

namespace AutomationPracticeDemo.Tests.Tests._02_Login
{

	public class LoginTest : TestBase
	{

		[Test, TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.UsersIsValid))]
		public void Caso2_LoginTest(string email, string password, bool isValid)
		{
			var menuPage = new MenuPage(Driver);
			var loginPage = new LoginPage(Driver);

			//Se hace clic en el enlace "Signup / Login" para acceder a la página de registro
			menuPage.Click_SignupLink();
			loginPage.Fill_LoginForm(email, password);
			loginPage.Click_LoginButton();

			//Se valida si el dato es valido o no para ver que elemento debe ser validado en cada caso
			if (isValid)
			{
				//Se valida que se muestra el nombre del usuario después de iniciar sesión
				ScreenshotHelper.TakeScreenshot(Driver, "loggedUser.png");
				Assert.That(menuPage.LogoutText, Is.EqualTo("Logout"), "La opción de Logout debería mostrarse");
			}
			else 
			{
				//Se valida que se muestra el mensaje de error cuando se ingresa con datos incorrectos
				ScreenshotHelper.TakeScreenshot(Driver, "ErrorMessageAfterLogin.png");
				Assert.That(loginPage.Check_ErrorMessage, Is.EqualTo("Your email or password is incorrect!"), "El mensaje de error debería mostrarse");
			}
		}

	}

}