using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa
{
    public struct AfaDate : IComparable<AfaDate>, IComparable, IEquatable<AfaDate>
    {
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public int DayOfYear
        {
            get
            {
                return (Month - 1) * 30 + Day;
            }
        }

        public int TotalDays
        {
            get
            {
                return Year * 360 + DayOfYear;
            }
        }

        public AfaDatePrecision Precision { get; private set; }

        public AfaDate(DateTime dt, AfaDatePrecision precision)
            : this(dt.Year, dt.Month, dt.Day, precision)
        {
        }

        public AfaDate(int year, int month, int day, AfaDatePrecision precision)
            : this()
        {
            FixDate(ref year, ref month, ref day);

            Precision = precision;
            Year = year;
            Month = month;
            Day = day;
        }

        public static AfaDate GetEndOfMonth(int year, int month)
        {
            return new AfaDate(year, month, 30, AfaDatePrecision.Day);
        }

        public static AfaDate GetBeginOfMonth(int year, int month)
        {
            return new AfaDate(year, month, 1, AfaDatePrecision.Day);
        }

        private static void FixDate(ref int year, ref int month, ref int day)
        {
            if (day > 31)
                day = 30;
        }

        private static void CalculateDate(ref int year, ref int month, ref int day)
        {
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
        }

        public int CompareTo(AfaDate other)
        {
            if (this.Precision != other.Precision)
                throw new InvalidOperationException("Both dates must be of the same precision!");
            return this.ToNormalizedDate().TotalDays.CompareTo(other.ToNormalizedDate().TotalDays);
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
            var day = Day + days;
            var month = Month;
            var year = Year;
            CalculateDate(ref year, ref month, ref day);
            return new AfaDate(year, month, day, Precision);
        }

        public AfaDate AddMonths(double months)
        {
            var days = (int)(GetFraction(months) * 30);

            var day = Day + days;
            var month = Month + (int)months;
            var year = Year;
            CalculateDate(ref year, ref month, ref day);
            
            return new AfaDate(year, month, day, Precision);
        }

        public AfaDate AddYears(double years)
        {
            var months = GetFraction(years) * 12;
            var days = GetFraction(months) * 30;

            var day = Day + (int)days;
            var month = Month + (int)months;
            var year = Year + (int)years;
            CalculateDate(ref year, ref month, ref day);

            return new AfaDate(year, month, day, Precision);
        }

        public AfaDate Add(TimeSpan timespan)
        {
            return AddDays((int)timespan.TotalDays);
        }

        public static TimeSpan operator -(AfaDate d1, AfaDate d2)
        {
            if (d1.Precision != d2.Precision)
                throw new InvalidOperationException("Both dates must be of the same precision!");
            return TimeSpan.FromDays(d1.ToNormalizedDate().TotalDays - d2.ToNormalizedDate().TotalDays);
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

        public AfaDate ToNormalizedDate()
        {
            AfaDate result;
            switch (Precision)
            {
                case AfaDatePrecision.Day:
                    result = this;
                    break;
                case AfaDatePrecision.HalfMonth:
                    result = new AfaDate(Year, Month, 1, AfaDatePrecision.Day);
                    if (Day >= 15)
                        result = result.AddMonths(1);
                    break;
                case AfaDatePrecision.Month:
                    result = new AfaDate(Year, Month, 1, AfaDatePrecision.Day);
                    break;
                case AfaDatePrecision.HalfYear:
                    result = new AfaDate(Year, 1, 1, AfaDatePrecision.Day);
                    if (Month >= 7)
                        result = result.AddYears(1);
                    break;
                case AfaDatePrecision.Year:
                    result = new AfaDate(Year, 1, 1, AfaDatePrecision.Day);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return result;
        }
    }
}
