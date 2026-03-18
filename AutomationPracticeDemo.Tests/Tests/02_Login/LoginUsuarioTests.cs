using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.Asserts;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Tests.Login.Asserts;
using NUnit.Framework;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class LoginUsuarioTests : Base.TestBase
    {
        [Test, TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.UsersIsValid))]
        public void LoginValido(string email, string password)
        {
            var loginPage = new LoginPage(Driver);
            loginPage.GoTo();
            loginPage.Login(email, password);

            LoginAsserts.AssertLoginExitoso(Driver.PageSource);
        }

        [Test, TestCaseSource(typeof(LoginDataSource), nameof(LoginDataSource.UsersNotValid))]
        public void LoginInvalido(string email, string password)
        {
            var loginPage = new LoginPage(Driver);
            loginPage.GoTo();
            loginPage.Login(email, password);

            LoginAsserts.AssertLoginFallido(Driver.PageSource);
        }
    }


}
