using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa
{
    public struct AfaDate : IComparable<AfaDate>, IComparable, IEquatable<AfaDate>
    {
        private static readonly DateTime _startDate = new DateTime(1753, 1, 1);

        public const AfaDatePrecision DefaultPrecision = AfaDatePrecision.Days30;

        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public int DayOfYear
        {
            get
            {
                switch (Precision)
                {
                    case AfaDatePrecision.Days30:
                        return (Month - 1) * 30 + Day;
                    case AfaDatePrecision.DaysActual:
                        return new DateTime(Year, Month, Day).DayOfYear;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public int TotalDays
        {
            get
            {
                switch (Precision)
                {
                    case AfaDatePrecision.Days30:
                        return (Year - _startDate.Year) * 360 + DayOfYear;
                    case AfaDatePrecision.DaysActual:
                        return (new DateTime(Year, Month, Day) - _startDate).Days;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public AfaDatePrecision Precision { get; private set; }

        public AfaDate(DateTime dt, AfaDatePrecision precision = DefaultPrecision)
            : this(dt.Year, dt.Month, dt.Day, precision)
        {
        }

        public AfaDate(int year, int month, int day, AfaDatePrecision precision = DefaultPrecision)
            : this()
        {
            // Zuerst das Datum glatt ziehen
            Year = year;
            Month = month;
            Day = day;

            var tmp = CalculateDateActual(0, 0, 0);
            
            // Dann die gewünschte Präzision verwenden
            Precision = precision;

            year = tmp.Year;
            month = tmp.Month;
            day = tmp.Day;

            FixDate(ref year, ref month, ref day);

            Year = year;
            Month = month;
            Day = day;
        }

        public static AfaDate GetEndOfMonth(int year, int month, AfaDatePrecision precision = DefaultPrecision)
        {
            var dt = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
            return new AfaDate(dt, precision);
        }

        public static AfaDate GetBeginOfMonth(int year, int month, AfaDatePrecision precision = DefaultPrecision)
        {
            return new AfaDate(year, month, 1, precision);
        }

        private void FixDate(ref int year, ref int month, ref int day)
        {
            switch (Precision)
            {
                case AfaDatePrecision.Days30:
                    if (day > 31)
                        day = 30;
                    break;
                case AfaDatePrecision.DaysActual:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private AfaDate CalculateDate360(int addYear, int addMonth, int addDay)
        {
            var day = Day + addDay;
            var month = Month + addMonth;
            var year = Year + addYear;

            month += day / 30;
            day %= 30;

            if (day < 0)
            {
                day += 30;
                --month;
            }
            year += month / 12;
            month %= 12;
            if (month < 0)
            {
                month += 12;
                --year;
            }

            return new AfaDate(year, month, day, Precision);
        }

        private AfaDate CalculateDateActual(int addYear, int addMonth, int addDay)
        {
            var dt = new DateTime(Year, Month, Day)
                .AddYears(addYear)
                .AddMonths(addMonth)
                .AddDays(addDay);
            return new AfaDate(dt.Year, dt.Month, dt.Day, Precision);
        }

        private AfaDate CalculateDate(int addYear, int addMonth, int addDay)
        {
            switch (Precision)
            {
                case AfaDatePrecision.Days30:
                    return CalculateDate360(addYear, addMonth, addDay);
                case AfaDatePrecision.DaysActual:
                    return CalculateDateActual(addYear, addMonth, addDay);
                default:
                    throw new NotImplementedException();
            }
        }

        public int CompareTo(AfaDate other)
        {
            if (this.Precision != other.Precision)
                throw new InvalidOperationException("Both dates must be of the same precision!");
            return this.CompareTo(other.TotalDays);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            return CompareTo((AfaDate)obj);
        }

        public bool Equals(AfaDate other)
        {
            return CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            return Equals((AfaDate)obj);
        }

        public override int GetHashCode()
        {
            return TotalDays.GetHashCode();
        }

        static double GetFraction(double v)
        {
            double floor = Math.Floor(v) + (v < 0 ? 1 : 0);
            return v - floor;
        }

        public AfaDate AddDays(int days)
        {
            return CalculateDate(0, 0, days);
        }

        public AfaDate AddMonths(double months)
        {
            var days = GetFraction(months) * 30;
            return CalculateDate(0, (int)months, (int)days);
        }

        public AfaDate AddYears(double years)
        {
            var months = GetFraction(years) * 12;
            var days = GetFraction(months) * 30;
            return CalculateDate((int)years, (int)months, (int)days);
        }

        public AfaDate Add(TimeSpan timespan)
        {
            return AddDays((int)timespan.TotalDays);
        }

        public static TimeSpan operator -(AfaDate d1, AfaDate d2)
        {
            if (d1.Precision != d2.Precision)
                throw new InvalidOperationException("Both dates must be of the same precision!");
            return TimeSpan.FromDays(d1.TotalDays - d2.TotalDays);
        }

        public static AfaDate operator -(AfaDate d, TimeSpan t)
        {
            return d.AddDays(-(int)t.TotalDays);
        }

        public static AfaDate operator +(AfaDate d, TimeSpan t)
        {
            return d.Add(t);
        }

        public static bool operator ==(AfaDate d1, AfaDate d2)
        {
            return d1.CompareTo(d2) == 0;
        }

        public static bool operator >(AfaDate d1, AfaDate d2)
        {
            return d1.CompareTo(d2) > 0;
        }

        public static bool operator >=(AfaDate d1, AfaDate d2)
        {
            return d1.CompareTo(d2) >= 0;
        }

        public static bool operator !=(AfaDate d1, AfaDate d2)
        {
            return d1.CompareTo(d2) != 0;
        }

        public static bool operator <(AfaDate d1, AfaDate d2)
        {
            return d1.CompareTo(d2) < 0;
        }

        public static bool operator <=(AfaDate d1, AfaDate d2)
        {
            return d1.CompareTo(d2) <= 0;
        }

        public static implicit operator DateTime(AfaDate d)
        {
            return new DateTime(d.Year, d.Month, d.Day);
        }

        public static implicit operator DateTime?(AfaDate? d)
        {
            if (!d.HasValue)
                return null;
            return new DateTime(d.Value.Year, d.Value.Month, d.Value.Day);
        }

        public AfaDate Round(AfaDateRounding mode)
        {
            AfaDate result;
            switch (mode)
            {
                case AfaDateRounding.Day:
                    result = this;
                    break;
                case AfaDateRounding.Month:
                    result = new AfaDate(Year, Month, 1, Precision);
                    if (Day >= 15)
                        result = result.AddMonths(1);
                    break;
                case AfaDateRounding.BeginOfMonth:
                    result = new AfaDate(Year, Month, 1, Precision);
                    break;
                case AfaDateRounding.HalfYear:
                    if (Month >= 7)
                    {
                        result = new AfaDate(Year, 7, 1, Precision);
                    }
                    else
                    {
                        result = new AfaDate(Year, 1, 1, Precision);
                    }
                    break;
                case AfaDateRounding.BeginOfYear:
                    result = new AfaDate(Year, 1, 1, Precision);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return result;
        }
    }
}
