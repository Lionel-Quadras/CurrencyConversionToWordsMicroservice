using FluentValidation;

namespace CurrencyConversionToWordsMicroservice.Validators
{
    public class CurrencyValidator : AbstractValidator<double>
    {
        public CurrencyValidator() 
        {
            RuleFor(x => x)
                .NotNull()
                .NotEmpty()
                .WithMessage("Amount cannot be Null")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Amount cannot be negative")
                .LessThan(1000000000)
                .WithMessage("Please enter amount less than 1 Billion")
                .Must(HaveNoMoreThanTwoDigitsAfterDecimal)
                .WithMessage("Cent Cannot have more than 99");
            
        }

        private static bool HaveNoMoreThanTwoDigitsAfterDecimal(double amount)
        {
            var num = (decimal)Math.Abs(amount);
            var centamount = (num - Math.Truncate(num));
            if (centamount == 0)
                return true;

            var centLength = centamount.ToString().Length - 2;
            if (centLength > 2)
            {
                return false;
            }

            return true;
        }

    }
}
