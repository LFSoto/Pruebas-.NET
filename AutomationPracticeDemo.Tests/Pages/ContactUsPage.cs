using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class ContactUsPage
    {

        private readonly IWebDriver _driver;

        public ContactUsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Variables para los elementos de la página de contacto
        private IWebElement nameInput => _driver.FindElement(By.CssSelector("input[data-qa='name']"));
        private IWebElement emailInput => _driver.FindElement(By.CssSelector("input[data-qa='email']"));
        private IWebElement subjectInput => _driver.FindElement(By.CssSelector("input[data-qa='subject']"));
        private IWebElement messageInput => _driver.FindElement(By.CssSelector("textarea[data-qa='message']"));
        private IWebElement submitButton => _driver.FindElement(By.CssSelector("input[data-qa='submit-button']"));



        // Método para verificar que está en la página de contacto y devuelve el mensaje "Contact Us"
        public string MessageContactUs()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var messageContactUs = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[normalize-space()='Contact Us']")));
            return messageContactUs.Text;
        }

        //Método para llenar el formulario de contacto
        public void FillContactUsForm(string name, string email, string subject, string message)
        {
            nameInput.SendKeys(name);
            emailInput.SendKeys(email);
            subjectInput.SendKeys(subject);
            messageInput.SendKeys(message);

            string filePath = @"C:\Users\kenne\source\repos\LFSoto\Pruebas-.NET\AutomationPracticeDemo.Tests\Utils\semana.pdf"; // Reemplaza con la ruta real del archivo que deseas subir
            var fileInput = _driver.FindElement(By.CssSelector("input[type='file']"));
            fileInput.SendKeys(filePath);
        }

        public void ClickSubmitButton()
        {
            submitButton.Click();
        }


        //Método para validar alerta
        public string GetAlertMessage()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var alert = wait.Until(ExpectedConditions.AlertIsPresent());
            string alertText = alert.Text;
            alert.Accept(); // Acepta la alerta
            return alertText;
        }

        //Método para validar que el mensaje de éxito se muestra después de enviar el formulario de contacto
        public string GetSuccessMessage()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var successMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='status alert alert-success']")));
            return successMessage.Text;
        }
    }
}
