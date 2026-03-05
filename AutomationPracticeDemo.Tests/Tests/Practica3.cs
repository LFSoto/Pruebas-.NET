using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Tests
{
	public class Practica3 : TestBase
	{
		static readonly int random = new Random().Next(1, 1000);
		string emailRandom = "fran" + random + "@cenfotec.com";

		[Test]
		public void Caso1_RegistroUsuarioNuevo()
		{ 
			var SignupLink = Driver.FindElement(By.CssSelector("a[href='/login']"));
			SignupLink.Click();

			var NameInput = Driver.FindElement(By.CssSelector("input[data-qa='signup-name']"));
			NameInput.SendKeys("Fran");

			var EmailInput = Driver.FindElement(By.CssSelector("input[data-qa='signup-email']"));
			EmailInput.SendKeys(emailRandom);

			var SignupButton = Driver.FindElement(By.CssSelector("button[data-qa='signup-button']"));
			SignupButton.Click();

			var titleRadiobutton = Driver.FindElement(By.Id("id_gender2"));
			titleRadiobutton.Click();

			var NameInput2 = Driver.FindElement(By.Id("name"));
			NameInput2.SendKeys("Fran");

			/*var EmailInput2 = Driver.FindElement(By.Id("email"));
			EmailInput2.SendKeys(emailRandom);*/

			var Password = Driver.FindElement(By.Id("password"));
			Password.SendKeys("Fran");

			var Day = Driver.FindElement(By.Id("days"));
			Day.Click();
			SelectElement DaySelected = new SelectElement(Day);
			DaySelected.SelectByValue("1");


			var FirstName = Driver.FindElement(By.Id("first_name"));
			FirstName.SendKeys("Fran");

			var LastName = Driver.FindElement(By.Id("last_name"));
			LastName.SendKeys("Prueba");

			var Address = Driver.FindElement(By.Id("address1"));
			Address.SendKeys("Mi dirección");

			var Country = Driver.FindElement(By.Id("country"));
			Country.SendKeys("País");

			var State = Driver.FindElement(By.Id("state"));
			State.SendKeys("Estado");

			var City = Driver.FindElement(By.Id("city"));
			City.SendKeys("Ciudad");

			var Zipcode = Driver.FindElement(By.Id("zipcode"));
			Zipcode.SendKeys("159753");

			var MobileNumber = Driver.FindElement(By.Id("mobile_number"));
			MobileNumber.SendKeys("12345678");

			ScreenshotHelper.TakeScreenshot(Driver, "FormDone.png");

			var CreateAccuntButton = Driver.FindElement(By.CssSelector("button[data-qa='create-account']"));
			CreateAccuntButton.Click();

			var CreatedMessage = Driver.FindElement(By.CssSelector("h2[data-qa='account-created']"));
			ScreenshotHelper.TakeScreenshot(Driver, "accountCreated.png");

			Assert.That(CreatedMessage.Text, Is.EqualTo("ACCOUNT CREATED!"), "El mensaje de éxito debería mostrarse");
		
	}

	}

}