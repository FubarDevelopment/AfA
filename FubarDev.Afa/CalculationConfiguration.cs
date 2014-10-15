using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FubarDev.Afa
{
    public delegate decimal RoundingDelegate(decimal value);

    public class CalculationConfiguration
    {
        public RoundingDelegate ValueRounding { get; set; }

        public CalculationConfiguration()
        {
            ValueRounding = (v) => Math.Round(v, 0, MidpointRounding.AwayFromZero);
        }
    }
}
