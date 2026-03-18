using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Asserts;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class NewsletterTests : Base.TestBase
    {
        [Test]
        public void SuscripcionNewsletter()
        {
            var newsletterPage = new NewsletterPage(Driver, Wait);
            newsletterPage.Subscribe("dayana.newsletter@mail.com");

            NewsletterAsserts.AssertSuscripcionExitosa(newsletterPage.IsSubscribed());
        }
    }
}

