using System;

using NodaTime;

namespace FubarDev.Afa
{
    /// <summary>
    /// Datum für AfA-Berechnungen
    /// </summary>
    /// <typeparam name="T">Der Typ, der die Genauigkeit des AfA-Datums definiert</typeparam>
    public struct AfaDate<T> : IComparable<AfaDate<T>>, IComparable, IEquatable<AfaDate<T>>
        where T : IAfaDatePrecisionHandler
    {
        private readonly IAfaDatePrecisionHandler _handler;

        /// <summary>
        /// Holt das Jahr
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// Holt den Monat
        /// </summary>
        public int Month { get; }

        /// <summary>
        /// Holt den Tag
        /// </summary>
        public int Day { get; }

        /// <summary>
        /// Holt den Tag des Jahres
        /// </summary>
        public int DayOfYear => _handler.GetDayOfYear(new LocalDate(Year, Month, Day));

        /// <summary>
        /// Holt die Anzahl an Tagen seit dem 1753-01-01
        /// </summary>
        public long TotalDays => _handler.GetTotalDays(new LocalDate(Year, Month, Day));

        /// <summary>
        /// Holt die Anzahl an Tagen für das aktuelle Jahr
        /// </summary>
        public int TotalDaysOfYear => _handler.GetTotalDaysOfYear(Year);

        /// <summary>
        /// Holt den Monatsanfang
        /// </summary>
        public AfaDate<T> BeginOfMonth => new AfaDate<T>(new LocalDate(Year, Month, 1), _handler);

        /// <summary>
        /// Holt das Monatsende
        /// </summary>
        public AfaDate<T> EndOfMonth => new AfaDate<T>(_handler.Add(_handler.Add(new LocalDate(Year, Month, 1), 0, 1, 0), 0, 0, -1), _handler);

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="AfaDate{T}"/> Klasse.
        /// </summary>
        /// <param name="dt">Das Datum mit dem diese Instanz initialisiert werden soll</param>
        /// <param name="handler">Der Handler für die Genauigkeit des Datums</param>
        public AfaDate(LocalDate dt, IAfaDatePrecisionHandler handler)
            : this(dt.Year, dt.Month, dt.Day, handler)
        {
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="AfaDate{T}"/> Klasse.
        /// </summary>
        /// <param name="year">Das zu setzende Jahr</param>
        /// <param name="month">Den zu setzenden Monat</param>
        /// <param name="day">Den zu setzenden Tag</param>
        /// <param name="handler">Der Handler für die Genauigkeit des Datums</param>
        public AfaDate(int year, int month, int day, IAfaDatePrecisionHandler handler)
        {
            _handler = handler;

            var dt = handler.Fix(new LocalDate(year, month, day));

            Year = dt.Year;
            Month = dt.Month;
            Day = dt.Day;
        }

        /// <summary>
        /// Vergleicht das aktuelle Objekt mit einem anderen Objekt desselben Typs.
        /// </summary>
        /// <returns>
        /// Ein Wert, der die relative Reihenfolge der verglichenen Objekte angibt. Der Rückgabewert hat folgende Bedeutung: 
        /// Wert  	Bedeutung  	
        /// Kleiner als 0  	Dieses Objekt ist kleiner als der <paramref name="other"/>-Parameter. 	
        /// Null  	Dieses Objekt ist gleich <paramref name="other"/>.  	
        /// Größer als 0 (null)  	Dieses Objekt ist größer als <paramref name="other"/>.
        /// </returns>
        /// <param name="other">Ein Objekt, das mit diesem Objekt verglichen werden soll.</param>
        public int CompareTo(AfaDate<T> other)
        {
            return TotalDays.CompareTo(other.TotalDays);
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            return CompareTo((AfaDate<T>)obj);
        }

        /// <summary>
        /// Gibt an, ob das aktuelle Objekt gleich einem anderen Objekt des gleichen Typs ist.
        /// </summary>
        /// <returns>
        /// true, wenn das aktuelle Objekt gleich dem <paramref name="other"/>-Parameter ist, andernfalls false.
        /// </returns>
        /// <param name="other">Ein Objekt, das mit diesem Objekt verglichen werden soll.</param>
        public bool Equals(AfaDate<T> other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Gibt an, ob diese Instanz und ein angegebenes Objekt gleich sind.
        /// </summary>
        /// <returns>
        /// true, wenn <paramref name="obj"/> und diese Instanz denselben Typ aufweisen und denselben Wert darstellen, andernfalls false.
        /// </returns>
        /// <param name="obj">Das Objekt, das mit der aktuellen Instanz verglichen werden soll.</param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return Equals((AfaDate<T>)obj);
        }

        /// <summary>
        /// Gibt den Hashcode für diese Instanz zurück.
        /// </summary>
        /// <returns>
        /// Eine 32-Bit-Ganzzahl mit Vorzeichen. Diese ist der Hashcode für die Instanz.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return TotalDays.GetHashCode();
        }

        /// <summary>
        /// Addiert Tage zu diesem Datum hinzu
        /// </summary>
        /// <param name="days">Die Anzahl an Tagen die zu addieren sind (kann auch &lt; 0 sein)</param>
        /// <returns>Das neu errechnete Datum</returns>
        public AfaDate<T> AddDays(long days)
        {
            return new AfaDate<T>(_handler.Add(new LocalDate(Year, Month, Day), 0, 0, days), _handler);
        }

        /// <summary>
        /// Addiert Monate zu diesem Datum hinzu
        /// </summary>
        /// <param name="months">Die Anzahl an Monate die zu addieren sind (kann auch &lt; 0 sein)</param>
        /// <returns>Das neu errechnete Datum</returns>
        public AfaDate<T> AddMonths(long months)
        {
            return new AfaDate<T>(_handler.Add(new LocalDate(Year, Month, Day), 0, months, 0), _handler);
        }

        /// <summary>
        /// Addiert Jahre zu diesem Datum hinzu
        /// </summary>
        /// <param name="years">Die Anzahl an Jahren die zu addieren sind (kann auch &lt; 0 sein)</param>
        /// <returns>Das neu errechnete Datum</returns>
        public AfaDate<T> AddYears(long years)
        {
            return new AfaDate<T>(_handler.Add(new LocalDate(Year, Month, Day), years, 0, 0), _handler);
        }

        /// <summary>
        /// Errechnet die Differenz in Tagen
        /// </summary>
        /// <param name="d1">Das erste Datum</param>
        /// <param name="d2">Das zweite Datum</param>
        /// <returns>Die Differenz in Tagen</returns>
        public static long operator -(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.TotalDays - d2.TotalDays;
        }

        /// <summary>
        /// Subtrahiert Tage vom aktuellen Datum
        /// </summary>
        /// <param name="d">Das aktuelle Datum</param>
        /// <param name="days">Die zu subtrahierenden Tage</param>
        /// <returns>Das neu errechnete Datum</returns>
        public static AfaDate<T> operator -(AfaDate<T> d, long days)
        {
            return d.AddDays(-days);
        }

        /// <summary>
        /// Addiert Tage zum aktuellen Datum
        /// </summary>
        /// <param name="d">Das aktuelle Datum</param>
        /// <param name="days">Die zu addierenden Tage</param>
        /// <returns>Das neu errechnete Datum</returns>
        public static AfaDate<T> operator +(AfaDate<T> d, long days)
        {
            return d.AddDays(days);
        }

        /// <summary>
        /// Vergleicht, ob die beiden Datumsangaben auf den gleichen Tag verweisen.
        /// </summary>
        /// <param name="d1">Das erste Datum</param>
        /// <param name="d2">Das zweite Datum</param>
        /// <returns><code>true</code> wenn beide Datumsangaben auf den gleichen Tag verweisen</returns>
        public static bool operator ==(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) == 0;
        }

