using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa.Entities
{
    public enum Abschreibungsstatus
    {
        Zugang = 'Z',
        Abgang = 'A',
        Beides = 'B',
        Normal = 'N',
        Ignorieren = 'I',
        Weg = 'W',
    }
}
