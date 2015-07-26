using System;

using FubarDev.Afa.CalculationMethods;
using FubarDev.Afa.CalculationRoundings;

using Xunit;

namespace FubarDev.Afa.Tests.CalculationMethods
{
    public class GeometrischDegressivTest
    {
        private void TestResult(CalculationResult result, decimal expectedRemainingValue, decimal expectedDepreciation)
        {
            Assert.Equal(expectedRemainingValue, result.RemainingValue);
            Assert.Equal(expectedDepreciation, result.Depreciation);
        }

        [Fact]
        public void TestGeometrischDegressiv1()
        {
            var data = new CalculationData(150000, 18000, 5);
            var rounding = new FullValueRounding();
            var calc = new GeometrischDegressiv();

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
