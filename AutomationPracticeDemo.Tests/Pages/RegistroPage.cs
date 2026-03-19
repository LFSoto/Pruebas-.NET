using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using AutomationPracticeDemo.Tests.Utils;
using System.Reflection;
using System.Xml.Linq;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class RegistroPage
    {
        private readonly IWebDriver _driver;

        public RegistroPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement SingUpBotton => _driver.FindElement(By.XPath("//a[text()=' Signup / Login']"));
        private IWebElement InputNombre_Registro => _driver.FindElement(By.XPath("//input[@data-qa='signup-name']"));
        private IWebElement InputCorreo_Registro => _driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));
        private IWebElement IngresarBotton_Registro => _driver.FindElement(By.XPath("//button[text()='Signup']"));
        private IWebElement Form => _driver.FindElement(By.XPath("//form[@action='/signup']"));
        private IWebElement Contrasena => _driver.FindElement(By.Id("password"));
        private IWebElement PrimerNombre => _driver.FindElement(By.Id("first_name"));
        private IWebElement Apellido => _driver.FindElement(By.Id("last_name"));
        private IWebElement Direccion => _driver.FindElement(By.Id("address1"));
        private IWebElement Pais => _driver.FindElement(By.Id("country"));
        private IWebElement Estado => _driver.FindElement(By.Id("state"));
        private IWebElement Cuidad => _driver.FindElement(By.Id("city"));
        private IWebElement CodigoZip => _driver.FindElement(By.Id("zipcode"));
        private IWebElement NumeroTelefono => _driver.FindElement(By.Id("mobile_number"));
        private IWebElement CrearCuenta_Boton => _driver.FindElement(By.XPath("//button[text()='Create Account']"));
        private IWebElement MensajeCreacion => _driver.FindElement(By.XPath("//h2[@data-qa='account-created']"));
        private IWebElement Continuar_Boton => _driver.FindElement(By.XPath("//a[@data-qa='continue-button']"));
        private IWebElement Validarlogout => _driver.FindElement(By.XPath("//a[text()=' Logout']"));
        private IWebElement EliminarCuenta_Boton => _driver.FindElement(By.XPath("//a[text()=' Delete Account']"));
        private IWebElement ContinuarEliminacion_Boton => _driver.FindElement(By.XPath("//a[@data-qa='continue-button']"));

        public void BotonSingUp_Click()
        {
            EsperasHelper.Esperar(_driver, SingUpBotton,30);
            SingUpBotton.Click();
        }
        
        public void LlenarRegistro(string contrasena, string primerNombre, string apellido, string direccion, string pais,
            string estado, string ciudad, string codigoZip, string numeroTelefono)
        {
            EsperasHelper.Esperar(_driver, Form, 30);
            Contrasena.SendKeys(contrasena);
            PrimerNombre.SendKeys(primerNombre);
            Apellido.SendKeys(apellido);
            Direccion.SendKeys(direccion);
            Pais.SendKeys(pais);
            Estado.SendKeys(estado);
            Cuidad.SendKeys(ciudad);
            CodigoZip.SendKeys(codigoZip);
            NumeroTelefono.SendKeys(numeroTelefono);
           
        }
        public void LlenarRegistroIngresar(string inputNombre_Registro, string inputCorreo_Registro)
        {
            InputNombre_Registro.SendKeys(inputNombre_Registro);
            InputCorreo_Registro.SendKeys(inputCorreo_Registro);

        }
        //public string ValidarLogin()
        //{
        //    var esperaLogin = new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(a => a.FindElement(By.XPath("//a[contains(text(),'Logged in as')]")));
        //    var validarlogout = _driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]"));
        //    return validarlogout.Text;
        //}
        public void EnviarIngresoRegistroBoton()
        {
            IngresarBotton_Registro.Click();
        }
        public void CrearCuentaBoton()
        {
            EsperasHelper.Esperar(_driver, CrearCuenta_Boton, 30);
            CrearCuenta_Boton.Click();
        }
        public void ContinuatBoton()
        {
            EsperasHelper.Esperar(_driver, Continuar_Boton, 30);
            Continuar_Boton.Click();
        }
        public void EliminarCuentaBoton()
        {
            EsperasHelper.Esperar(_driver, EliminarCuenta_Boton, 30);
            EliminarCuenta_Boton.Click();
        }
        public void ContinuarEliminacionBoton()
        {
            EsperasHelper.Esperar(_driver, ContinuarEliminacion_Boton, 30);
            ContinuarEliminacion_Boton.Click();
        }
        public string ValidarMensajeCreacion()
        {
            EsperasHelper.Esperar(_driver, MensajeCreacion, 30);
            return MensajeCreacion.Text;
        }
        public string ValidarLogout()
        {
            EsperasHelper.Esperar(_driver, Validarlogout, 30);
            return Validarlogout.Text;
        }
    }
}
