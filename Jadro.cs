using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestGlad.Udalosti;

namespace TestGlad
{
    public class Jadro
    {
        public SortedList<TimeSpan, Udalost> listUdalosti { get; set; }
        private WebBrowser _wb;
        private TimeSpan SimCas;
        public bool SimulaciaBezi;

        public delegate void ZmenaKalendarUdalostiHandler();
        public event ZmenaKalendarUdalostiHandler ZmenaKalendarUdalosti;

        public Jadro(WebBrowser wb)
        {
            listUdalosti = new SortedList<TimeSpan, Udalost>();
            _wb = wb;
            SimulaciaBezi = false;
        }


        public void SpustSimulaciu()
        {
            SimCas = new TimeSpan(0,0,0,0,0);
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 5);
            listUdalosti.Add(simCasUdalosti, new KlikLogo(simCasUdalosti, _wb));

            SpustBehSimulacie();

            if (ZmenaKalendarUdalosti != null) 
                ZmenaKalendarUdalosti();
        }

        private async void SpustBehSimulacie()
        {
            SimulaciaBezi = true;

            while (SimulaciaBezi)
            {
                Console.WriteLine(SimCas);
                if (SimCas.Equals(listUdalosti.First().Value.CasSimulacie))
                {
                    listUdalosti.First().Value.Vykonaj();
                }

                SimCas = SimCas.Add(new TimeSpan(0,0,0,0,100));
                await PutTaskDelay();
            }

            SimulaciaBezi = false;
        }

        async Task PutTaskDelay()
        {
            await Task.Delay(100);
        }

        public void NaplanujDalsiuAktivitu()
        {
            var predchAktivita = listUdalosti.First().Value.TypAktivity;
            listUdalosti.RemoveAt(0);

            switch (predchAktivita)
            {
                case 1:
                    NasledujucaPoKlikLogo();
                    break;
                case 2:
                    NasledujucaPoKlikKontakt();
                    break;
                case 3:
                    NasledujucaPoKlikZlavy();
                    break;
            }

            if (ZmenaKalendarUdalosti != null) //vyvolani udalosti
                ZmenaKalendarUdalosti();
        }

        private void NasledujucaPoKlikZlavy()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 5);
            listUdalosti.Add(simCasUdalosti, new KlikKontakt(simCasUdalosti, _wb));
        }

        private void NasledujucaPoKlikLogo()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 10);
            listUdalosti.Add(simCasUdalosti, new KlikZlavy(simCasUdalosti, _wb));
        }

        private void NasledujucaPoKlikKontakt()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 5);
            listUdalosti.Add(simCasUdalosti, new KlikLogo(simCasUdalosti, _wb));
        }
    }

}
