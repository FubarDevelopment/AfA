using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FubarDev.Afa.CalculationMethods
{
    public class Linear : ICalculationMethod
    {
        public CalculationMethodResult CalculateDepreciation(CalculationData data, int period)
        {
            if (period < 1 || period > data.DepreciationRange)
                throw new ArgumentOutOfRangeException("period", "The period must be greater than 0 and less than the value of depreciationRange.");

            var depreciation = (data.AcquisitionValue - data.TargetRemainingValue) / data.DepreciationRange;
            var remainingValue = data.AcquisitionValue - depreciation * period;

            return new CalculationMethodResult(period, depreciation, remainingValue);
        }
    }
}
