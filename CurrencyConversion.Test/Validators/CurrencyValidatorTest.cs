using CurrencyConversion.Host.Constants;
using CurrencyConversion.Test.Controller;
using CurrencyConversionToWordsMicroservice.Validators;
using Xunit;

namespace CurrencyConversion.UnitTests.Validators
{
    public class CurrencyValidatorTest
    {
        [Fact]
        public void CurrencyValidator_Test()
        {
            var validator = new CurrencyValidator();
            double amount = 1;
            //populate with dummy data
            var result = validator.Validate(amount);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CurrencyValidator_NegativeAmountTest()
        {
            var validator = new CurrencyValidator();
            double amount = -51;
            var result = validator.Validate(amount);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(CurrencyConversionConstant.NegativeAmountErrorMessage, result.Errors?.FirstOrDefault()?.ErrorMessage);
        }

        [Fact]
        public void CurrencyValidator_MaxDollarAmountTest()
        {
            var validator = new CurrencyValidator();
            double amount = CurrencyConversionConstant.OneBillion;
            var result = validator.Validate(amount);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(CurrencyConversionConstant.MaxDollarAmountErrorMessage, result.Errors?.FirstOrDefault()?.ErrorMessage);
        }

        [Fact]
        public void CurrencyValidator_CentAmountMaxLimitTest()
        {
            var validator = new CurrencyValidator();
            double amount = 10.234;
            var result = validator.Validate(amount);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(CurrencyConversionConstant.MaxCentAmountErrorMessage, result.Errors?.FirstOrDefault()?.ErrorMessage);
        }
    }
}
