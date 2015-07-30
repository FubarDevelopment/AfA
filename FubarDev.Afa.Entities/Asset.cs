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
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Holt oder setzt die Kontonummer
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// Holt oder setzt den ersten Teil des Namens
        /// </summary>
        public virtual string Name1 { get; set; }

        /// <summary>
        /// Holt oder setzt den zweiten Teil des Namens
        /// </summary>
        public virtual string Name2 { get; set; }

        /// <summary>
        /// Holt oder setzt die Anzahl
        /// </summary>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// Holt oder setzt die Kostenstelle
        /// </summary>
        public virtual string CostCentre { get; set; }

        /// <summary>
        /// Holt oder setzt den Anschaffungswert
        /// </summary>
        public virtual decimal AcquisitionValue { get; set; }

        /// <summary>
        /// Holt oder setzt den Restbuchwert
        /// </summary>
        public virtual decimal RemainingValue { get; set; }

        /// <summary>
        /// Holt oder setzt das Anschaffungsdatum
        /// </summary>
        public virtual LocalDate AcquisitionDate { get; set; }

        /// <summary>
        /// Holt oder setzt das Anschaffungsdatum als <see cref="DateTime"/>
        /// </summary>
        public virtual DateTime AcquisitionDateTime
        {
            get { return AcquisitionDate.AtMidnight().ToDateTimeUnspecified(); }
            set { AcquisitionDate = LocalDateTime.FromDateTime(value).Date; }
        }

        /// <summary>
        /// Holt oder setzt das Abgangsdatum
        /// </summary>
        public virtual LocalDate? DispatchDate { get; set; }

        /// <summary>
        /// Holt oder setzt das Abgangsdatum als <see cref="DateTime"/>
        /// </summary>
        public virtual DateTime? DispatchDateTime
        {
            get { return (DispatchDate?.AtMidnight())?.ToDateTimeUnspecified(); }
            set { DispatchDate = value == null ? null : (LocalDate?)LocalDateTime.FromDateTime(value.Value).Date; }
        }

        /// <summary>
        /// Holt oder setzt ein beliebig vergebbares Kennzeichen
        /// </summary>
        public virtual int? UserValue { get; set; }

        /// <summary>
        /// Holt oder setzt alle Abschreibungseinstellungen für diese Anlage
        /// </summary>
        public virtual ICollection<AssetWriteOff> AssetWriteOffs { get; set; }

        /// <summary>
        /// Holt oder setzt zusätzliche Zu- oder Abschreibungen für diese Anlage
        /// </summary>
        public virtual ICollection<AdditionalWriteUpOrOff> AdditionalWriteUpOrOffs { get; set; }
    }
}
