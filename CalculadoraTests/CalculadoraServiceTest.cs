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
        var mockTimeProvider = new Mock<ITimeProvider>();
        mockTimeProvider.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 22, 10, 0, 0));

        var mockRepo = new Mock<IOperacionRepository>();
        var calculadora = new Calculadora(mockTimeProvider.Object);

        var service = new CalculadoraService(calculadora, mockRepo.Object);
        // Act
        var resultado = service.SumarYGuardar(5, 3);

        // Assert
        Assert.That(resultado, Is.EqualTo(8));
        mockRepo.Verify(r => r.GuardarOperacion("5 + 3", 8), Times.Once);

    }
}
