using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa
{
    public class Calculator
    {
        public int Year { get; private set; }
        public AfaDate StartDate { get; private set; }
        public AfaDate EndDate { get; private set; }

        public EventHandler<GwgAccountEventArgs> GwgAccount;

        public Calculator(int year)
        {
            Year = year;
            StartDate = AfaDate.GetBeginOfMonth(year, 1);
            EndDate = AfaDate.GetEndOfMonth(year, 12);
        }

        protected virtual bool OnGwgAccount(string account, bool isGwg)
        {
            var args = new GwgAccountEventArgs(account, isGwg);
            if (GwgAccount != null)
                GwgAccount(this, args);
            return args.IsGwg;
        }

        public void Calculate(Entities.Anlage anlage)
        {
            if (anlage.AktivesJahr != Year)
                throw new InvalidOperationException("Das aktive Jahr für die Anlage stimmt nicht mit dem für die Berechnung gewählten Jahr überein.");

            var abschreibung = anlage.AktuelleAbschreibung;

            // Hier kann man GwG für ein Konto erzwingen
            var abschreibungsart =
                (OnGwgAccount(anlage.Konto, abschreibung.Abschreibungsart == Entities.Abschreibungsart.GWG)
                ? Entities.Abschreibungsart.GWG
                : abschreibung.Abschreibungsart);

            var precision = (abschreibung.GenauesDatum ? AfaDateRounding.Day : AfaDateRounding.HalfYear);

            var zugangsdatum = anlage.Anschaffungsdatum.ToAfaDate(precision);
            var abgangsdatum = anlage.Abgangsdatum.ToAfaDate(precision);

            var nutzungsdauer = (decimal)abschreibung.Nutzungsdauer.GetValueOrDefault();
            var anschaffungswert = anlage.Anschaffungswert;

            if (abschreibung.Typ != Entities.AfaTyp.Normal)
            {
                zugangsdatum = NormalizeDate(abschreibung, zugangsdatum);
                var daysSinceBuy = StartDate - zugangsdatum;
                var remainingDays = nutzungsdauer * 360 - daysSinceBuy.Days;
                nutzungsdauer = remainingDays / 360;

                System.Diagnostics.Debug.Assert(abschreibung.SonderAfaBetrag != null);

                anschaffungswert = abschreibung.SonderAfaBetrag.GetValueOrDefault();
            }
            switch (abschreibung.Typ)
            {
                case Entities.AfaTyp.Abschreibung:
                    System.Diagnostics.Debug.Assert(abschreibung.SonderAfaDatum != null);
                    zugangsdatum = AfaDate.GetEndOfMonth(Year - 1, 12);
                    abgangsdatum = abschreibung.SonderAfaDatum.GetValueOrDefault().ToAfaDate(precision);
                    break;
                case Entities.AfaTyp.Zuschreibung:
                    System.Diagnostics.Debug.Assert(abschreibung.SonderAfaDatum != null);
                    zugangsdatum = abschreibung.SonderAfaDatum.GetValueOrDefault().ToAfaDate(precision);
                    abgangsdatum = AfaDate.GetBeginOfMonth(Year + 1, 1);
                    break;
            }

            var istZugang = zugangsdatum.Year == Year;
            var istAbgang = abgangsdatum != null && abgangsdatum.Value.Year == Year;

            if (nutzungsdauer != decimal.Zero)
            {
                var percentMax = abschreibung.AbschreibungProzent;
            }
        }

        private static AfaDate NormalizeDate(Entities.Abschreibung abschreibung, AfaDate date)
        {
            if (abschreibung.GenauesDatum)
                return date;
            return new AfaDate(date.Year, (date.Month < 6) ? 1 : 7, 1, AfaDateRounding.HalfYear);
        }
    }
}
