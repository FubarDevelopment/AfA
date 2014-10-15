using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa
{
    public interface ICalculationMethod
    {
        CalculationResult CalculateDepreciation(CalculationData data, int period);
    }
}
