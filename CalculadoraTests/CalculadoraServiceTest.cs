using NUnit.Framework;
using Moq;
using CalculadoraLib;

namespace CalculadoraTests;

[TestFixture]
public class CalculadoraServiceTests
{
    [Test]
    public void SumarYGuardar_DeberiaGuardarOperacionEnRepositorio()
    {
        // Arrange
        var repoMock = new Mock<IOperacionRepository>();
        var timeMock = new Mock<ITimeProvider>();
        timeMock.SetupGet(t => t.Now).Returns(new DateTime(2026, 1, 1, 10, 0, 0));
        var calc = new Calculadora(timeMock.Object);
        var service = new CalculadoraService(calc, repoMock.Object);

        // Act
        var resultado = service.SumarYGuardar(2, 3);

        // Assert
        Assert.That(resultado, Is.EqualTo(5));

        // Verificamos que se haya llamado al método GuardarOperacion
        repoMock.Verify(r => r.GuardarOperacion("2 + 3", 5), Times.Once);
    }

    [Test]
    public void SumarYGuardar_EnHorarioNoPermetido_DeberiaLanzarExepcion()
    {
        // Arrange: hora fuera del rango permitido (08:00-18:00)
        var repoMock = new Mock<IOperacionRepository>();
        var timeMock = new Mock<ITimeProvider>();
        timeMock.SetupGet(t => t.Now).Returns(new DateTime(2026, 1, 1, 20, 0, 0));
        var calc = new Calculadora(timeMock.Object);
        var service = new CalculadoraService(calc, repoMock.Object);

        // Act & Assert: la operación debe lanzar InvalidOperationException y no guardar en el repositorio
        Assert.Throws<InvalidOperationException>(() => service.SumarYGuardar(2, 3));
        repoMock.Verify(r => r.GuardarOperacion(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
    }
}
