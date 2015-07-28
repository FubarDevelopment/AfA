using System;

namespace FubarDev.Afa
{
    /// <summary>
    /// Schnittstelle für die Rundung einer Abschreibungsberechnung
    /// </summary>
    public interface ICalculationRounding
    {
        /// <summary>
        /// Berechnet den Abschreibungs- und Restbuchwert und rundet das Ergebnis entsprechend der Implementation.
        /// </summary>
        /// <param name="method">Das Verfahren für die Abschreibungsberechnung</param>
        /// <param name="data">Die Basis-Daten anhand derer der Abschreibungswert berechnet wird.</param>
        /// <param name="period">Das Abrechnungsjahr (beginnend bei 0 = keine Abschreibung)</param>
        /// <returns>Das Ergebnis der Berechnung und Rundung</returns>
        CalculationResult Calculate(ICalculationMethod method, CalculationData data, int period);
    }
}
