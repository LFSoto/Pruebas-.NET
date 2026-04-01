using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.MainComponents;
using AutomationPracticeDemo.Tests.Utils;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using Reqnroll;
using System;
using System.Xml.Linq;

namespace AutomationPracticeDemo.Tests.StepDefinitions
{
    [Binding]
    public class ContactUsFormSubmissionStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _driver;
        private ContactUSPage _contactUsPage;
        private menuPage _menuPage;

        public ContactUsFormSubmissionStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = _scenarioContext.Get<IWebDriver>();
            _contactUsPage = new ContactUSPage(_driver);
            _menuPage = new menuPage(_driver);

        }

        [When("I navigate to the CONTACT US page")]
        public void WhenINavigateToThePage()
        {
            // Navegación a la página de Contact Us
            _menuPage.ClickContactUsOption();
            

        }

        [Then("I should see the title \"(.*)\" in page header")]
        public void ThenIShouldSeeTheTitleInPageHeader(string title)
        {
            Assert.That(_contactUsPage.GetTitleContactUSPage, Is.EqualTo(title));
        }


        [When("I fill the contact form with:\"(.*)\",\"(.*)\",\"(.*)\",\"(.*)\"")]
        public void WhenIFillTheContactFormWith(string name, string email, string subject, string message)
        {
            //Llenado del formulario de Contact Us
            _contactUsPage.FillContactForm(name, email, subject, message);
        }


        [When("I upload the file \"(.*)\"")]
        public void WhenIUploadTheFile(string imagen)
        {
            //Ruta del archivo a subir
            var imagePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Asserts", imagen);

            //Subir un archivo (opcional)
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Imagen no encontrada: {imagePath}");
            }
            else _contactUsPage.UploadFile(imagePath);
            ScreenshotHelper.TakeScreenshot(_driver, "ContactUs_test.png");
        }

        [When("I click on the button to submit the contact form")]
        public void WhenIClickOnTheButtonToSubmitTheContactForm()
        {
            _contactUsPage.SubmitContactForm();
        }

        [Then("I should see an alert with message \"(.*)\"")]
        public void ThenIShouldSeeAnAlertWithMessage(string message)
        {
            //Validar mensaje de éxito
            Assert.That(_contactUsPage.GetAlertMessage(), Is.EqualTo(message));
        }

        [When("I click accept button the alert")]
        public void WhenIClickAcceptButtonTheAlert()
        {
            _contactUsPage.AcceptAlert();
        }

        [Then("I should see the success message \"(.*)\"")]
        public void ThenIShouldSeeTheSuccessMessage(string messag)
        {
            //Validar mensaje de éxito
            Assert.That(_contactUsPage.GetSuccessMessage(), Is.EqualTo("Success! Your details have been submitted successfully."));
        }
    }
}
