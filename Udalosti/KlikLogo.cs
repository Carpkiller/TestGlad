using System;
using System.Windows.Forms;

namespace TestGlad.Udalosti
{
    public class KlikLogo : Udalost
    {
        public KlikLogo(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = 1;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementsByTagName("a");
            foreach (HtmlElement htmlElement in c)
            {
                if (htmlElement.GetAttribute("id") == "logo")
                {
                    htmlElement.InvokeMember("Click");
                }
            }
        }
    }
}
