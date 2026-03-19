using AutomationPracticeDemoTest.Tests.Login.Asserts;
using AutomationPracticeDemoTests.Pages;
using AutomationPracticeDemoTests.Utils;

namespace AutomationPracticeDemoTests.Tests.Login;

[TestFixture]
public class LoginTest : BaseTest
{
    private string folderName = "LoginTest";

    [Test, Category("Login"), TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.UsersIsValid))]
    public void LoginWithValidUser(LoginData data)
    {
        var homePage = new HomePage(Driver);
        var singUpLoginPage = homePage.TopMenu.GoToSingUpPage();
        // Verificar que el titulo mostrado en la pagina de login es el esperado
        Assert.That(singUpLoginPage.GetTitleLoginAccount(), Is.EqualTo("Login to your account"),
            "Error el titulo mostrado no corresponde al esperado");
        singUpLoginPage.LoginUsuario(data.Email, data.Password);
        // Verificar que el usuario logueado es el esperado
        Assert.That(homePage.TopMenu.GetLoggedUser(), Does.Contain(data.NombreUsuario), "El usuario logueado no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, $"LoginWithValidUserTest_{data.NombreUsuario}".Trim(), folderName);
    }


    [Test, Category("Login"), TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.UsersNotValid))]
    public void LoginWithNotValidUser(LoginData data)
    {
        var homePage = new HomePage(Driver);
        var singUpLoginPage = homePage.TopMenu.GoToSingUpPage();
        // Verificar que el titulo mostrado en la pagina de login es el esperado
        Assert.That(singUpLoginPage.GetTitleLoginAccount(), Is.EqualTo("Login to your account"),
            "Error el titulo mostrado no corresponde al esperado");
        singUpLoginPage.LoginUsuario(data.Email, data.Password);
        // Verificar que el usuario logueado es el esperado
        Assert.That(singUpLoginPage.GetInvalidLoginErrorMessage(), Is.EqualTo("Your email or password is incorrect!"),
            "El mensaje de error mostrado no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, $"LoginWithNotValidUserTest_{data.NombreUsuario}".Trim(), folderName);
    }
}
