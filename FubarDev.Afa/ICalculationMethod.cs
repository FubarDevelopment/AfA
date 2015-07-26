using System;

namespace FubarDev.Afa
{
    public interface ICalculationMethod
    {
        CalculationResult CalculateDepreciation(CalculationData data, int period);
    }
}
