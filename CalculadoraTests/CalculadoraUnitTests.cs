using NUnit.Framework;
using CalculadoraLib;
using Moq;
namespace CalculadoraTests;

[TestFixture]
public class CalculadoraUnitTests
{
    private Calculadora _calc;
    private Mock<ITimeProvider> _timeMock;

    [SetUp]
    public void Setup()
    {
        // Configurar el mock para simular la hora actual
        _timeMock = new Mock<ITimeProvider>();
        // Simular una hora dentro del rango permitido (08:00-18:00)
        _timeMock.SetupGet(t => t.Now).Returns(new DateTime(2026, 1, 1, 10, 0, 0));
        // Crear la instancia de Calculadora con el mock
        _calc = new Calculadora(_timeMock.Object);
    }

    [Test]
    public void Sumar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Sumar(2, 3);
        Assert.That(resultado, Is.EqualTo(5));
    }

    [Test]
    public void Restar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Restar(5, 3);
        Assert.That(resultado, Is.EqualTo(2));
    }

    [Test]
    public void Multiplicar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Multiplicar(4, 3);
        Assert.That(resultado, Is.EqualTo(12));
    }

    [Test]
    public void Dividir_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Dividir(10, 2);
        Assert.That(resultado, Is.EqualTo(5.0));
    }

    [Test]
    public void Dividir_EntreCeroRetornaExepcion()
    {
        Assert.Throws<DivideByZeroException>(() => _calc.Dividir(10, 0));
    }

    [Test]
    public void Operacion_EnHoraInvalidaRetornaExcepcion()
    {
        // Simular hora fuera del rango de 08:00-18:00
        _timeMock.SetupGet(t => t.Now).Returns(new DateTime(2026, 1, 1, 06, 0, 0));

        Assert.Throws<InvalidOperationException>(() => _calc.Sumar(1, 1));
        Assert.Throws<InvalidOperationException>(() => _calc.Restar(2, 1));
        Assert.Throws<InvalidOperationException>(() => _calc.Multiplicar(2, 2));
        Assert.Throws<InvalidOperationException>(() => _calc.Dividir(10, 2));
    }

    [Test]
    public void Operacion_EnHoraValidaRetornaResultadoCorrecto()
    {
        // Asegurar hora dentro del rango permitido (08:00-18:00)
        _timeMock.SetupGet(t => t.Now).Returns(new DateTime(2026, 1, 1, 10, 30, 0));

        Assert.That(_calc.Sumar(1, 2), Is.EqualTo(3));
        Assert.That(_calc.Restar(5, 2), Is.EqualTo(3));
        Assert.That(_calc.Multiplicar(3, 4), Is.EqualTo(12));
        Assert.That(_calc.Dividir(9, 3), Is.EqualTo(3.0));
    }





}