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
    public partial class TransaktionenUebersicht : Form
    {
        public TransaktionenUebersicht()
        {
            InitializeComponent();
            abfrage(false);
            textSeite.Text = "1";
        }
        int page = 0;

        private void abfrage(Boolean alt) { 

            int[] liste = new int[60];

            MySqlCommand cmdRead = new MySqlCommand("SELECT DISTINCT u.idUmzuege FROM Umzuege u, Transaktionen t WHERE u.idUmzuege = t.Umzuege_idUmzuege AND u.datUmzug != '2017-01-01' AND (t.Kartons != 0 OR t.FlaschenKartons != 0 OR t.KleiderKartons != 0 OR t.GlaeserKartons != 0) ORDER BY u.datUmzug ASC;", Program.conn);
            //Trigger nur alte Transaktionen
            if (alt)
            {
                DateTime vgl = DateTime.Now.AddMonths(-11);
                cmdRead = new MySqlCommand("SELECT DISTINCT u.idUmzuege FROM Umzuege u, Transaktionen t WHERE u.idUmzuege = t.Umzuege_idUmzuege AND u.datUmzug != '2017-01-01' AND (t.Kartons != 0 OR t.FlaschenKartons != 0 OR t.KleiderKartons != 0 OR t.GlaeserKartons != 0) AND t.datTransaktion < "+Program.DateMachine(vgl)+" ORDER BY u.datUmzug ASC;", Program.conn);
            }
            else {

                cmdRead = new MySqlCommand("SELECT DISTINCT u.idUmzuege FROM Umzuege u, Transaktionen t WHERE u.idUmzuege = t.Umzuege_idUmzuege AND u.datUmzug != '2017-01-01' AND (t.Kartons != 0 OR t.FlaschenKartons != 0 OR t.KleiderKartons != 0 OR t.GlaeserKartons != 0) ORDER BY u.datUmzug ASC;", Program.conn);
            }

            MySqlDataReader rdr;
            int counter = 0;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    //skip
                    if (counter < page * 60)
                    {

                    }
                    else {
                        liste[counter-page*60] = rdr.GetInt32(0);
                    }
                    
                    counter++;

                    if (counter >= page*60+60) {
                        break;
                    }

                }
                rdr.Close();

                
            }
            catch (Exception sqlEx)
            {
                textTransaktionLog.Text += sqlEx.ToString();
                return;
            }

            for (int i = 0; i < 60; i++)
            {                
                MySqlCommand cmdReadKonto = new MySqlCommand("SELECT Kartons, GlaeserKartons, FlaschenKartons, KleiderKartons FROM Transaktionen WHERE Umzuege_idUmzuege=" + liste[i] + " AND unbenutzt != 2;", Program.conn);
                MySqlDataReader rdrKonto;

                int Kartons = 0;
                int GlaeserKartons = 0;
                int FlaschenKartons = 0;
                int KleiderKartons = 0;

                try
                {
                    rdrKonto = cmdReadKonto.ExecuteReader();
                    while (rdrKonto.Read())
                    {
                        Kartons += rdrKonto.GetInt32(0);
                        GlaeserKartons += rdrKonto.GetInt32(1);
                        FlaschenKartons += rdrKonto.GetInt32(2);
                        KleiderKartons += rdrKonto.GetInt32(3);

                    }
                    rdrKonto.Close();

                    if (Kartons + GlaeserKartons + FlaschenKartons + KleiderKartons != 0)
                    {

                        textKartons.AppendText(Kartons.ToString() + "\r\n");
                        textGlaeser.AppendText(GlaeserKartons.ToString() + "\r\n");
                        textFlaschen.AppendText(FlaschenKartons.ToString() + "\r\n");
                        textKleider.AppendText(KleiderKartons.ToString() + "\r\n");
                        textUmzugsNr.AppendText(liste[i].ToString() + "\r\n");
                    }
                    else {
                        liste[i] = 0;
                    }

                }
                catch (Exception sqlEx)
                {
                    textTransaktionLog.Text += sqlEx.ToString();
                    return;
                }
            }

            for (int c = 0; c < 60; c++) {

                if (liste[c] != 0)
                {
                    MySqlCommand cmdReadKunde = new MySqlCommand("SELECT k.Anrede, k.Nachname, k. Handynummer, u.datUmzug, k.idKunden, u.StraßeB, u.HausnummerB, u.PLZB, u.OrtB FROM Kunden k, Umzuege u WHERE u.idUmzuege=" + liste[c] + " AND u.Kunden_idKunden = k.idKunden;", Program.conn);
                    MySqlDataReader rdrKunde;

                    try
                    {
                        rdrKunde = cmdReadKunde.ExecuteReader();

                        while (rdrKunde.Read())
                        {

                            textName.AppendText(rdrKunde.GetString(1) + ", " + rdrKunde.GetString(0) + "\r\n");
                            textKontakt.AppendText(rdrKunde[2] + "\r\n");
                            textDate.AppendText(rdrKunde.GetDateTime(3).ToShortDateString() + "\r\n");
                            textKundenNr.AppendText(rdrKunde[4] + "\r\n");
                            textAdresse.AppendText(rdrKunde[5] + " " + rdrKunde[6] + ", " + rdrKunde[7] + " " + rdrKunde[8] + "\r\n");
                        }

                        rdrKunde.Close();
                    }
                    catch (Exception sqlEx)
                    {
                        textTransaktionLog.Text += sqlEx.ToString();
                        return;
                    }
                }
            }
        }

        private void buttonSeiteZurueck_Click_1(object sender, EventArgs e)
        {
            if (page >= 1)
            {
                page--;
            }
            else { textTransaktionLog.AppendText("Erste Seite erreicht \r\n"); }

            clear();
            abfrage(false);

            textSeite.Text = (page + 1).ToString();
        }

        private void buttonSeiteVor_Click(object sender, EventArgs e)
        {
            page++;
            clear();

            abfrage(false);

            textSeite.Text = (page + 1).ToString();
        }

        private void buttonFaellig_Click(object sender, EventArgs e)
        {
            page = 0;

            textSeite.Text = (page + 1).ToString();

            clear();
            abfrage(true);
        }

        private void clear()
        {
            textKundenNr.Text = "";
            textUmzugsNr.Text = "";
            textName.Text = "";
            textKontakt.Text = "";
            textAdresse.Text = "";
            textKartons.Text = "";
            textGlaeser.Text = "";
            textFlaschen.Text = "";
            textKleider.Text = "";
            textDate.Text = "";

        }
                
    }
}
