using NUnit.Framework;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormTests : TestBase
    {
        [Test]
        [Description("Flujo completo de registro de nuevo usuario hasta validación de login")]
        public void UserRegistrationFlow_ShouldRegisterAndLoginSuccessfully()
        {
            // 1. Inicialización
            var formPage = new FormPage(Driver);
            formPage.ClickSignup();

            formPage.NewUser("KennethTest", "visual@studio.com");
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 1000);
            string emailRandom = "PracticaClase3" + randomNumber + "@cenfotec.com";


            formPage.ClickSignupButton();

            // 2. Ejecución de pasos (Act)
            formPage.SelectTitle();

            formPage.Password1("Prueba123456");
            formPage.CompleteInformation(
                "Kenneth", "Oviedo", "Microsoft", "Calle Falsa 123",
                "United States", "California", "Los Angeles", "90001", "1234567890"
            );

            formPage.ValidateCreateUserButton();

            // 3. Validaciones (Assert)
            // Usamos Assert.Multiple para validar varias cosas al mismo tiempo
            Assert.Multiple(() =>
            {
                // Validar mensaje de éxito
                string actualMessage = formPage.ValidateMessage();
                Assert.That(actualMessage, Is.EqualTo("ACCOUNT CREATED!"), "El mensaje de éxito no coincide");

                // Continuar al área de login
                formPage.ValidateContinueButton();

                // Validar que el usuario aparece logueado (el elemento de tu imagen)
                Assert.That(formPage.IsUserLoggedIn(), $"El usuario no aparece como conectado en la cabecera.");
            });
        }
    }
}

//[Test]
//public void Click_SignupTest()
//{
//    var formPage = new FormPage(Driver);
//    formPage.ClickSignup();
//    //ScreenshotHelper.TakeScreenshot(Driver, "form_test.png");
//    Assert.Pass("Click en Signup");
//}

//[Test]
//public void Enter_Name()
//{
//    var formPage = new FormPage(Driver);
//    formPage.NewUser("KennethTest", "visual@studio.com");
//    Assert.Pass("Usuario registrado correctamente");
//}

//[Test]
//public void Select_Title()
//{
//    var formPage = new FormPage(Driver);
//    formPage.SelectTitle();
//    Assert.Pass("Title seleccionado correctamente");
//}

//[Test]
//public void Enter_Password()
//{
//    var formPage = new FormPage(Driver);
//    formPage.Password1("Prueba123456");
//    Assert.Pass("Contraseña ingresada correctamente");

//}
//[Test]
//public void Information()
//{
//    var formPage = new FormPage(Driver);
//    formPage.CompleteInformation("Kenneth", "Oviedo", "Microsoft", "Calle Falsa 123", "United States", "California", "Los Angeles", "90001", "1234567890");
//    Assert.Pass("Información completa ingresada correctamente");
//}

//[Test]
//public void Create_Account()
//{
//    var formPage = new FormPage(Driver);
//    formPage.ValidateCreateUserButton();
//    Assert.Pass("Cuenta creada correctamente");
//}

//[Test]
//public void Validate_Message()
//{
//    var formPage = new FormPage(Driver);
//    string actualMessage = formPage.ValidateMessage();
//    string expectedMessage = "ACCOUNT CREATED!";
//    Assert.That(actualMessage, Is.EqualTo(expectedMessage), "El mensaje de éxito no coincide");
//}

//[Test]
//public void Validate_btnContinue()
//{
//    var formPage = new FormPage(Driver);
//    formPage.ValidateContinueButton();
//    Assert.Pass("Continuar a la cuenta creada correctamente");
//}

//[Test]
//public void Validate_Logged()
//{
//    var formPage = new FormPage(Driver);
//    formPage.IsUserLoggedIn();
//    Assert.Pass("Usuario logueado correctamente");
//}



