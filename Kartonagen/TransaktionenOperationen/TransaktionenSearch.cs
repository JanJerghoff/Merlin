using Google.Apis.Calendar.v3.Data;
using Kartonagen.Objekte;
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
        Transaktion transObj;
        Umzug umzObj;
        int maxTransaktionsnummer;
        String Userchanged;

        public TransaktionenSearch()
        {
            this.Icon = Properties.Resources.icon_Fnb_icon;
            InitializeComponent();

            //Maximale Transaktionsnummer um OutOfBounds vorzubeugen
            
            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdReadTrans = new MySqlCommand("SELECT * FROM Transaktionen ORDER BY idTransaktionen DESC LIMIT 1;", Program.conn);
                MySqlDataReader rdrTrans = cmdReadTrans.ExecuteReader();
                while (rdrTrans.Read())
                {
                    maxTransaktionsnummer = rdrTrans.GetInt32(0);
                }
                rdrTrans.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der max Transaktionsnummer \r\n Bereits dokumentiert.");
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

            //Beschaffen der Umzugsnummer zur gegebenen Transaktionsnummer
            // Füllen der Transaktion(alt wie geändert) aus der Transaktion

           //Objekte, Kunde ist in der Transaktion enthalten
           transObj = new Transaktion(TransNummer);
            Console.WriteLine("Transaktion geladen, umzObj zur nummer " + transObj.IdUmzuege);
            umzObj = new Umzug(1119);

            //Block Stammdaten

            textUmzugsdatum.Text = umzObj.DatUmzug.ToShortDateString();
            textUmzugsnummer.Text = umzObj.Id + "";
            textVorNachname.Text = transObj.Kunde.getVollerName();
            textKundennummer.Text = transObj.IdKunden + "";

            //Block Änderbare Daten

            if (transObj.Kartons1 > 0 || transObj.Kleiderkartons1 > 0 || transObj.Glaeserkartons1 > 0 || transObj.Flaschenkartons1 > 0)
            {
                radioAusgang.Checked = true;
                radioAusgangAendern.Checked = true;
            }
            else
            {
                radioEingang.Checked = true;
                radioEingangAendern.Checked = true;
                if (transObj.Unbenutzt)
                {
                    radioUnbenutzt.Checked = true;
                    radioUnbenutztAendern.Checked = true;
                }
            }


            dateTimeTransaktion.Value = transObj.DatKalender;
            dateTimeTransaktionAendern.Value = transObj.DatKalender;
            timeTransaktion.Value = transObj.DatKalender;
            timeAendern.Value = transObj.DatKalender;

            numericKarton.Value = Math.Abs(transObj.Kartons1);
            numericKartonAendern.Value = Math.Abs(transObj.Kartons1);
            numericFlaschenKarton.Value = Math.Abs(transObj.Flaschenkartons1);
            numericFlaschenKartonAendern.Value = Math.Abs(transObj.Flaschenkartons1);
            numericGlaeserkarton.Value = Math.Abs(transObj.Glaeserkartons1);
            numericGlaeserKartonAendern.Value = Math.Abs(transObj.Glaeserkartons1);
            numericKleiderKarton.Value = Math.Abs(transObj.Kleiderkartons1);
            numericKleiderKartonAendern.Value = Math.Abs(transObj.Kleiderkartons1);

            textBemerkung.Text = transObj.Bemerkung1;
            textBemerkungAendern.Text = transObj.Bemerkung1;

            // Rechnungsnummer einfüllen
            textRechnungsnummer.Text = transObj.Rechnungsnummer1;
            textRechnungsnummerAendern.Text = transObj.Rechnungsnummer1;
            // UserChanged merken
            Userchanged = transObj.UserChanged1;

            textTransaktionLog.AppendText("Transaktion erfolgreich aufgerufen \r\n");

            //try
            //{
            //    rdrTrans = cmdReadTrans.ExecuteReader();
            //    while (rdrTrans.Read())
            //    {
            //        umzNummer = rdrTrans.GetInt32(6);
            //        // Check positive Werte für Kartons = Auslieferung
            //        if (rdrTrans.GetInt32(2) > 0 || rdrTrans.GetInt32(3) > 0 || rdrTrans.GetInt32(4) > 0 || rdrTrans.GetInt32(5) > 0)
            //        {
            //            radioAusgang.Checked = true;
            //            radioAusgangAendern.Checked = true;
            //        }
            //        // Check benutzt
            //        
            //        // Einfüllen Zahlenwerte ohne Vorzeichen
            //        numericKarton.Value = Math.Abs(rdrTrans.GetInt32(2));
            //        numericKartonAendern.Value = Math.Abs(rdrTrans.GetInt32(2));
            //        numericFlaschenKarton.Value = Math.Abs(rdrTrans.GetInt32(3));
            //        numericFlaschenKartonAendern.Value = Math.Abs(rdrTrans.GetInt32(3));
            //        numericGlaeserkarton.Value = Math.Abs(rdrTrans.GetInt32(4));
            //        numericGlaeserKartonAendern.Value = Math.Abs(rdrTrans.GetInt32(4));
            //        numericKleiderKarton.Value = Math.Abs(rdrTrans.GetInt32(5));
            //        numericKleiderKartonAendern.Value = Math.Abs(rdrTrans.GetInt32(5));

            //        // Einfüllen Datum + Zeit
            //        dateTimeTransaktion.Value = rdrTrans.GetDateTime(1);
            //        dateTimeTransaktionAendern.Value = rdrTrans.GetDateTime(1);

            //DateTime dummy = DateTime.Today + rdrTrans.GetDateTime(13).TimeOfDay;

            //        timeTransaktion.Value = dummy;
            //        timeAendern.Value = dummy;

            //        // Bemerkungen einfüllen



            //    }
            //    rdrTrans.Close();
            //}
            //catch (Exception sqlEx)
            //{
            //    Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auffüllen der Transaktionen \r\n Bereits dokumentiert.");
            //    return;
            //}

            //// Umzugsdaten aus dem Umzug ziehen

            //MySqlCommand cmdReadUmzug = new MySqlCommand("SELECT Kunden_idKunden, datUmzug FROM Umzuege WHERE idUmzuege=" + umzNummer + ";", Program.conn);
            //MySqlDataReader rdrUmzug;

            //try
            //{
            //    rdrUmzug = cmdReadUmzug.ExecuteReader();
            //    while (rdrUmzug.Read())
            //    {
            //        textKundennummer.Text = rdrUmzug[0] + "";
            //        textUmzugsdatum.Text = rdrUmzug.GetDateTime(1).ToShortDateString();
            //        textUmzugsnummer.Text = umzNummer + "";
            //    }
            //    rdrUmzug.Close();
            //}
            //catch (Exception sqlEx)
            //{
            //    Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Umzugsdaten fürs Auffüllen \r\n Bereits dokumentiert.");
            //    return;
            //}

            //// Personendaten aus dem Kunden ziehen

            //MySqlCommand cmdReadKunde = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden=" + textKundennummer.Text + ";", Program.conn);
            //MySqlDataReader rdrKunde;

            //try
            //{
            //    rdrKunde = cmdReadKunde.ExecuteReader();
            //    while (rdrKunde.Read())
            //    {

            //        textVorNachname.Text = rdrKunde[1] + " " + rdrKunde[2] + " " + rdrKunde[3];
            //    }
            //    rdrKunde.Close();
            //}
            //catch (Exception sqlEx)
            //{
            //    Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Personendaten \r\n Bereits dokumentiert.");
            //    return;
            //}
        }

        //
        //
        //


        private void buttonAbsenden_Click(object sender, EventArgs e)
        {

            //Alten Kalendereintrag killen
            if (transObj != null) {
                if (transObj.KalenderRemove())
                {
                    textTransaktionLog.AppendText("Alter Kalendereintrag entfernt! " + Environment.NewLine);
                }
                else {
                    textTransaktionLog.AppendText("Alten Kalendereintrag nicht gefunden! " + Environment.NewLine);
                }
            }
            else
            {
                //TODO Popup-Warnmeldung keine Transaktion eingeloggt
            }

            transObj.DatKalender = Program.getUtil().mergeDatetime(dateTimeTransaktionAendern.Value, timeAendern.Value);
            transObj.DatTransaktion = dateTimeTransaktionAendern.Value;

            if (radioEingangAendern.Checked)
            {
                transObj.Kartons1 = decimal.ToInt32(decimal.Zero - numericKartonAendern.Value);
                transObj.Kleiderkartons1 = decimal.ToInt32(decimal.Zero - numericKleiderKartonAendern.Value);
                transObj.Glaeserkartons1 = decimal.ToInt32(decimal.Zero - numericGlaeserKartonAendern.Value);
                transObj.Flaschenkartons1 = decimal.ToInt32(decimal.Zero - numericFlaschenKartonAendern.Value);
            }

            else {

                transObj.Kartons1 = decimal.ToInt32(numericKartonAendern.Value);
                transObj.Kleiderkartons1 = decimal.ToInt32(numericKleiderKartonAendern.Value);
                transObj.Glaeserkartons1 = decimal.ToInt32(numericGlaeserKartonAendern.Value);
                transObj.Flaschenkartons1 = decimal.ToInt32(numericFlaschenKartonAendern.Value);
            }

            transObj.Rechnungsnummer1 = textRechnungsnummerAendern.Text;
            transObj.Unbenutzt = radioUnbenutztAendern.Checked;
            transObj.Bemerkung1 = textBemerkungAendern.Text;

            transObj.updateDB(idBearbeitend+"");

            Console.WriteLine("Update ist durch!");
            textTransaktionLog.AppendText("Datenbankänderung erfolgreich! " + Environment.NewLine);

            //Kalendereintrag
            if (transObj.KalenderAdd())
            {
                textTransaktionLog.AppendText("Kalendereintrag neu hinzugefügt! " + Environment.NewLine);
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
                
                Program.absender(delete, "Löschen der Transaktion");

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
