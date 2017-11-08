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

namespace Mitarbeiter
{
    public partial class UebersichtFahrzeuge : Form
    {
        public UebersichtFahrzeuge()
        {
            InitializeComponent();
            fuellen();
        }

        public void fuellen() {

            List<int> nummern = new List<int>();
            String basis = "SELECT idFahrzeug, Name FROM Fahrzeug ORDER BY idFahrzeug ASC";

            // Greift alle Fahrzeuge
            MySqlCommand cmdHisto = new MySqlCommand(basis, Program.conn);
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

            foreach (var item in nummern)
            {
                string suche = "SELECT EndKM FROM Fahrt WHERE Fahrzeug_idFahrzeug = " + item + " ORDER BY EndKM DESC;";

                MySqlCommand cmdKM = new MySqlCommand(basis, Program.conn);
                MySqlDataReader rdrKM;
                try
                {
                    rdrKM = cmdKM.ExecuteReader();
                    while (rdrKM.Read())
                    {
                        textKM.AppendText(rdrKM.GetInt32(0) + "\r\n");
                        break;
                    }
                    rdrKM.Close();

                }
                catch (Exception sqlEx)
                {
                    // TODO Bugreporting
                    return;
                }
            }

        }
    }
}
