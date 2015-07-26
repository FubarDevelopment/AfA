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

        [Fact]
        public void TestGeometrischDegressiv2()
        {
            var data = new CalculationData(60000, 10678.71m, 6);
            var rounding = new FullValueRounding(2);
            var calc = new GeometrischDegressiv();

            TestResult(rounding.Calculate(calc, data, 0), 60000.00m, 0);
            TestResult(rounding.Calculate(calc, data, 1), 45000.00m, 15000.00m);
            TestResult(rounding.Calculate(calc, data, 2), 33750.00m, 11250.00m);
            TestResult(rounding.Calculate(calc, data, 3), 25312.50m, 8437.50m);
            TestResult(rounding.Calculate(calc, data, 4), 18984.37m, 6328.13m);
            TestResult(rounding.Calculate(calc, data, 5), 14238.28m, 4746.09m);
            TestResult(rounding.Calculate(calc, data, 6), 10678.71m, 3559.57m);
        }
    }
}
