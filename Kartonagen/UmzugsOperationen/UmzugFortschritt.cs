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
    public partial class UmzugFortschritt : Form
    {
        Umzug umzObj;
        int idBearbeitend;
        int Umzugsnummer;
        List <String> arbeiter = new List<string> ();
        List <Decimal> loehne = new List<decimal> ();
        String[] arbeiter1;
        Decimal[] loehne1;

        // Einzugsadresse für Umzugs-Abschluss und Ersetzung

        int IDKunde;
        String Strasse;
        String Hausnummer;
        String Ort;
        int PLZ = 0;
        String Land;

        //

        public void setBearbeitend(int code) {
            idBearbeitend = code;
        }


        public UmzugFortschritt()
        {
            InitializeComponent();
                        
        }

        public void fuellen(int umzNr)
        {
            reset();

            umzObj = new Umzug(umzNr);

            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Umzuege WHERE idUmzuege = '" + umzNr + "';", Program.conn);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    textUmzNummerBlock.Text = rdr[0].ToString();
                    Umzugsnummer = rdr.GetInt32(0);
                    textKundennummer.Text = rdr[1].ToString();                       //Impliziter Cast weil Typ des SQL-Attributs bekannt
                    dateBesicht.Value = rdr.GetDateTime(2);
                    dateUmzug.Value = rdr.GetDateTime(3);
                    //
                    textAuszug.Text = rdr[30].ToString();
                    textAuszug.Text += " " + rdr[31].ToString();
                    textAuszug.Text += ", " + rdr[32].ToString();
                    textAuszug.Text += " " + rdr[33].ToString();
                    //
                    textEinzug.Text = " " + rdr[35].ToString();
                    textEinzug.Text += " " + rdr[36].ToString();
                    textEinzug.Text += ", " + rdr[37].ToString();
                    textEinzug.Text += " " + rdr[38].ToString();
                    //
                    Strasse = rdr.GetString(35);
                    Hausnummer = rdr.GetString(36);
                    PLZ = rdr.GetInt32(37);
                    Ort = rdr.GetString(38);
                    Land = rdr.GetString(39);
                    // Trigger für Versicherungen
                    

                }
                rdr.Close();
            }
            catch (Exception exc) { }

            // Personendaten aus dem Kunden ziehen

            MySqlCommand cmdReadKunde = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden=" + textKundennummer.Text + " ;", Program.conn);
            MySqlDataReader rdrKunde;

            try
            {
                rdrKunde = cmdReadKunde.ExecuteReader();
                while (rdrKunde.Read())
                {
                    IDKunde = rdrKunde.GetInt32(0);
                    textVorNachname.Text = rdrKunde[1] + " " + rdrKunde[2] + " " + rdrKunde[3];
                    textTelefonnummer.Text = rdrKunde[4] + "";
                    textHandynummer.Text = rdrKunde[5] + "";
                    textEmail.Text = rdrKunde[6] + "";
                }
                rdrKunde.Close();
            }
            catch (Exception sqlEx) { }

            // Fortschrittsdaten aus der DB ziehen

            MySqlCommand cmdFort = new MySqlCommand("SELECT * FROM Umzugsfortschritt WHERE Umzuege_idUmzuege = '" + umzNr + "' LIMIT 1;", Program.conn);
            MySqlDataReader rdrF;

            try
            {
                rdrF = cmdFort.ExecuteReader();
                while (rdrF.Read())
                {
                    //Besichtigung, passiv!
                    if (rdrF.GetInt32(1) != 8) {
                        textBesichtigung.AppendText(getName(rdrF.GetInt32(1)));
                        dateBesichtigung.Value = rdrF.GetDateTime(2);
                    }
                    //KVA Mail-> Post
                    if (rdrF.GetInt32(3) != 8)
                    {
                        textKVAMail.AppendText(getName(rdrF.GetInt32(3)));
                        dateKVAMail.Value = rdrF.GetDateTime(4);
                        buttonKVAMail.Enabled = false;
                    }

                    if (rdrF.GetInt32(5) != 8)
                    {
                        textKVAPost.AppendText(getName(rdrF.GetInt32(5)));
                        dateKVAPost.Value = rdrF.GetDateTime(6);
                        buttonKVAPost.Enabled = false;
                    }

                    if (rdrF.GetInt32(23) != 8)
                    {
                        textKorrektur.AppendText(getName(rdrF.GetInt32(23)));
                        dateKorrektur.Value = rdrF.GetDateTime(24);
                        buttonKorrektur.Enabled = false;
                    }
                    // Buchungen

                    if (rdrF.GetInt32(7) != 8)
                    {
                        textTelBuch.AppendText(getName(rdrF.GetInt32(7)));
                        dateTelBuch.Value = rdrF.GetDateTime(8);
                        buttonTelBuch.Enabled = false;
                    }

                    if (rdrF.GetInt32(31) != 8)
                    {
                        textErinnerung.AppendText(getName(rdrF.GetInt32(31)));
                        dateErinnerung.Value = rdrF.GetDateTime(32);
                        buttonErinnerung.Enabled = false;                      

                    }

                    if (rdrF.GetInt32(9) != 8)
                    {
                        textSchriftBuch.AppendText(getName(rdrF.GetInt32(9)));
                        dateSchriftBuch.Value = rdrF.GetDateTime(10);
                        buttonTextBuch.Enabled = false;
                    }

                    // Buchung

                    if (rdrF.GetInt32(11) != 8) //passiv! Später!
                    {
                        textUmzugEintrag.AppendText(getName(rdrF.GetInt32(11)));
                        dateUmzugEintrag.Value = rdrF.GetDateTime(12);

                        buttonUmzugEingtragen.Enabled = false;
                    }
                    // Buchungsbestätigung
                    if (rdrF.GetInt32(13) != 8)
                    {
                        textBestaetigung.AppendText(getName(rdrF.GetInt32(13)));
                        dateBestaetigung.Value = rdrF.GetDateTime(14);
                        buttonBestaetigung.Enabled = false;
                    }
                    // LKW
                    if (rdrF.GetInt32(15) != 8)
                    {
                        textLKW.AppendText(getName(rdrF.GetInt32(15)));
                        dateLKW.Value = rdrF.GetDateTime(16);
                        buttonLKW.Enabled = false;
                    }
                    // HVZ Wunder -> Antrag
                    if (rdrF.GetInt32(17) != 8)
                    {
                        textHVZWunder.AppendText(getName(rdrF.GetInt32(17)));
                        dateHVZWunder.Value = rdrF.GetDateTime(18);
                        buttonHVZWunder.Enabled = false;
                    }

                    if (rdrF.GetInt32(19) != 8)
                    {
                        textHVZ.AppendText(getName(rdrF.GetInt32(19)));
                        dateHVZ.Value = rdrF.GetDateTime(20);
                        buttonHVZ.Enabled = false;
                    }
                    // Kueche
                    if (rdrF.GetInt32(21) != 8)
                    {
                        textKueche.AppendText(getName(rdrF.GetInt32(21)));
                        dateKueche.Value = rdrF.GetDateTime(22);
                        buttonKueche.Enabled = false;
                    }

                    // Schadensmeldung
                    if (rdrF.GetInt32(34) != 8)
                    {
                        textSchaden.AppendText(getName(rdrF.GetInt32(34)));
                        dateSchaden.Value = rdrF.GetDateTime(35);
                        buttonSchaden.Enabled = false;
                    }

                    // Rechnung
                    if (rdrF.GetInt32(36) != 8)
                    {
                        textRechnung.AppendText(getName(rdrF.GetInt32(36)));
                        dateRechnung.Value = rdrF.GetDateTime(37);
                        buttonRechnung.Enabled = false;
                    }

                    // Versicherung
                    if (rdrF.GetInt32(38) != 8)
                    {
                        textVersicherung.AppendText(getName(rdrF.GetInt32(38)));
                        dateVersicherung.Value = rdrF.GetDateTime(39);
                        buttonVersicherung.Enabled = false;
                    }

                    if (umzObj.Versicherung == 0)
                    {
                        buttonVersicherung.Enabled = false;
                    }
                    textNote.Text = rdrF[25].ToString();
                    //numericSchaden.Value = rdrF.GetDecimal(27);
                    //numericHVZKosten.Value = rdrF.GetDecimal(28);
                    //numericSonderkosten.Value = rdrF.GetDecimal(29);
                    //numericSumme.Value = rdrF.GetDecimal(30);
                    

                    // Schon geschlossen?
                    if (rdrF.GetInt32(33) != 8)
                    {
                        buttonKVAMail.Enabled = false;
                        buttonKVAPost.Enabled = false;
                        buttonKueche.Enabled = false;
                        buttonBestaetigung.Enabled = false;
                        buttonHVZWunder.Enabled = false;
                        buttonHVZ.Enabled = false;
                        buttonLKW.Enabled = false;
                        buttonTelBuch.Enabled = false;
                        buttonTextBuch.Enabled = false; // TextBuch <=> SchriftBuch
                        buttonErinnerung.Enabled = false;
                        buttonKorrektur.Enabled = false;
                        buttonUmzugEingtragen.Enabled = false;
                        buttonSchaden.Enabled = false;
                        buttonRechnung.Enabled = false;
                        buttonVersicherung.Enabled = false;

                        //
                        buttonAbschluss.Enabled = false;

                        textSchließer.Text = getName(rdrF.GetInt32(33));
                    }
                    else { textSchließer.Text = ""; }
                }
                rdrF.Close();
            }
            catch (Exception exc) {
                Program.FehlerLog(exc.ToString(),"Abrufen der Fortschrittsdaten aus der DB zum Füllen");
            }
        }

        private String getName(int ID) {

            switch (ID)
            {
                case 0:
                    return "Rita";

                case 1:
                    return "Jonas";

                case 2:
                    return "Eva";

                case 3:
                    return "Jan";

                default:
                    return "Fehler!";
            }

        }

        private void reset() {
            textKVAMail.ResetText();
            textKVAPost.ResetText();
            textKueche.ResetText();
            textBesichtigung.ResetText();
            textUmzugEintrag.ResetText();
            textBestaetigung.ResetText();
            textHVZWunder.ResetText();
            textHVZ.ResetText();
            textLKW.ResetText();
            textTelBuch.ResetText();
            textSchriftBuch.ResetText();
            textKorrektur.ResetText();
            textErinnerung.ResetText();
            textSchaden.ResetText();
            textRechnung.ResetText();
            textVersicherung.ResetText();

            buttonKVAMail.Enabled = true;
            buttonKVAPost.Enabled = true;
            buttonKueche.Enabled = true;
            buttonBestaetigung.Enabled = true;
            buttonHVZWunder.Enabled = true;
            buttonHVZ.Enabled = true;
            buttonLKW.Enabled = true;
            buttonTelBuch.Enabled = true;
            buttonTextBuch.Enabled = true; // TextBuch <=> SchriftBuch
            buttonErinnerung.Enabled = true;
            buttonSchaden.Enabled = true;
            buttonRechnung.Enabled = true;
            buttonVersicherung.Enabled = true;

            //Testfall
            buttonKorrektur.Enabled = true;
            buttonUmzugEingtragen.Enabled = true;

        }

        private void push(String st) {
            MySqlCommand cmdAdd = new MySqlCommand(st, Program.conn);
            try
            {
                cmdAdd.ExecuteNonQuery();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.AppendText(sqlEx.ToString());
            }
        }

        private void buttonKVAPost_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET KVAPost = " + idBearbeitend + ", datKVAPost = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonKVAMail_Click(object sender, EventArgs e)
        {

            String k = "UPDATE Umzugsfortschritt SET KVAMail = " + idBearbeitend + ", datKVAMail = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonTelBuch_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET BuchungTel = " + idBearbeitend + ", datBuchungTel = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonErinnerung_Click(object sender, EventArgs e)
        {
            buttonErinnerung.Enabled = false;
            //
            String k = "UPDATE Umzugsfortschritt SET Erinnerung = " + idBearbeitend + ", datErinnerung = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonTextBuch_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET BuchungSchrift = " + idBearbeitend + ", datBuchungSchrift = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonBestaetigung_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET Bestaetigung = " + idBearbeitend + ", datBestaetigung = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonLKW_Click_1(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET LKW = " + idBearbeitend + ", datLKW = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonHVZWunder_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET HVZWunder = " + idBearbeitend + ", datHVZWunder = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonHVZ_Click_1(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET HVZAntrag = " + idBearbeitend + ", datHVZAntrag = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonKueche_Click(object sender, EventArgs e)
        {           
            String k = "UPDATE Umzugsfortschritt SET KuecheTermin = " + idBearbeitend + ", datKuecheTermin = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonUmzugEingtragen_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET BuchungFin = " + idBearbeitend + ", datBuchungFin = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonKorrektur_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET KVAKorrektur = " + idBearbeitend + ", datKVAKorrektur = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonBemerkung_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET Bemerkung = '" + textNote.Text + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }


        private void buttonSchaden_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET SchadenMeldung = " + idBearbeitend + ", datSchadenMeldung = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }


        private void buttonRechnung_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET Rechnung = " + idBearbeitend + ", datRechnung = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonVersicherung_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET Versicherung = " + idBearbeitend + ", datVersicherung = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonAbschluss_Click(object sender, EventArgs e)
        {
            // Als Abgeschlossen markieren
            String abs = "UPDATE Umzugsfortschritt SET abgeschlossen = " + idBearbeitend + " WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(abs);

            //Kundenadresse updaten wenn gewünscht

            var bestätigungAdressaenderung = MessageBox.Show("Soll die Einzugsadresse als neue Adresse des Kunden übernommen werden?", "Adressänderung", MessageBoxButtons.YesNo);
            if (bestätigungAdressaenderung == DialogResult.Yes)
            {
                textUmzugLog.AppendText(Program.AdresseErsaetzen(IDKunde, Strasse, Hausnummer, Ort, PLZ, Land));
                
            }

            fuellen(Umzugsnummer);
        }






        //private void numericMasterMitarbeiter_ValueChanged(object sender, EventArgs e)
        //{
        //    numericMitarbeiter1.Value = numericMasterMitarbeiter.Value;
        //    numericMitarbeiter2.Value = numericMasterMitarbeiter.Value;
        //    numericMitarbeiter3.Value = numericMasterMitarbeiter.Value;
        //    numericMitarbeiter4.Value = numericMasterMitarbeiter.Value;
        //    numericMitarbeiter5.Value = numericMasterMitarbeiter.Value;
        //    numericMitarbeiter6.Value = numericMasterMitarbeiter.Value;
        //}

        //private void numericMasterPacker_ValueChanged(object sender, EventArgs e)
        //{
        //    numericPacker1.Value = numericMasterPacker.Value;
        //    numericPacker2.Value = numericMasterPacker.Value;
        //    numericPacker3.Value = numericMasterPacker.Value;
        //    numericPacker4.Value = numericMasterPacker.Value;
        //    numericPacker5.Value = numericMasterPacker.Value;
        //    numericPacker6.Value = numericMasterPacker.Value;
        //}

        //private double kostenpunkt(TextBox text, NumericUpDown stunden) {

        //    int i = arbeiter1.Length;

        //    if (text.Text != "") {
        //        for (int t = i; t > 0; t--) {
        //            if (text.Text == arbeiter1[t-1]) {
        //                return (decimal.ToDouble(stunden.Value) * decimal.ToDouble(loehne1[t-1]));
        //            }
        //        }

        //    }
        //    return 0.0;
        //}

        //public int Kostenrechnung() {

        //    double temp = 0.0;

        //    temp += kostenpunkt(textMitarbeiter1, numericMitarbeiter1);
        //    temp += kostenpunkt(textMitarbeiter2, numericMitarbeiter2);
        //    temp += kostenpunkt(textMitarbeiter3, numericMitarbeiter3);
        //    temp += kostenpunkt(textMitarbeiter4, numericMitarbeiter4);
        //    temp += kostenpunkt(textMitarbeiter5, numericMitarbeiter5);
        //    temp += kostenpunkt(textMitarbeiter6, numericMitarbeiter6);

        //    temp += kostenpunkt(textPacker1, numericPacker1);
        //    temp += kostenpunkt(textPacker2, numericPacker2);
        //    temp += kostenpunkt(textPacker3, numericPacker3);
        //    temp += kostenpunkt(textPacker4, numericPacker4);
        //    temp += kostenpunkt(textPacker5, numericPacker5);
        //    temp += kostenpunkt(textPacker6, numericPacker6);

        //    temp += Decimal.ToDouble(numericHVZKosten.Value);
        //    temp += Decimal.ToDouble(numericSchaden.Value);
        //    temp += Decimal.ToDouble(numericSonderkosten.Value);


        //    return Convert.ToInt32(temp);
        //}

        //private void kostenspeichern() {

        //}

        //private void buttonKostenrechnung_Click(object sender, EventArgs e)
        //{
        //    numericSumme.Value = Kostenrechnung();
        //}

        //private void buttonKostenSpeichern_Click(object sender, EventArgs e)
        //{
        //    String k = "UPDATE Umzugsfortschritt SET Schaden = " + numericSchaden.Value + ", HVZKosten = "+numericHVZKosten.Value+", Sonderkosten = "+numericSonderkosten.Value+
        //        ", SummeKosten = "+numericSumme.Value+" WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
        //    push(k);
        //    fuellen(Umzugsnummer);
        //}
    }    
}
