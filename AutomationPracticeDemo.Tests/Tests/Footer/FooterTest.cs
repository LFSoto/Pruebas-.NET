using AutomationPracticeDemo.Tests.Pages.Components;
using AutomationPracticeDemo.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests.Footer
{
    public  class FooterTest : TestBase
    {
        private footerPage footer;

        [SetUp]
        public void SetUp()
        {
            footer = new footerPage(Driver);
        }

        [Test]
        public void validateSuscription()
        {            
            footer.InputEmailSuscription("kenneth123@pruebas.com");
            footer.ClickSubscribeButton();

            string messageFooter = footer.GetMessageSuscription();

            Assert.That(messageFooter, Is.EqualTo("You have been successfully subscribed!"), "El mensaje de suscripción no es el esperado.");
        }
    }
}
