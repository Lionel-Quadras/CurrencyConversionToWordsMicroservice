using CurrencyConversionToWordsMicroservice.Handler;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConversionToWordsMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyNumberToWordConverterController : ControllerBase
    {

        private readonly ILogger<CurrencyNumberToWordConverterController> _logger;
        private readonly INumberToWordConverterHandler _numberToWordConverterHandler;

        public CurrencyNumberToWordConverterController(ILogger<CurrencyNumberToWordConverterController> logger, INumberToWordConverterHandler numberToWordConverterHandler)
        { 
            _logger = logger;
            _numberToWordConverterHandler = numberToWordConverterHandler;
        }

        [HttpGet("{amount}")]
        public IActionResult Get(double amount, [FromServices]  IValidator<double> validator)
        {
            _logger.LogInformation("Amount: {}", amount);
            var validationResult = validator.Validate(amount);
            if (!validationResult.IsValid)
            {
                var errorList = new List<string>();
                foreach(var errors in validationResult.Errors)
                {
                    errorList.Add(errors.ErrorMessage);
                }
                return BadRequest(errorList);
            }
            return Ok(new { amount = _numberToWordConverterHandler.Handle(amount) });
        }
    }
}
