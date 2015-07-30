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
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Holt oder setzt die Abschreibungs-Vorschrift für die dieser berechnete Eintrag gilt
        /// </summary>
        public virtual AssetWriteOff AssetWriteOff { get; set; }

        /// <summary>
        /// Holt oder setzt den Kalendermonat (Tag ist immer 01.)
        /// </summary>
        public virtual LocalDate Month
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
        /// Holt oder setzt den Kalendermonat (Tag ist immer 01.) als <see cref="DateTime"/>
        /// </summary>
        public virtual DateTime MonthDateTime
        {
            get { return Month.AtMidnight().ToDateTimeUnspecified(); }
            set { Month = LocalDateTime.FromDateTime(value).Date; }
        }

        /// <summary>
        /// Holt oder setzt den Abschreibungsbetrag
        /// </summary>
        public virtual decimal Depreciation { get; set; }

        /// <summary>
        /// Holt oder setzt den Restbuchwert
        /// </summary>
        public virtual decimal RemainingValue { get; set; }

        /// <summary>
        /// Holt oder setzt den Status für diesen Eintrag
        /// </summary>
        public virtual AssetStatus Status { get; set; }

        /// <summary>
        /// Holt oder setzt einen Wert der angibt, ob dieser Eintrag unveränderlich ist
        /// </summary>
        public virtual bool IsFixed { get; set; }
    }
}
