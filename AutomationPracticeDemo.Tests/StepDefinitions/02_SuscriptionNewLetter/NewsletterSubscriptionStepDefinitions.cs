using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.MainComponents;
using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using Reqnroll;
using System;

namespace AutomationPracticeDemo.Tests.StepDefinitions
{
    [Binding]
    public class NewsletterSubscriptionStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _driver;
        private footerPage _footerPage;
     
        public NewsletterSubscriptionStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = _scenarioContext.Get<IWebDriver>();
            _footerPage = new footerPage(_driver);
        }

        [When("I enter \"(.*)\" in the newsletter subscription field")]
        public void WhenIEnterInTheNewsletterSubscriptionField(string email)
        {
            // Suscripción al newsletter
            _footerPage.FillSuscribeInput(email);
        }

        [When("I click the Subscribe button")]
        public void WhenIClickTheButton()
        {
            _footerPage.SumitSuscibeButton();
            ScreenshotHelper.TakeScreenshot(_driver, "Suscription_test.png");
        }

        [Then("I should see the message \"(.*)\"")]
        public void ThenIShouldSeeTheMessage(string message)
        {
            //Validar mensaje de éxito
            Assert.That(_footerPage.GetSuscribeMessage(), Is.EqualTo(message));
        }
    }
}
