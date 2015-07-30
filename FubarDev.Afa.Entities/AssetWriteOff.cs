using System;
using System.Collections.Generic;

namespace FubarDev.Afa.Entities
{
    /// <summary>
    /// Einstellungen für die Berechnung der Abschreibung anhand der hier definierten Werte
    /// </summary>
    public class AssetWriteOff
    {
        private bool _changeToLinear;

        private decimal? _percent;

        /// <summary>
        /// Holt oder setzt die ID dieses Objektes
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Holt oder setzt die Anlage für die dieser Eintrag gilt
        /// </summary>
        public virtual Asset Asset { get; set; }

        /// <summary>
        /// Holt oder setzt die Jahre relativ zum Anschaffungsjahr
        /// </summary>
        /// <remarks>
        /// Das Jahr 0 ist das Anschaffungsjahr.
        /// </remarks>
        public virtual int YearsSinceAcquisition { get; set; }

        /// <summary>
        /// Holt oder setzt den aktuellen Buchwert der Anlage
        /// </summary>
        public virtual decimal CurrentValue { get; set; }

        /// <summary>
        /// Holt oder setzt die Nutzungsdauer in Jahren
        /// </summary>
        public virtual int Lifetime { get; set; }

        /// <summary>
        /// Holt oder setzt die Art der Datumsrundung
        /// </summary>
        public virtual DateRoundingMode Rounding { get; set; }

        /// <summary>
        /// Holt oder setzt die Information, wie genau die Datumsrechnung sein soll
        /// </summary>
        public virtual AfaDatePrecision Precision { get; set; }

        /// <summary>
        /// Holt oder setzt das Berechnungsverfahren
        /// </summary>
        public virtual AfaCalculationMethod CalculationMethod { get; set; }

        /// <summary>
        /// Holt oder setzt die Prozente für das Berechnungsverfahren <see cref="AfaCalculationMethod.PercentToLinear"/>
        /// </summary>
        public virtual decimal? Percent
        {
            get
            {
                if (CalculationMethod == AfaCalculationMethod.PercentToLinear)
                    return _percent;
                return null;
            }
            set
            {
                _percent = value;
            }
        }

        /// <summary>
        /// Holt oder setzt einen Wert der angibt, ob es einen Wechsel von einer degressiven zu einer Linearen Berechnung gibt.
        /// </summary>
        public virtual bool ChangeToLinear
        {
            get
            {
                switch (CalculationMethod)
                {
                    case AfaCalculationMethod.PercentToLinear:
                        return true;
                    case AfaCalculationMethod.GeometricProgressive:
                    case AfaCalculationMethod.ArithmeticProgressive:
                    case AfaCalculationMethod.LowValueFixedAsset:
                        return false;
                }
                return _changeToLinear;
            }
            set
            {
                _changeToLinear = value;
            }
        }

        /// <summary>
        /// Holt oder setzt die berechneten Abschreibungen
        /// </summary>
        public virtual ICollection<CalculatedWriteOff> CalculatedWriteOffs { get; set; }
    }
}
