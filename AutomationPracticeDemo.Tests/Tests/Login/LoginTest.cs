using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Tests.Login.Asserts;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Drawing;

namespace AutomationPracticeDemo.Tests.Login
{
    [TestFixture]
    public class LoginTest:TestBase
    {
        
        [Test, Category("Login"),TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.AllUsers))] 
        public void ValidarLogin(LoginData login)
        {
            var loginPage = new LoginPage(Driver);
            // se le da click al boton sing up/login
            loginPage.BotonSingUp_Click();
            ScreenshotHelper.TakeScreenshot(Driver, "Login_1.png");
            // se llena la información del login
            loginPage.LlenarLogin(login.Email, login.Password);
            // dar click al boton enviar
            loginPage.Enviar();
            //se valida si es login valido o invalido 
            if (login.IsValid) { 
                //se valida login valido
                Assert.That(loginPage.ValidarInicioExitoso(), Does.Contain("Logged in as"));
                ScreenshotHelper.TakeScreenshot(Driver, "Login_2.png");
            }
            else { 
                //se valida login invalido
                Assert.That(loginPage.ValidarInicioFallido(), Does.Contain("Your email or password is incorrect!"));
                ScreenshotHelper.TakeScreenshot(Driver, "Login_3.png");
            }

        }
    }
}

