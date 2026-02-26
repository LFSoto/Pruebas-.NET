namespace CalculadoraLib
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}