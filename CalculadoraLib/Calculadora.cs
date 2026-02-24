namespace CalculadoraLib;


public class Calculadora
{

	ITimeProvider i_TimeProvider;
	public Calculadora(ITimeProvider iTimeProvider)
	{
		i_TimeProvider = iTimeProvider;
	}

    public int Sumar(int a, int b)
    {
        validarHora();
        return a + b;
    }

    public int Restar(int a, int b)
    {
        validarHora();
        return a - b;
    }

    public int Multiplicar(int a, int b)
    {
        validarHora();
        return a * b;
    }

    public double Dividir(int a, int b)
    {
        validarHora();
        if (b == 0) throw new DivideByZeroException("No se puede dividir entre cero.");
        return (double)a / b;
    }

    public void validarHora()
    {
		var ahora = i_TimeProvider.Now;

		bool dentroDeHorarioValido = ahora.Hour < 8 || ahora.Hour >= 18;

		if (!dentroDeHorarioValido)
		{
			throw new InvalidOperationException(
				"La operación NO está permitida entre las 08:00 y 18:00.");
		}
	}
}