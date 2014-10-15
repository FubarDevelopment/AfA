using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FubarDev.Afa
{
    public class CalculationMethodResult
    {
        public CalculationMethodResult(int period, decimal depreciation, decimal remainingValue)
        {
            Period = period;
            Depreciation = depreciation;
            RemainingValue = remainingValue;
        }

        public int Period { get; private set; }
        public decimal Depreciation { get; private set; }
        public decimal RemainingValue { get; private set; }
    }
}
