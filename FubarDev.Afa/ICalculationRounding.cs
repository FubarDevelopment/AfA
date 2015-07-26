using System;

namespace FubarDev.Afa
{
    public interface ICalculationRounding
    {
        CalculationResult Calculate(ICalculationMethod method, CalculationData data, int period);
    }
}
