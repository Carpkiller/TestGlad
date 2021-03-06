﻿using System;
using System.Windows.Forms;

namespace TestGlad
{
    public partial class Form1 : Form
    {
        private readonly Jadro _jadro;
        public Form1()
        {
            InitializeComponent();
            _jadro = new Jadro(webBrowser1);
            _jadro.ZmenaKalendarUdalosti += AktualizujKalendarUdalosti;
        }

        private void AktualizujKalendarUdalosti()
        {
            textBox1.Text = "";

            foreach (var udalost in _jadro.KalendarUdalosti.Values)
            {
                textBox1.AppendText(string.Format("{0} - {1}{2}", udalost.CasSimulacie, udalost.TypAktivity, Environment.NewLine));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _jadro.SpustSimulaciu();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (_jadro.SimulaciaBezi && _jadro.Naplanova)
            {
                _jadro.NaplanujDalsiuAktivitu();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _jadro.SimulaciaBezi = false;
        }
    }
}
