using CalculatorAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorAPI.Tests
{
    public class CalculatorServiceTests
    {
        private readonly CalculationService _calculator;

        public CalculatorServiceTests()
        {
            _calculator = new CalculationService();
        }

        [Theory]
        [InlineData("2+2", 4)]
        [InlineData("10-3", 7)]
        [InlineData("4*5", 20)]
        [InlineData("15/3", 5)]
        public void Calculate_SimpleOperations_ReturnsCorrectResult(string expression, double expected)
        {
            double result = _calculator.Calculate(expression);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2+2*2", 6)]
        [InlineData("10-4/2", 8)]
        public void Calculate_OperatorPriorities_ReturnsCorrectResult(string expression, double expected)
        {
            double result = _calculator.Calculate(expression);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("(2+2)*2", 8)]
        [InlineData("2*(3+4)-5", 9)]
        [InlineData("10/(2+3)", 2)]
        public void Calculate_WithParentheses_ReturnsCorrectResult(string expression, double expected)
        {
            double result = _calculator.Calculate(expression);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1.5+2.5", 4)]
        [InlineData("5.5-1.2", 4.3)]
        public void Calculate_FloatingPointNumbers_ReturnsCorrectResult(string expression, double expected)
        {
            double result = _calculator.Calculate(expression);
            Assert.Equal(expected, result, 3);
        }
    }
}
