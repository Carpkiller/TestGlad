﻿using System;
using System.Windows.Forms;

namespace TestGlad
{
    public abstract class Udalost
    {
        public TimeSpan CasSimulacie { get; set; }
        protected WebBrowser wb;
        public TypAktivityEnum TypAktivity { get; set; }

        public abstract void Vykonaj();

        public override string ToString()
        {
            return TypAktivity.ToString();
        }
    }
}
