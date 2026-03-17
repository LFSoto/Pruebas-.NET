using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Reflection;
using System.Xml.Linq;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class ContactoPage
    {
        private readonly IWebDriver _driver;

        public ContactoPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement ContactoBoton => _driver.FindElement(By.XPath("//a[text()=' Contact us']"));
        private IWebElement Nombre => _driver.FindElement(By.XPath("//input[@data-qa='name']"));
        private IWebElement Correo => _driver.FindElement(By.XPath("//input[@data-qa='email']"));
        private IWebElement Asunto => _driver.FindElement(By.XPath("//input[@data-qa='subject']"));
        private IWebElement Mensaje => _driver.FindElement(By.XPath("//textarea[@data-qa='message']"));
        private IWebElement Archivo => _driver.FindElement(By.XPath("//input[@name='upload_file']"));
        private IWebElement SubmintBoton => _driver.FindElement(By.XPath("//input[@data-qa='submit-button']"));
        private IWebElement MensajeExito => _driver.FindElement(By.CssSelector(".status.alert.alert-success"));
        public void LlenarContacto(string nombre, string correo, string asunto, string mensaje, string archivo)
        {
            Nombre.SendKeys(nombre);
            Correo.SendKeys(correo);
            Asunto.SendKeys(asunto);
            Mensaje.SendKeys(mensaje);
            Archivo.SendKeys(archivo);
        }
        public string ValidarMensajeExitoso()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(MensajeExito));
            return MensajeExito.Text;
        }
        public void ContactoClick()
        {
            ContactoBoton.Click();
        }
        public string MensajeAlerta()
        {
            var alert = _driver.SwitchTo().Alert();
            return alert?.Text ?? string.Empty;
        }

        public void AceptarAlerta()
        {
            _driver.SwitchTo().Alert().Accept();
        }

        public void SubmintClick()
        {
            SubmintBoton.Click();
        }
    }
}
