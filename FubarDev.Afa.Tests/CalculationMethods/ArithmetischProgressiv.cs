using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FubarDev.Afa.Tests.CalculationMethods
{
    [TestClass]
    public class ArithmetischProgressiv
    {
        private void TestResult(CalculationResult result, decimal expectedRemainingValue, decimal expectedDepreciation)
        {
            Assert.AreEqual(expectedRemainingValue, result.RemainingValue);
            Assert.AreEqual(expectedDepreciation, result.Depreciation);
        }

        [TestMethod]
        public void TestArithmetischProgressiv1()
        {
            var data = new Afa.CalculationData(150000, 18000, 5);
            var rounding = new Afa.CalculationRoundings.FullValueRounding();
            var calc = new Afa.CalculationMethods.ArithmetischProgressiv();

            TestResult(rounding.Calculate(calc, data, 0), 150000, 0);
            TestResult(rounding.Calculate(calc, data, 1), 141200, 8800);
            TestResult(rounding.Calculate(calc, data, 2), 123600, 17600);
            TestResult(rounding.Calculate(calc, data, 3), 97200, 26400);
            TestResult(rounding.Calculate(calc, data, 4), 62000, 35200);
            TestResult(rounding.Calculate(calc, data, 5), 18000, 44000);
        }
    }
}
