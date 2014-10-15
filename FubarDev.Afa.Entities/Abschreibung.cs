using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa.Entities
{
    public class Abschreibung
    {
        public Abschreibung()
        {
            Abschreibungsart = Abschreibungsart.Linear;
            StatusFuerJahr = Abschreibungsstatus.Ignorieren;
            StatusProMonat = new Abschreibungsstatus[] {
                Abschreibungsstatus.Ignorieren,
                Abschreibungsstatus.Ignorieren,
                Abschreibungsstatus.Ignorieren,
                Abschreibungsstatus.Ignorieren,
                Abschreibungsstatus.Ignorieren,
                Abschreibungsstatus.Ignorieren,
                Abschreibungsstatus.Ignorieren,
                Abschreibungsstatus.Ignorieren,
                Abschreibungsstatus.Ignorieren,
                Abschreibungsstatus.Ignorieren,
                Abschreibungsstatus.Ignorieren,
                Abschreibungsstatus.Ignorieren,
            };
            Typ = AfaTyp.Normal;
        }

        public Abschreibung(string konto, int neuesJahr, Abschreibung vorherige)
            : this()
        {
            Jahr = neuesJahr;
            Konto = konto;
            if (vorherige != null)
            {
                GenauesDatum = vorherige.GenauesDatum;
                Nutzungsdauer = vorherige.Nutzungsdauer;
                Restbuchwert = Buchwert = vorherige.Restbuchwert;
                AfAAufgelaufen = vorherige.AfAAufgelaufen;
                Abschreibungsart = vorherige.Abschreibungsart;
            }
        }

        public virtual int Jahr { get; set; }
        public virtual string Konto { get; set; }
        public virtual Abschreibungsart Abschreibungsart { get; set; }
        public virtual bool GenauesDatum { get; set; }
        public virtual int? Nutzungsdauer { get; set; }
        public virtual decimal? AbschreibungProzent { get; set; }
        public virtual decimal Buchwert { get; set; }
        public virtual decimal Reserviert1 { get; set; }
        public virtual decimal Restbuchwert { get; set; }
        public virtual decimal AfALaufendesJahr { get; set; }
        public virtual decimal AfAAufgelaufen { get; set; }
        public virtual decimal AfAMonat1 { get; set; }
        public virtual decimal AfAMonat2 { get; set; }
        public virtual decimal AfAMonat3 { get; set; }
        public virtual decimal AfAMonat4 { get; set; }
        public virtual decimal AfAMonat5 { get; set; }
        public virtual decimal AfAMonat6 { get; set; }
        public virtual decimal AfAMonat7 { get; set; }
        public virtual decimal AfAMonat8 { get; set; }
        public virtual decimal AfAMonat9 { get; set; }
        public virtual decimal AfAMonat10 { get; set; }
        public virtual decimal AfAMonat11 { get; set; }
        public virtual decimal AfAMonat12 { get; set; }
        public virtual Abschreibungsstatus StatusFuerJahr { get; set; }
        public virtual Abschreibungsstatus[] StatusProMonat { get; set; }
        public virtual AfaTyp Typ { get; set; }
        public virtual decimal? SonderAfaBetrag { get; set; }
        public virtual DateTime? SonderAfaDatum { get; set; }
        public virtual string Reserviert2 { get; set; }

        public override bool Equals(object obj)
        {
            var other = (Abschreibung)obj;
            return string.Equals(Konto, other.Konto) && Jahr == other.Jahr;
        }

        public override int GetHashCode()
        {
            return Jahr.GetHashCode() ^ (Konto ?? string.Empty).GetHashCode();
        }
    }
}
