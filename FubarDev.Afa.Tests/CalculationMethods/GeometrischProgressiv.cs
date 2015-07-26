using System;

using FubarDev.Afa.CalculationRoundings;

using Xunit;

namespace FubarDev.Afa.Tests.CalculationMethods
{
    public class GeometrischProgressiv
    {
        private void TestResult(CalculationResult result, decimal expectedRemainingValue, decimal expectedDepreciation)
        {
            Assert.Equal(expectedRemainingValue, result.RemainingValue);
            Assert.Equal(expectedDepreciation, result.Depreciation);
        }

        [Fact]
        public void TestGeometrischProgressiv1()
        {
            var data = new CalculationData(150000, 18000, 5);
            var rounding = new FullValueRounding();
            var calc = new GeometrischProgressiv();

            TestResult(rounding.Calculate(calc, data, 0), 150000, 0);
            TestResult(rounding.Calculate(calc, data, 1), 140493, 9507);
            TestResult(rounding.Calculate(calc, data, 2), 125966, 14527);
            TestResult(rounding.Calculate(calc, data, 3), 103766, 22200);
            TestResult(rounding.Calculate(calc, data, 4), 69842, 33924);
            TestResult(rounding.Calculate(calc, data, 5), 18000, 51842);
        }
    }
}
