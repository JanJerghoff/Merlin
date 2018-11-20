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
            String basis = "SELECT u.Kunden_idKunden, u.idUmzuege, u.datBesichtigung, u.datUmzug, u.StraßeA, u.HausnummerA, u.OrtA,  u.StraßeB, u.HausnummerB, u.OrtB, k.Anrede, k.Vorname, k.Nachname, k.Email, k.Telefonnummer, k.Handynummer FROM Umzuege u, Kunden k  WHERE u.Kunden_idKunden = k.idKunden ORDER BY ";
            String fin = basis + cmd;
            
            // Greift alle Umzugsdaten und Kundendaten per Join
            
            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdHisto = new MySqlCommand(fin, Program.conn);
                MySqlDataReader rdrHisto = cmdHisto.ExecuteReader();
                while (rdrHisto.Read())
                {
                    String tempTelefon = "";

                    if (rdrHisto.GetString(13) == "0")
                    {
                        tempTelefon = rdrHisto.GetString(12);
                    }
                    else {
                        tempTelefon = rdrHisto.GetString(13);
                    }

                    Object[] rowtemp = { rdrHisto.GetInt32(0),rdrHisto.GetInt32(1), rdrHisto.GetString(10)+" "+ rdrHisto.GetString(11) + " " + rdrHisto.GetString(12), tempTelefon, rdrHisto.GetString(13), rdrHisto.GetDateTime(2).ToShortDateString(), rdrHisto.GetDateTime(3).ToShortDateString(), rdrHisto.GetString(4)+" "+rdrHisto.GetString(5), rdrHisto.GetString(6), rdrHisto.GetString(7) + " " + rdrHisto.GetString(8), rdrHisto.GetString(9) };
                    Console.WriteLine("Line Kundennummer "+rdrHisto.GetInt32(0));
                    DataGridUmzugsuebersicht.Rows.Add(rowtemp);
                }
                rdrHisto.Close();
                Program.conn.Close();

            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der Historie aus der Datenbank");
                return;
            }
        }

        private void buttonHistorie_Click(object sender, EventArgs e)
        {
            letztenX("u.idUmzuege DESC LIMIT 100;");
        }

        private void buttonUmzugstermin_Click(object sender, EventArgs e)
        {
            letztenX("u.datUmzug DESC LIMIT 100;");
        }

        private void buttonBesichtigungstermin_Click(object sender, EventArgs e)
        {
            letztenX("u.datBesichtigung DESC LIMIT 100;");
        }
    }
}
