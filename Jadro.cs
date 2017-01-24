using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestGlad.Udalosti;

namespace TestGlad
{
    public class Jadro
    {
        public SortedList<TimeSpan, Udalost> KalendarUdalosti { get; set; }
        public bool Naplanova { get; set; }

        private readonly WebBrowser wb;
        private TimeSpan simCas;
        public bool SimulaciaBezi;

        public delegate void ZmenaKalendarUdalostiHandler();
        public event ZmenaKalendarUdalostiHandler ZmenaKalendarUdalosti;

        public Jadro(WebBrowser wb)
        {
            KalendarUdalosti = new SortedList<TimeSpan, Udalost>();
            this.wb = wb;
            SimulaciaBezi = false;
        }


        public void SpustSimulaciu()
        {
            simCas = new TimeSpan(0,0,0,0,0);
            KalendarUdalosti = new SortedList<TimeSpan, Udalost>();
            Naplanova = true;

            var simCasUdalosti = simCas + new TimeSpan(0, 0, 5);
            KalendarUdalosti.Add(simCasUdalosti, new KlikLogo(simCasUdalosti, wb));

            SpustBehSimulacie();

            if (ZmenaKalendarUdalosti != null) 
                ZmenaKalendarUdalosti();
        }

        private async void SpustBehSimulacie()
        {
            SimulaciaBezi = true;

            while (SimulaciaBezi)
            {
                Console.WriteLine(simCas + "  --  " + KalendarUdalosti.Values.Count);
                if (simCas.Equals(KalendarUdalosti.First().Value.CasSimulacie))
                {
                    KalendarUdalosti.First().Value.Vykonaj();
                    Naplanova = true;
                }

                simCas = simCas.Add(new TimeSpan(0,0,0,0,100));
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
            var predchAktivita = KalendarUdalosti.First().Value.TypAktivity;
            KalendarUdalosti.RemoveAt(0);

            switch (predchAktivita)
            {
                case TypAktivityEnum.Logo:
                    NasledujucaPoKlikLogo();
                    break;
                case TypAktivityEnum.Kontakt:
                    NasledujucaPoKlikKontakt();
                    break;
                case TypAktivityEnum.Zlavy:
                    NasledujucaPoKlikZlavy();
                    break;
                case TypAktivityEnum.Refresh:
                    break;
            }

            Naplanova = false;

            if (ZmenaKalendarUdalosti != null) //vyvolani udalosti
                ZmenaKalendarUdalosti();
        }

        private void NasledujucaPoKlikZlavy()
        {
            var simCasUdalosti = simCas + new TimeSpan(0, 0, 5);
            KalendarUdalosti.Add(simCasUdalosti, new KlikKontakt(simCasUdalosti, wb));
        }

        private void NasledujucaPoKlikLogo()
        {
            var simCasUdalosti = simCas + new TimeSpan(0, 0, 25);
            KalendarUdalosti.Add(simCasUdalosti, new KlikZlavy(simCasUdalosti, wb));

            simCasUdalosti = simCas + new TimeSpan(0, 0, 5);
            KalendarUdalosti.Add(simCasUdalosti, new RefreshStranky(simCasUdalosti, wb));
        }

        private void NasledujucaPoKlikKontakt()
        {
            var simCasUdalosti = simCas + new TimeSpan(0, 0, 5);
            KalendarUdalosti.Add(simCasUdalosti, new KlikLogo(simCasUdalosti, wb));
        }
    }

}
