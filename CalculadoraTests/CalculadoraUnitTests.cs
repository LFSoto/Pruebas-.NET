using NUnit.Framework;
using CalculadoraLib;
using Moq;
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
        _timeProviderMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 19, 10, 0, 0));
        _calc = new Calculadora(_timeProviderMock.Object);
    }

    [Test]
    public void Sumar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Sumar(2, 3);
        Assert.AreEqual(5, resultado);
    }

    [Test]
    public void Resta_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Restar(5, 1);
        Assert.AreEqual(4, resultado);
    }

    [Test]
    public void Multiplicar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Multiplicar(4, 4);
        Assert.AreEqual(16, resultado);
    }

    [Test]
    public void Division_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Dividir(10, 2);
        Assert.AreEqual(5, resultado);
    }

    [Test]
    public void Division_Cero_DeberiaRetornarResultadoCorrecto()
    {
        Assert.Throws<DivideByZeroException>(() => _calc.Dividir(10, 0));
    }
}