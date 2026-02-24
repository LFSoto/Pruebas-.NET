namespace CalculadoraLib;

public class Calculadora
{
    private ITimeProvider _timeProvider;
    public Calculadora(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }
    private void VerificarHora()
    {
        var hora = _timeProvider.Now.Hour;
        if (hora < 8 || hora > 18) throw new InvalidOperationException("No se puede ejecutar la operación fuera de horario 08:00 - 18:00.");
    }

    public int Sumar(int a, int b)
    {
        VerificarHora();
        return a + b;
    }

    public int Restar(int a, int b) {
        VerificarHora();
        return a - b;
    }
    public int Multiplicar(int a, int b) {
        VerificarHora();
        return a * b;
    }
    public double Dividir(int a, int b)
    {
        VerificarHora();
        if (b == 0) throw new DivideByZeroException("No se puede dividir entre cero.");
        return (double)a / b;
    }
}