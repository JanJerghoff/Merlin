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
    public partial class TransaktionenSearch : Form
    {
        int maxTransaktionsnummer;
        String Userchanged;

        public TransaktionenSearch()
        {
            InitializeComponent();

            //Maximale Transaktionsnummer um OutOfBounds vorzubeugen

            MySqlCommand cmdReadTrans = new MySqlCommand("SELECT * FROM Transaktionen ORDER BY idTransaktionen DESC LIMIT 1;", Program.conn);
            MySqlDataReader rdrTrans;

            try
            {
                rdrTrans = cmdReadTrans.ExecuteReader();
                while (rdrTrans.Read())
                {
                    maxTransaktionsnummer = rdrTrans.GetInt32(0);
                }
                rdrTrans.Close();
            }
            catch (Exception sqlEx)
            {
                textTransaktionLog.Text += sqlEx.ToString();
                return;
            }
        }

        private int idBearbeitend = 0; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.

        public void setBearbeiter(int wer)
        {
            idBearbeitend = wer;
        }


        public void fuellen(int TransNummer)
        {

            // Füllen des Transaktionsnummer-Feldes, wenn nicht 1
            if (numericTransaktionsnummer.Value != 1)
            {
                textTransaktion.Text = TransNummer.ToString();
            }
            else
            {
                textTransaktionLog.AppendText("1 ist keine gültige Transaktion \r\n");
            }
            // Beschaffen der Umzugsnummer zur gegebenen Transaktionsnummer
            // Füllen der Transaktion (alt wie geändert) aus der Transaktion
            int umzNummer = 0;
            MySqlCommand cmdReadTrans = new MySqlCommand("SELECT * FROM Transaktionen WHERE idTransaktionen = " + TransNummer + ";", Program.conn);
            MySqlDataReader rdrTrans;

            //Break wenn Transaktionsnummer ungültig
            if (TransNummer > maxTransaktionsnummer) {
                textTransaktionLog.AppendText("Transaktionsnummer nicht vergeben, bitte überprüfen \r\n");
                return;
            }

            try
            {
                rdrTrans = cmdReadTrans.ExecuteReader();
                while (rdrTrans.Read())
                {
                    umzNummer = rdrTrans.GetInt32(6);
                    // Check positive Werte für Kartons = Auslieferung
                    if (rdrTrans.GetInt32(2) > 0 || rdrTrans.GetInt32(3) > 0 || rdrTrans.GetInt32(4) > 0 || rdrTrans.GetInt32(5) > 0)
                    {
                        radioAusgang.Checked = true;
                        radioAusgangAendern.Checked = true;
                    }
                    // Check benutzt
                    else
                    {
                        radioEingang.Checked = true;
                        radioEingangAendern.Checked = true;
                        if (rdrTrans.GetInt32(11)!=0)
                        {
                            radioUnbenutzt.Checked = true;
                            radioUnbenutztAendern.Checked = true;
                        }
                    }
                    // Einfüllen Zahlenwerte ohne Vorzeichen
                    numericKarton.Value = Math.Abs(rdrTrans.GetInt32(2));
                    numericKartonAendern.Value = Math.Abs(rdrTrans.GetInt32(2));
                    numericFlaschenKarton.Value = Math.Abs(rdrTrans.GetInt32(3));
                    numericFlaschenKartonAendern.Value = Math.Abs(rdrTrans.GetInt32(3));
                    numericGlaeserkarton.Value = Math.Abs(rdrTrans.GetInt32(4));
                    numericGlaeserKartonAendern.Value = Math.Abs(rdrTrans.GetInt32(4));
                    numericKleiderKarton.Value = Math.Abs(rdrTrans.GetInt32(5));
                    numericKleiderKartonAendern.Value = Math.Abs(rdrTrans.GetInt32(5));

                    // Einfüllen Datum
                    dateTimeTransaktion.Value = rdrTrans.GetDateTime(1);
                    dateTimeTransaktionAendern.Value = rdrTrans.GetDateTime(1);

                    // Bemerkungen einfüllen
                    textBemerkung.Text = rdrTrans[8].ToString();
                    textBemerkungAendern.Text = rdrTrans[8].ToString();

                    // Rechnungsnummer einfüllen
                    textRechnungsnummer.Text = rdrTrans[12].ToString();
                    textRechnungsnummerAendern.Text = rdrTrans[12].ToString();
                    // UserChanged merken
                    Userchanged = rdrTrans[9].ToString();

                    textTransaktionLog.AppendText("Transaktion erfolgreich aufgerufen \r\n");
                }
                rdrTrans.Close();
            }
            catch (Exception sqlEx)
            {
                textTransaktionLog.Text += sqlEx.ToString();
                return;
            }

            // Umzugsdaten aus dem Umzug ziehen

            MySqlCommand cmdReadUmzug = new MySqlCommand("SELECT Kunden_idKunden, datUmzug FROM Umzuege WHERE idUmzuege=" + umzNummer + ";", Program.conn);
            MySqlDataReader rdrUmzug;

            try
            {
                rdrUmzug = cmdReadUmzug.ExecuteReader();
                while (rdrUmzug.Read())
                {
                    textKundennummer.Text = rdrUmzug[0] + "";
                    textUmzugsdatum.Text = rdrUmzug.GetDateTime(1).ToShortDateString();
                    textUmzugsnummer.Text = umzNummer + "";
                }
                rdrUmzug.Close();
            }
            catch (Exception sqlEx)
            {
                textTransaktionLog.Text += sqlEx.ToString();
                return;
            }

            // Personendaten aus dem Kunden ziehen

            MySqlCommand cmdReadKunde = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden=" + textKundennummer.Text + ";", Program.conn);
            MySqlDataReader rdrKunde;

            try
            {
                rdrKunde = cmdReadKunde.ExecuteReader();
                while (rdrKunde.Read())
                {

                    textVorNachname.Text = rdrKunde[1] + " " + rdrKunde[2] + " " + rdrKunde[3];
                }
                rdrKunde.Close();
            }
            catch (Exception sqlEx)
            {
                textTransaktionLog.Text += sqlEx.ToString();
                return;
            }
        }

        //
        //
        //


        private void buttonAbsenden_Click(object sender, EventArgs e)
        {
            
            
            //String bauen (get it? eh?)
            String push = "UPDATE Transaktionen SET ";
            String shove = " WHERE idTransaktionen = " + textTransaktion.Text + ";"; 

            push += "datTransaktion = '" + Program.DateMachine(dateTimeTransaktionAendern.Value) + "', ";

            if (radioAusgangAendern.Checked)
            {
                push += "Kartons = " + numericKartonAendern.Value + ", ";
                push += "FlaschenKartons = " + numericFlaschenKartonAendern.Value + ", ";
                push += "GlaeserKartons = " + numericGlaeserKartonAendern.Value + ", ";
                push += "KleiderKartons = " + numericKleiderKartonAendern.Value + ", ";
            }
            else
            {
                push += "Kartons = -" + numericKartonAendern.Value + ", ";
                push += "FlaschenKartons = -" + numericFlaschenKartonAendern.Value + ", ";
                push += "GlaeserKartons = -" + numericGlaeserKartonAendern.Value + ", ";
                push += "KleiderKartons = -" + numericKleiderKartonAendern.Value + ", ";
            }


            //push += "Umzuege_idUmzuege = "+ textUmzugsnummer.Text + ", ";
            //push += "Umzuege_Kunden_idKunden = " + textKundennummer.Text + ", ";
            push += "Bemerkungen = '"  + textBemerkungAendern.Text + " ', ";
            push += "Erstelldatum = '" + Program.DateMachine(DateTime.Now) + "', ";

            push += "UserChanged = '" + Userchanged + idBearbeitend + "', ";

            // Rechnungsnummer

            push += "RechnungsNr = '" + textRechnungsnummerAendern.Text + "', ";
            // Kartons unbenútzt zurück?
            if (radioUnbenutztAendern.Checked && radioEingangAendern.Checked)
            {
                push += "unbenutzt = 1 ";
            }
            else
            {
                push += "unbenutzt = 0 ";
            }

            MySqlCommand cmdAdd = new MySqlCommand(push+shove, Program.conn);
            try
            {

                cmdAdd.ExecuteNonQuery();
                textTransaktionLog.AppendText("Erfolgreich gespeichert.\r\n");
            }
            catch (Exception sqlEx)
            {
                textTransaktionLog.Text += sqlEx.ToString() + "\r\n";
                return;
            }
                        
            //Neu Fuellen
            fuellen(int.Parse(textTransaktion.Text));
            
        }

        private void buttonUmzugsNrSuche_Click(object sender, EventArgs e)
        {
            fuellen(decimal.ToInt32(numericTransaktionsnummer.Value));
        }

        private void buttonLoeschen_Click(object sender, EventArgs e)
        {
            var bestätigung = MessageBox.Show("Die Transaktion wirklich löschen?", "Löschen bestätigen", MessageBoxButtons.YesNo);
            if (bestätigung == DialogResult.Yes)

            //Erst Transaktionen zum Umzug, dann Umzüge selbst löschen.
            {
                
                String delete = "DELETE FROM Transaktionen WHERE idTransaktionen = " + textTransaktion.Text + " ;";
                MySqlCommand cmdSend = new MySqlCommand(delete, Program.conn);
                try
                {
                    cmdSend.ExecuteNonQuery();
                    textTransaktionLog.AppendText("Transaktion erfolgreich gelöscht\r\n");
                }
                catch (Exception sqlEx)
                {
                    textTransaktionLog.Text += sqlEx.ToString();
                }

            }
            else
            {
                textTransaktionLog.AppendText("Löschvorgang abgebrochen\r\n");
            }
        }

        private void radioAusgangAendern_CheckedChanged(object sender, EventArgs e)
        {
            radioBenutztAendern.Checked = true;
            groupBox4.Enabled = false;
        }

        private void radioEingangAendern_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Enabled = true;
        }
    }
}
