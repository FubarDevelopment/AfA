using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa.Entities
{
    public class Anlage
    {
        public Anlage(string konto, int aktivesJahr)
        {
            Anzahl = 1;
            Anschaffungsdatum = DateTime.Now.Date;
            Abschreibungen = new Dictionary<int, Abschreibung>();
            Konto = konto;
            AktivesJahr = aktivesJahr;
        }

        protected Anlage()
        {
        }

        public virtual string Konto { get; set; }
        public virtual string Text1 { get; set; }
        public virtual string Text2 { get; set; }
        public virtual int Anzahl { get; set; }
        public virtual string Kostenstelle { get; set; }
        public virtual decimal Anschaffungswert { get; set; }
        public virtual DateTime Anschaffungsdatum { get; set; }
        public virtual DateTime? Abgangsdatum { get; set; }
        public virtual int? Zuordnung { get; set; }
        public virtual int AktivesJahr { get; set; }

        public virtual IDictionary<int, Abschreibung> Abschreibungen { get; set; }

        public virtual Abschreibung AktuelleAbschreibung
        {
            get
            {
                if (!Abschreibungen.ContainsKey(AktivesJahr))
                {
                    if (!string.IsNullOrEmpty(Konto))
                        Abschreibungen.Add(AktivesJahr, new Abschreibung(Konto, AktivesJahr, LetzteAbschreibung));
                }
                return Abschreibungen[AktivesJahr];
            }
        }

        public virtual Abschreibung LetzteAbschreibung
        {
            get
            {
                return Abschreibungen.Values.Where(x => x.Jahr < AktivesJahr).OrderByDescending(x => x.Jahr).FirstOrDefault();
            }
        }
    }
}
