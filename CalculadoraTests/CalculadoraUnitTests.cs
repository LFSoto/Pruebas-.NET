using CalculadoraLib;
using Moq;
using NUnit.Framework;
namespace CalculadoraTests;

[TestFixture]
public class CalculadoraUnitTests
{
    private Calculadora _calc;

    [SetUp]
    public void Setup()
    {
        var timeMock = new Mock<ITimeProvider>();
        timeMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 19, 18, 0, 0));
        _calc = new Calculadora(timeMock.Object);
    }

    [Test]
    public void Sumar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Sumar(2, 3);
        //Assert.AreEqual(5, resultado);
        Assert.That(resultado, Is.EqualTo(5));
    }
    [Test]
    public void Restar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Restar(5, 3);
        //Assert.AreEqual(2, resultado);
        Assert.That(resultado, Is.EqualTo(2));
    }
    [Test]
    public void Multiplicar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Multiplicar(2, 3);
        //Assert.AreEqual(6, resultado);
        Assert.That(resultado, Is.EqualTo(6));
    }
    [Test]
    public void Dividir_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Dividir(15, 3);
        //Assert.AreEqual(5, resultado);
        Assert.That(resultado, Is.EqualTo(5));
    }
}