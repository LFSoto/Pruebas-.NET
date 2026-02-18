namespace CalculadoraLib;

public interface IOperacionRepository
{
    void GuardarOperacion(string descripcion, double resultado);
}