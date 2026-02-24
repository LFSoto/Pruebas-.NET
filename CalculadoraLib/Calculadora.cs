using System;

namespace CalculadoraLib;

public class Calculadora
{
    private readonly ITimeProvider _timeProvider;

    // Inyectamos el proveedor de tiempo para facilitar las pruebas unitarias
    public Calculadora(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }
    public int Sumar(int a, int b)
    {
        ValidarHora();
        return a + b;
    }

    public int Restar(int a, int b)
    {
        ValidarHora();
        return a - b;
    }

    public int Multiplicar(int a, int b)
    {
        ValidarHora();
        return a * b;
    }

    public double Dividir(int a, int b)
    {
        ValidarHora();
        if (b == 0) throw new DivideByZeroException("No se puede dividir entre cero.");
        return (double)a / b;
    }

    private void ValidarHora()
    {
        var now = _timeProvider.Now.TimeOfDay;
        var start = TimeSpan.FromHours(8);
        var end = TimeSpan.FromHours(18);
        if (now < start || now > end)
        {
            throw new InvalidOperationException("Operaciones solo permitidas entre las 08:00 y las 18:00.");
        }
    }
}