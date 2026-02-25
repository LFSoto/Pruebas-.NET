using NUnit.Framework;
using Moq;
using CalculadoraLib;

namespace CalculadoraTests;

[TestFixture]
public class CalculadoraUnitTests
{
    private Calculadora _calc;
    private Mock<ITimeProvider> _timeProviderMock;

    [SetUp]
    public void Setup()
    {
        _timeProviderMock = new Mock<ITimeProvider>();
        // Default to a time within business hours (10:00)
        _timeProviderMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 1, 1, 10, 0, 0));
        _calc = new Calculadora(_timeProviderMock.Object);
    }

    [Test]
    public void Sumar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Sumar(2, 3);
        Assert.AreEqual(5, resultado);
    }

    [Test]
    public void Restar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Restar(5, 2);
        Assert.AreEqual(3, resultado);
    }

    [Test]
    public void Multiplicar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Multiplicar(4, 3);
        Assert.AreEqual(12, resultado);
    }

    [Test]
    public void Dividir_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Dividir(10, 2);
        Assert.AreEqual(5.0, resultado);
    }

    [Test]
    public void Dividir_PorCero_DeberiaLanzarDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => _calc.Dividir(5, 0));
    }

    [Test]
    public void Operacion_FueraDeHorario_DeberiaLanzarInvalidOperationException()
    {
        var timeMock = new Mock<ITimeProvider>();
        timeMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 1, 1, 20, 0, 0)); // 20:00 fuera de horario
        var calcFuera = new Calculadora(timeMock.Object);

        Assert.Throws<InvalidOperationException>(() => calcFuera.Sumar(1, 1));
    }


}
