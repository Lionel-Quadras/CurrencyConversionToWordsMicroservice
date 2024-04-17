namespace CurrencyConversionToWordsMicroservice.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message) 
        {
            StatusCode = statusCode;
            Message = message;
        }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
