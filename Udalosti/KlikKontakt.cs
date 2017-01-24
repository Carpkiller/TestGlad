using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGlad.Udalosti
{
    public class KlikKontakt : Udalost
    {
        public KlikKontakt(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = 2;
        }
        public override void Vykonaj()
        {
            var c = wb.Document.GetElementsByTagName("a");
            foreach (HtmlElement htmlElement in c)
            {
                if (htmlElement.InnerText == "Kontakt")
                {
                    htmlElement.InvokeMember("Click");
                }
            }
        }
    }
}
