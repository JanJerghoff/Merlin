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
    public partial class UebersichtTouren : Form
    {
        public UebersichtTouren()
        {
            InitializeComponent();
        }

        public void fuellen()
        {            
            String basis = "SELECT idTour, Name FROM Tour ORDER BY idTour ASC";

            // Greift alle Fahrzeuge
            MySqlCommand cmdHisto = new MySqlCommand(basis, Program.conn2);
            MySqlDataReader rdrHisto;

            try
            {
                rdrHisto = cmdHisto.ExecuteReader();
                while (rdrHisto.Read())
                {
                    nummern.Add(rdrHisto.GetInt32(0));
                    textID.AppendText(rdrHisto.GetInt32(0) + "\r\n");
                    textName.AppendText(rdrHisto.GetString(1) + "\r\n");
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
