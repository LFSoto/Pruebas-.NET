using NUnit.Framework;
using CalculadoraLib;
using Moq;
namespace CalculadoraTests;

[TestFixture]
public class CalculadoraUnitTests
{
    private Calculadora _calc;
	private Mock<ITimeProvider> mockTime;

	[SetUp]
    public void Setup()
    {
        mockTime = new Mock<ITimeProvider>();
        _calc = new Calculadora(mockTime.Object);
    }

    [Test]
    public void Sumar_DeberiaRetornarResultadoCorrecto()
    {
        var resultado = _calc.Sumar(2, 3);
		var esperado = 5;
		mockTime.Setup(tp => tp.Now).Returns(new DateTime(2025, 1, 1, 7, 0, 0));
		Assert.That(esperado, Is.EqualTo(resultado));
    }

	[Test]
	public void Restar_DeberiaRetornarResultadoCorrecto()
	{
		var resultado = _calc.Restar(8, 3);
		var esperado = 5;
		mockTime.Setup(tp => tp.Now).Returns(new DateTime(2025, 1, 1, 7, 0, 0));
		Assert.That(esperado, Is.EqualTo(resultado));	
	}

	[Test]
	public void Multiplicar_DeberiaRetornarResultadoCorrecto()
	{
		var resultado = _calc.Multiplicar(2, 2);
		var esperado = 4;
		mockTime.Setup(tp => tp.Now).Returns(new DateTime(2025, 1, 1, 7, 0, 0));
		Assert.That(esperado, Is.EqualTo(resultado));
	}

	[Test]
	public void Dividir_DeberiaRetornarResultadoCorrecto()
	{
		var resultado = _calc.Dividir(10, 2);
		var esperado = 5;
		mockTime.Setup(tp => tp.Now).Returns(new DateTime(2025, 1, 1, 7, 0, 0));
		Assert.That(esperado, Is.EqualTo(resultado));
	}

	[Test]
	public void Dividir_DeberíaRetornarErrorDivisionCero()
	{
		mockTime.Setup(tp => tp.Now).Returns(new DateTime(2025, 1, 1, 7, 0, 0));
		Assert.Throws<DivideByZeroException>(() => _calc.Dividir(10, 0));
	}
}