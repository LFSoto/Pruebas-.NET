using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.MainComponents;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using Reqnroll;
using System;

namespace AutomationPracticeDemo.Tests.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _driver;
        private LoginPage _loginPage;
        private menuPage _menuPage;
        //private readonly LoginDataSource _loginData; 
        public LoginStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = _scenarioContext.Get<IWebDriver>();
            _loginPage = new LoginPage(_driver);
            _menuPage = new menuPage(_driver);
            //_loginData = new LoginDataSource();
        }


        [Given(@"I am on the start page")]
        public void GivenIAmOnTheStartPage()
        {
            _menuPage.ClickHomeOption();
        }

        [Given("I navigate to the Login page")]
        public void GivenINavigateToTheLoginPage()
        {
            _menuPage.ClickSignUpLogin();
        }

        [Then(@"I should see the title ""(.*)""")]
        public void ThenIShouldSeeTheTitle(string title)
        {
            Assert.That(_loginPage.GetTitleLoginAccount, Is.EqualTo(title));
        }

        [When(@"I fill the login form with email ""(.*)"" and password ""(.*)""")]
        public void WhenIFillTheLoginFormWithEmailAndPassword(string email, string password)
        {
            _loginPage.FillLogin(email, password);
        }

        [When("I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _loginPage.SubmitLogin();
            ScreenshotHelper.TakeScreenshot(_driver, "LoginByUserTest_test.png");
        }

        [Then(@"I should see the ""(.*)"" button")]
        public void ThenIShouldSeeTheButton(string textOption)
        {
            Assert.That(_menuPage.validatedUserLogout(), Is.EqualTo(textOption));
            ScreenshotHelper.TakeScreenshot(_driver, "Logout_test.png");
        }

        [Then("I should see the error message \"(.*)\"")]
        public void ThenIShouldSeeTheErrorMessage(string message)
        {
            Assert.That(_loginPage.GetMessageIncorrectPassword, Is.EqualTo(message));
            ScreenshotHelper.TakeScreenshot(_driver, "LoginWithNotValidUser.png");
        }
    }
}
