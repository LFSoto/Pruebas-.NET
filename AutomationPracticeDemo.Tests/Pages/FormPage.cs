using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class FormPage
    {
        private readonly IWebDriver _driver;

        public FormPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement Signup => _driver.FindElement(By.LinkText("Signup / Login"));
        private IWebElement Name => _driver.FindElement(By.Name("name"));
        private IWebElement Email => _driver.FindElement(By.CssSelector("input[data-qa='signup-email']"));
        private IWebElement SignupButton => _driver.FindElement(By.CssSelector("button[data-qa='signup-button']"));
        private IWebElement Title => _driver.FindElement(By.Id("id_gender1"));
        private IWebElement Password => _driver.FindElement(By.Id("password"));
        private IWebElement FirstName => _driver.FindElement(By.Id("first_name"));
        private IWebElement LastName => _driver.FindElement(By.Id("last_name"));
        private IWebElement Company => _driver.FindElement(By.Id("company"));
        private IWebElement Address => _driver.FindElement(By.Id("address1"));
        private IWebElement Country => _driver.FindElement(By.Id("country"));
        private IWebElement State => _driver.FindElement(By.Id("state"));
        private IWebElement City => _driver.FindElement(By.Id("city"));
        private IWebElement ZipCode => _driver.FindElement(By.CssSelector("input[data-qa='zipcode']"));
        private IWebElement MobileNumber => _driver.FindElement(By.CssSelector("input[data-qa='mobile_number']"));
        private IWebElement btnCreateAccount => _driver.FindElement(By.CssSelector("button[data-qa='create-account']"));
        private IWebElement Message => _driver.FindElement(By.CssSelector("h2[data-qa='account-created']"));
        private IWebElement continueButton => _driver.FindElement(By.CssSelector("a[data-qa='continue-button']"));

        private IWebElement LoginEmail => _driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
        private IWebElement LoginPassword => _driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
        private IWebElement btnLogin => _driver.FindElement(By.CssSelector("button[data-qa='login-button']"));


        private IWebElement productsLink => _driver.FindElement(By.XPath("//a[contains(., 'Products')]"));

        private IWebElement contactUs => _driver.FindElement(By.XPath("//a[contains(., 'Contact us')]"));
        private IWebElement NameContact => _driver.FindElement(By.CssSelector("input[data-qa='name']"));
        private IWebElement EmailContact => _driver.FindElement(By.CssSelector("input[data-qa='email']"));
        private IWebElement SubjectContact => _driver.FindElement(By.CssSelector("input[data-qa='subject']"));
        private IWebElement MessageContact => _driver.FindElement(By.CssSelector("textarea[data-qa='message']"));

        private IWebElement SubmitContact => _driver.FindElement(By.CssSelector("input[data-qa='submit-button']"));

        //HACER CLICK EN SIGNUP
        public void ClickSignup()
        {
            Signup.Click();
        }

        //COMPLETAR NOMBRE
        public void NewUser(string name, string email)
        {
            Name.SendKeys(name);
            Email.SendKeys(email);

        }

        //HACER CLICK EN SIGNUP
        public void ClickSignupButton()
        {
            SignupButton.Click();
        }

        //SELECCIONAR TITLE
        public void SelectTitle()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement titleRadio = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("id_gender1")));
            Title.Click();
        }

        //COMPLETAR CONTRASEÑA
        public void Password1(string password1)
        {
            Password.SendKeys(password1);
        }

        //COMPLETAR INFORMACIÓN
        public void CompleteInformation(string firstName, string lastName, string company, string address, string country, string state, string city, string zipCode, string MobileNumber1)
        {
            FirstName.SendKeys(firstName);
            LastName.SendKeys(lastName);
            Company.SendKeys(company);
            Address.SendKeys(address);
            Country.SendKeys(country);
            State.SendKeys(state);
            City.SendKeys(city);
            ZipCode.SendKeys(zipCode);
            MobileNumber.SendKeys(MobileNumber1);
        }

        //VALIDAR BOTÓN CREAR USUARIO
        public void ValidateCreateUserButton()
        {
            btnCreateAccount.Click();
        }

        //VALIDAR MENSAJE DE CUENTA CREADA
        public string ValidateMessage()
        {
            return Message.Text;

        }

        //VALIDAR CLIC BTN CONTINUE
        public void ValidateContinueButton()
        {
            continueButton.Click();
        }



        //LOGIN - INGRESAR CREDENCIALES
        public void EnterLoginCredentials(string email, string password)
        {
            LoginEmail.Clear();
            LoginEmail.SendKeys(email);
            LoginPassword.Clear();
            LoginPassword.SendKeys(password);
        }

        //CLICK EN LOGIN
        public void ClickLoginButton()
        {
            btnLogin.Click();
        }

        //VALIDAR QUE USUARIO ESTÁ LOGUEADO Y MUESTRA EL NOMBRE
        public bool IsUserLoggedIn(string username)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                // Esperamos a que el elemento con el texto "Logged in as" sea visible
                var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(., 'Logged in as')]")));
                var text = element.Text?.Trim() ?? string.Empty;
                // Validar que el texto sea exactamente "Logged in as [username]" o contenga esa secuencia
                if (string.Equals(text, $"Logged in as {username}", StringComparison.OrdinalIgnoreCase))
                    return true;

                return text.IndexOf($"Logged in as {username}", StringComparison.OrdinalIgnoreCase) >= 0;
            }
            catch (WebDriverTimeoutException)
            {
                return false; // Si pasan 10 segundos y no aparece, devuelve false
            }
        }

        // 3 - Agregar productos al carrito y verificar total
        public void AddProductsToCartAndVerifyTotal()
        {
            // Aquí iría la lógica para agregar productos al carrito y verificar el total
            // Esto podría incluir:
            // - Navegar a la sección de productos
            // - Agregar uno o más productos al carrito
            // - Ir al carrito y verificar que el total sea correcto

            productsLink.Click();

            //Esperar a que los productos se carguen
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".product-image-wrapper")));

            //Obtener la lista de todos los productos
            var products = _driver.FindElements(By.CssSelector(".product-image-wrapper"));

            Actions actions = new Actions(_driver);

            // --- AGREGAR EL PRIMER PRODUCTO ---
            actions.MoveToElement(products[0]).Perform();

            //Buscar el botón "Add to cart" dentro del primer producto y hacer clic
            var addToCartBtn1 = products[0].FindElement(By.CssSelector(".product-overlay .add-to-cart"));
            addToCartBtn1.Click();

            //Cerrar el modal de "Continue Shopping"
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Continue Shopping']"))).Click();

            // --- AGREGAR EL SEGUNDO PRODUCTO ---
            actions.MoveToElement(products[1]).Perform();
            var addToCartBtn2 = products[1].FindElement(By.CssSelector(".product-overlay .add-to-cart"));
            addToCartBtn2.Click();

        }

        // 4 - Contact Us fomr

        public void ContactUsForm(string NameC, string EmailC, string SubjectC, string MessageC, string filePath)
        {
            contactUs.Click();

            // Esperar a que el formulario de contacto se cargue
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href='/contact_us']")));

            NameContact.SendKeys(NameC);
            EmailContact.SendKeys(EmailC);
            SubjectContact.SendKeys(SubjectC);
            MessageContact.SendKeys(MessageC);

            var fileInput = _driver.FindElement(By.Name("upload_file"));
            fileInput.SendKeys(filePath); // Reemplaza con la ruta real del archivo que deseas subir

            SubmitContactForm();

        }

        public void SubmitContactForm()
        {
            // 1. Clic en el botón Submit
            _driver.FindElement(By.Name("submit")).Click();

            // 2. Manejo de la alerta (Sincronización obligatoria)
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

            // Aceptar la alerta para que Selenium pueda continuar con la página
            alert.Accept();
        }

        public string GetContactSuccessMessage()
        {
            // 1. Sincronización: Esperar a que el mensaje aparezca tras el refresco
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var messageElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".status.alert-success")));

            // 2. Retornar el texto para el Assert
            return messageElement.Text;
        }



    }
}
