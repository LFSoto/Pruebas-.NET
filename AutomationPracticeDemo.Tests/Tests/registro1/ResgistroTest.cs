using AutomationPracticeDemo.Tests.Pages;

using AutomationPracticeDemo.Tests.Tests.registro1.Asserts;
using AutomationPracticeDemo.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests.registro1
{
    public class ResgistroTest : TestBase
    {
        [Test, Category("SingUp"), TestCaseSource(typeof(SingUpDataSource), nameof(SingUpDataSource.AccountInformation))]
        public void Registro(string name,AccountData userData)
        {
            var RegistroPage = new RegistroPage(Driver);
            AccountInfo infoAccount = userData.GetAccountInformation();
            AddressInfo infoAddress = userData.GetAddressInformation();
            String[] fecha = infoAccount.DateOfBirth.Split('/');
            int random = new Random().Next(1, 1000);
            string emailRandom = "PracticaClase4" + random + "@cenfotec.com";
            RegistroPage.ClickLogin();
            RegistroPage.filllogin(name, emailRandom);
            RegistroPage.ClickSignup();
            RegistroPage.FillAccountInformation(infoAccount.Name, infoAccount.Title, infoAccount.Password, fecha[0], fecha[1], fecha[2]);
            RegistroPage.FillAddressInformation(infoAddress.FirstName, infoAddress.LastName, infoAddress.Company, infoAddress.Address, infoAddress.Address2, infoAddress.Country, infoAddress.State, infoAddress.City, infoAddress.Zipcode, infoAddress.MobileNumber);
            ScreenshotHelper.TakeScreenshot(Driver, "Information.png");
            RegistroPage.ClickCreateAccount();
            string mensaje = RegistroPage.messagingCreateAccount();
            Assert.That(mensaje, Is.EqualTo("ACCOUNT CREATED!"), "El mensaje no es el esperado");
            ScreenshotHelper.TakeScreenshot(Driver, "AccountCreated.png");
            RegistroPage.ClickContinue();
            Assert.That(RegistroPage.validatedUserLogout(), Is.EqualTo("Logout"));



        }
    }

}
