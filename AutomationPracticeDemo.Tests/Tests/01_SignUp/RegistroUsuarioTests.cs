using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.Asserts;
using AutomationPracticeDemo.Tests.Models;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class RegistroUsuarioTests : Base.TestBase
    {
        [Test, TestCaseSource(typeof(DataHelper), nameof(DataHelper.GetUsers))]
        public void RegistroUsuarioNuevo(UserData user)
        {
            Driver.Navigate().GoToUrl("https://automationexercise.com/login");

            var signUpPage = new SignUpPage(Driver, Wait);

            // Paso 1: llenar nombre y correo (email único con Guid)
            string uniqueEmail = $"{Guid.NewGuid()}@mail.com";
            signUpPage.FillSignup(user.AccountInformation.Name, uniqueEmail);

            // Paso 2: llenar Account Information
            var dob = user.AccountInformation.DateOfBirth.Split('/');
            signUpPage.FillAccountInformation(
                user.AccountInformation.Title,
                user.AccountInformation.Name,
                user.AccountInformation.Password,
                dob[0], dob[1], dob[2]
            );

            // Paso 3: llenar Address Information
            signUpPage.FillAddressInformation(
                user.AddressInformation.FirstName,
                user.AddressInformation.LastName,
                user.AddressInformation.Company,
                user.AddressInformation.Address,
                user.AddressInformation.Address2,
                user.AddressInformation.Country,
                user.AddressInformation.State,
                user.AddressInformation.City,
                user.AddressInformation.Zipcode,
                user.AddressInformation.MobileNumber
            );

            // 📸 Pantallazo antes de crear la cuenta
            ScreenshotHelper.TakeScreenshot(Driver, "Datos_Usuario_Antes_CrearCuenta");

            // Paso 4: crear cuenta
            signUpPage.SubmitCreateAccount();

            // Validar que la cuenta fue creada
            Assert.That(signUpPage.IsAccountCreated(), Is.True);

            // 📸 Pantallazo donde se vea ACCOUNT CREATED
            ScreenshotHelper.TakeScreenshot(Driver, "Cuenta_Creada");

            // Continuar después de crear la cuenta
            signUpPage.ClickContinue();
        }
    }
}


