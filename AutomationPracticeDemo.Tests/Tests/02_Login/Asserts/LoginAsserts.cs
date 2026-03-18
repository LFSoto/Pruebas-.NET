using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AutomationPracticeDemo.Tests.Asserts
{
    public static class LoginAsserts
    {
        public static void AssertLoginExitoso(string pageSource)
        {
            Assert.That(pageSource.Contains("Logged in as"),
                "El usuario debería estar logueado correctamente.");
        }

        public static void AssertLoginFallido(string pageSource)
        {
            Assert.That(!pageSource.Contains("Logged in as"),
                "El login debería fallar con credenciales inválidas.");
        }
    }
}