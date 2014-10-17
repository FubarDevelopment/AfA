using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FubarDev.Afa
{
    public static class DateTimeExtensions
    {
        public static AfaDate ToAfaDate(this DateTime d, AfaDateRounding precision)
        {
            return new AfaDate(d, precision);
        }

        public static AfaDate? ToAfaDate(this DateTime? d, AfaDateRounding precision)
        {
            if (d == null)
                return null;
            return new AfaDate(d.Value, precision);
        }
    }
}
