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

namespace Mitarbeiter.Uebersichten
{
    public partial class UebersichtMitarbeiter : Form
    {
        public UebersichtMitarbeiter()
        {
            InitializeComponent();
            fuellen();
        }

        public void fuellen()
        {
            String basis = "SELECT idMitarbeiter, Vorname, Nachname, StundenanteilMinuten FROM Mitarbeiter ORDER BY idMitarbeiter DESC";

            // Greift alle Mitarbeiter
            MySqlCommand cmdHisto = new MySqlCommand(basis, Program.conn2);
            MySqlDataReader rdrHisto;

            double stunden;

            try
            {
                rdrHisto = cmdHisto.ExecuteReader();
                while (rdrHisto.Read())
                {
                    textID.AppendText(rdrHisto.GetInt32(0) + "\r\n");
                    textName.AppendText(rdrHisto.GetString(1) +" "+ rdrHisto.GetString(2) + "\r\n");
                    stunden = (double) rdrHisto.GetInt32(3);
                    stunden = stunden / 60.0;
                    stunden = Math.Round(stunden, 2);
                    textStundenanteil.AppendText(stunden + "\r\n");

                }
                rdrHisto.Close();

            }
            catch (Exception sqlEx)
            {
                // TODO Bugreporting
                return;
            }

        }
    }
}
