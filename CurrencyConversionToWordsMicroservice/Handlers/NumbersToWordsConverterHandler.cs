using CurrencyConversion.Host.Constants;

namespace CurrencyConversionToWordsMicroservice.Handler
{
    public class NumbersToWordsConverterHandler : INumbersToWordsConverterHandler
    {
        private static string[] ones = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private static string[] teens = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static string[] tens = { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        public string Handle(double amount)
        {
            if (amount == 0)
                return CurrencyConversionConstant.ZeroDollar;

            //seperate dollar part and cent part from amount
            int dollars = (int)amount;
            int cents = (int)((amount - dollars) * 100);

            // get dollar part amount in words
            string dollarsString = ConvertNumberToWords(dollars);            
            string result = dollarsString + (dollars == 1 ? CurrencyConversionConstant.Dollar : CurrencyConversionConstant.Dollars);

            if (cents > 0)
            {
                //get cent part amount in words
                string centsString = ConvertNumberToWords(cents);
                //concatinate both dollar part amount in words and cent part amount in words
                result += CurrencyConversionConstant.And + centsString + (cents == 1 ? CurrencyConversionConstant.Cent : CurrencyConversionConstant.Cents);
            }

            return result;
        }

        private static string ConvertNumberToWords(int number)
        {
            string result = string.Empty;

            if (number == 0)
                return CurrencyConversionConstant.Zero;
            
            //To check whether amount is greather then a million.
            if (number / 1000000 >= 1 && number / 1000000 < 1000)
            {
                result = ConvertLessThanOneThousand(number / 1000000) + CurrencyConversionConstant.Million;
                number %= 1000000;

                //if amount ends in million and no thousands or hundred is present.
                if (number == 0)
                    return result;

                result += CurrencyConversionConstant.OneSpace;
            }

            //To check whether amount is greather then a thousand.
            if (number / 1000 >= 1 && number / 1000 < 1000)
            {
                result += ConvertLessThanOneThousand(number / 1000) + CurrencyConversionConstant.Thousand ;
                number = number % 1000;

                //if amount ends in thousand and no hundred is present.
                if (number == 0)
                    return result;

                result += CurrencyConversionConstant.OneSpace;
            }

            return result + ConvertLessThanOneThousand(number);

        }

        private static string ConvertLessThanOneThousand(int number)
        {
            string result = string.Empty;

            // to get last 2 digit of a number into words
            if (number % 100 > 0)
                result += number % 100 < 10 ? ones[number % 100]
                    : number % 100 < 20 ?  teens[number % 10]
                    : tens[(number % 100) / 10] + (number % 10 != 0 ? "-" + ones[number % 10] : string.Empty);

            // to get 3rd digit of a number into words.
            if (number >= 100)
                result = ones[number / 100] + CurrencyConversionConstant.Hundred + result;

            return result;
        }
    }
}
