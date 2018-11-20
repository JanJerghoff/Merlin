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
        }

        

        public void abfrage(String cmd)
        {

            //Basisstring immer gleich, endung anhängen
            String basis = "SELECT u.Kunden_idKunden, u.idUmzuege, t.idTransaktionen, k.Anrede, k.Vorname, k.Nachname, t.datTransaktion, t.Kartons, t.Flaschenkartons, t.Glaeserkartons, t.Kleiderkartons FROM Umzuege u, Kunden k, Transaktionen t  WHERE u.Kunden_idKunden = k.idKunden AND t.Umzuege_idUmzuege = u.idUmzuege ORDER BY ";
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

                    Object[] rowtemp = { rdrHisto.GetInt32(0), rdrHisto.GetInt32(1), rdrHisto.GetInt32(2), rdrHisto.GetDateTime(6).ToShortDateString(), rdrHisto.GetString(3) + " " + rdrHisto.GetString(4) + " " + rdrHisto.GetString(5), rdrHisto.GetInt32(7), rdrHisto.GetInt32(8), rdrHisto.GetInt32(9), rdrHisto.GetInt32(10)};
                    Console.WriteLine("Line Kundennummer " + rdrHisto.GetInt32(0));
                    dataGridausstehendeKartonagen.Rows.Add(rowtemp);
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

        private void buttonHistorieUmzNr_Click(object sender, EventArgs e)
        {
            abfrage("t.idTransaktionen DESC LIMIT 100;");
        }
    }
}
