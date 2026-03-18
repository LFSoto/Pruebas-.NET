using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Pages.MainComponents;
using AutomationPracticeDemo.Tests.Utils;


namespace AutomationPracticeDemo.Tests.Tests.SuscriptionNewLetter
{
    public class SuscriptionNewLetterTest : TestBase
    {
        const string email = "SOFT-740@cenfotec.com";
        
        /// <summary>
        /// Ejercicio 5: Sucripción al newsletter
        /// </summary
        [Test]
        public void suscriptionNewLetterTest()
        {
            var footerPage = new footerPage(Driver);

            // Suscripción al newsletter
            footerPage.FillSuscribeInput(email);
            footerPage.SumitSuscibeButton();
            ScreenshotHelper.TakeScreenshot(Driver, "Suscription_test.png");

            //Validar mensaje de éxito
            Assert.That(footerPage.GetSuscribeMessage(), Is.EqualTo("You have been successfully subscribed!"));
        }

    }
}
