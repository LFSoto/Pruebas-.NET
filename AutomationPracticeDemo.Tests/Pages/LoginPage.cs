using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Reflection;
using System.Xml.Linq;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement SingUpBotton => _driver.FindElement(By.XPath("//a[text()=' Signup / Login']"));
        private IWebElement InputEmail => _driver.FindElement(By.XPath("//input[@data-qa='login-email']"));
        private IWebElement InputContrasena => _driver.FindElement(By.XPath("//input[@data-qa='login-password']"));
        private IWebElement IngresarBotton => _driver.FindElement(By.XPath("//button[text()='Login']"));
        private IWebElement ValidarLoginExitoso => _driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]"));
        private IWebElement ValidarLoginFallido => _driver.FindElement(By.CssSelector("form[action=\"/login\"] p"));

        public void LlenarLogin(string correo, string contrasena)
        {
            InputEmail.SendKeys(correo);
            InputContrasena.SendKeys(contrasena);
        }
        public string ValidarInicioExitoso()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(ValidarLoginExitoso));
            return ValidarLoginExitoso.Text;
        }
        public string ValidarInicioFallido()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(ValidarLoginFallido));
            return ValidarLoginFallido.Text;
        }
        public void Enviar()
        {
            IngresarBotton.Click();
        }
        public void BotonSingUp_Click()
        {
            SingUpBotton.Click();
        }
    }
}
