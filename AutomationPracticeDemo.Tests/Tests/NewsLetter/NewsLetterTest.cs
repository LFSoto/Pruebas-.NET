using AutomationPracticeDemo.Tests;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;

namespace AutomationPracticeDemo.Test.Tests.NewsLetter
{
    public class NewsLetterTest : BaseTest
    {
        [Test]
        public void ValidarRegistroNewsletter()
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var email = $"auto2demo_{timestamp}@test.com";

            var homePage = new HomePage(Driver);
            homePage.Footer.SubscribeToNewsletter(email);

            Assert.That(homePage.Footer.GetSuccessSubscriptionMessage(), Is.EqualTo("You have been successfully subscribed!"),
                "El mensaje de subscripcion de éxito no es el esperado.");
            ScreenshotHelper.TakeScreenshot(Driver, $"Registro_Newsletter_{email}".Replace("@test.com", "").Trim());
        }
    }
}
