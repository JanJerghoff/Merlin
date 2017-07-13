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
    public partial class KundenUebersicht : Form
    {
        public KundenUebersicht()
        {
            InitializeComponent();
        }

        private int idBearbeitend = 0; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.

        public void setBearbeiter(int wer)
        {
            idBearbeitend = wer;
        }

        public void letztenX() {
            MySqlCommand cmdRead = new MySqlCommand("SELECT Anrede, Vorname, Nachname, Handynummer, Email, Straße, Hausnummer, Ort, idKunden FROM Kunden ORDER BY idKunden DESC LIMIT 50;", Program.conn);
            MySqlDataReader rdr;
            
            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    textAnrede.Text += rdr[0] + "\r\n";
                    textVorname.Text += rdr[1] + "\r\n";
                    textNachname.Text += rdr[2] + "\r\n";
                    textHandyNr.Text += rdr[3] + "\r\n";
                    textEmail.Text += rdr[4] + "\r\n";
                    textStraße.Text += rdr[5] + " " + rdr[6] + "\r\n";
                    textOrt.Text += rdr[7] + "\r\n";
                    textKundenNr.Text += rdr[8] + "\r\n";
                }
                rdr.Close();
               
            }
            catch (Exception sqlEx)
            {
                textNachname.Text += sqlEx.ToString();
                return;
            }
        }

        private void buttonHistorie_Click(object sender, EventArgs e)
        {
            letztenX();
        }
    }
}
