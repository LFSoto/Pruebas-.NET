namespace CalculadoraLib;

public class Calculadora
{
    private readonly ITimeProvider _timeProvider;

    public Calculadora(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    private void HoraActual()
    {
        var hora = _timeProvider.Now.TimeOfDay;
        var inicio = TimeSpan.FromHours(8);
        var fin = TimeSpan.FromHours(18);
        if (hora < inicio || hora > fin)
        {
            throw new InvalidOperationException("Operaciones no permitidas fuera de horario (08:00-18:00).");
        }
            
    }

    public int Sumar(int a, int b)
    {
        HoraActual();
        return a + b;
    }

    public int Restar(int a, int b)
    {
        HoraActual();
        return a - b;
    }

    public int Multiplicar(int a, int b)
    {
        HoraActual();
        return a * b;
    }

    public double Dividir(int a, int b)
    {
        HoraActual();
        if (b == 0) throw new DivideByZeroException("No se puede dividir entre cero.");
        return (double)a / b;
    }






}

//Contiene la lógica pura
