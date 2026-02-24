namespace CalculadoraLib;

public class CalculadoraService
{
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

	public int RestarYGuardar(int a, int b)
	{
		var resultado = _calculadora.Restar(a, b);
		_repo.GuardarOperacion($"{a} - {b}", resultado);
		return resultado;
	}

	public int MultiplicarYGuardar(int a, int b)
	{
		var resultado = _calculadora.Multiplicar(a, b);
		_repo.GuardarOperacion($"{a} * {b}", resultado);
		return resultado;
	}

	public int DividirYGuardar(int a, int b)
	{
		var resultado = _calculadora.Dividir(a, b);
		_repo.GuardarOperacion($"{a} / {b}", resultado);
		return (int)resultado;
	}
}
