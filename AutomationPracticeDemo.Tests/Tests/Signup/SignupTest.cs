using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.Components;
using AutomationPracticeDemo.Tests.Utils;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace AutomationPracticeDemo.Tests.Tests.Signup
{
    [TestFixture]
    public class SignupTest : TestBase
    {

        
        string _nameSignup; 
        string _emailRandom;

        [SetUp]
        public void Setup()
        {
            int random = new Random().Next(1, 99999);
            _nameSignup = "Kenneth" + random;
            _emailRandom = "kenneth" + random + "@cenfotec.com";
        }


        [Test]
        public void SignupNewUser()
        {
            var menuPage = new menuPage(Driver);
            var SignUp = new SignUp(Driver, Wait);

            // Navegación a la página de registro
            menuPage.ClickSignupLogin();

            //Llenado del formulario de login
            SignUp.InputSignup(_nameSignup, _emailRandom);
            SignUp.ClickSignupButton();

            ScreenshotHelper.TakeScreenshot(Driver, "Signup_Test.png");

            string title = SignUp.MessageSignup();

            Assert.That(title, Is.EqualTo("ENTER ACCOUNT INFORMATION").IgnoreCase, "El título de la página de registro no coincide");
        }

        [Test]

        public void SignupInformation()
        {
            var menuPage = new menuPage(Driver);
            var SignUp = new SignUp(Driver, Wait);

            // Navegación a la página de registro
            menuPage.ClickSignupLogin();

            //Llenado del formulario de login
            SignUp.InputSignup(_nameSignup, _emailRandom);
            SignUp.ClickSignupButton();

            //Seleccionar Mr. o Mrs.
            SignUp.TitleSignup();
            ScreenshotHelper.TakeScreenshot(Driver, "TitleSignUp_Test.png");

            SignUp.PasswordSignup("12345678");
            ScreenshotHelper.TakeScreenshot(Driver, "PasswordSignUp_Test.png");

            SignUp.FirstNameSignup(_nameSignup);
            ScreenshotHelper.TakeScreenshot(Driver, "NameSignUp_Test.png");

            SignUp.LastNameSignup("Smith");
            ScreenshotHelper.TakeScreenshot(Driver, "LastName_Test.png");

            SignUp.AddressSignup("123 Main St");
            ScreenshotHelper.TakeScreenshot(Driver, "Address_Test.png");

            SignUp.CountrySignup("New Zealand");
            ScreenshotHelper.TakeScreenshot(Driver, "Country_Test.png");

            SignUp.StateSignup("Florida");
            ScreenshotHelper.TakeScreenshot(Driver, "State_Test.png");

            SignUp.CitySignup("Miami");
            ScreenshotHelper.TakeScreenshot(Driver, "City_Test.png");

            SignUp.ZipCodeSignup("33101");
            ScreenshotHelper.TakeScreenshot(Driver, "ZipCode_Test.png");

            SignUp.MobileSignup("1234567890");
            ScreenshotHelper.TakeScreenshot(Driver, "MobileNumber_Test.png");

            SignUp.CreateAccountButton();
            ScreenshotHelper.TakeScreenshot(Driver, "CreateAccount_Test.png");

            string message = SignUp.MessageAccountCreated();
            Assert.That(message, Is.EqualTo("ACCOUNT CREATED!"), "El mensaje de cuenta creada no coincide");
        }
    }
}