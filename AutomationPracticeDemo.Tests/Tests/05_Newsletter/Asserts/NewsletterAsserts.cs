using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AutomationPracticeDemo.Tests.Asserts
{
    public static class NewsletterAsserts
    {
        public static void AssertSuscripcionExitosa(bool success)
        {
            Assert.That(success, Is.True,
                "La suscripción al newsletter debería completarse exitosamente.");
        }
    }
}

