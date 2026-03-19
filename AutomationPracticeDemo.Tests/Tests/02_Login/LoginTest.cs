using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.MainComponents;
using AutomationPracticeDemo.Tests.Tests.Login.Asserts;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests.Login
{

    /// <summary>
    /// Ejercicio 2: Login con usuario existente
    /// </summary>

    [TestFixture]
    public class LoginTest : TestBase
    {


        [Test, Category("Login"), TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.UsersIsValid))]
        public void LoginWithValidUser(string email, string password)
        {
            var menuPage = new menuPage(Driver);
            var loginPage = new LoginPage(Driver);

            // Navegación a la página de registro
            menuPage.ClickSignUpLogin();
            Assert.That(loginPage.GetTitleLoginAccount, Is.EqualTo("Login to your account"));

            //Llenado del formulario de login
            loginPage.FillLogin(email, password);
            loginPage.SubmitLogin();
            ScreenshotHelper.TakeScreenshot(Driver, "LoginByUserTest_test.png");

            Assert.That(menuPage.validatedUserLogout(), Is.EqualTo("Logout"));
            ScreenshotHelper.TakeScreenshot(Driver, "Logout_test.png");
        }

        [Test, Category("Login"), TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.UsersNotValid))]
        public void LoginWithNotValidUser(string email, string password)
        {
            var menuPage = new menuPage(Driver);
            var loginPage = new LoginPage(Driver);

            //Navegación a la página de registro
            menuPage.ClickSignUpLogin();
            Assert.That(loginPage.GetTitleLoginAccount, Is.EqualTo("Login to your account"));

            //Llenado del formulario de login
            loginPage.FillLogin(email, password);
            loginPage.SubmitLogin();
            Assert.That(loginPage.GetMessageIncorrectPassword, Is.EqualTo("Your email or password is incorrect!"));
            ScreenshotHelper.TakeScreenshot(Driver, "LoginWithNotValidUser.png");
        }
    }
}
