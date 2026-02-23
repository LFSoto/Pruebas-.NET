namespace CalculadoraLib;

public class Calculadora
{
    private readonly ITimeProvider _timeProvider;

    public Calculadora(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    private void ValidarHorario()
    {
        var horaActual = _timeProvider.Now.TimeOfDay;
        var inicio = new TimeSpan(8, 0, 0);
        var fin = new TimeSpan(18, 0, 0);

        if (horaActual < inicio || horaActual > fin)
        {
            throw new InvalidOperationException("Operaciones permitidas solo entre 08:00 y 18:00.");
        }
    }


    public int Sumar(int a, int b)
    {
        ValidarHorario();
        return a + b;
    }


    public int Restar(int a, int b)
    {
        ValidarHorario();
        return a - b;
    }
    public int Multiplicar(int a, int b)
    {
        ValidarHorario();
        return a * b;

    }

    public int Dividir(int a, int b)
    {
        ValidarHorario();
        if (b == 0) throw new DivideByZeroException();
        return a / b;
    }
}