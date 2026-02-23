using NUnit.Framework;
using CalculadoraLib;
using Moq;
using System;
namespace CalculadoraTests;

[TestFixture]
public class CalculadoraUnitTests
{
    
    [Test] //-- Suma
    public void Sumar_DentrodeHorarioPermitido_DeberiaRetornarResultadoCorrecto()
    {
        var mockTimeProvider = new Mock<ITimeProvider>();
        mockTimeProvider.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 22, 10, 0, 0)); // 10:00 AM
        var calculadora = new Calculadora(mockTimeProvider.Object);
        var resultado = calculadora.Sumar(5, 3);

        Assert.That(resultado, Is.EqualTo(8));

    }

    [Test] //-- Resta
    public void Restar_DentrodeHorarioPermitido_DeberiaRetornarResultadoCorrecto()
    {
        var mockTimeProvider = new Mock<ITimeProvider>();
        mockTimeProvider.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 22, 10, 0, 0)); // 10:00 AM
        var calculadora = new Calculadora(mockTimeProvider.Object);
        var resultado = calculadora.Restar(10, 3);

        Assert.That(resultado, Is.EqualTo(7));

    }

    [Test] //-- Multiplicacion
    public void Multiplicar_DeberiaRetornarResultadoCorrecto()
    {
        var mockTimeProvider = new Mock<ITimeProvider>();
        mockTimeProvider.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 22, 10, 0, 0)); // 10:00 AM
        var calculadora = new Calculadora(mockTimeProvider.Object);
        var resultado = calculadora.Multiplicar(7,4);

        Assert.That(resultado, Is.EqualTo(28));

    }

    [Test] //-- Division
    public void Division_DeberiaRetornarResultadoCorrecto()
    {
        var mockTimeProvider = new Mock<ITimeProvider>();
        mockTimeProvider.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 22, 10, 0, 0)); // 10:00 AM
        var calculadora = new Calculadora(mockTimeProvider.Object);
        var resultado = calculadora.Dividir(9,3);

        Assert.That(resultado, Is.EqualTo(3));

    }

    [Test] //-- Division con error
    public void Division_DeberiaRetornarResultadoError()
    {
        var mockTimeProvider = new Mock<ITimeProvider>();
        mockTimeProvider.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 22, 10, 0, 0)); // dentro del horario permitido

        var calculadora = new Calculadora(mockTimeProvider.Object);

        Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(10, 0));

    }
}