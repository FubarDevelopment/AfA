using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa
{
    public class Calculator
    {
        public int Year { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Calculator(int year)
        {
            Year = year;
            StartDate = new DateTime(year, 1, 1);
            EndDate = new DateTime(year, 12, 31);
        }

        public void Calculate(Entities.Abschreibung item)
        {

        }
    }
}
