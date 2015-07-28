using System;

using FubarDev.Afa.CalculationMethods;
using FubarDev.Afa.CalculationRoundings;

using Xunit;

namespace FubarDev.Afa.Tests.CalculationMethods
{
    public class ArithmeticDegressiveTest
    {
        private void TestResult(CalculationResult result, decimal expectedRemainingValue, decimal expectedDepreciation)
        {
            Assert.Equal(expectedRemainingValue, result.RemainingValue);
            Assert.Equal(expectedDepreciation, result.Depreciation);
        }

        [Fact]
        public void TestArithmetischDegressiv1()
        {
            var data = new CalculationData(150000, 18000, 5);
            var rounding = new DefaultRounding();
            var calc = new ArithmeticDegressive();

            TestResult(rounding.Calculate(calc, data, 0), 150000, 0);
            TestResult(rounding.Calculate(calc, data, 1), 106000, 44000);
            TestResult(rounding.Calculate(calc, data, 2), 70800, 35200);
            TestResult(rounding.Calculate(calc, data, 3), 44400, 26400);
            TestResult(rounding.Calculate(calc, data, 4), 26800, 17600);
            TestResult(rounding.Calculate(calc, data, 5), 18000, 8800);
        }

        [Fact]
        public void TestArithmetischDegressiv2()
        {
            var data = new CalculationData(100000, 0, 10);
            var rounding = new DefaultRounding(2);
            var calc = new ArithmeticDegressive();

            TestResult(rounding.Calculate(calc, data, 0), 100000, 0);
            TestResult(rounding.Calculate(calc, data, 1), 81818.18m, 18181.82m);
            TestResult(rounding.Calculate(calc, data, 2), 65454.55m, 16363.63m);
            TestResult(rounding.Calculate(calc, data, 3), 50909.09m, 14545.46m);
            TestResult(rounding.Calculate(calc, data, 4), 38181.82m, 12727.27m);
            TestResult(rounding.Calculate(calc, data, 5), 27272.73m, 10909.09m);
            TestResult(rounding.Calculate(calc, data, 6), 18181.82m, 9090.91m);
            TestResult(rounding.Calculate(calc, data, 7), 10909.09m, 7272.73m);
            TestResult(rounding.Calculate(calc, data, 8), 5454.55m, 5454.54m);
            TestResult(rounding.Calculate(calc, data, 9), 1818.18m, 3636.37m);
            TestResult(rounding.Calculate(calc, data, 10), 0, 1818.18m);
        }

        [Fact]
        public void TestArithmetischDegressiv3()
        {
            var data = new CalculationData(42000, 0, 6);
            var rounding = new DefaultRounding();
            var calc = new ArithmeticDegressive();

            TestResult(rounding.Calculate(calc, data, 0), 42000, 0);
            TestResult(rounding.Calculate(calc, data, 1), 30000, 12000);
            TestResult(rounding.Calculate(calc, data, 2), 20000, 10000);
            TestResult(rounding.Calculate(calc, data, 3), 12000, 8000);
            TestResult(rounding.Calculate(calc, data, 4), 6000, 6000);
            TestResult(rounding.Calculate(calc, data, 5), 2000, 4000);
            TestResult(rounding.Calculate(calc, data, 6), 0, 2000);
        }
    }
}
