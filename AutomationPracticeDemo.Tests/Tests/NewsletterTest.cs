using NUnit.Framework;
using OpenQA.Selenium;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Tests.Tests
{
    [TestFixture]
    public class NewsletterTest
    {
        private IWebDriver Driver;

        [SetUp]
        public void SuscribirseAlNewsletter()
        {
            var newsletterPage = new NewsletterPage(Driver);

            // Usamos un email de prueba
            newsletterPage.SubscribeToNewsletter("silvia.news@test.com");

            var success = newsletterPage.GetSuccessMessage();
            Assert.That(success, Does.Contain("You have been successfully subscribed!"));
        }
    }
}