using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

		//Webelements con los que se interactúa 
				
		private IWebElement EmailAddress => _driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
		private IWebElement PasswordField => _driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
		private IWebElement LoginButton => _driver.FindElement(By.CssSelector("button[data-qa='login-button']"));
		private IWebElement LoginLabel => _driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]"));

		//Métodos necesarios para interactuar con los elementos de la página

		//Se llena el campo de correo electrónico
		public void Fill_EmailAddress(String email)
		{
			EmailAddress.SendKeys(email);
		}

		//Se llena el campo de contraseña
		public void Fill_PasswordField(String password)
		{
			PasswordField.SendKeys(password);
		}

		//Se hace clic en el botón "Login" para enviar el formulario de inicio de sesión
		public void Click_LoginButton()
		{
			LoginButton.Click();
		}

		// Retorna el mensaje de éxito que se muestra después de enviar el formulario de contacto
		public string Check_LoginLabel()
		{
			return LoginLabel.Text;
		}


		/*
	//Se valida que se muestra el nombre del usuario después de iniciar sesión
	ScreenshotHelper.TakeScreenshot(Driver, "loggedUser.png");
	Assert.That(LoginLabel().Text, Is.EqualTo("Logged in as " + name), "El nombre de usuario debería mostrarse");private IWebElement NameInput => _driver.FindElement(By.Id("name"));
		*/

	}
}
