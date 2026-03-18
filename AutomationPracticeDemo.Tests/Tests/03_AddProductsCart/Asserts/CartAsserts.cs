using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace AutomationPracticeDemo.Tests.Asserts
{
    public static class CartAsserts
    {
        public static void AssertCantidadProductos(List<IWebElement> filas, int esperado)
        {
            Assert.That(filas.Count, Is.EqualTo(esperado),
                $"El carrito debería contener {esperado} productos.");
        }

        public static void AssertTotalesCorrectos(int precio, int total)
        {
            Assert.That(total, Is.EqualTo(precio),
                "El total del producto debería coincidir con el precio.");
        }
    }
}

