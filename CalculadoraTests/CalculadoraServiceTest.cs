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
        var timerMock = new Mock<ITimeProvider>();
        timerMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 19, 8, 0, 0));
        var calc = new Calculadora(timerMock.Object);
        var service = new CalculadoraService(calc, repoMock.Object);

        // Act
        var resultado = service.SumarYGuardar(2, 3);

        // Assert
        //Assert.AreEqual(5, resultado);
        Assert.That(resultado, Is.EqualTo(5));

        // Verificamos que se haya llamado al método GuardarOperacion
        repoMock.Verify(r => r.GuardarOperacion("2 + 3", 5), Times.Once);
    }
}
