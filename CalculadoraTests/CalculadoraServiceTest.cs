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
        // Arrange
        var repoMock = new Mock<IOperacionRepository>();
        var service = new CalculadoraService(calc, repoMock.Object);

        // Act
        var resultado = service.SumarYGuardar(2, 3);

        // Assert
        Assert.AreEqual(5, resultado);

        // Verificamos que se haya llamado al método GuardarOperacion
        repoMock.Verify(r => r.GuardarOperacion("2 + 3", 5), Times.Once);
    }

    [Test]
    public void DentroHorarioPermitido_DeberiaRetornarResultadoCorrecto()
    {
        
        _timeProviderMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 19, 10, 0, 0));
        calc = new Calculadora(_timeProviderMock.Object);

        // Act
        var resultado = calc.Sumar(5, 3);

        // Assert
        Assert.AreEqual(8, resultado);
    }

    [Test]
    public void FueraHorarioPermitido_DeberiaLanzarExcepcion()
    {
        
        _timeProviderMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 19, 20, 0, 0));
        calc = new Calculadora(_timeProviderMock.Object);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => calc.Sumar(5, 3));
    }

}
