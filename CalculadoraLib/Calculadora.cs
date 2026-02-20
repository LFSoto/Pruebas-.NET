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
            throw new InvalidOperationException("Fuera de horario, operacion no permitida");
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

    public double Dividir(int a, int b)
    {
        ValidarHorario();
        if (b == 0) throw new DivideByZeroException("No se puede dividir entre cero.");
        return (double)a / b;
    }
}