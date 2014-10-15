using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FubarDev.Afa
{
    public interface ICalculationRounding
    {
        CalculationMethodResult Calculate(ICalculationMethod method, CalculationData data, int period);
    }
}
