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
        }

        int seite = 0;

        public void fuellen()
        {
            String basis = "SELECT f.idFahrt, f.Start, f.Tour_idTour, f.Mitarbeiter_idMitarbeiter, t.Name, m.Vorname, m.Nachname FROM Fahrt f, Tour t, Mitarbeiter m ORDER BY";

            // Greift alle Fahrten
            MySqlCommand cmdHisto = new MySqlCommand(basis, Program.conn2);
            MySqlDataReader rdrHisto;

            int count = 0;

            try
            {
                rdrHisto = cmdHisto.ExecuteReader();
                while (rdrHisto.Read())
                {
                    textID.AppendText(rdrHisto.GetInt32(0) + "\r\n");
                    textDatum.AppendText(rdrHisto.GetString(1) + "\r\n");

                    switch (rdrHisto.GetInt32(2))
                    {
                        case 0:
                            textMitarbeiter.AppendText(" Umzug \r\n");
                            break;
                        case 1:
                            textMitarbeiter.AppendText(" Kundenzahl \r\n");
                            break;
                        case 2:
                            textMitarbeiter.AppendText(" Stückzahl \r\n");
                            break;
                        case 3:
                            textMitarbeiter.AppendText(" Buero / Basisfahrt \r\n");
                            break;

                        default:
                            break;
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
