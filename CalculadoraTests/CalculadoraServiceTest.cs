using NUnit.Framework;
using Moq;
using CalculadoraLib;

namespace CalculadoraTests;

[TestFixture]
public class CalculadoraServiceTests
{
    private Mock<ITimeProvider> _timeProviderMock;
    private Calculadora calc;

    [SetUp]

    public void Setup()
    {
        _timeProviderMock = new Mock<ITimeProvider>();
    }

    [Test]
    public void SumarYGuardar_DeberiaGuardarOperacionEnRepositorio()
    {
        // Arrange - Preparar
        var repoMock = new Mock<IOperacionRepository>();
        //var calc = new Calculadora();
        var service = new CalculadoraService(calc, repoMock.Object);

        // Act - Actuar
        var resultado = service.SumarYGuardar(2, 3);

        // Assert - Afirmar
        Assert.AreEqual(5, resultado);

        // Verificamos que se haya llamado al método GuardarOperacion
        repoMock.Verify(r => r.GuardarOperacion("2 + 3", 5), Times.Once);
    }

    [Test]

    public void DentroDelHorarioPermitido_DeberiaRetornarElResultadoCorrecto()
    {
        _timeProviderMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 19, 10, 0, 0));
        calc = new Calculadora(_timeProviderMock.Object);

        // Act - Actuar
        var resultado = calc.Sumar(5, 3);

        // Assert - Afirmar
        Assert.AreEqual(8, resultado);
    }

    [Test]

    public void FueraDelHorarioPermitido_DeberiaLanzarUnaExcepcion()
    {
        _timeProviderMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 19, 20, 0, 0));
        calc = new Calculadora(_timeProviderMock.Object);

        // Act & Assert - Actuar y Afirmar
        Assert.Throws<InvalidOperationException>(() => calc.Sumar(5, 3));

    }
}
