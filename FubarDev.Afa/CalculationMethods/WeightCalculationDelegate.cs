using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FubarDev.Afa.CalculationMethods
{
    /// <summary>
    /// Berechnet die Gewichtung einer Periode.
    /// </summary>
    /// <param name="part">Gewichtung für das Jahr (beginnt mit 1)</param>
    /// <param name="depreciationRange">Nutzungsdauer</param>
    /// <returns>Gewichtung</returns>
    public delegate int WeightCalculationDelegate(int period, int depreciationRange);
}
