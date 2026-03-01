using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using NUnit.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormTests : TestBase
    {

        [Test]
        public void Full_Form()
        {
            var formPage = new FormPage(Driver);
            formPage.FillForm("Juan Perez", "juan@test.com", "88888888", "Costa Rica");
            ScreenshotHelper.TakeScreenshot(Driver, "form_test.png");
            Thread.Sleep(1000);

            //TEXTBOX
            var name = Driver.FindElement(By.Id("name"));
            var email = Driver.FindElement(By.Id("email"));
            var phone = Driver.FindElement(By.Id("phone"));
            var address = Driver.FindElement(By.Id("textarea"));

            name.Clear();
            name.SendKeys("Silvia Muñoz");
            Assert.AreEqual("Silvia Muñoz", name.GetAttribute("value"));
            ScreenshotHelper.TakeScreenshot(Driver, "name.png");
            Thread.Sleep(1000);
      

            email.Clear();
            email.SendKeys("silvia@gmail.com");
            Assert.AreEqual("silvia@gmail.com", email.GetAttribute("value"));
            ScreenshotHelper.TakeScreenshot(Driver, "email.png");
            Thread.Sleep(1000);
     

            phone.Clear();
            phone.SendKeys("8954-4363");
            Assert.AreEqual("8954-4363", phone.GetAttribute("value"));
            ScreenshotHelper.TakeScreenshot(Driver, "phone.png");
            Thread.Sleep(1000);
          

            address.Clear();
            address.SendKeys("Guanacaste");
            Assert.AreEqual("Guanacaste", address.GetAttribute("value"));
            ScreenshotHelper.TakeScreenshot(Driver, "adress.png");
            Thread.Sleep(1000);
          

            //RADIOBUTTON
            var radioMale = Driver.FindElement(By.Id("male"));
            radioMale.Click();
            Assert.IsTrue(radioMale.Selected);
            ScreenshotHelper.TakeScreenshot(Driver, "male.png");
            Thread.Sleep(1000);

            var radioFemale = Driver.FindElement(By.Id("female"));
            radioFemale.Click();
            Assert.IsTrue(radioFemale.Selected);
            ScreenshotHelper.TakeScreenshot(Driver, "female.png");
            Thread.Sleep(1000);

            //DROPDOWN
            var dropdown = Driver.FindElement(By.Id("country"));
            var select = new SelectElement(dropdown);
            select.SelectByValue("usa");
            Assert.AreEqual("United States", select.SelectedOption.Text);
            ScreenshotHelper.TakeScreenshot(Driver, "usa.png");
            Thread.Sleep(1000);
            select.SelectByValue("canada");
            Assert.AreEqual("Canada", select.SelectedOption.Text);
            ScreenshotHelper.TakeScreenshot(Driver, "canada.png");
            Thread.Sleep(1000);
            select.SelectByValue("uk");
            Assert.AreEqual("United Kingdom", select.SelectedOption.Text);
            ScreenshotHelper.TakeScreenshot(Driver, "uk.png");
            Thread.Sleep(1000);
            select.SelectByValue("germany");
            Assert.AreEqual("Germany", select.SelectedOption.Text);
            ScreenshotHelper.TakeScreenshot(Driver, "germany.png");
            Thread.Sleep(1000);
            select.SelectByValue("france");
            Assert.AreEqual("France", select.SelectedOption.Text);
            ScreenshotHelper.TakeScreenshot(Driver, "france.png");
            Thread.Sleep(1000);
            select.SelectByValue("australia");
            Assert.AreEqual("Australia", select.SelectedOption.Text);
            ScreenshotHelper.TakeScreenshot(Driver, "australia.png");
            Thread.Sleep(1000);
            select.SelectByValue("japan");
            Assert.AreEqual("Japan", select.SelectedOption.Text);
            ScreenshotHelper.TakeScreenshot(Driver, "japan.png");
            Thread.Sleep(1000);
            select.SelectByValue("china");
            Assert.AreEqual("China", select.SelectedOption.Text);
            ScreenshotHelper.TakeScreenshot(Driver, "china.png");
            Thread.Sleep(1000);
            select.SelectByValue("brazil");
            Assert.AreEqual("Brazil", select.SelectedOption.Text);
            ScreenshotHelper.TakeScreenshot(Driver, "brazil.png");
            Thread.Sleep(1000);
            select.SelectByValue("india");
            Assert.AreEqual("India", select.SelectedOption.Text);
            ScreenshotHelper.TakeScreenshot(Driver, "india.png");
            Thread.Sleep(1000);

            //DATEPICKER
            var datePicker = Driver.FindElement(By.CssSelector(".hasDatepicker"));
            datePicker.Click(); // abre el calendario
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));

            var day = wait.Until(d => d.FindElement(By.CssSelector("a[data-date='28']")));
            day.Click();
            // Validar que el campo quedó con la fecha
            Assert.AreEqual("02/28/2026", datePicker.GetAttribute("value"));
            ScreenshotHelper.TakeScreenshot(Driver, "datepicker.png");

            formPage.Submit();
            Thread.Sleep(2000);
            Assert.Pass("Formulario llenado y enviado.");
        }

    }
}
