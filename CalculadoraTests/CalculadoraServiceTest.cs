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
        // Arrange- preparar
        var repoMock = new Mock<IOperacionRepository>();
        var timeMock = new Mock<ITimeProvider>();
        timeMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 1, 1, 10, 0, 0));
        var calc = new Calculadora(timeMock.Object);
        var service = new CalculadoraService(calc, repoMock.Object);

        // Act - actuar
        var resultado = service.SumarYGuardar(2, 3);

        // Assert - afirmar
        Assert.AreEqual(5, resultado);

        // Verificamos que se haya llamado al método GuardarOperacion
        repoMock.Verify(r => r.GuardarOperacion("2 + 3", 5), Times.Once);
    }

    [Test]
    public void SumarYGuardar_FueraDeHorario_DeberiaLanzarInvalidOperationExceptionYNoGuardar()
    {
        // Arrange
        var repoMock = new Mock<IOperacionRepository>();
        var timeMock = new Mock<ITimeProvider>();
        timeMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 1, 1, 20, 0, 0)); // 20:00 fuera de horario
        var calc = new Calculadora(timeMock.Object);
        var service = new CalculadoraService(calc, repoMock.Object);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => service.SumarYGuardar(2, 3));

        // Verificamos que no se haya intentado guardar la operación
        repoMock.Verify(r => r.GuardarOperacion(It.IsAny<string>(), It.IsAny<double>()), Times.Never);
    }
}