using System;

using NodaTime;

namespace FubarDev.Afa.Entities
{
    /// <summary>
    /// Berechnete Abschreibung
    /// </summary>
    public class CalculatedWriteOff
    {
        private LocalDate _month;

        /// <summary>
        /// Holt oder setzt die ID dieses Objektes
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Holt oder setzt die Abschreibungs-Vorschrift für die dieser berechnete Eintrag gilt
        /// </summary>
        public AssetWriteOff AssetWriteOff { get; set; }

        /// <summary>
        /// Holt oder setzt den Kalendermonat (Tag ist immer 01.)
        /// </summary>
        public LocalDate Month
        {
            get { return _month; }
            set
            {
                if (value.Day != 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "Der Tag des Monats muss immer 1 sein.");
                _month = value;
            }
        }

        /// <summary>
        /// Holt oder setzt den Abschreibungsbetrag
        /// </summary>
        public decimal Depreciation { get; set; }

        /// <summary>
        /// Holt oder setzt den Restbuchwert
        /// </summary>
        public decimal RemainingValue { get; set; }

        /// <summary>
        /// Holt oder setzt den Status für diesen Eintrag
        /// </summary>
        public AssetStatus Status { get; set; }

        /// <summary>
        /// Holt oder setzt einen Wert der angibt, ob dieser Eintrag unveränderlich ist
        /// </summary>
        public bool IsFixed { get; set; }
    }
}
