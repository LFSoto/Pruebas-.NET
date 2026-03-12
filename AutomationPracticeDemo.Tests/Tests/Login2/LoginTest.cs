using AutomationPracticeDemo.Tests.Utils;
using System;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Tests.Login2.Asserts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests.Login2
{
    public class LoginTest : TestBase
    {
        [Test, Category("Login"), TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.UsersIsValid))]
        public void LoginIsValid(string email, string password)
        {
            var loginPage = new Pages.LoginPage(Driver);
            loginPage.ClickLogin();
            loginPage.ingresarLogin(email, password);
            loginPage.ClickLogin2();

   
                Assert.That(loginPage.validatedUserLogout(), Is.EqualTo("Logout"));
                ScreenshotHelper.TakeScreenshot(Driver, "Logout.png");
          
               
    
            }

        [Test, Category("Login"), TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.UsersNotValid))]
        public void LoginWithNotValidUser(string email, string password)
        {
           
            var loginPage = new Pages.LoginPage(Driver);
            loginPage.ClickLogin();
            loginPage.ingresarLogin(email, password);
            loginPage.ClickLogin2();
            Assert.That(loginPage.GetMessageIncorrectPassword(), Is.EqualTo("Your email or password is incorrect!"));
            ScreenshotHelper.TakeScreenshot(Driver, "LogoutNoValido.png");
        }


    }
}
