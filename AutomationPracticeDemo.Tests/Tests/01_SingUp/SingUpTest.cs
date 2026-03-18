using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.MainComponents;
using AutomationPracticeDemo.Tests.Tests._01_SingUp.Asserts;
using AutomationPracticeDemo.Tests.Utils;


namespace AutomationPracticeDemo.Tests.Tests._01_SingUp
{
    [TestFixture]
    public class SingUpTest : TestBase
    {
       
        /// <summary>
        /// Ejercicio 1: Registro de usuario nuevo
        /// </summary
        [Test, Category("SingUp"), TestCaseSource(typeof(SingUpDataSource),nameof(SingUpDataSource.AccountInformation))]
        public void SingupTest(string name,AccountData userData)
        {

            //Declaración de variables
            var menuPage = new menuPage(Driver);
            var signUpPage = new SignUpPage(Driver);
            int random = new Random().Next(1, 10000);
            string emailRandom = "AutDemo00" + random + "@cenfotec.com";
            AccountInfo infoAccount = userData.GetAccountInformation();
            AddressInfo infoAddress = userData.GetAddressInformation();
            String[] fecha= infoAccount.DateOfBirth.Split('/');

            // Navegación a la página de registro
            menuPage.ClickSignUpLogin();
            Assert.That(signUpPage.GetTitleNewUserSignup(), Is.EqualTo("New User Signup!"));

            //Navegación al formulario de registro
            signUpPage.FillSignup(name, emailRandom);
            signUpPage.SubmitSignup();
            Assert.That(signUpPage.GetTitleEnterAccountInfo(), Is.EqualTo("ENTER ACCOUNT INFORMATION"));

            //Llenado del formulario de registro
            signUpPage.FillAccountInformation(infoAccount.Name, infoAccount.Title, infoAccount.Password, fecha[0], fecha[1], fecha[2]);
            Assert.That(signUpPage.ShouldValidatedTextEmailInput(), Is.EqualTo(emailRandom));
            ScreenshotHelper.TakeScreenshot(Driver, "AccountInformation_test.png");

            signUpPage.FillAddressInformation(infoAddress.FirstName, infoAddress.LastName, infoAddress.Company, infoAddress.Address, infoAddress.Address2, infoAddress.Country, infoAddress.State, infoAddress.City, infoAddress.Zipcode, infoAddress.MobileNumber);
            ScreenshotHelper.TakeScreenshot(Driver, "AddressInformation_test.png");
            signUpPage.SubmitCreateAccount();

            //Validación de cuenta creada
            Assert.That(signUpPage.GetAccountCreatedTitle(), Is.EqualTo("ACCOUNT CREATED!"));
            Assert.That(signUpPage.GetAccountCreatedMessage, Does.Contain("Congratulations! Your new account has been successfully created! " +
            "You can now take advantage of member privileges to enhance your online shopping experience with us."));
            ScreenshotHelper.TakeScreenshot(Driver, "AccountCreated_test.png");

            //Continuar a la página principal
            signUpPage.SumitContinue();

            //Validar que el usuario se ha logueado correctamente
            Assert.That(menuPage.validatedUserLogout(), Is.EqualTo("Logout"));
        }
    }
}
