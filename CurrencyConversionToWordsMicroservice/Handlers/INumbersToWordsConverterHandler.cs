namespace CurrencyConversionToWordsMicroservice.Handler
{
    public interface INumbersToWordsConverterHandler
    {
        string Handle(double amount);
    }
}
