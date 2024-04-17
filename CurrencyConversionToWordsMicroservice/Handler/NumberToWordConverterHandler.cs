namespace CurrencyConversionToWordsMicroservice.Handler
{
    public class NumberToWordConverterHandler : INumberToWordConverterHandler
    {
        private static string[] ones = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private static string[] teens = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static string[] tens = { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        public string Handle(double amount)
        {
            if (amount == 0)
                return "zero dollars";

            int dollars = (int)amount;
            int cents = (int)((amount - dollars) * 100);

            string dollarsString = ConvertNumberToWord(dollars);

            string result = dollarsString + (dollars == 1 ? " dollar" : " dollars");

            if (cents > 0)
            {
                string centsString = ConvertNumberToWord(cents);
                result += " and " + centsString + (cents == 1 ? " cent" : " cents");
            }

            return result;
        }

        private static string ConvertNumberToWord(int number)
        {
            string result = "";
            if (number / 1000000 >= 1 && number / 1000000 < 1000)
            {
                result = ConvertLessThanOneThousand(number / 1000000) + " million";
                number %= 1000000;

                if (number == 0)
                    return result;

                result += " ";
            }
            if (number / 1000 >= 1 && number / 1000 < 1000)
            {
                result += ConvertLessThanOneThousand(number / 1000) + " thousand" ;
                number = number % 1000;

                if (number == 0)
                    return result;

                result += " ";
            }

            return result + ConvertLessThanOneThousand(number);

        }

        private static string ConvertLessThanOneThousand(int number)
        {
            string result = "";

            if (number % 100 > 0)
            {
                if (number % 100 < 10)
                    result += ones[number % 100];
                else if (number % 100 < 20)
                    result += teens[number % 10];
                else
                    result += tens[(number % 100) / 10] + (number % 10 != 0 ? "-" + ones[number % 10] : string.Empty);
            }

            if (number >= 100)
                result = ones[number / 100] + " hundred " + result;

            return result;
        }
    }
}
