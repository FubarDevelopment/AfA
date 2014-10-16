using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa.CalculationMethods
{
    public class ArithmetischDegressiv : ArithmetischProgressiv
    {
        public ArithmetischDegressiv()
            : base((period, range) => (range - period + 1))
        {

        }

        public ArithmetischDegressiv(WeightCalculationDelegate weightCalculator)
            : base(weightCalculator)
        {
        }
    }
}
