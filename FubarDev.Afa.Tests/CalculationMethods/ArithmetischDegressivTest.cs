﻿using System;

using FubarDev.Afa.CalculationMethods;
using FubarDev.Afa.CalculationRoundings;

using Xunit;

namespace FubarDev.Afa.Tests.CalculationMethods
{
    public class ArithmetischDegressivTest
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
            var rounding = new FullValueRounding();
            var calc = new ArithmetischDegressiv();

            TestResult(rounding.Calculate(calc, data, 0), 150000, 0);
            TestResult(rounding.Calculate(calc, data, 1), 106000, 44000);
            TestResult(rounding.Calculate(calc, data, 2), 70800, 35200);
            TestResult(rounding.Calculate(calc, data, 3), 44400, 26400);
            TestResult(rounding.Calculate(calc, data, 4), 26800, 17600);
            TestResult(rounding.Calculate(calc, data, 5), 18000, 8800);
        }
    }
}