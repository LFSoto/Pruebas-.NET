using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Reflection;
using System.Xml.Linq;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class SuscripcionPage
    {
        private readonly IWebDriver _driver;

        public SuscripcionPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement InputEmail => _driver.FindElement(By.Id("susbscribe_email"));
        private IWebElement EnviarBotton => _driver.FindElement(By.Id("subscribe"));
        private IWebElement MensajeExito => _driver.FindElement(By.CssSelector(".alert-success.alert"));

        public void LlenarSuscripcion(string correo)
        {
            InputEmail.SendKeys(correo);
        }
        public string ValidarMensajeExitoso()
        {
            EsperasHelper.Esperar(_driver, MensajeExito, 30);

            return MensajeExito.Text;
        }
        public void Enviar()
        {
            EnviarBotton.Click();
        }
    }
}
