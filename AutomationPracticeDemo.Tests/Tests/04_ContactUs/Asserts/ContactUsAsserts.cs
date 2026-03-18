using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AutomationPracticeDemo.Tests.Asserts
{
    public static class ContactUsAsserts
    {
        public static void AssertFormularioExitoso(bool success)
        {
            Assert.That(success, Is.True,
                "El formulario de contacto debería enviarse correctamente.");
        }
    }
}

