using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MFP4
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void wurzelBtn_Click(object sender, EventArgs e)
        {   
            thermTb.Text += "V";
        }

        protected void pot2Btn_Click(object sender, EventArgs e)
        {
            thermTb.Text += "^2";
        }

        protected void pot3Btn_Click(object sender, EventArgs e)
        {
            thermTb.Text += "^3";
        }

        protected void potxBtn_Click(object sender, EventArgs e)
        {
            thermTb.Text += "^";
        }

        protected void piBtn_Click(object sender, EventArgs e)
        {
            thermTb.Text += Math.PI.ToString();
        }

        protected void parseBtn_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            string Therm = thermTb.Text;
            Parser.Parser parser = new Parser.Parser();
            string[] Struktur = parser.Strukturieren(Therm);
            if (Struktur != null)
            {
                string Ergebnis = parser.Berechnen(Struktur);
                if (Ergebnis != null)
                {
                    ergebnisLbl.Text = Ergebnis;
                }
                else
                    ergebnisLbl.Text = "FEHLER!";
            }
            else
                ergebnisLbl.Text = "FEHLER!";
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}", ts.Milliseconds);
            timeLbl.Text = elapsedTime.ToString();
        }
    }
}