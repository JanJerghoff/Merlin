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
    public partial class Erinnerungen : Form
    {

        List <AbstractAlert> gesamt = new List <AbstractAlert>();

        public Erinnerungen()
        {
            InitializeComponent();

            // Sammeln aller Erinnerungen

            gesamt = Transaktionen(gesamt);

        }

        private List<AbstractAlert> Transaktionen (List<AbstractAlert> gesamt)
        {
            //Abfrage
            MySqlCommand cmd = new MySqlCommand("SELECT t.idTransaktionen, t.Kartons, t.Flaschenkartons, t.Glaeserkartons, t.Kleiderkartons, t.timeTransaktion, t.datTransaktion, t.UserChanged, k.Anrede, k.Nachname, k.Straße, k.Hausnummer, k.Ort, k.PLZ, t.Bemerkungen FROM Transaktionen t, Kunden k WHERE t.final = 0 AND t.datTransaktion <= '" + Program.DateMachine(DateTime.Now)+"' AND t.Umzuege_Kunden_idKunden = k.idKunden", Program.conn2);
            MySqlDataReader rdr;
            try
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    gesamt.Add(new TransaktionAlert(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetInt32(4), (rdr.GetDateTime(6).ToShortDateString() + " " + rdr.GetDateTime(5).ToShortTimeString()), rdr.GetString(7), (rdr.GetString(10) + " " + rdr.GetString(11) + " " + rdr.GetString(13) + " " + rdr.GetString(12)), (rdr.GetString(8) + " " + rdr.GetString(9)), rdr.GetString(14));
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                return gesamt;
            }

            return gesamt;
        }
    }
}
