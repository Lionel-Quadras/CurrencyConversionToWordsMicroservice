namespace CurrencyConversionToWordsMicroservice.Handler
{
    public interface INumberToWordConverterHandler
    {
        string Handle(double amount);
    }
}
