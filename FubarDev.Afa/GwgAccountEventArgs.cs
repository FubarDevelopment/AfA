using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa
{
    public class GwgAccountEventArgs : EventArgs
    {
        public GwgAccountEventArgs(string account, bool isGwg)
        {
            Account = account;
            IsGwg = isGwg;
        }

        public string Account { get; private set; }
        public bool IsGwg { get; set; }
    }
}
