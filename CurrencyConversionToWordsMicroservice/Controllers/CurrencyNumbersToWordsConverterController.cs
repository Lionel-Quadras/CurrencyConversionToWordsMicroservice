using CurrencyConversionToWordsMicroservice.Handler;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConversionToWordsMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyNumberToWordsConverterController : ControllerBase
    {
        private readonly ILogger<CurrencyNumberToWordsConverterController> _logger;
        private readonly INumbersToWordsConverterHandler _numberToWordConverterHandler;

        public CurrencyNumberToWordsConverterController(ILogger<CurrencyNumberToWordsConverterController> logger, INumbersToWordsConverterHandler numberToWordConverterHandler)
        { 
            _logger = logger;
            _numberToWordConverterHandler = numberToWordConverterHandler;
        }

        [HttpGet("{amount}")]
        public IActionResult AmountInWords(double amount, [FromServices]  IValidator<double> validator)
        {
            _logger.LogInformation("Amount: {}", amount);
            //to check whether data is valid or not
            var validationResult = validator.Validate(amount);
            if (!validationResult.IsValid)
            {
                var errorList = new List<string>();
                foreach(var errors in validationResult.Errors)
                {
                    errorList.Add(errors.ErrorMessage);
                }
                //invalid data throw bad request
                return BadRequest(errorList);
            }

            //Calling Handler to convert number into words.
            return Ok(new { amount = _numberToWordConverterHandler.Handle(amount) });
        }
    }
}
