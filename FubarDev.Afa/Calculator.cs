using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FubarDev.Afa.DatePrecisions;

namespace FubarDev.Afa
{
    public class Calculator<T> where T : IAfaDatePrecisionHandler
    {
        private readonly T _handler;

        public int Year { get; private set; }
        public AfaDate<T> StartDate { get; private set; }
        public AfaDate<T> EndDate { get; private set; }

        public EventHandler<GwgAccountEventArgs> GwgAccount;

        public Calculator(int year, T handler)
        {
            _handler = handler;
            Year = year;
            StartDate = new AfaDate<T>(year, 1, 1, _handler);
            EndDate = new AfaDate<T>(year, 12, 1, _handler).EndOfMonth;
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
            var precision = AfaDatePrecision30.Default;

            var zugangsdatum = anlage.Anschaffungsdatum.ToAfaDate(precision).Round(rounding);
            var abgangsdatum = anlage.Abgangsdatum.ToAfaDate(precision).Round(rounding);

            var nutzungsdauer = (decimal)abschreibung.Nutzungsdauer.GetValueOrDefault();
            var anschaffungswert = anlage.Anschaffungswert;

            if (abschreibung.Typ != Entities.AfaTyp.Normal)
            {
                var daysSinceBuy = new AfaDate<AfaDatePrecision30>(Year, 1, 1, precision) - zugangsdatum;
                var remainingDays = nutzungsdauer * 360 - daysSinceBuy.Days;
                nutzungsdauer = remainingDays / 360;

                System.Diagnostics.Debug.Assert(abschreibung.SonderAfaBetrag != null);

                anschaffungswert = abschreibung.SonderAfaBetrag.GetValueOrDefault();
            }
            switch (abschreibung.Typ)
            {
                case Entities.AfaTyp.Abschreibung:
                    System.Diagnostics.Debug.Assert(abschreibung.SonderAfaDatum != null);
                    zugangsdatum = new AfaDate<AfaDatePrecision30>(Year - 1, 12, 1, precision).EndOfMonth;
                    abgangsdatum = abschreibung.SonderAfaDatum.Value.ToAfaDate(precision).Round(rounding);
                    break;
                case Entities.AfaTyp.Zuschreibung:
                    System.Diagnostics.Debug.Assert(abschreibung.SonderAfaDatum != null);
                    zugangsdatum = abschreibung.SonderAfaDatum.Value.ToAfaDate(precision).Round(rounding);
                    abgangsdatum = new AfaDate<AfaDatePrecision30>(Year + 1, 1, 1, precision);
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
