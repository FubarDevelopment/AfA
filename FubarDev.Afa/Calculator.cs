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

            var rounding = (abschreibung.GenauesDatum ? AfaDateRounding.Day : AfaDateRounding.HalfYear);
            var precision = AfaDatePrecision.Days30;

            var zugangsdatum = anlage.Anschaffungsdatum.ToAfaDate(precision).Round(rounding);
            var abgangsdatum = anlage.Abgangsdatum.ToAfaDate(precision).Round(rounding);

            var nutzungsdauer = (decimal)abschreibung.Nutzungsdauer.GetValueOrDefault();
            var anschaffungswert = anlage.Anschaffungswert;

            if (abschreibung.Typ != Entities.AfaTyp.Normal)
            {
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
                    abgangsdatum = abschreibung.SonderAfaDatum.Value.ToAfaDate(precision).Round(rounding);
                    break;
                case Entities.AfaTyp.Zuschreibung:
                    System.Diagnostics.Debug.Assert(abschreibung.SonderAfaDatum != null);
                    zugangsdatum = abschreibung.SonderAfaDatum.Value.ToAfaDate(precision).Round(rounding);
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
    }
}
