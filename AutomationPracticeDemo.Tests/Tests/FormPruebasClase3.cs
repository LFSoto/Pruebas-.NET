using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Drawing;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormPruebasClase3
    {
        protected IWebDriver Driver;
        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            //options.AddArgument("--headless=new");
            //options.AddArgument("--window-size=1920,1080");
            //options.AddArgument("--disable-notifications");
            //options.AddArgument("--disable-infobars");
            Driver = new ChromeDriver(options);
            Driver.Navigate().GoToUrl("https://automationexercise.com/");
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
        [Test]
        public void ValidarRegistroNuevoUsuario()
        {
            var singUpBotton = Driver.FindElement(By.XPath("//a[text()=' Signup / Login']"));
            singUpBotton.Click();
            var inputName = Driver.FindElement(By.XPath("//input[@data-qa='signup-name']"));
            inputName.SendKeys("Maria");
            var inputEmail = Driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));
            inputEmail.SendKeys("aria4@qa.com");
            var ingresarBotton = Driver.FindElement(By.XPath("//button[text()='Signup']"));
            ingresarBotton.Click();
            var contrasena = Driver.FindElement(By.Id("password"));
            contrasena.SendKeys("123456");
            var primerNombre = Driver.FindElement(By.Id("first_name"));
            primerNombre.SendKeys("Maria");
            var apellido = Driver.FindElement(By.Id("last_name"));
            apellido.SendKeys("Mora");
            var direccion = Driver.FindElement(By.Id("address1"));
            direccion.SendKeys("calle 21");
            var pais = Driver.FindElement(By.Id("country"));
            pais.SendKeys("Israel");
            var estado = Driver.FindElement(By.Id("state"));
            estado.SendKeys("Israel");
            var cuidad = Driver.FindElement(By.Id("city"));
            cuidad.SendKeys("Tangamandapio");
            var codigoZip = Driver.FindElement(By.Id("zipcode"));
            codigoZip.SendKeys("21212");
            var numeroTelefono = Driver.FindElement(By.Id("mobile_number"));
            numeroTelefono.SendKeys("88888888");
            var crearCuenta = Driver.FindElement(By.XPath("//button[text()='Create Account']"));
            crearCuenta.Click();
            var mensajeCreacion = Driver.FindElement(By.XPath("//h2[@data-qa='account-created']"));
            var mensaje =mensajeCreacion.Text;
            Assert.That(mensaje, Is.EqualTo("ACCOUNT CREATED!"));
            var continuar = Driver.FindElement(By.XPath("//a[@data-qa='continue-button']"));
            continuar.Click();
            var validarlogout = Driver.FindElement(By.XPath("//a[text()=' Logout']"));
            var login = validarlogout.Text;
            Assert.That(login, Is.EqualTo("Logout"));
            var eliminarCuenta = Driver.FindElement(By.XPath("//a[text()=' Delete Account']"));
            eliminarCuenta.Click();
            var continuarEliminacion = Driver.FindElement(By.XPath("//a[@data-qa='continue-button']"));
            continuarEliminacion.Click();
        }

        [Test]
        public void ValidarLogin ()
        {
            var singUpBotton = Driver.FindElement(By.XPath("//a[text()=' Signup / Login']"));
            singUpBotton.Click();
            var inputEmail = Driver.FindElement(By.XPath("//input[@data-qa='login-email']"));
            inputEmail.SendKeys("felipe.soto@email.com");
            var inputContrasena = Driver.FindElement(By.XPath("//input[@data-qa='login-password']"));
            inputContrasena.SendKeys("Password");
            var ingresarBotton = Driver.FindElement(By.XPath("//button[text()='Login']"));
            ingresarBotton.Click();
            var esperaLogin = new WebDriverWait(Driver, TimeSpan.FromSeconds(15)).Until(a=> a.FindElement(By.XPath("//a[contains(text(),'Logged in as')]")));
            var validarlogout = Driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]"));
            var login = validarlogout.Text;
            Assert.That(login, Is.EqualTo("Logged in as Felipe"));
        }
    }

        //[Test]
        //public void Should_FillAndSubmitForm()
        //{
        //    var formPage = new FormPage(Driver);
        //    formPage.FillForm("Juan Perez", "juan@test.com", "88888888", "Costa Rica");
        //    formPage.Submit();

        //    ScreenshotHelper.TakeScreenshot(Driver, "form_test.png");
        //    Assert.Pass("Formulario llenado y enviado.");
        //}
        //[Test]
        //public void VerificarRadioButtom_Genero()
        //{
        //    var formPage = new FormPage(Driver);
        //    formPage.FillGender("Male");

        //    ScreenshotHelper.TakeScreenshot(Driver, "form_RadioButtom.png");
        //    Assert.Pass("Seleccionado el genero correctamente");
        //}
        //[Test]
        //public void VerificarCheckBox_Days()
        //{
        //    var formPage = new FormPage(Driver);
        //    string[] dias = { "monday", "friday", "sunday" };
        //    foreach (var dia in dias){
        //        formPage.FillDays(dia);
        //    }

        //    ScreenshotHelper.TakeScreenshot(Driver, "form_CheckBoxDays.png");
        //    Assert.Pass($"Seleccionado los dias {string.Join(",", dias)} correctamente");
        //}
        //[Test]
        //public void VerificarRangeDates()
        //{
        //    var formPage = new FormPage(Driver);
        //    formPage.FillRangeDates(new DateTime(2026,02,03).ToString("dd-MM-yyyy"), new DateTime(2026, 03, 03).ToString("dd-MM-yyyy"));
        //    formPage.SubmitDates();

        //    ScreenshotHelper.TakeScreenshot(Driver, "form_RangeDate.png");
        //    Assert.Pass("Seleccionado el rango de fechas y submit.");
        //}
        //[Test]
        //public void VerificarPromptAlert()
        //{
        //    var formPage = new FormPage(Driver);
        //    formPage.OpenAlertPrompt();
        //    // Esperar un instante para que el alerta aparezca
        //    Thread.Sleep(200);
        //    ScreenshotHelper.TakeScreenshot(Driver, "form_PromptAlert.png",true);

        //    IAlert alert = Driver.SwitchTo().Alert();
        //    string actualAlertText = alert.Text;
        //    Assert.That(actualAlertText, Is.EqualTo("Please enter your name:"));
        //    alert.Accept();

        //}
 }

