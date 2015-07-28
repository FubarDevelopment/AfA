using FubarDev.Afa.CalculationMethods;
using FubarDev.Afa.CalculationRoundings;

using Xunit;

namespace FubarDev.Afa.Tests.CalculationMethods
{
    public class LinearToPercentTest
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
            var calc = new DegressiveToLinear(new PercentDegressive(0.30m));

            TestResult(rounding.Calculate(calc, data, 0), 10000, 0);
            TestResult(rounding.Calculate(calc, data, 1), 7000, 3000);
            TestResult(rounding.Calculate(calc, data, 2), 4900, 2100);

            // Ab hier wird zur linearen Abrechnung gewechselt
            TestResult(rounding.Calculate(calc, data, 3), 3267, 1633);
            TestResult(rounding.Calculate(calc, data, 4), 1633, 1634);
            TestResult(rounding.Calculate(calc, data, 5), 0, 1633);
        }
    }
}