using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AutomationExercise.Tests.Utils;

namespace AutomationExercise.Tests.Tests;

public class RegistrationTests : TestBase
{
    [Test]
    public void Should_RegisterNewUser()
    {
        // 1. Abrir https://automationexercise.com/ (hecho en Setup)

        // 2. Click en Signup / Login
        var signupLogin = Driver.FindElement(By.XPath("//a[contains(., 'Signup / Login') or contains(., 'Signup / login') or contains(., 'Signup /login')]") );
        signupLogin.Click();

        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElement(By.Name("name")));

        // 3. Completar nombre y correo.
        var nameInput = Driver.FindElement(By.Name("name"));
        var emailInput = Driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));

        var userName = "testuser_" + Guid.NewGuid().ToString("N").Substring(0, 6);
        var userEmail = userName + "@example.com";

        nameInput.SendKeys(userName);
        emailInput.SendKeys(userEmail);

        Driver.FindElement(By.XPath("//button[@data-qa='signup-button']")).Click();

        // 4. Continuar con los datos del formulario
        wait.Until(d => d.FindElement(By.Id("id_gender1")));

        // completar contraseña
        Driver.FindElement(By.Id("password")).SendKeys("Password123!");

        // fecha de nacimiento
        var day = new SelectElement(Driver.FindElement(By.Id("days")));
        day.SelectByIndex(5);
        var month = new SelectElement(Driver.FindElement(By.Id("months")));
        month.SelectByIndex(3);
        var year = new SelectElement(Driver.FindElement(By.Id("years")));
        year.SelectByText("1990");

        // checkbox suscripciones
        Driver.FindElement(By.Id("newsletter")).Click();
        Driver.FindElement(By.Id("optin")).Click();

        // nombre y apellido
        Driver.FindElement(By.Id("first_name")).SendKeys("Test");
        Driver.FindElement(By.Id("last_name")).SendKeys("User");

        // company, address, country, state, city, zipcode, mobile
        Driver.FindElement(By.Id("company")).SendKeys("MyCompany");
        Driver.FindElement(By.Id("address1")).SendKeys("123 Main St");
        Driver.FindElement(By.Id("country")).SendKeys("United States");
        Driver.FindElement(By.Id("state")).SendKeys("CA");
        Driver.FindElement(By.Id("city")).SendKeys("San Francisco");
        Driver.FindElement(By.Id("zipcode")).SendKeys("94101");
        Driver.FindElement(By.Id("mobile_number")).SendKeys("+11234567890");

        // 5. Click en Create Account.
        Driver.FindElement(By.XPath("//button[@data-qa='create-account']")).Click();

        // 6. Validar mensaje: Account Created!
        wait.Until(d => d.FindElement(By.XPath("//h2[contains(., 'Account Created') or contains(., 'Account Created!')]") ) );
        var createdText = Driver.FindElement(By.XPath("//h2[contains(., 'Account Created') or contains(., 'Account Created!')]")).Text;
        Assert.That(createdText.ToLower(), Does.Contain("account created"));

        // 7. Click en Continue ? validar que el usuario está logueado.
        Driver.FindElement(By.XPath("//a[contains(., 'Continue')]")).Click();

        // En algunos casos el sitio carga en una nueva página con anuncios, esperar y validar presencia de 'Logged in as'
        wait.Until(d => d.FindElement(By.XPath("//a[contains(., 'Logged in as') or contains(., 'Logged in as ')]")));
        var loggedInText = Driver.FindElement(By.XPath("//a[contains(., 'Logged in as') or contains(., 'Logged in as ')]")).Text;
        Assert.That(loggedInText.ToLower(), Does.Contain("logged in as"));

        ScreenshotHelper.TakeScreenshot(Driver, "registration_result.png");
    }
}
