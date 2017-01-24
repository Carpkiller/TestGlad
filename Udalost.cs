using System;
using System.Windows.Forms;

namespace TestGlad
{
    public abstract class Udalost
    {
        public TimeSpan CasSimulacie { get; set; }
        protected WebBrowser wb;
        public int TypAktivity { get; set; }

        public abstract void Vykonaj();
    }
}
