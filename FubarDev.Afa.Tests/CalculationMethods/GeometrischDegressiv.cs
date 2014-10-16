using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FubarDev.Afa.Tests.CalculationMethods
{
    [TestClass]
    public class GeometrischDegressiv
    {
        private void TestResult(CalculationResult result, decimal expectedRemainingValue, decimal expectedDepreciation)
        {
            Assert.AreEqual(expectedRemainingValue, result.RemainingValue);
            Assert.AreEqual(expectedDepreciation, result.Depreciation);
        }

        [TestMethod]
        public void TestGeometrischDegressiv1()
        {
            var data = new Afa.CalculationData(150000, 18000, 5);
            var rounding = new Afa.CalculationRoundings.FullValueRounding();
            var calc = new Afa.CalculationMethods.GeometrischDegressiv();

            // Das waren die Werte aus dem Beispiel, doch die scheinen ungenau zu sein:
            // http://www.rechnungswesen-portal.de/Fachinfo/Anlagevermoegen/Geometrisch-degressive-Abschreibung.html
            //TestResult(rounding.Calculate(calc, data, 0), 150000, 0);
            //TestResult(rounding.Calculate(calc, data, 1), 98160, 51840);
            //TestResult(rounding.Calculate(calc, data, 2), 64236, 33924);
            //TestResult(rounding.Calculate(calc, data, 3), 42036, 22200);
            //TestResult(rounding.Calculate(calc, data, 4), 27508, 14528);
            //TestResult(rounding.Calculate(calc, data, 5), 18000, 9508);

            TestResult(rounding.Calculate(calc, data, 0), 150000, 0);
            TestResult(rounding.Calculate(calc, data, 1), 98158, 51842);
            TestResult(rounding.Calculate(calc, data, 2), 64234, 33924);
            TestResult(rounding.Calculate(calc, data, 3), 42034, 22200);
            TestResult(rounding.Calculate(calc, data, 4), 27507, 14527);
            TestResult(rounding.Calculate(calc, data, 5), 18000, 9507);
        }
    }
}
