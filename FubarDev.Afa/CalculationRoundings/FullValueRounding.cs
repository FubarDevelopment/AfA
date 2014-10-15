using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FubarDev.Afa.CalculationRoundings
{
    public class FullValueRounding : ICalculationRounding
    {
        public CalculationMethodResult Calculate(ICalculationMethod method, CalculationData data, int period)
        {
            if (period < 0 || period > data.DepreciationRange)
                throw new ArgumentOutOfRangeException("period", "The period must be greater or equal than 0 and less than the value of depreciationRange.");

            if (period == 0)
                return new CalculationMethodResult(period, 0, data.AcquisitionValue);

            var result = method.CalculateDepreciation(data, period);
            CalculationMethodResult resultOld;
            if (period == 1)
            {
                resultOld = new CalculationMethodResult(0, 0, data.AcquisitionValue);
            }
            else
            {
                resultOld = method.CalculateDepreciation(data, period - 1);
            }

            var remainingValue = Math.Round(result.RemainingValue);
            var remainingValueOld = Math.Round(resultOld.RemainingValue);

            return new CalculationMethodResult(period, remainingValueOld - remainingValue, remainingValue);
        }
    }
}
