using CurrencyConversionToWordsMicroservice.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyConversion.UnitTests.Handler
{
    public class NumbersToWordsConverterHandlerTest
    {
        [Fact]
        public void Handle_Test()
        {
            //Arrange
            var result = "twenty-five dollars and ten cents";
            var handler = new NumbersToWordsConverterHandler();
            double amount = 25.1;

            //Act
            var response = handler.Handle(amount);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(result, response);

        }

    }
}
