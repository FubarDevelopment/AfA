using System;

using FubarDev.Afa.CalculationMethods;
using FubarDev.Afa.CalculationRoundings;

using Xunit;

namespace FubarDev.Afa.Tests.CalculationMethods
{
    public class LinearTest
    {
        private void TestResult(CalculationResult result, decimal expectedRemainingValue, decimal expectedDepreciation)
        {
            Assert.Equal(expectedRemainingValue, result.RemainingValue);
            Assert.Equal(expectedDepreciation, result.Depreciation);
        }

        [Fact]
        public void TestLinear1()
        {
            var data = new CalculationData(10000, 0, 5);
            var rounding = new DefaultRounding();
            var calc = new Linear();

            TestResult(rounding.Calculate(calc, data, 0), 10000, 0);
            TestResult(rounding.Calculate(calc, data, 1), 8000, 2000);
            TestResult(rounding.Calculate(calc, data, 2), 6000, 2000);
            TestResult(rounding.Calculate(calc, data, 3), 4000, 2000);
            TestResult(rounding.Calculate(calc, data, 4), 2000, 2000);
            TestResult(rounding.Calculate(calc, data, 5), 0, 2000);
        }
    }
}
