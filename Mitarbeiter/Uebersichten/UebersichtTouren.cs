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
            fuellen();
        }

        public void fuellen()
        {            
            String basis = "SELECT idTour, Name, Type FROM Tour ORDER BY idTour ASC";

            // Greift alle Fahrzeuge
            MySqlCommand cmdHisto = new MySqlCommand(basis, Program.conn2);
            MySqlDataReader rdrHisto;

            try
            {
                rdrHisto = cmdHisto.ExecuteReader();
                while (rdrHisto.Read())
                {                    
                    textID.AppendText(rdrHisto.GetInt32(0) + "\r\n");
                    textName.AppendText(rdrHisto.GetString(1) + "\r\n");

                    switch (rdrHisto.GetInt32(2))
                    {
                        case 0:
                            textTyp.AppendText( " Umzug \r\n");
                            break;
                        case 1:
                            textTyp.AppendText(" Kundenzahl \r\n");
                            break;
                        case 2:
                            textTyp.AppendText(" Stückzahl \r\n");
                            break;
                        case 3:
                            textTyp.AppendText(" Buero / Basisfahrt \r\n");
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
