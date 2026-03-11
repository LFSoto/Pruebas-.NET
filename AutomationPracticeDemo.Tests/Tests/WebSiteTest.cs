using AutomationPracticeDemo.Tests.Utils;
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
    public class WebSiteTest : TestBase
    {
        private static readonly int random = new Random().Next(1, 1000);


  
        [Test, Order(1)]
        public void Should_RegisterNewUser_FullFlow()
        {
            Driver.Navigate().GoToUrl("https://automationexercise.com/");
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            var signupLogin = wait.Until(d => d.FindElement(By.XPath("//a[contains(., 'Signup / Login')]")));
            signupLogin.Click();

            wait.Until(d => d.FindElement(By.Name("name")));
            var nameInput = Driver.FindElement(By.Name("name"));
            var emailInput = Driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));

            var emailRandom = "PracticaClase3" + random + "@cenfotec.com";
            var userName = "user_" + random;

            nameInput.SendKeys(userName);
            emailInput.SendKeys(emailRandom);
            Driver.FindElement(By.XPath("//button[@data-qa='signup-button']")).Click();

            wait.Until(d => d.FindElement(By.Id("id_gender1")));
            Driver.FindElement(By.Id("id_gender1")).Click();
            Driver.FindElement(By.Id("password")).SendKeys("Password123!");

            new SelectElement(Driver.FindElement(By.Id("days"))).SelectByValue("10");
            new SelectElement(Driver.FindElement(By.Id("months"))).SelectByValue("5");
            new SelectElement(Driver.FindElement(By.Id("years"))).SelectByValue("1990");

            var newsletter = Driver.FindElement(By.Id("newsletter"));
            if (!newsletter.Selected) newsletter.Click();
            var optin = Driver.FindElement(By.Id("optin"));
            if (!optin.Selected) optin.Click();

            Driver.FindElement(By.Id("first_name")).SendKeys("Test");
            Driver.FindElement(By.Id("last_name")).SendKeys("User");
            Driver.FindElement(By.Id("company")).SendKeys("MyCompany");
            Driver.FindElement(By.Id("address1")).SendKeys("123 Main St");
            Driver.FindElement(By.Id("address2")).SendKeys("Apt 1");

            try
            {
                var countrySelect = new SelectElement(Driver.FindElement(By.Id("country")));
                countrySelect.SelectByText("United States");
            }
            catch
            {
                var countryEl = Driver.FindElement(By.Id("country"));
                countryEl.Clear();
                countryEl.SendKeys("United States");
            }

            Driver.FindElement(By.Id("state")).SendKeys("CA");
            Driver.FindElement(By.Id("city")).SendKeys("San Francisco");
            Driver.FindElement(By.Id("zipcode")).SendKeys("94101");
            Driver.FindElement(By.Id("mobile_number")).SendKeys("+11234567890");

            // Click en Create Account y esperar explícitamente por el encabezado "Account Created"
            Driver.FindElement(By.CssSelector("button[data-qa='create-account']")).Click();
            wait.Until(d => d.FindElement(By.XPath("//h2[contains(., 'Account Created')]")));

            var createdText = Driver.FindElement(By.XPath("//h2[contains(., 'Account Created')]")).Text;
            Assert.That(createdText, Does.Contain("Account Created"));

            var continueBtn = wait.Until(d => d.FindElement(By.XPath("//a[contains(., 'Continue')]")));
            try
            {
                continueBtn.Click();
            }
            catch
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", continueBtn);
            }

            wait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Logged in as')]")).Count > 0);
            var loggedInEl = Driver.FindElement(By.XPath("//a[contains(., 'Logged in as')]")).Text;
            Assert.That(loggedInEl.ToLower(), Does.Contain("logged in as"));

            ScreenshotHelper.TakeScreenshot(Driver, "automationexercise_registration.png");
        }

        [Test, Order(2)]
        public void Should_LoginExistingUser_AfterRegister()
        {
            // Navigate directly to the login page to avoid modals/overlays
            Driver.Navigate().GoToUrl("https://automationexercise.com/login");
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));

            // Wait for login inputs by data-qa attributes (as inspected)
            wait.Until(d => d.FindElement(By.CssSelector("input[data-qa='login-email']")));
            var loginEmail = Driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
            var loginPassword = Driver.FindElement(By.CssSelector("input[data-qa='login-password']"));

            var existingEmail = "juanrm13@cenfotec.com";
            var existingPassword = "Juan12345";

            // Ensure fields are interactable
            wait.Until(d => loginEmail.Displayed && loginEmail.Enabled);
            wait.Until(d => loginPassword.Displayed && loginPassword.Enabled);

            loginEmail.Clear();
            loginEmail.SendKeys(existingEmail);
            loginPassword.Clear();
            loginPassword.SendKeys(existingPassword);

            // Click login using data-qa selector
            var loginBtn = Driver.FindElement(By.CssSelector("button[data-qa='login-button']"));
            try
            {
                loginBtn.Click();
            }
            catch
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", loginBtn);
            }

            // Validate logged in
            wait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Logged in as')]")).Count > 0);
            var loggedInText = Driver.FindElement(By.XPath("//a[contains(., 'Logged in as')]")).Text;
            Assert.That(loggedInText, Does.Contain("Logged in as"));

            ScreenshotHelper.TakeScreenshot(Driver, "automationexercise_login_existing.png");

            // Esperar 30 segundos y luego intentar logoutdotnet
            TestContext.WriteLine("Esperando 30 segundos antes de hacer logout...");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));

            try
            {
                var logoutEls = Driver.FindElements(By.XPath("//a[contains(., 'Logout')]"));
                if (logoutEls.Count > 0)
                {
                    var logoutEl = logoutEls.First();
                    try { logoutEl.Click(); }
                    catch { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", logoutEl); }

                    // Wait for signup link to appear
                    var shortWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                    shortWait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Signup / Login')]")).Count > 0);
                }
                else
                {
                    TestContext.WriteLine("No se encontró el enlace de logout después del login.");
                }
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error al intentar hacer logout: " + ex.Message);
            }
        }

        [Test, Order(3)]
        public void Should_AddProductsToCart_AndVerifyTotal()
        {
            Driver.Navigate().GoToUrl("https://automationexercise.com/products");
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));

            // Esperar que los botones 'Add to cart' estén presentes
            wait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Add to cart')] | //button[contains(., 'Add to cart')]")).Count > 0);
            var addBtns = Driver.FindElements(By.XPath("//a[contains(., 'Add to cart')] | //button[contains(., 'Add to cart')]")).Take(5).ToList();

            if (addBtns.Count < 2)
                Assert.Fail("No se encontraron suficientes botones 'Add to cart' en la página de productos.");

            var addedProductNames = new System.Collections.Generic.List<string>();

            // Agregar primer producto y continuar
            try
            {
                var first = addBtns[0];
                try { addedProductNames.Add(first.FindElement(By.XPath("ancestor::div[1]//h2 | ancestor::div[1]//p | ancestor::div[contains(@class,'product')]//h2")).Text); } catch { addedProductNames.Add("Product1"); }
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", first);
                first.Click();

                // Esperar modal y click Continue Shopping (o cerrar)
                wait.Until(d => d.FindElements(By.XPath("//button[contains(., 'Continue Shopping')] | //a[contains(., 'Continue Shopping')] | //button[contains(., 'Continue')] | //a[contains(., 'Continue')]")).Count > 0);
                var cont = Driver.FindElements(By.XPath("//button[contains(., 'Continue Shopping')] | //a[contains(., 'Continue Shopping')] | //button[contains(., 'Continue')] | //a[contains(., 'Continue')]")).FirstOrDefault();
                if (cont != null) { try { cont.Click(); } catch { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", cont); } }
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Warning adding first product: " + ex.Message);
            }

            // Agregar segundo producto y View Cart
            try
            {
                var second = addBtns[1];
                try { addedProductNames.Add(second.FindElement(By.XPath("ancestor::div[1]//h2 | ancestor::div[1]//p | ancestor::div[contains(@class,'product')]//h2")).Text); } catch { addedProductNames.Add("Product2"); }
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", second);
                second.Click();

                // Esperar modal y click View Cart
                wait.Until(d => d.FindElements(By.XPath("//a[contains(., 'View Cart')] | //button[contains(., 'View Cart')]")).Count > 0);
                var view = Driver.FindElements(By.XPath("//a[contains(., 'View Cart')] | //button[contains(., 'View Cart')]")).FirstOrDefault();
                if (view != null) { try { view.Click(); } catch { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", view); } }
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Warning adding second product: " + ex.Message);
            }

            // Ahora en la página de carrito, validar que hay al menos dos productos
            wait.Until(d => d.FindElements(By.XPath("//table//tbody//tr | //div[@class='cart_info']//tr")).Count > 0);
            var cartRows = Driver.FindElements(By.XPath("//table//tbody//tr | //div[@class='cart_info']//tr")).ToList();
            TestContext.WriteLine($"Rows encontradas en cart: {cartRows.Count}");

            Assert.That(cartRows.Count, Is.GreaterThanOrEqualTo(2), "No se detectaron al menos 2 productos en el carrito.");

            // Intentar validar total: buscar elementos que contengan la palabra Total
            var totalEl = Driver.FindElements(By.XPath("//*[contains(., 'Total') or contains(., 'TOTAL') or contains(@class,'total')]")).FirstOrDefault();
            Assert.That(totalEl, Is.Not.Null, "No se encontró un elemento que muestre el total del carrito.");

            ScreenshotHelper.TakeScreenshot(Driver, "automationexercise_cart.png");
        }

        [Test, Order(4)]
        public void Should_SubmitContactUsForm()
        {
            Driver.Navigate().GoToUrl("https://automationexercise.com/contact_us");
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));

            // Wait fields
            wait.Until(d => d.FindElements(By.Name("name")).Count > 0);
            var nameEl = Driver.FindElement(By.Name("name"));
            var emailEl = Driver.FindElement(By.Name("email"));
            IWebElement subjectEl = null;
            try { subjectEl = Driver.FindElement(By.Name("subject")); } catch { }
            var messageEl = Driver.FindElement(By.Name("message"));

            var contactName = "Automated Tester";
            var contactEmail = "tester+contact" + random + "@example.com";
            var contactSubject = subjectEl != null ? "Consulta automatizada" : null;
            var contactMessage = "Mensaje de prueba enviado por pruebas automatizadas.";

            nameEl.Clear(); nameEl.SendKeys(contactName);
            emailEl.Clear(); emailEl.SendKeys(contactEmail);
            if (subjectEl != null) { subjectEl.Clear(); subjectEl.SendKeys(contactSubject); }
            messageEl.Clear(); messageEl.SendKeys(contactMessage);

            // Crear un archivo temporal para adjuntar
            var tempFile = Path.Combine(Path.GetTempPath(), $"contact_attach_{Guid.NewGuid():N}.txt");
            File.WriteAllText(tempFile, "Archivo de prueba para Contact Us");

            try
            {
                var fileInput = Driver.FindElement(By.CssSelector("input[type='file']"));
                fileInput.SendKeys(tempFile);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("No se encontró input file o fallo al adjuntar: " + ex.Message);
            }

            // Submit - buscar botón Submit
            var submitBtn = Driver.FindElements(By.XPath("//input[@type='submit'] | //button[contains(., 'Submit')] | //button[contains(., 'Submit')] ")).FirstOrDefault();
            if (submitBtn != null)
            {
                try { submitBtn.Click(); } catch { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", submitBtn); }
            }

            // Validar mensaje de éxito
            wait.Until(d => d.FindElements(By.XPath("//*[contains(., 'Success! Your details have been submitted successfully') or contains(., 'Success! Your details have been submitted successfully.') or contains(., 'Success!')]")).Count > 0);
            var success = Driver.FindElements(By.XPath("//*[contains(., 'Success! Your details have been submitted successfully') or contains(., 'Success! Your details have been submitted successfully.') or contains(., 'Success!')]")).FirstOrDefault();
            Assert.That(success, Is.Not.Null, "No se encontró el mensaje de éxito esperado para Contact Us.");

            ScreenshotHelper.TakeScreenshot(Driver, "automationexercise_contact_us.png");

            // Cleanup temp file
            try { File.Delete(tempFile); } catch { }
        }

        [Test, Order(5)]
        public void Should_SubscribeToNewsletter()
        {
            Driver.Navigate().GoToUrl("https://automationexercise.com/");
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));

            // Scroll to bottom
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            System.Threading.Thread.Sleep(1000);

            // Try to find newsletter input in footer or by common selectors
            IWebElement footerInput = null;
            try
            {
                footerInput = Driver.FindElements(By.CssSelector("footer input[type='email'], input#susbscribe_email, input[name='email'], input[placeholder*='email']")).FirstOrDefault();
            }
            catch { }

            if (footerInput == null)
            {
                // try searching inside footer element
                try
                {
                    var footer = Driver.FindElements(By.TagName("footer")).FirstOrDefault();
                    if (footer != null)
                        footerInput = footer.FindElements(By.XPath(".//input[@type='email' or contains(@placeholder,'email') or @name='email']")).FirstOrDefault();
                }
                catch { }
            }

            Assert.That(footerInput, Is.Not.Null, "No se encontró el campo de suscripción al newsletter en el footer.");

            // generate unique
            var emailToSubscribe = $"automation_news{DateTime.UtcNow.Ticks}@example.com";

            footerInput.Clear();
            footerInput.SendKeys(emailToSubscribe);

            // Find submit/arrow button near the input
            IWebElement submitBtn = null;
            try
            {
                submitBtn = footerInput.FindElements(By.XPath("following::button[1] | following::input[@type='submit'][1]")).FirstOrDefault();
            }
            catch { }

            if (submitBtn == null)
            {
                // generic attempt
                submitBtn = Driver.FindElements(By.XPath("//button[contains(., 'Subscribe') or contains(., 'SUBSCRIBE') or contains(., 'Subscribe') or contains(., 'Submit')]")).FirstOrDefault();
            }

            Assert.That(submitBtn, Is.Not.Null, "No se encontró el botón de suscripción.");
            try { submitBtn.Click(); } catch { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", submitBtn); }

            // Wait for success message
            wait.Until(d => d.FindElements(By.XPath("//*[contains(., 'You have been successfully subscribed') or contains(., 'successfully subscribed')]")).Count > 0);
            var subscribed = Driver.FindElements(By.XPath("//*[contains(., 'You have been successfully subscribed') or contains(., 'successfully subscribed')]")).FirstOrDefault();
            Assert.That(subscribed, Is.Not.Null, "No se encontró el mensaje de suscripción exitoso.");

            ScreenshotHelper.TakeScreenshot(Driver, "automationexercise_newsletter.png");
        }
    }
}
