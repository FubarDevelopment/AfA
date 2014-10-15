using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FubarDev.Afa
{
    public class CalculationData
    {
        public CalculationData(decimal acquisitionValue, decimal targetRemainingValue, int depreciationRange)
        {
            if (targetRemainingValue >= acquisitionValue)
                throw new ArgumentOutOfRangeException("targetRemainingValue", "The target remaining value must be less than the value of acquisitionValue.");
            if (depreciationRange < 1)
                throw new ArgumentOutOfRangeException("depreciationRange", "The depreciation range must be greater than 0.");

            AcquisitionValue = acquisitionValue;
            TargetRemainingValue = targetRemainingValue;
            DepreciationRange = depreciationRange;
        }

        /// <summary>
        /// Anschaffungswert
        /// </summary>
        public decimal AcquisitionValue { get; private set; }
        /// <summary>
        /// Restwert
        /// </summary>
        public decimal TargetRemainingValue { get; private set; }
        /// <summary>
        /// Nutzungsdauer
        /// </summary>
        public int DepreciationRange { get; private set; }
    }
}
