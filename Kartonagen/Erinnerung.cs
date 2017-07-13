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
    public partial class Erinnerung : Form
    {
        public Erinnerung()
        {
            InitializeComponent();
        }

        private void next()
        {


        }

        private void getHVZUngebucht  ()
        {
            DateTime Stichtag = DateTime.Now.AddDays(14);

            MySqlCommand cmdRead = new MySqlCommand("SELECT u.idUmzuege, k.Nachname FROM Umzugsfortschritt m, Umzuege u, Kunden k WHERE m.Umzuege_idUmzuege = u.idUmzuege AND u.Kunden_idKunden = k.idKunden AND m.HVZAntrag = 8 AND m.BuchungFin != 8 AND u.datUmzug < '"+Program.DateMachine(Stichtag) +"';", Program.conn);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    var bestätigung = MessageBox.Show("Der Umzug Nr." + rdr[0] + " von " + rdr[1] + "\r\n hat keine HVZ angemeldet, findet in unter 2 Wochen statt");
                    if (bestätigung == DialogResult.Yes)
                    {
                        next();
                        break;
                    }
                    break;
                }
                rdr.Close();
            }
            catch (Exception sqlEx) { }


        }


        private void Erinnerung_Load(object sender, EventArgs e)
        {

        }
    }
}
