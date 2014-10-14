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

        public AfaDate(DateTime dt)
            : this(dt.Year, dt.Month, dt.Day)
        {
        }

        public AfaDate(int year, int month, int day)
            : this()
        {
            FixDate(ref year, ref month, ref day);

            Year = year;
            Month = month;
            Day = day;
        }

        private static void FixDate(ref int year, ref int month, ref int day)
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
            return TotalDays.CompareTo(other.TotalDays);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            return CompareTo((AfaDate)obj);
        }

        public bool Equals(AfaDate other)
        {
            return TotalDays == other.TotalDays;
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
            return new AfaDate(Year, Month, Day + days);
        }

        public AfaDate AddMonths(double months)
        {
            var days = GetFraction(months) * 30;
            return new AfaDate(Year, Month + (int)months, Day + (int)days);
        }

        public AfaDate AddYears(double years)
        {
            var months = GetFraction(years) * 12;
            var days = GetFraction(months) * 30;
            return new AfaDate(Year + (int)years, Month + (int)months, Day + (int)days);
        }

        public AfaDate Add(TimeSpan timespan)
        {
            return AddDays((int)timespan.TotalDays);
        }

        public static TimeSpan operator -(AfaDate d1, AfaDate d2)
        {
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
    }
}
