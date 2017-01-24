using System;
using System.Windows.Forms;

namespace TestGlad.Udalosti
{
    public class RefreshStranky : Udalost
    {
        public RefreshStranky(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.Refresh;
        }

        public override void Vykonaj()
        {
            wb.Refresh(WebBrowserRefreshOption.Completely);
        }
    }
}