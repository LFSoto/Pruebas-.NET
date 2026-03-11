using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormTests
    {
        private IWebDriver Driver;
        private WebDriverWait wait; // Para esperas explícitas

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");

            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("https://automationexercise.com/");

            // Si no encuentras algo, espera hasta X segundos antes de lanzar un error
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

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

        static readonly int random = new Random().Next(1, 1000);
        string nameLogin = "cenfo";
        string emailRandom = "kenneth" + random + "@cenfotec.com";
        string emailLogin = "cenfo123@gmail.com";
        string passwordLogin = "cenfo123";


        [Test]
        [Description("Flujo completo de registro de nuevo usuario hasta validación de login")]
        public void RegistroUsuarioNuevo()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            // 1. Ir a Signup / Login
            var signupPage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href='/login']")));
            signupPage.Click();

            // 2. Registro inicial (Nombre y Email)
            var nameSignupInput = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[data-qa='signup-name']")));
            nameSignupInput.SendKeys("Kenneth");

            var emailSignupInput = Driver.FindElement(By.CssSelector("input[data-qa='signup-email']"));
            emailSignupInput.SendKeys(emailRandom);

            var signupButton = Driver.FindElement(By.CssSelector("button[data-qa='signup-button']"));
            signupButton.Click();

            // 3. Llenar detalles de la cuenta (Gender y Password)
            // Esperamos a que la nueva página cargue realmente
            var titleRadioButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("id_gender1")));
            js.ExecuteScript("arguments[0].click();", titleRadioButton);

            Driver.FindElement(By.Id("password")).SendKeys("123456");

            // 4. Datos de dirección
            var firstNameInput = Driver.FindElement(By.Id("first_name"));
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", firstNameInput);

            firstNameInput.SendKeys("Kenneth");
            Driver.FindElement(By.Id("last_name")).SendKeys("Oviedo");
            Driver.FindElement(By.Id("address1")).SendKeys("San José");

            new SelectElement(Driver.FindElement(By.Id("country"))).SelectByText("United States");

            Driver.FindElement(By.Id("state")).SendKeys("Curridabat");
            Driver.FindElement(By.Id("city")).SendKeys("San José");
            Driver.FindElement(By.Id("zipcode")).SendKeys("11801");
            Driver.FindElement(By.Id("mobile_number")).SendKeys("88888888");

            // 5. Crear cuenta (ACCIÓN CORREGIDA)
            var btnCreateAccount = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[data-qa='create-account']")));

            // Hacemos scroll para que el botón esté visible
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", btnCreateAccount);

            // IMPORTANTE: Aquí estaba el error. Hay que dar el CLIC.
            // Usamos JS Click porque esta página tiene muchos anuncios que bloquean clics normales.
            js.ExecuteScript("arguments[0].click();", btnCreateAccount);

            // 6. Validar mensaje de éxito
            // Primero esperamos a que el elemento aparezca en el DOM de la nueva página
            var accountCreatedMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h2[data-qa='account-created']")));

            // LUEGO de que aparece, limpiamos anuncios para que la captura de pantalla salga limpia
            LimpiarAnuncios();

            ScreenshotHelper.TakeScreenshot(Driver, "cuentacreada.png");
            Assert.That(accountCreatedMessage.Text.ToUpper(), Is.EqualTo("ACCOUNT CREATED!"), "El mensaje de éxito no es el esperado.");

        }

        [Test]
        [Description("Validar login existente")]
        public void ValidarLoginExistente()
        {
            // 1. Ir a la página de Login
            var signupLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/login']")));
            signupLink.Click();

            // 2. Llenar credenciales
            var emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[data-qa='login-email']")));
            emailField.Clear();
            emailField.SendKeys(emailLogin);

            var passwordField = Driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
            passwordField.Clear();
            passwordField.SendKeys(passwordLogin);

            // 3. Clic en Login (REFORZADO)
            var btnLogin = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[data-qa='login-button']")));
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", btnLogin);
            System.Threading.Thread.Sleep(500);

            // Ejecutamos el clic vía JavaScript para asegurar que no lo bloquee un anuncio
            js.ExecuteScript("arguments[0].click();", btnLogin);

            // VERIFICACIÓN DE TRANSICIÓN: 
            // En lugar de solo esperar invisibilidad, esperamos a que la URL cambie de '/login'
            try
            {
                wait.Until(ExpectedConditions.UrlContains("https://automationexercise.com/"));
                // Si la URL sigue siendo /login, es que fallaron las credenciales o el clic
                if (Driver.Url.Contains("/login"))
                {
                    ScreenshotHelper.TakeScreenshot(Driver, "error_credenciales_o_clic.png");
                    Assert.Fail("El login no redirigió. Posible error de credenciales o clic no procesado.");
                }
            }
            catch (WebDriverTimeoutException)
            {
                ScreenshotHelper.TakeScreenshot(Driver, "timeout_redireccion.png");
                throw;
            }

            // 4. Estabilización de la nueva página
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            System.Threading.Thread.Sleep(2000);
            LimpiarAnuncios();
            js.ExecuteScript("window.scrollTo(0, 0);");

            // 5. Validar el elemento de "Logged in as"
            // XPath ultra-flexible: busca el nombre dentro de un 'b' que esté cerca del texto 'Logged in as'
            var loggedInElement = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath($"//li[contains(., 'Logged in as')]//b[contains(., '{nameLogin}')]")
            ));

            // 6. Validación final
            string actualText = loggedInElement.Text.Trim();
            ScreenshotHelper.TakeScreenshot(Driver, "login_confirmado.png");

            Assert.That(actualText, Is.EqualTo(nameLogin).IgnoreCase,
                $"Error: Se esperaba '{nameLogin}' pero se encontró '{actualText}'.");

            Console.WriteLine("Login exitoso confirmado para: " + actualText);

        }

        [Test]
        [Description("Validar agregar productos al carrito")]
        public void AgregarProductoAlCarrito()
        {
            // 1. Ir a Products
            var productsLink = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href='/products']")));
            productsLink.Click();

            // 2. Limpiar anuncios iniciales
            LimpiarAnuncios();

            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            // 3. Obtener la lista de botones (Buscamos los botones específicos del área de productos)
            var allProducts = Driver.FindElements(By.CssSelector(".features_items .add-to-cart"));
            int n = 2;
            Random random = new Random();
            List<int> usedIndices = new List<int>();

            for (int i = 0; i < n; i++)
            {
                // Limpiamos anuncios al inicio de cada vuelta por si aparecieron nuevos
                LimpiarAnuncios();

                int randomIndex;
                do
                {
                    randomIndex = random.Next(allProducts.Count);
                } while (usedIndices.Contains(randomIndex));

                usedIndices.Add(randomIndex);
                var randomProduct = allProducts[randomIndex];

                // Scroll y Clic forzado con JS para evitar el ElementClickInterceptedException
                js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", randomProduct);
                js.ExecuteScript("arguments[0].click();", randomProduct);

                // --- PUNTO CRÍTICO ---
                // Volvemos a limpiar antes de interactuar con el modal
                LimpiarAnuncios();

                // Esperar al modal y dar clic en 'Continue Shopping'
                var continueBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.close-modal")));

                // Usamos JS Click también aquí para asegurar que cierre el modal sin problemas
                js.ExecuteScript("arguments[0].click();", continueBtn);

                Console.WriteLine($"Producto aleatorio #{i + 1} agregado con éxito.");
            }

            // 4. Click en View Cart
            var cartLink = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href='/view_cart']")));
            cartLink.Click();

            // Limpieza final antes de la validación y captura
            LimpiarAnuncios();

            // 5. Validar productos en la tabla y el TOTAL
            var cartRows = Driver.FindElements(By.CssSelector("table#cart_info_table tbody tr"));
            Assert.That(cartRows.Count, Is.EqualTo(n), "La cantidad de productos en el carrito no es la esperada.");

            double totalCalculado = 0;

            foreach (var row in cartRows)
            {
                // Extraemos el texto del total por fila
                string totalFilaRaw = row.FindElement(By.ClassName("cart_total")).Text;

                // Limpiamos el texto usando Regex para obtener solo números
                string numericValue = System.Text.RegularExpressions.Regex.Replace(totalFilaRaw, @"[^\d]", "");

                if (!string.IsNullOrEmpty(numericValue))
                {
                    totalCalculado += double.Parse(numericValue);
                }
            }

            Console.WriteLine($"Suma total calculada: Rs. {totalCalculado}");

            // 6. Centrar la tabla para que el screenshot sea perfecto
            if (cartRows.Count > 0)
            {
                js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", cartRows[0]);
            }

            // 7. Tomar captura
            ScreenshotHelper.TakeScreenshot(Driver, "carrito_final_validado.png");

            // Validación final
            Assert.That(totalCalculado, Is.GreaterThan(0), "El total del carrito debería ser mayor a cero.");

        }


        [Test]
        [Description("Formulario de Contacto")]

        public void ValidarContacto()
        {

            var ContactUsLink = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href='/contact_us']")));
                ContactUsLink.Click();

            var NameInput = Driver.FindElement(By.CssSelector("input[data-qa='name']"));
                NameInput.SendKeys("Kenneth");

            var EmailInput = Driver.FindElement(By.CssSelector("input[data-qa='email']"));
                EmailInput.SendKeys(emailRandom);

            var SubjectInput = Driver.FindElement(By.CssSelector("input[data-qa='subject']"));
                SubjectInput.SendKeys("Consulta sobre productos");

            var MessageInput = Driver.FindElement(By.CssSelector("textarea[data-qa='message']"));
                MessageInput.SendKeys("Hola, me gustaría saber más sobre sus productos.");

            string filePath = @"C:\Users\kenne\source\repos\LFSoto\Pruebas-.NET\AutomationPracticeDemo.Tests\Utils\semana.pdf"; // Reemplaza con la ruta real del archivo que deseas subir

            var fileInput = Driver.FindElement(By.CssSelector("input[type='file']"));
                fileInput.SendKeys(filePath); 

            var btnSubmit = Driver.FindElement(By.CssSelector("input[data-qa='submit-button']"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", btnSubmit);
            js.ExecuteScript("arguments[0].click();", btnSubmit);

            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
            alert.Accept();

            var successMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.status.alert.alert-success")));

            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", successMessage);

            ScreenshotHelper.TakeScreenshot(Driver, "formulario.png");

            Assert.That(successMessage.Text, Is.EqualTo("Success! Your details have been submitted successfully."), "El mensaje de éxito no es el esperado.");
             
            Console.WriteLine("Mensaje de contacto enviado: " + successMessage.Text);
        }

        [Test]
        [Description("Validar suscripción al newsletter")]
        public void ValidarNewsletter()
        {
            var NewsletterInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("susbscribe_email")));
            NewsletterInput.SendKeys(emailRandom);

            var btnSubscribe = Driver.FindElement(By.Id("subscribe"));
            btnSubscribe.Click();

            var successMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[class='alert-success alert']")));

            ScreenshotHelper.TakeScreenshot(Driver, "newsletter.png");

            Assert.That(successMessage.Text, Is.EqualTo("You have been successfully subscribed!"), "El mensaje de suscripción no es el esperado.");

            Console.WriteLine("Mensaje de suscripción: " + successMessage.Text);


        }

        private void LimpiarAnuncios()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            // Eliminamos iframes e insignias de Google Ads
            js.ExecuteScript(@"
            var ads = document.querySelectorAll('iframe, ins.adsbygoogle, #aswift_0_host, #ad_unit, .adsbygoogle');
            for(var i=0; i<ads.length; i++){
            ads[i].remove();
            }");
        }

    }
}