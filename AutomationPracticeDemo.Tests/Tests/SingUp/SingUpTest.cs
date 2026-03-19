using AutomationPracticeDemoTest.Tests.SingUp.Assets;
using AutomationPracticeDemoTests.Pages;
using AutomationPracticeDemoTests.Utils;

namespace AutomationPracticeDemoTests.Tests.SingUpTest;

[TestFixture]
public class SingUpTest : BaseTest
{
    private string folderName = "SingUpTest";

    [Test, Category("SingUp"), TestCaseSource(typeof(SingUpDataSource), nameof(SingUpDataSource.AccountInformation))]
    public void RegistroDeUsuarioTest(AccountData data)
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var email = $"autodemo_{timestamp}@test.com";
        var usuario = data.AccountInformation.Name;
        var password = data.AccountInformation.Password;
        var firstName = data.AddressInformation.FirstName;
        var lastName = data.AddressInformation.LastName;
        var address = data.AddressInformation.Address;
        var country = data.AddressInformation.Country;
        var state = data.AddressInformation.State;
        var city = data.AddressInformation.City;
        var zipCode = data.AddressInformation.Zipcode;
        var mobilePhone = data.AddressInformation.MobileNumber;

        var homePage = new HomePage(Driver);
        var singUpLoginPage = homePage.TopMenu.GoToSingUpPage();
        var signUpPage = singUpLoginPage.SingUpNuevoUsuario(usuario, email);
        signUpPage.FillSignUpForm(password, firstName, lastName, address,
            country, state, city, zipCode, mobilePhone);
        var accountCreatedPage =  signUpPage.GoToAccountCreatedPage();
        Assert.That(accountCreatedPage.GetCreatedAccountTitle(), Is.EqualTo("ACCOUNT CREATED!"),
            "El mensaje de cuenta creada no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, $"Cuenta_Creada_{email}".Replace("@test.com","").Trim(), folderName);
        homePage = accountCreatedPage.ClickContinueButton();
        Assert.That(homePage.TopMenu.GetLoggedUser(), Does.Contain(usuario), "El usuario logueado no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, $"Nueva_Cuenta_Login_{email}".Replace("@test.com","").Trim(), folderName);
    }
}
