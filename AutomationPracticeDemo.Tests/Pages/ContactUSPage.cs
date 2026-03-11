using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Data.Common;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class ContactUSPage
    {
        private readonly IWebDriver _driver;
        public ContactUSPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Declaración de los elementos de la página
        private IWebElement contactUsTitle => _driver.FindElement(By.CssSelector("#contact-page div.col-sm-12  h2.title.text-center"));
        private IWebElement nameInput => _driver.FindElement(By.CssSelector("input[name=\"name\"]"));
        private IWebElement emailInput => _driver.FindElement(By.CssSelector("input[name=\"email\"]"));
        private IWebElement subjectInput => _driver.FindElement(By.Name("subject"));
        private IWebElement messageInput => _driver.FindElement(By.Id("message"));
        private IWebElement uploadFileInput => _driver.FindElement(By.CssSelector("input[name=\"upload_file\"]"));
        private IWebElement submitButton => _driver.FindElement(By.Name("submit"));
        private IWebElement successMessage => _driver.FindElement(By.CssSelector("div.status.alert.alert-success"));

        //Validar que estamos en la página de contacto
        public string GetTitleContactUSPage()
        {
            return contactUsTitle.Text;
        }
        //Llenar el formulario de contacto
        public void FillContactForm(string name, string email, string subject, string message)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => nameInput.Displayed && emailInput.Displayed && subjectInput.Displayed && messageInput.Displayed);

            nameInput.SendKeys(name);
            emailInput.SendKeys(email);
            subjectInput.SendKeys(subject);
            messageInput.SendKeys(message);
        }
        //Subir un archivo
        public void UploadFile(string filePath)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => uploadFileInput.Displayed);
            uploadFileInput.SendKeys(filePath);
        }

        //Enviar el formulario
        public void SubmitContactForm()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => submitButton.Enabled && submitButton.Displayed);
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
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(driver => successMessage.Displayed);
            return successMessage.Text;
        }
    }


}
