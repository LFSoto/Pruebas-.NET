using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AutomationPracticeDemo.Tests.Asserts
{
    public static class SignupAsserts
    {
        public static void AssertCuentaCreada(string pageSource)
        {
            Assert.That(pageSource.Contains("Account Created!"),
                "La cuenta debería haberse creado exitosamente.");
        }
    }
}

