using CurrencyConversionToWordsMicroservice.Controllers;
using CurrencyConversionToWordsMicroservice.Handler;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyConversion.Test.Controller
{
    public class CurrencyNumbersToWordsConverterControllerTest
    {
        private readonly Mock<ILogger<CurrencyNumberToWordsConverterController>> _mockLogger;
        private readonly Mock<INumbersToWordsConverterHandler> _mockNumberToWordConverterHandler;
        private readonly CurrencyNumberToWordsConverterController _currencyNumberToWordsConverterController;
        private readonly Mock<IValidator<double>>_mockCurrencyValidator;

        public CurrencyNumbersToWordsConverterControllerTest()
        {
            _mockLogger = new Mock<ILogger<CurrencyNumberToWordsConverterController>>();
            _mockNumberToWordConverterHandler = new Mock<INumbersToWordsConverterHandler>();
            _mockCurrencyValidator = new Mock<IValidator<double>>();
            _currencyNumberToWordsConverterController = new CurrencyNumberToWordsConverterController(_mockLogger.Object, _mockNumberToWordConverterHandler.Object);
        }


        [Fact]
        public void GetAmount_PositiveTest()
        {
            //Arrange
            double amount = 25.1;
            var result = "twenty - five dollars and ten cents";
            _mockNumberToWordConverterHandler.Setup(x => x.Handle(It.IsAny<double>())).Returns(result);
            _mockCurrencyValidator.Setup(x => x.Validate(It.IsAny<double>())).Returns(new FluentValidation.Results.ValidationResult());
            
            //Act
            var response = _currencyNumberToWordsConverterController.AmountInWords(amount, _mockCurrencyValidator.Object);
            var okResult = response as ObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
                
        }

        [Fact]
        public void GetAmount_NegativeTest()
        {
            //Arrange
            double amount = 1000000000;
            var result = "Please enter amount less than 1 Billion";
            _mockCurrencyValidator.Setup(x => x.Validate(It.IsAny<double>())).Returns(new FluentValidation.Results.ValidationResult()
            {
                Errors = new List<FluentValidation.Results.ValidationFailure> { new FluentValidation.Results.ValidationFailure("amount", result) }
            });

            //Act
            var response = _currencyNumberToWordsConverterController.AmountInWords(amount, _mockCurrencyValidator.Object);
            var okResult = response as ObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.True(okResult is BadRequestObjectResult);
            Assert.Equal(StatusCodes.Status400BadRequest, okResult.StatusCode);

        }


    }
}
