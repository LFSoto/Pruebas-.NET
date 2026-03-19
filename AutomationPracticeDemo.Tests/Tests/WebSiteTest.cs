using AutomationPracticeDemo.Tests.Utils;
using AutomationPracticeDemo.Tests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutomationPracticeDemo.Tests.Tests
{
    [NonParallelizable]
    public class WebSiteTest : TestBase
    {
        private static readonly int random = new Random().Next(1, 1000);

        /// <summary>
        /// Registra un nuevo usuario usando datos del JSON (DDT).
        /// Entradas: RegisterData (proporcionado por TestCaseSource).
        /// Efecto: crea una cuenta en el sitio, valida que aparezca "Logged in as" y toma captura.
        /// </summary>
        [Test, Order(1), TestCaseSource(typeof(JsonDataProvider), nameof(JsonDataProvider.GetRegisterData))]
        public void Should_RegisterNewUser_FullFlow(RegisterData data)
        {
            var regPage = new RegisterPage(Driver, TimeSpan.FromSeconds(20));
            regPage.GoTo();

            // use provided dataset email or generate one unique
            if (string.IsNullOrEmpty(data.email)) data.email = $"PracticaClase3{random}@example.com";
            if (string.IsNullOrEmpty(data.name)) data.name = "user_" + random;
            if (string.IsNullOrEmpty(data.password)) data.password = "Password123!";

            regPage.Register(data);

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Logged in as')]")).Count > 0);
            var loggedInEl = Driver.FindElement(By.XPath("//a[contains(., 'Logged in as')]")).Text;
            Assert.That(loggedInEl.ToLower(), Does.Contain("logged in as"));

            ScreenshotHelper.TakeScreenshot(Driver, "automationexercise_registration.png");
        }

        /// <summary>
        /// Escenario negativo de login: intenta iniciar sesión con credenciales inválidas.
        /// Acciones: navega a /login, envía credenciales inválidas y captura el mensaje de error mostrado.
        /// Validación: existe texto de error esperado.
        /// </summary>
        [Test, Order(2)]
        public void Should_ShowError_When_LoginWithInvalidCredentials()
        {
            var loginPage = new LoginPage(Driver, TimeSpan.FromSeconds(20));
            try { EnsureLoggedOut(); } catch { }

            loginPage.GoTo();

            var invalidEmail = $"juanrm13@cenfote.com";
            var invalidPassword = "Prueba1234";

            var errorMessage = loginPage.LoginExpectingFailure(invalidEmail, invalidPassword);
            TestContext.WriteLine("Observed login error message: " + (errorMessage ?? "<none>"));

            Assert.That(errorMessage, Is.Not.Null.And.Not.Empty, "Expected an error message for invalid login but none was found.");

            ScreenshotHelper.TakeScreenshot(Driver, "automationexercise_login_invalid.png");
        }

        /// <summary>
        /// Login con usuario existente (credenciales fijas actualmente).
        /// Acciones: navega a /login, realiza el login y toma captura; espera hasta encontrar el enlace de logout y luego intenta logout.
        /// Validación: si el login falla, el método Login lanzará excepción por timeout.
        /// </summary>
        [Test, Order(3)]
        public void Should_LoginExistingUser_AfterRegister()
        {
            var loginPage = new LoginPage(Driver, TimeSpan.FromSeconds(20));
            // Ensure logged out
            try { EnsureLoggedOut(); } catch { }

            loginPage.GoTo();
            // Using fallback hardcoded credentials; could be DDT-driven
            loginPage.Login("juanrm13@cenfotec.com", "Juan12345");

            ScreenshotHelper.TakeScreenshot(Driver, "automationexercise_login_existing.png");

            // Espera hasta que aparezca el enlace Cerrar sesión 
            try
            {
                var waitLogout = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
                waitLogout.Until(d => d.FindElements(By.XPath("//a[contains(., 'Logout')]")).Count > 0);
            }
            catch { /* timeout - proceed to attempt logout anyway */ }

            try
            {
                var logoutEls = Driver.FindElements(By.XPath("//a[contains(., 'Logout')]")).ToList();
                if (logoutEls.Count > 0)
                {
                    var logoutEl = logoutEls.First();
                    try { logoutEl.Click(); } catch { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", logoutEl); }
                    var shortWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                    shortWait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Signup / Login')]")).Count > 0);
                }
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error al intentar hacer logout: " + ex.Message);
            }
        }

        /// <summary>
        /// Añade dos productos al carrito y valida que el carrito muestre al menos dos filas y el total.
        /// Acciones: usa ProductsPage para agregar productos y CartPage para validar filas y total.
        /// </summary>
        [Test, Order(4)]
        public void Should_AddProductsToCart_AndVerifyTotal()
        {
            var products = new ProductsPage(Driver, TimeSpan.FromSeconds(20));
            var cart = new CartPage(Driver, TimeSpan.FromSeconds(20));

            products.GoTo();
            products.AddToCartByIndex(0);
            // continue shopping
            var cont = Driver.FindElements(By.XPath("//button[contains(., 'Continue Shopping')] | //a[contains(., 'Continue Shopping')]")).FirstOrDefault();
            if (cont != null) { try { cont.Click(); } catch { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", cont); } }

            products.AddToCartByIndex(1);
            products.ClickViewCartFromModal();

            var rows = cart.GetCartRows();
            Assert.That(rows.Count, Is.GreaterThanOrEqualTo(2));

            var total = cart.GetTotalText();
            Assert.That(total, Is.Not.Null.And.Not.Empty);

            ScreenshotHelper.TakeScreenshot(Driver, "automationexercise_cart.png");
        }

        /// <summary>
        /// Envía el formulario Contact Us usando datasets JSON (DDT).
        /// Acciones: rellena campos, adjunta archivo temporal si corresponde y valida mensaje de éxito.
        /// </summary>
        [Test, Order(5), TestCaseSource(typeof(JsonDataProvider), nameof(JsonDataProvider.GetContactUsData))]
        public void Should_SubmitContactUsForm(ContactData data)
        {
            var contactPage = new ContactPage(Driver, TimeSpan.FromSeconds(20));
            contactPage.GoTo();

            string tempFile = null;
            if (data.attachFile)
            {
                tempFile = Path.Combine(Path.GetTempPath(), $"contact_attach_{Guid.NewGuid():N}.txt");
                File.WriteAllText(tempFile, "Archivo de prueba para Contact Us");
            }

            contactPage.Fill(data, tempFile);
            contactPage.Submit();

            var success = contactPage.GetSuccessMessage();
            Assert.That(success, Does.Contain("Success!"));

            ScreenshotHelper.TakeScreenshot(Driver, $"automationexercise_contact_us_{data.name}.png");

            try { if (tempFile != null) File.Delete(tempFile); } catch { }
        }

        /// <summary>
        /// Suscribe correos al newsletter usando datasets JSON (DDT).
        /// Acciones: hace scroll al footer, envía el email y espera el mensaje de suscripción exitosa.
        /// </summary>
        [Test, Order(6), TestCaseSource(typeof(JsonDataProvider), nameof(JsonDataProvider.GetNewsletterData))]
        public void Should_SubscribeToNewsletter(NewsletterData data)
        {
            var footer = new FooterComponent(Driver, TimeSpan.FromSeconds(20));
            Driver.Navigate().GoToUrl("https://automationexercise.com/");
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            // Añade espera explícita para la entrada del boletín informativo.
            var shortWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            shortWait.Until(d => footer.FindNewsletterInput() != null);

            footer.Subscribe(data.email);

            ScreenshotHelper.TakeScreenshot(Driver, $"automationexercise_newsletter_{data.email}.png");
        }
    }
}