        /// <summary>
        /// Vergleicht, ob <paramref name="d1"/> größer als <paramref name="d2"/> ist.
        /// </summary>
        /// <param name="d1">Das erste Datum</param>
        /// <param name="d2">Das zweite Datum</param>
        /// <returns><code>true</code> wenn <paramref name="d1"/> größer als <paramref name="d2"/> ist</returns>
        public static bool operator >(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) > 0;
        }

        /// <summary>
        /// Vergleicht, ob <paramref name="d1"/> größer oder gleich <paramref name="d2"/> ist.
        /// </summary>
        /// <param name="d1">Das erste Datum</param>
        /// <param name="d2">Das zweite Datum</param>
        /// <returns><code>true</code> wenn <paramref name="d1"/> größer oder gleich <paramref name="d2"/> ist</returns>
        public static bool operator >=(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) >= 0;
        }

        /// <summary>
        /// Vergleicht, ob die beiden Datumsangaben auf unterschiedliche Tage verweisen.
        /// </summary>
        /// <param name="d1">Das erste Datum</param>
        /// <param name="d2">Das zweite Datum</param>
        /// <returns><code>true</code> wenn beide Datumsangaben auf unterschiedliche Tage verweisen</returns>
        public static bool operator !=(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) != 0;
        }

        /// <summary>
        /// Vergleicht, ob <paramref name="d1"/> kleiner als <paramref name="d2"/> ist.
        /// </summary>
        /// <param name="d1">Das erste Datum</param>
        /// <param name="d2">Das zweite Datum</param>
        /// <returns><code>true</code> wenn <paramref name="d1"/> kleiner als <paramref name="d2"/> ist</returns>
        public static bool operator <(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) < 0;
        }

        /// <summary>
        /// Vergleicht, ob <paramref name="d1"/> kleiner oder gleich <paramref name="d2"/> ist.
        /// </summary>
        /// <param name="d1">Das erste Datum</param>
        /// <param name="d2">Das zweite Datum</param>
        /// <returns><code>true</code> wenn <paramref name="d1"/> kleiner oder gleich <paramref name="d2"/> ist</returns>
        public static bool operator <=(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) <= 0;
        }

        /// <summary>
        /// Konvertiert ein AfA-Datum in ein <see cref="LocalDate"/>
        /// </summary>
        /// <param name="d">Das zu konvertierende Datum</param>
        public static implicit operator LocalDate(AfaDate<T> d)
        {
            return new LocalDate(d.Year, d.Month, d.Day);
        }

        /// <summary>
        /// Konvertiert ein AfA-Datum in ein <see cref="LocalDate"/>
        /// </summary>
        /// <param name="d">Das zu konvertierende Datum</param>
        public static implicit operator LocalDate? (AfaDate<T>? d)
        {
            if (!d.HasValue)
                return null;
            return new LocalDate(d.Value.Year, d.Value.Month, d.Value.Day);
        }

        /// <summary>
        /// Rundung des AfA-Datums
        /// </summary>
        /// <param name="mode">Der Rundungsmodus</param>
        /// <returns>Das gerundete AfA-Datum</returns>
        public AfaDate<T> Round(AfaDateRounding mode)
        {
            LocalDate result;
            switch (mode)
            {
                case AfaDateRounding.Day:
                    result = this;
                    break;
                case AfaDateRounding.Month:
                    result = new LocalDate(Year, Month, 1);
                    if (Day >= 15)
                        result = result + Period.FromMonths(1);
                    break;
                case AfaDateRounding.BeginOfMonth:
                    result = new LocalDate(Year, Month, 1);
                    break;
                case AfaDateRounding.HalfYear:
                    if (Month >= 7)
                    {
                        result = new LocalDate(Year, 7, 1);
                    }
                    else
                    {
                        result = new LocalDate(Year, 1, 1);
                    }
                    break;
                case AfaDateRounding.BeginOfYear:
                    result = new LocalDate(Year, 1, 1);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return new AfaDate<T>(result, _handler);
        }
    }
}
