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
        var mockTime = new Mock<ITimeProvider>();
        mockTime.Setup(t => t.Now).Returns(new DateTime(2024, 1, 1, 7, 0, 0));
        var calc = new Calculadora(mockTime.Object);
        var service = new CalculadoraService(calc, repoMock.Object);

        // Act
        var resultado = service.SumarYGuardar(2, 3);
		var esperado = 5;

		// Assert
		Assert.That(esperado, Is.EqualTo(resultado));

		// Verificamos que se haya llamado al método GuardarOperacion
		repoMock.Verify(r => r.GuardarOperacion("2 + 3", 5), Times.Once);
    }

	[Test]
	public void SumarDentroHorario_DeberiaSerExitoso()
	{
		// Arrange
		var repoMock = new Mock<IOperacionRepository>();
		var mockTime = new Mock<ITimeProvider>();
		mockTime.Setup(t => t.Now).Returns(new DateTime(2024, 1, 1, 7, 0, 0));
		var calc = new Calculadora(mockTime.Object);
		var service = new CalculadoraService(calc, repoMock.Object);

		// Act
		var resultado = service.SumarYGuardar(2, 3);
		var esperado = 5;

		// Assert
		Assert.That(esperado, Is.EqualTo(resultado));

	}

	[Test]
	public void SumarFueraHorario_DeberiaSerFallido()
	{
		// Arrange
		var mockTime = new Mock<ITimeProvider>();
		mockTime.Setup(t => t.Now).Returns(new DateTime(2024, 1, 1, 12, 0, 0));
		var calc = new Calculadora(mockTime.Object);

		// Assert
		Assert.Throws<InvalidOperationException>(() => calc.Sumar(2, 3));
	}

	[Test]
	public void RestarDentroHorario_DeberiaSerExitoso()
	{
		// Arrange
		var repoMock = new Mock<IOperacionRepository>();
		var mockTime = new Mock<ITimeProvider>();
		mockTime.Setup(t => t.Now).Returns(new DateTime(2024, 1, 1, 7, 0, 0));
		var calc = new Calculadora(mockTime.Object);
		var service = new CalculadoraService(calc, repoMock.Object);

		// Act
		var resultado = service.RestarYGuardar(8, 3);
		var esperado = 5;

		// Assert
		Assert.That(esperado, Is.EqualTo(resultado));

	}

	[Test]
	public void RestarFueraHorario_DeberiaSerFallido()
	{
		// Arrange
		var mockTime = new Mock<ITimeProvider>();
		mockTime.Setup(t => t.Now).Returns(new DateTime(2024, 1, 1, 12, 0, 0));
		var calc = new Calculadora(mockTime.Object);

		// Assert
		Assert.Throws<InvalidOperationException>(() => calc.Restar(5, 3));

	}

	[Test]
	public void MultiplicarDentroHorario_DeberiaSerExitoso()
	{
		// Arrange
		var repoMock = new Mock<IOperacionRepository>();
		var mockTime = new Mock<ITimeProvider>();
		mockTime.Setup(t => t.Now).Returns(new DateTime(2024, 1, 1, 7, 0, 0));
		var calc = new Calculadora(mockTime.Object);
		var service = new CalculadoraService(calc, repoMock.Object);

		// Act
		var resultado = service.MultiplicarYGuardar(2, 2);
		var esperado = 4;

		// Assert
		Assert.That(esperado, Is.EqualTo(resultado));

	}

	[Test]
	public void MultiplicarFueraHorario_DeberiaSerFallido()
	{
		// Arrange
		var mockTime = new Mock<ITimeProvider>();
		mockTime.Setup(t => t.Now).Returns(new DateTime(2024, 1, 1, 12, 0, 0));
		var calc = new Calculadora(mockTime.Object);

		// Assert
		Assert.Throws<InvalidOperationException>(() => calc.Multiplicar(2, 3));

	}

	[Test]
	public void DividirDentroHorario_DeberiaSerExitoso()
	{
		// Arrange
		var repoMock = new Mock<IOperacionRepository>();
		var mockTime = new Mock<ITimeProvider>();
		mockTime.Setup(t => t.Now).Returns(new DateTime(2024, 1, 1, 7, 0, 0));
		var calc = new Calculadora(mockTime.Object);
		var service = new CalculadoraService(calc, repoMock.Object);

		// Act
		var resultado = service.DividirYGuardar(8, 2);
		var esperado = 4;

		// Assert
		Assert.That(esperado, Is.EqualTo(resultado));

	}

	[Test]
	public void DividirFueraHorario_DeberiaSerFallido()
	{
		// Arrange
		var mockTime = new Mock<ITimeProvider>();
		mockTime.Setup(t => t.Now).Returns(new DateTime(2024, 1, 1, 12, 0, 0));
		var calc = new Calculadora(mockTime.Object);

		// Assert
		Assert.Throws<InvalidOperationException>(() => calc.Dividir(9, 3));

	}


}
