using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Models;
using AutomationPracticeDemo.Tests.Utils;
using System.Collections.Generic;

namespace AutomationPracticeDemo.Tests.Tests
{
    [TestFixture]
    public class ContactUsTest
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
        }

        [Test]
        public void EnviarFormularioContactUs()
        {
            // Cargar datos desde JSON usando el helper independiente
            List<ContactUsModel> contacts = ContactUsJsonHelper.LoadContactUsData("Data/ContactUsData.json");

            foreach (var contact in contacts)
            {
                _driver.Navigate().GoToUrl("https://automationexercise.com");
                _driver.FindElement(By.CssSelector("a[href='/contact_us']")).Click();

                var contactPage = new ContactUsPage(_driver);

                contactPage.FillContactForm(contact.Name, contact.Email, contact.Subject, contact.Message);
                contactPage.SubmitForm(); // ahora maneja el alert
                var success = contactPage.GetSuccessMessage();
                Assert.That(success, Does.Contain("Success! Your details have been submitted successfully."));

            }
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}