using System;

using FubarDev.Afa.CalculationRoundings;

using Xunit;

namespace FubarDev.Afa.Tests.CalculationMethods
{
    public class ArithmetischProgressiv
    {
        private void TestResult(CalculationResult result, decimal expectedRemainingValue, decimal expectedDepreciation)
        {
            Assert.Equal(expectedRemainingValue, result.RemainingValue);
            Assert.Equal(expectedDepreciation, result.Depreciation);
        }

        [Fact]
        public void TestArithmetischProgressiv1()
        {
            var data = new CalculationData(150000, 18000, 5);
            var rounding = new FullValueRounding();
            var calc = new ArithmetischProgressiv();

            TestResult(rounding.Calculate(calc, data, 0), 150000, 0);
            TestResult(rounding.Calculate(calc, data, 1), 141200, 8800);
            TestResult(rounding.Calculate(calc, data, 2), 123600, 17600);
            TestResult(rounding.Calculate(calc, data, 3), 97200, 26400);
            TestResult(rounding.Calculate(calc, data, 4), 62000, 35200);
            TestResult(rounding.Calculate(calc, data, 5), 18000, 44000);
        }
    }
}
