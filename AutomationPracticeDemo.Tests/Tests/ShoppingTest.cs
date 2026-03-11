using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Collections.Generic;

namespace AutomationPracticeDemo.Tests.Tests;

public class ShoppingTest
{
    private IWebDriver Driver;
    private readonly string[] _initialProductNames = new string[] { "Rust Red Linen Saree", "Frozen Tops For Kids", "Sleeveless Dress", "Winter Top", "Blue Top", "Colour Blocked Shirt – Sky Blue" };
    private List<string> availableProducts;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        //options.AddArgument("--start-maximized");
        options.AddArgument("--headless=new");
        options.AddArgument("--window-size=1920,1080");
        options.AddArgument("--remote-allow-origins=*");
        Driver = new ChromeDriver(options);
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        Driver.Navigate().GoToUrl("https://automationexercise.com/");        
        // Inicializar lista de productos disponibles para selección aleatoria sin repetición
        availableProducts = new List<string>(_initialProductNames);
    }

    [TearDown]
    public void TearDown()
    {
        CloseBrowser();
    }

    [Test]
    public void ValidarRegistrarNuevoUsuario()
    {
        var usuario = "Erick Meneses";
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var email = $"emeneses_{timestamp}@test.com";
        var password = "password12345!";
        Console.WriteLine($"Email generado para la prueba: {email}");

        // Click en el botón de Signup / Login
        var singUpLocator = Driver.FindElement(By.XPath("//a[text()=' Signup / Login']"));
        singUpLocator.Click();
        // Name
        var nameInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='signup-name']"));
        nameInputLocator.SendKeys(usuario);
        // Email
        var emailInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='signup-email']"));
        emailInputLocator.SendKeys(email);
        // Click en el botón de Signup
        var signupButtonLocator = Driver.FindElement(By.XPath("//*[@data-qa='signup-button']"));
        signupButtonLocator.Click();
        // Password
        var passwordInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='password']"));
        passwordInputLocator.SendKeys(password);
        // First Name
        var firstNameInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='first_name']"));
        firstNameInputLocator.SendKeys("Erick");
        // Last Name
        var lastNameInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='last_name']"));
        lastNameInputLocator.SendKeys("Meneses");
        // Address
        var addressInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='address']"));
        addressInputLocator.SendKeys("San Carlos");
        // Country Dropdown
        var countrySelectLocator = Driver.FindElement(By.XPath("//*[@data-qa='country']"));
        var select = new SelectElement(countrySelectLocator);
        select.SelectByText("United States");
        // State
        var stateInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='state']"));
        stateInputLocator.SendKeys("Florida");
        // City
        var cityInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='city']"));
        cityInputLocator.SendKeys("Miami");
        // Zipcode
        var zipCodeInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='zipcode']"));
        zipCodeInputLocator.SendKeys("33206-3206");
        // MobileNumber
        var mobileNumberInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='mobile_number']"));
        mobileNumberInputLocator.SendKeys("(786) 322-2032");
        // Click en el botón de Create Account
        var createAccountButtonLocator = Driver.FindElement(By.XPath("//*[@data-qa='create-account']"));
        createAccountButtonLocator.Click();
        // Mensaje de cuenta creada
        var createdAccountH2Locator = Driver.FindElement(By.XPath("//*[@data-qa='account-created']"));
        Assert.That(createdAccountH2Locator.Text, Is.EqualTo("ACCOUNT CREATED!"), "El mensaje de cuenta creada no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, "Cuenta_Creada");
        // Click en el botón de Create Account
        var continueButtonLocator = Driver.FindElement(By.XPath("//*[@data-qa='continue-button']"));
        continueButtonLocator.Click();
        // Label de usuario logueado
        var loggedUserLocator = Driver.FindElement(By.XPath("//a[contains(text(), 'Logged in as')]"));
        Assert.That(loggedUserLocator.Text, Does.Contain(usuario), "El usuario logueado no es el esperado.");

    }

    [Test]
    public void ValidarLoginConUsuarioExistente()
    {
        var email = "emeneses_1772680170689@test.com";
        var password = "password12345!";
        var usuario = "Erick Meneses";

        LoginUsuarioExistente(email, password);

        // Label de usuario logueado
        var loggedUserLocator = Driver.FindElement(By.XPath("//a[contains(text(), 'Logged in as')]"));
        Assert.That(loggedUserLocator.Text, Does.Contain(usuario), "El usuario logueado no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, "Usuario_Login_Correcto");
    }

    [Test]
    public void ValidarTotalEnCarritoPorProducto()
    {
        // Click la opcion Products en el menu
        var productosLocator = Driver.FindElement(By.XPath("//a[text()=' Products']"));
        productosLocator.Click();

        // Se agregan dos productos al carrito, uno con click en View Cart y otro con click en Continue Shopping para validar ambos flujos
        var producto1 = AgregarProductoAlCarrito(false);
        var producto2 = AgregarProductoAlCarrito(true);

        // Se valida que el total del producto sea igual al precio por la cantidad
        ValidarTotalProducto(producto1);
        ValidarTotalProducto(producto2);
        ScreenshotHelper.TakeScreenshot(Driver, "Carrito_Productos");
    }

    [Test]
    public void ValidarFormularioContactUs()
    {
        var name = "Erick Meneses";
        var email = "emeneses_1772680170689@test.com";
        var subject = "Consulta sobre camiseta";
        var message = "Hola, me gustaría obtener informcacion acerca de los colores de camisetas a la venta";

        // Click la opcio  Contact us en el menu
        var contactUsMenuLocator = Driver.FindElement(By.XPath("//a[text()=' Contact us']"));
        contactUsMenuLocator.Click();

        // Formulario Contact Us
        var nameInputLocator = GetElementUntilIsVisible(By.CssSelector($"[data-qa='name']"));
        nameInputLocator.SendKeys(name);
        var emailInputLocator = GetElementUntilIsVisible(By.CssSelector($"[data-qa='email']"));
        emailInputLocator.SendKeys(email);
        var subjetInputLocator = GetElementUntilIsVisible(By.CssSelector($"[data-qa='subject']"));
        subjetInputLocator.SendKeys(subject);
        var messageInputLocator = GetElementUntilIsVisible(By.CssSelector($"[data-qa='message']"));
        subjetInputLocator.SendKeys(message);
        // Se sube una imagen relacionada a la consulta para validar el funcionamiento del input de tipo file
        UploadImage(By.CssSelector($"[name='upload_file']"), "camiseta_01.jpg");
        var submitButtonLocator = GetElementUntilIsVisible(By.CssSelector($"[data-qa='submit-button']"));
        submitButtonLocator.Click();
        IAlert alert = Driver.SwitchTo().Alert();
        alert.Accept();

        var alertSuccessMessageLocator = GetElementUntilIsVisible(By.XPath($"//div[@class='status alert alert-success' and contains(., 'Success')]"));
        Assert.That(alertSuccessMessageLocator.Text, Is.EqualTo("Success! Your details have been submitted successfully."),
            "El mensaje de éxito no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, "registro_formulario_contactus");
    }

    [Test]
    public void ValidarRegistroNewsletter()
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var email = $"emeneses_{timestamp}@test.com";
        // Click la opcion Products en el menu
        var newsletterInputLocator = Driver.FindElement(By.Id("susbscribe_email"));
        // Mouse hover sobre el botton add to card
        var actions = new OpenQA.Selenium.Interactions.Actions(Driver);
        actions.MoveToElement(newsletterInputLocator).Perform();
        newsletterInputLocator.SendKeys(email);
        var suscribeButtonLocator = GetElementUntilIsVisible(By.Id("subscribe"));
        suscribeButtonLocator.Click();
        var alertSuccessMessageLocator = GetElementUntilIsVisible(By.XPath($"//div[@class='alert-success alert' and contains(., 'subscribed!')]"));
        Assert.That(alertSuccessMessageLocator.Text, Is.EqualTo("You have been successfully subscribed!"),
            "El mensaje de éxito no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, "Registro_Newsletter");
    }


    public void ValidarTotalProducto(string nombreProducto)
    {
        var priceLocator = GetElementUntilIsVisible(By.XPath($"//tr[contains(., '{nombreProducto}')]//td[@class='cart_price']/p"));
        var priceProduct = int.Parse(priceLocator.Text.Replace("Rs. ",""));
        Console.WriteLine($"Precio del producto '{nombreProducto}' en el carrito: {priceProduct}");
        var quantityLocator = GetElementUntilIsVisible(By.XPath($"//tr[contains(., '{nombreProducto}')]//td[@class='cart_quantity']/button"));
        var quantityProduct = int.Parse(quantityLocator.Text);
        Console.WriteLine($"Cantidad del producto '{nombreProducto}' en el carrito: {quantityProduct}");
        var totalLocator = GetElementUntilIsVisible(By.XPath($"//tr[contains(., '{nombreProducto}')]//td[@class='cart_total']/p"));
        var totalProduct = int.Parse(totalLocator.Text.Replace("Rs. ",""));
        Console.WriteLine($"Total del producto '{nombreProducto}' en el carrito: {totalProduct}");
        // Se valida que el total del producto sea igual al precio por la cantidad
        Assert.That(totalProduct, Is.EqualTo(priceProduct * quantityProduct), $"El total del producto '{nombreProducto}' no es correcto.");
    }

    public string AgregarProductoAlCarrito(bool viewCart)
    {
        // Si ya no quedan productos disponibles, reiniciar la lista
        if (availableProducts == null || availableProducts.Count == 0)
        {
            availableProducts = new List<string>(_initialProductNames);
        }

        var random = new Random();
        var index = random.Next(availableProducts.Count);
        var randomProductName = availableProducts[index];
        // Remover el producto seleccionado para evitar repetición
        availableProducts.RemoveAt(index);
        Console.WriteLine($"Nombre de producto seleccionado aleatoriamente: {randomProductName}");

        var ramdomProduct = GetElementUntilIsVisible(By.XPath($"(//div[@class='single-products' and contains(., '{randomProductName}')]//i)[1]"));
        // Mouse hover sobre el botton add to card
        var actions = new OpenQA.Selenium.Interactions.Actions(Driver);
        actions.MoveToElement(ramdomProduct).Perform();
        // Esperar hasta que el elemento esté presente
        WaitUntilCssValueIs(By.XPath($"//div[contains(@class, 'product-overlay') and .//h2[contains(., '{randomProductName}')]]"), "display", "block");
        var buttonAddCartProducto = GetElementUntilIsVisible(By.XPath($"(//div[@class='single-products' and contains(., '{randomProductName}')]//i)[2]"));
        actions.MoveToElement(buttonAddCartProducto).Perform();
        buttonAddCartProducto.Click();
        var modalLabelProductAdded = GetElementUntilIsVisible(By.XPath("//div[@id='cartModal']//h4[contains(., 'Added!')]"));
        Assert.That(modalLabelProductAdded.Text, Is.EqualTo("Added!"), "El mensaje de producto agregado no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, $"producto_agregado_{randomProductName}".Replace(".","").Replace(" ", ""));
        if (viewCart)
        {
            var modalButtonViewCart = GetElementUntilIsVisible(By.XPath("//div[@id='cartModal']//u[contains(., 'View Cart')]"));
            modalButtonViewCart.Click();            
        } else
        {
            var modalButtonContinueShopping = GetElementUntilIsVisible(By.XPath("//div[@id='cartModal']//button[contains(., 'Continue Shopping')]"));
            modalButtonContinueShopping.Click();
        }

        return randomProductName;
    }

    public void LoginUsuarioExistente(string email, string password)
    {
        // Click en el botón de Signup / Login
        var singUpLocator = Driver.FindElement(By.XPath("//a[text()=' Signup / Login']"));
        singUpLocator.Click();
        // Email
        var emailInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='login-email']"));
        emailInputLocator.SendKeys(email);
        // Name
        var passwordInputLocator = Driver.FindElement(By.XPath("//*[@data-qa='login-password']"));
        passwordInputLocator.SendKeys(password);
        // Click en el botón de Login
        var loginButtonLocator = Driver.FindElement(By.XPath("//*[@data-qa='login-button']"));
        loginButtonLocator.Click();
    }

    private void UploadImage(By locator, string imageFileName)
    {
        // Obtiene el input type=file y envía la ruta completa del archivo para subirlo
        var fileInput = GetElementUntilIsVisible(locator);

        // Build images directory under project: <projectRoot>/Reporte/Images
        var projectPath = ScreenshotHelper.GetPathFromProject();
        var imagesDir = Path.Combine(projectPath, "Images", imageFileName);

        if (!File.Exists(imagesDir))
            throw new FileNotFoundException($"No se encontró la imagen '{imageFileName}' en rutas esperadas.", imagesDir);

        fileInput.SendKeys(imagesDir);
    }


    private void CloseBrowser()
    {
        if (Driver != null)
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }

    private void WaitUntilCssValueIs(By locator, string cssProperty, string expectedValue, int timeoutInSeconds = 30)
    {
        try
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            localWait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);
                    string actualValue = element.GetCssValue(cssProperty);
                    return actualValue == expectedValue;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            });
        } 
        catch(WebDriverTimeoutException ex)
        {
            throw new Exception($"El elemento con el locator {locator} no alcanzó el valor CSS '{expectedValue}' para" +
                $" la propiedad '{cssProperty}' después de {timeoutInSeconds} segundos.", ex);
        }
    }

    private IWebElement GetElementUntilIsVisible(By locator, int timeoutInSeconds = 30)
    {
        try 
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return localWait.Until(d =>
            {
                try 
                {
                    var element = d.FindElement(locator);
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

        }
        catch (WebDriverTimeoutException ex)
        {
            throw new NoSuchElementException($"El elemento con el locator {locator} no se volvió visible después de {timeoutInSeconds} segundos.", ex);
        }
    }
}
