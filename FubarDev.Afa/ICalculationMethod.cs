using System;

namespace FubarDev.Afa
{
    /// <summary>
    /// Schnittstelle für Verfahren für die Berechnung von Abschreibungswerten
    /// </summary>
    public interface ICalculationMethod
    {
        /// <summary>
        /// Berechnung des Abschreibungs- und Restbuchwertes (<see cref="CalculationResult"/>) für das Abschreibungsjahr (<paramref name="period"/>).
        /// </summary>
        /// <param name="data">Die grundlegenden Daten für die Abschreibungsberechnung</param>
        /// <param name="period">Das Abschreibungsjahr für das die Daten errechnet werden sollen.</param>
        /// <returns>Die errechneten Abschreibungs- und Restbuchwerte</returns>
        CalculationResult CalculateDepreciation(CalculationData data, int period);
    }
}
