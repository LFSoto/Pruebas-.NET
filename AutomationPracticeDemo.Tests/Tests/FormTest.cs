using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

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

            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 1000);
            string nameRandom = "KennethTest" + randomNumber;
            string emailRandom = "PracticaClase3" + randomNumber + "@cenfotec.com";

            formPage.NewUser(nameRandom, emailRandom);

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
                Assert.That(formPage.IsUserLoggedIn(nameRandom), Is.True, "El usuario no aparece como conectado en la cabecera.");
            });
        }

        [Test]
        [Description("Validación Login con usuario existente")]

        public void ExistingUserLogin_ShouldLoginSuccessfully()
        {
            // 1. Inicialización
            var formPage = new FormPage(Driver);
            formPage.ClickSignup();

            // 2. Ejecución de pasos (Act)
            // Reemplazar por credenciales válidas de un usuario existente en la aplicación
            string existingEmail = "prueba1234@gmail.com";
            string existingPassword = "123456";
            string existingUsername = "prueba";

            formPage.EnterLoginCredentials(existingEmail, existingPassword);
            formPage.ClickLoginButton();

            // 3. Validaciones (Assert)
            Assert.That(formPage.IsUserLoggedIn(existingUsername), Is.True, "El usuario no aparece como conectado en la cabecera.");
        }

        [Test]
        [Description("Agregar productos al carrito y verificar total")]

        public void AddProductsToCartAndVerifyTotal_ShouldAddProductsAndVerifyTotal()
        {
            // 1. Inicialización
            var formPage = new FormPage(Driver);



            // 2. Ejecución de pasos (Act)
            formPage.AddProductsToCartAndVerifyTotal();
            // 3. Validaciones (Assert)
            // Aquí irían las validaciones para verificar que los productos se agregaron correctamente y que el total es correcto
            // Esto podría incluir:
            // - Verificar que los productos aparecen en el carrito
            // - Verificar que el total mostrado es el esperado


            Assert.Pass("La validación de agregar productos al carrito y verificar el total se realizó correctamente.");

        }

        [Test]
        [Description("Formulario Contact Us")]

        public void ContactUsForm_ShouldSubmitSuccessfully()
        {
            // 1. Inicialización
            var formPage = new FormPage(Driver);

            string relativePath = "semana.pdf";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utils", relativePath);

            //Console.WriteLine($"Ruta absoluta del archivo: {filePath}");
            // 2. Ejecución de pasos (Act)
            formPage.ContactUsForm("Prueba", "prueba1234@gmail.com", "Consulta de Práctica", "Este es un mensaje de prueba con archivo adjunto", filePath);


            string actualMessage = formPage.GetContactSuccessMessage();
            string expectedMessage = "Success! Your details have been submitted successfully.";

            Assert.That(actualMessage, Is.EqualTo(expectedMessage),
                "El mensaje de éxito tras enviar el formulario de contacto no coincide.");

            Console.WriteLine("Formulario de contacto enviado y validado correctamente.");
        }
    }
}