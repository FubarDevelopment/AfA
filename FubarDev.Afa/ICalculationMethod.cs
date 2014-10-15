using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa
{
    public interface ICalculationMethod
    {
        CalculationMethodResult CalculateDepreciation(CalculationData data, int period);
    }
}
