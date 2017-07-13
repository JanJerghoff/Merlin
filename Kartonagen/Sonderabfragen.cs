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
    public partial class Sonderabfragen : Form
    {
        public Sonderabfragen()
        {
            InitializeComponent();
        }

        private int idBearbeitend = 0; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.

        public void setBearbeiter(int wer)
        {
            idBearbeitend = wer;
        }


        private void buttonBilanz_Click(object sender, EventArgs e)
        {

            // Berechnung Ausstand / Umlauf

            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Transaktionen;", Program.conn);
            MySqlDataReader rdr;

            int Kartons = 0;
            int GlaeserKartons = 0;
            int FlaschenKartons = 0;
            int KleiderKartons = 0;

            int KartonsLager = 0;
            int GlaeserKartonsLager = 0;
            int FlaschenKartonsLager = 0;
            int KleiderKartonsLager = 0;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    Kartons += rdr.GetInt32(2);
                    GlaeserKartons += rdr.GetInt32(4); // Achtung, Reihenfolge hier beachten!!
                    FlaschenKartons += rdr.GetInt32(3);
                    KleiderKartons += rdr.GetInt32(5);
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }

            textLog.AppendText("Es stehen insgesamt " + Kartons + " Kartons, " + GlaeserKartons + " Gläserkartons, " + FlaschenKartons + " Flaschenkartons und " + KleiderKartons + " Kleiderkartons aus. \r\n");

            // Berechnung / Ausgabe Lagerstand und Nachgekauftes
            // Im Lagerbestand sind Positive Zahlen vorhanden und Negative kaputt / entsorgt

            MySqlCommand cmdReadBest = new MySqlCommand("SELECT * FROM Lagerbestaende;", Program.conn);
            MySqlDataReader rdrBest;
            
            
            try
            {
                rdrBest = cmdReadBest.ExecuteReader();
                while (rdrBest.Read())
                {
                    KartonsLager += rdrBest.GetInt32(2);
                    GlaeserKartonsLager += rdrBest.GetInt32(3);
                    FlaschenKartonsLager += rdrBest.GetInt32(4);
                    KleiderKartonsLager += rdrBest.GetInt32(5);
                }
                rdrBest.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                //rdrBest.Close();
                return;
            }
            
            textLog.AppendText("Wir haben " + (KartonsLager-Kartons) + " Kartons, " + (GlaeserKartonsLager-GlaeserKartons) + " Gläserkartons, " + (FlaschenKartonsLager - FlaschenKartons) + " Flaschenkartons und " + (KleiderKartonsLager - KleiderKartons) + " Kleiderkartons im Lager. \r\n");
            
        }
    }
}
