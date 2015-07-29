using System;
using System.Collections.Generic;

using NodaTime;

namespace FubarDev.Afa.Entities
{
    /// <summary>
    /// Anlage
    /// </summary>
    public class Asset
    {
        /// <summary>
        /// Holt oder setzt die ID dieses Objektes
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Holt oder setzt die Kontonummer
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Holt oder setzt den ersten Teil des Namens
        /// </summary>
        public string Name1 { get; set; }

        /// <summary>
        /// Holt oder setzt den zweiten Teil des Namens
        /// </summary>
        public string Name2 { get; set; }

        /// <summary>
        /// Holt oder setzt die Anzahl
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Holt oder setzt die Kostenstelle
        /// </summary>
        public string CostCentre { get; set; }

        /// <summary>
        /// Holt oder setzt den Anschaffungswert
        /// </summary>
        public decimal AcquisitionValue { get; set; }

        /// <summary>
        /// Holt oder setzt den Restbuchwert
        /// </summary>
        public decimal RemainingValue { get; set; }

        /// <summary>
        /// Holt oder setzt das Anschaffungsdatum
        /// </summary>
        public LocalDate AcquisitionDate { get; set; }

        /// <summary>
        /// Holt oder setzt das Abgangsdatum
        /// </summary>
        public LocalDate? DispatchDate { get; set; }

        /// <summary>
        /// Holt oder setzt ein beliebig vergebbares Kennzeichen
        /// </summary>
        public int UserValue { get; set; }

        /// <summary>
        /// Holt oder setzt alle Abschreibungseinstellungen für diese Anlage
        /// </summary>
        public ICollection<AssetWriteOff> AssetWriteOffs { get; set; }

        /// <summary>
        /// Holt oder setzt zusätzliche Zu- oder Abschreibungen für diese Anlage
        /// </summary>
        public ICollection<AdditionalWriteUpOrOff> AdditionalWriteUpOrOffs { get; set; }
    }
}
