﻿using System;

using NodaTime;

namespace FubarDev.Afa.Entities
{
    /// <summary>
    /// Manuelle Ab- oder Zuschreibung
    /// </summary>
    public class AdditionalWriteUpOrOff
    {
        /// <summary>
        /// Holt oder setzt die ID dieses Objektes
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Holt oder setzt die Anlage für die dieser Eintrag gilt
        /// </summary>
        public virtual Asset Asset { get; set; }

        /// <summary>
        /// Holt oder setzt den Wert, der ab- oder zugeschrieben wird
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>&gt; 0 : Zuschreibung</item>
        /// <item>&lt; 0 : Abschreibung</item>
        /// </list>
        /// </remarks>
        public virtual decimal Value { get; set; }

        /// <summary>
        /// Holt oder setzt das Datum für die Ab- oder Zuschreibung
        /// </summary>
        public virtual LocalDate Date { get; set; }

        /// <summary>
        /// Holt oder setzt das Datum für die Ab- oder Zuschreibung als <see cref="DateTime"/>
        /// </summary>
        public virtual DateTime DateTime
        {
            get { return Date.AtMidnight().ToDateTimeUnspecified(); }
            set { Date = LocalDateTime.FromDateTime(value).Date; }
        }
    }
}
