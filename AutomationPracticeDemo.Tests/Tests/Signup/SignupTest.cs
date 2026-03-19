using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.Components;
using AutomationPracticeDemo.Tests.Utils;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace AutomationPracticeDemo.Tests.Tests.Signup
{
    [TestFixture]
    public class SignupTest : TestBase
    {
        private string _nameSignup; 
        private string _emailRandom;

        [SetUp]
        public void SetupData()
        {
            int random = new Random().Next(1, 99999);
            _nameSignup = "Kenneth" + random;
            _emailRandom = "kenneth" + random + "@cenfotec.com";
        }


        [Test]
        public void SignupNewUser()
        {
            var menuPage = new menuPage(Driver, Wait);
            var signUp = new SignUp(Driver, Wait);

            // Navegación a la página de registro
            menuPage.ClickSignupLogin();

            //Llenado del formulario de login
            signUp.InputSignup(_nameSignup, _emailRandom);
            signUp.ClickSignupButton();

            ScreenshotHelper.TakeScreenshot(Driver, "Signup_Test.png");

            string title = signUp.MessageSignup();

            Assert.That(title, Is.EqualTo("ENTER ACCOUNT INFORMATION").IgnoreCase, "El título de la página de registro no coincide");
        }

        [Test]

        public void SignupInformation()
        {
            var menuPage = new menuPage(Driver, Wait);
            var signUp = new SignUp(Driver, Wait);

            // Navegación a la página de registro
            menuPage.ClickSignupLogin();

            //Llenado del formulario de login
            signUp.InputSignup(_nameSignup, _emailRandom);
            signUp.ClickSignupButton();

            //Seleccionar Mr. o Mrs.
            signUp.TitleSignup();
            ScreenshotHelper.TakeScreenshot(Driver, "TitleSignUp_Test.png");

            signUp.PasswordSignup("12345678");
            ScreenshotHelper.TakeScreenshot(Driver, "PasswordSignUp_Test.png");

            signUp.FirstNameSignup(_nameSignup);
            ScreenshotHelper.TakeScreenshot(Driver, "NameSignUp_Test.png");

            signUp.LastNameSignup("Smith");
            ScreenshotHelper.TakeScreenshot(Driver, "LastName_Test.png");

            signUp.AddressSignup("123 Main St");
            ScreenshotHelper.TakeScreenshot(Driver, "Address_Test.png");

            signUp.CountrySignup("New Zealand");
            ScreenshotHelper.TakeScreenshot(Driver, "Country_Test.png");

            signUp.StateSignup("Florida");
            ScreenshotHelper.TakeScreenshot(Driver, "State_Test.png");

            signUp.CitySignup("Miami");
            ScreenshotHelper.TakeScreenshot(Driver, "City_Test.png");

            signUp.ZipCodeSignup("33101");
            ScreenshotHelper.TakeScreenshot(Driver, "ZipCode_Test.png");

            signUp.MobileSignup("1234567890");
            ScreenshotHelper.TakeScreenshot(Driver, "MobileNumber_Test.png");

            signUp.CreateAccountButton();
            ScreenshotHelper.TakeScreenshot(Driver, "CreateAccount_Test.png");

            string message = signUp.MessageAccountCreated();
            Assert.That(message, Is.EqualTo("ACCOUNT CREATED!"), "El mensaje de cuenta creada no coincide");
        }
    }
}