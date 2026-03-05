using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics.Metrics;


namespace AutomationPracticeDemo.Tests.Tests;

public class ShoppingTest
{
    private IWebDriver Driver;

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

        // Label de usuario logueado
        var loggedUserLocator = Driver.FindElement(By.XPath("//a[contains(text(), 'Logged in as')]"));
        Assert.That(loggedUserLocator.Text, Does.Contain(usuario), "El usuario logueado no es el esperado.");
    }

    [Test]
    public void ValidarTotalAlAgregarEnCarrito()
    {
        var email = "emeneses_1772680170689@test.com";
        var password = "password12345!";
        var usuario = "Erick Meneses";

        // Click en el botón de Signup / Login
        var singUpLocator = Driver.FindElement(By.XPath("//a[text()=' Signup / Login']"));
        singUpLocator.Click();
    }


    private void CloseBrowser()
    {
        if (Driver != null)
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
