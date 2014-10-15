using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FubarDev.Afa.Tests.CalculationMethods
{
    [TestClass]
    public class Linear
    {
        private void TestResult(CalculationResult result, decimal expectedRemainingValue, decimal expectedDepreciation)
        {
            Assert.AreEqual(expectedRemainingValue, result.RemainingValue);
            Assert.AreEqual(expectedDepreciation, result.Depreciation);
        }

        [TestMethod]
        public void TestLinear1()
        {
            var data = new Afa.CalculationData(10000, 0, 5);
            var rounding = new Afa.CalculationRoundings.FullValueRounding();
            var calc = new Afa.CalculationMethods.Linear();

            TestResult(rounding.Calculate(calc, data, 0), 10000, 0);
            TestResult(rounding.Calculate(calc, data, 1), 8000, 2000);
            TestResult(rounding.Calculate(calc, data, 2), 6000, 2000);
            TestResult(rounding.Calculate(calc, data, 3), 4000, 2000);
            TestResult(rounding.Calculate(calc, data, 4), 2000, 2000);
            TestResult(rounding.Calculate(calc, data, 5), 0, 2000);
        }
    }
}
