namespace CalculadoraLib;

public class CalculadoraService
{
    //Esta clase no crea estos objetos, se llama inyección de dependencias (te paso lo que necesitas)
    private readonly Calculadora _calculadora;
    private readonly IOperacionRepository _repo;

    public CalculadoraService(Calculadora calculadora, IOperacionRepository repo)
    {
        _calculadora = calculadora;
        _repo = repo;
    }

    public int SumarYGuardar(int a, int b)
    {
        var resultado = _calculadora.Sumar(a, b);
        _repo.GuardarOperacion($"{a} + {b}", resultado);
        return resultado;
    }
}

//Coordinador