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
		private IWebElement ErrorMessage => _driver.FindElement(By.XPath("//p[contains(text(),'Your email or password is incorrect!')]"));

		//Métodos necesarios para interactuar con los elementos de la página

		//Se llena el campo de correo electrónico y contraseña
		public void Fill_LoginForm(String email, String password)
		{
			EmailAddress.SendKeys(email);
			PasswordField.SendKeys(password);
		}

		//Se hace clic en el botón "Login" para enviar el formulario de inicio de sesión
		public void Click_LoginButton()
		{
			LoginButton.Click();
		}

		// Retorna el mensaje de éxito que se muestra después de loguearse
		public string Check_LoginLabel()
		{
			return LoginLabel.Text;
		}

		// Retorna el mensaje de error que se muestra después de usar datos incorrectos
		public string Check_ErrorMessage()
		{
			return ErrorMessage.Text;
		}
	}
}
