using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.Components;
using AutomationPracticeDemo.Tests.Tests.Login.Asserts;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests.Login
{
    [TestFixture]
    public class LoginTest : TestBase
    {
        [Test, Category("Login"), TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.UsersIsValid))]
        public void LoginWithValidUser(string email, string password)
        {
            var menuPage = new menuPage(Driver, Wait);
            var loginPage = new LoginPage(Driver, Wait);

            // Navegación a la página de registro
            menuPage.ClickSignupLogin();
            
            //Llenado del formulario de login
            loginPage.InputLogin(email, password);
            loginPage.ClickLoginButton();
            ScreenshotHelper.TakeScreenshot(Driver, "Loggin_Test.png");

            Assert.That(menuPage.IsLoggedVisible(), Does.Contain("Logged in as"), "No se vio el login");
        }

        [Test, Category("Login"), TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.UsersNotValid))]

        public void LoginWithInvalidUser(string email, string password)
        {
            var menuPage = new menuPage(Driver, Wait);
            var loginPage = new LoginPage(Driver, Wait);
            // Navegación a la página de registro
            menuPage.ClickSignupLogin();
            //Llenado del formulario de login
            loginPage.InputLogin(email, password);
            loginPage.ClickLoginButton();
            Assert.That(loginPage.MessageInvalidLogin, Is.EqualTo("Your email or password is incorrect!"));
            ScreenshotHelper.TakeScreenshot(Driver, "Invalid User_Test.png");

        }
    }
}
