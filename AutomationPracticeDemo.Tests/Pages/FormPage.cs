using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class FormPage
    {
        private readonly IWebDriver _driver;

        public FormPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement NameInput => _driver.FindElement(By.Id("name"));
        private IWebElement EmailInput => _driver.FindElement(By.Id("email"));
        private IWebElement PhoneInput => _driver.FindElement(By.Id("phone"));
        private IWebElement CountryDropdown => _driver.FindElement(By.Id("country"));
        private IWebElement SubmitButton => _driver.FindElement(By.ClassName("submit-btn"));

        //Tarea, se agregan los identificadores de los elementos.

        //RadioButton
		private IWebElement GenderOption => _driver.FindElement(By.Id("female"));

		//DatePicker
		private IWebElement DatePicker1 => _driver.FindElement(By.Id("datepicker"));

		// Seleccionar el día 
		//private IWebElement DaySelected => _driver.FindElement(By.XPath("//a[@data-date='2'"));

		//Checkbox
		private IWebElement CheckBoxMonday => _driver.FindElement(By.Id("monday"));

        //TextBox
        private IWebElement TextBox1 => _driver.FindElement(By.Id("input1"));

        //Button
        private IWebElement Button1 => _driver.FindElement(By.Id("btn1"));

		public void FillForm(string name, string email, string phone, string country)
        {
            NameInput.SendKeys(name);
            EmailInput.SendKeys(email);
            PhoneInput.SendKeys(phone);
            CountryDropdown.SendKeys(country);
        }

        //Funcion para completar el campo de texto de la sección 1
  		public void FillSection1(string textBox1)
		{
			TextBox1.SendKeys(textBox1);
		}

		// Retorna el valor ingresado al campo de texto de la sección 1
		public string TextEntered() {
			var textBox1 = TextBox1.GetAttribute("value") ?? string.Empty;
			return textBox1; 
		}
			
		//Funcion para seleccionar el genero en el formulario
		public void SelectGender()
		{
			GenderOption.Click();
		}

		// Retorna si el radioButton del género está seleccionado
		public bool IsFemaleRadioButtonSelected => GenderOption.Selected;


		//Funcion para abrir el datepicker
		public void OpenDatePicker1()
		{
			DatePicker1.Click();

		}

		//Funcion para seleccionar el día en el datepicker
		public void SelectDay()
		{
			//Se crea un WebDriverWait para esperar 10 segundos hasta que el elemento esté
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
			IWebElement DaySelected = wait.Until(driver => driver.FindElement(By.XPath("//a[@data-date='2']")));
			DaySelected.Click();
		}

		// Retorna el valor del campo de datepicker luego de seleccionar el día
		public string DateSelected()
		{
			var dateSelected = DatePicker1.GetAttribute("value") ?? string.Empty;
			return dateSelected;
		}

		// Retorna si el checkbox del día lunes está seleccionado
		public bool IsCheckBoxMondaySelected => CheckBoxMonday.Selected;

		//Funcion para seleccionar el checkbox del día lunes
		public void SelectCheckBoxMonday()
		{
			//Primero valida si ya está seleccionado, si no lo está, hace click para seleccionarlo
			if (!CheckBoxMonday.Selected)
				CheckBoxMonday.Click();
		}

		public void Submit()
        {
            SubmitButton.Click();
        }

		//Funcion para hacer click en el botón de la sección 1
		public void SubmitButtonSection1()
		{
			Button1.Click();
		}

		// Retorna el valor del estilo del borde del botón luego de ser cliqueado en la sección 1
		public string ButtonBorderSection1()
		{
			// Obtener el estilo del botón de la sección 1 después de hacer clic
			string buttonBorder = Button1.GetCssValue("border");
			return buttonBorder;
		}
	}
}
