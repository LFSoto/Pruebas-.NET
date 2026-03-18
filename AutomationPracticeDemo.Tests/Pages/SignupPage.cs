using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using AutomationPracticeDemo.Tests.Tests.RegistrarUsuario.Asserts;
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
		private IWebElement TitleRadiobutton => _driver.FindElement(By.CssSelector("div.signup-form h2"));
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
		public void Fill_RegisterForm(String name, String emailRandom)
		{
			 
			NameInput.SendKeys(name);
			EmailInput.SendKeys(emailRandom);
		}

		//Se hace clic en el botón SignUp para enviar el formulario de registro
		public void Click_SignupButton()
		{
			SignupButton.Click();
		}

		//Se hace clic en el TitleRadiobutton para enviar el formulario de registro
		public void Click_TitleRadiobutton()
		{
			TitleRadiobutton.Click();
		}

		//Se completan los datos de la cuenta
		public void Fill_AccountInfo(string name, string password, string firstName, string lastName, string address, string country, string state, string city, string zipcode, string mobileNumber)
		{
			//NameInput.SendKeys(name);
			Password.SendKeys(password);
			FirstName.SendKeys(firstName);
			LastName.SendKeys(lastName);
			Address.SendKeys(address);
			Country.SendKeys(country);
			State.SendKeys(state);
			City.SendKeys(city);
			Zipcode.SendKeys(zipcode);
			MobileNumber.SendKeys(mobileNumber);
		}

		//Se selecciona la fecha de nacimiento en el formulario de registro
		public void Click_DaySelected()
		{
			Day.Click();
			SelectElement DaySelected = new SelectElement(Day);
			DaySelected.SelectByValue("1");
		}

		//Se hace clic en el botón "Create Account" para enviar el formulario de registro
		public void Click_CreateAccuntButton()
		{
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
			wait.Until(drv => CreateAccuntButton.Displayed);
			CreateAccuntButton.Click();
		}
		public void Click_ContinueButton()
		{
			//Se hace clic en el botón "Continue" para finalizar el proceso de registro
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
			wait.Until(drv => ContinueButton.Displayed);
			ContinueButton.Click();
		}

		// Retorna el valor del texto del mensaje de cuenta creada
		public string SuccessMessage()
		{
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
			wait.Until(drv => CreatedMessage.Displayed);
			return CreatedMessage.Text;
		}

	}
}
