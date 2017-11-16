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
    public partial class UebersichtFahrten : Form
    {
        public UebersichtFahrten()
        {
            InitializeComponent();
            fuellen(0);
        }

        int seite = 1;

        public void fuellen( int mode)
        {
            String basis = "SELECT f.idFahrt, f.Start, t.Name, m.Vorname, m.Nachname, f.Ende, f.Pause FROM Fahrt f, Tour t, Mitarbeiter m WHERE f.Tour_idTour = t.idTour AND f.Mitarbeiter_idMitarbeiter = m.idMitarbeiter ORDER BY";

            switch (mode)
            {
                case 0:
                    basis = basis + " f.idFahrt DESC;";
                    break;

                case 1:
                    basis = basis + " f.Start DESC;";
                    break;

                default:
                    break;
            }

            // Greift alle Fahrten
            MySqlCommand cmdHisto = new MySqlCommand(basis, Program.conn2);
            MySqlDataReader rdrHisto;

            int count = 0;

            try
            {
                rdrHisto = cmdHisto.ExecuteReader();
                while (rdrHisto.Read())
                {
                    if (count < (seite * 60) && count >= (seite * 60 - 60)) {

                        textID.AppendText(rdrHisto.GetInt32(0)+ "\r\n");
                        textDatum.AppendText(rdrHisto.GetDateTime(1).ToShortDateString() + "\r\n");
                        textTour.AppendText(rdrHisto.GetString(2) + "\r\n");
                        textMitarbeiter.AppendText(rdrHisto.GetString(3) + " " + rdrHisto.GetString(4) + "\r\n");
                        textDauer.AppendText(Math.Round((Program.ArbeitsZeitBlock(rdrHisto.GetDateTime(1), rdrHisto.GetDateTime(5), rdrHisto.GetInt32(6))/60.0),2) + "\r\n");

                        count++;
                    }
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
