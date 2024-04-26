using CurrencyConversion.Host.Constants;
using FluentValidation;

namespace CurrencyConversionToWordsMicroservice.Validators
{
    public class CurrencyValidator : AbstractValidator<double>
    {
        // Check whether data is a valid data or not.
        public CurrencyValidator() 
        {
            RuleFor(x => x)
                .GreaterThanOrEqualTo(0)
                .WithMessage(CurrencyConversionConstant.NegativeAmountErrorMessage)
                .LessThan(CurrencyConversionConstant.OneBillion)
                .WithMessage(CurrencyConversionConstant.MaxDollarAmountErrorMessage)
                .Must(HaveNoMoreThanTwoDigitsAfterDecimal)
                .WithMessage(CurrencyConversionConstant.MaxCentAmountErrorMessage);
            
        }

        private static bool HaveNoMoreThanTwoDigitsAfterDecimal(double amount)
        {
            var num = (decimal)Math.Abs(amount);
            var centamount = (num - Math.Truncate(num));
            if (centamount == 0)
                return true;

            var centLength = centamount.ToString().Length - 2;
            if (centLength > 2)
                return false;

            return true;
        }

    }
}
