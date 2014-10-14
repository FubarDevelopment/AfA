using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa.Entities
{
    public class Einstellung
    {
        public Einstellung()
        {
        }

        public Einstellung(string schluessel, object wert)
        {
            Schluessel = schluessel;
            Wert = wert;
        }

        public virtual string Schluessel { get; set; }
        public virtual object Wert { get; set; }
    }
}
