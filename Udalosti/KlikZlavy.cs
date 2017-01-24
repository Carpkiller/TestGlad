using System;
using System.Windows.Forms;

namespace TestGlad.Udalosti
{
    public class KlikZlavy : Udalost
    {

        public KlikZlavy(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = 3;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementsByTagName("a");
            foreach (HtmlElement htmlElement in c)
            {
                if (htmlElement.InnerText == "Nové zľavy")
                {
                    htmlElement.InvokeMember("Click");
                }
            }
        }
    }
}
