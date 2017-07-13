using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kartonagen
{
    public partial class UmzuegeUebersicht : Form
    {
        public UmzuegeUebersicht()
        {
            InitializeComponent();
        }

        private int idBearbeitend = 0; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.

        public void setBearbeiter(int wer)
        {
            idBearbeitend = wer;
        }

        // Füllt die letzten X Umzüge ein, geordnet nach Umzugsnummer
        public void letztenX(String cmd) {
            
            //Basisstring immer gleich, endung anhängen
            String basis = "SELECT u.Kunden_idKunden, u.idUmzuege, u.datBesichtigung, u.datUmzug, u.StraßeA, u.HausnummerA, u.OrtA,  u.StraßeB, u.HausnummerB, u.OrtB, k.Anrede, k.Vorname, k.Nachname FROM Umzuege u, Kunden k  WHERE u.Kunden_idKunden = k.idKunden ORDER BY ";
            String fin = basis + cmd;

            //Ale Felder leeren
            textKundenNr.Text = "";
            textUmzugsNr.Text = "";
            textBesichtigung.Text = "";
            textUmzug.Text = "";
            textAusStraße.Text = "";
            textAusOrt.Text = "";
            textEinStraße.Text = "";
            textEinOrt.Text = "";
            textName.Text = "";

            // Greift alle Umzugsdaten und Kundendaten per Join
            MySqlCommand cmdHisto = new MySqlCommand(fin, Program.conn);
            MySqlDataReader rdrHisto;

            try
            {
                rdrHisto = cmdHisto.ExecuteReader();
                while (rdrHisto.Read())
                {
                    
                    textKundenNr.Text += rdrHisto[0] + "\r\n";
                    textUmzugsNr.Text += rdrHisto[1] + "\r\n";
                    textBesichtigung.Text += rdrHisto.GetDateTime(2).ToShortDateString() + "\r\n";
                    textUmzug.Text += rdrHisto.GetDateTime(3).ToShortDateString() + "\r\n";
                    textAusStraße.Text += rdrHisto[4] +" "+ rdrHisto[5] + "\r\n";
                    textAusOrt.Text += rdrHisto[6] + "\r\n";
                    textEinStraße.Text += rdrHisto[7] + " " + rdrHisto[8] + "\r\n";
                    textEinOrt.Text += rdrHisto[9] + "\r\n";
                    textName.Text += rdrHisto[10] + " " + rdrHisto[11] + " " + rdrHisto[12] + "\r\n";
                }
                rdrHisto.Close();

            }
            catch (Exception sqlEx)
            {
                textName.AppendText(sqlEx.ToString());
                return;
            }
        }

        private void buttonHistorie_Click(object sender, EventArgs e)
        {
            letztenX("u.idUmzuege DESC LIMIT 50;");
        }

        private void buttonUmzugstermin_Click(object sender, EventArgs e)
        {
            letztenX("u.datUmzug DESC LIMIT 50;");
        }

        private void buttonBesichtigungstermin_Click(object sender, EventArgs e)
        {
            letztenX("u.datBesichtigung DESC LIMIT 50;");
        }
    }
}
