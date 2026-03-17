using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Tests.contacUs4.Asserts;
using AutomationPracticeDemo.Tests.Tests.Suscription5.Asserts;
using AutomationPracticeDemo.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests.Suscription5
{
    public class SuscriptionTest : TestBase
    {
        [Test, TestCaseSource(typeof(SuscriptionDataSource), nameof(SuscriptionDataSource.SuscriptionInfo))]
        public void Suscription(string email)
        {
            var SuscriptionPage = new Pages.SuscriptionPage(Driver);
            SuscriptionPage.Bajarscroll();
            SuscriptionPage.Fillemail(email);
            SuscriptionPage.btnclickSuscrition();
            ScreenshotHelper.TakeScreenshot(Driver, "Suscription5.png");
            Assert.That(SuscriptionPage.MessageSusbscribe, Is.EqualTo("You have been successfully subscribed!"));

        }
    }
    
}
