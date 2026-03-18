using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Data.Common;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class ContactUSPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public ContactUSPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // Declaración de los elementos de la página
        //Se implementa un localizador relativo por medio de XPath para el título de la página de contacto
        private IWebElement contactUsTitle => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@id=\"contact-page\"]/div[1]//h2")));
        private IWebElement nameInput => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=\"name\"]")));
        private IWebElement emailInput => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("input[name=\"email\"]")));
        private IWebElement subjectInput => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Name("subject")));
        private IWebElement messageInput => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("message")));
        private IWebElement uploadFileInput => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=\"upload_file\"]")));
        private IWebElement submitButton => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("submit")));
        private IWebElement successMessage => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div.status.alert.alert-success")));

        //Validar que estamos en la página de contacto
        public string GetTitleContactUSPage()
        {

            return contactUsTitle.Text;
        }
        //Llenar el formulario de contacto
        public void FillContactForm(string name, string email, string subject, string message)
        {
            nameInput.SendKeys(name);
            emailInput.SendKeys(email);
            subjectInput.SendKeys(subject);
            messageInput.SendKeys(message);
        }
        //Subir un archivo
        public void UploadFile(string filePath)
        {
            uploadFileInput.SendKeys(filePath);
        }

        //Enviar el formulario
        public void SubmitContactForm()
        {
            submitButton.Click();
        }

        //Validar el mensaje de éxito
        public string GetAlertMessage()
        {
            var alert = _driver.SwitchTo().Alert();
            return alert?.Text ?? string.Empty;
        }
        public void AcceptAlert()
        {
            _driver.SwitchTo().Alert().Accept();
        }
        public string GetSuccessMessage()
        {
            return successMessage.Text;
        }
    }


}
