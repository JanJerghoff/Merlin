﻿using MySql.Data.MySqlClient;
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

        List<int> nummern = new List<int>();    //Liste der Mitarbeiter in Reihenfolge der DB
        List<DateTime> daten = new List<DateTime>(); //Liste der Daten in der selben Reihenfolge
        DateTime dummy = new DateTime(2016, 1, 1);

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

            nummern = new List<int>();
            daten = new List<DateTime>();

            umzObj = new Umzug(umzNr);
            
            textUmzNummerBlock.Text = umzObj.Id.ToString();
            Umzugsnummer = umzObj.Id;
            textKundennummer.Text = umzObj.IdKunden.ToString();                     
            dateBesicht.Value = umzObj.DatBesichtigung;
            dateUmzug.Value = umzObj.DatUmzug.Date;
            //
            textAuszug.Text = umzObj.auszug.Straße1;
            textAuszug.Text += " " + umzObj.auszug.Hausnummer1;
            textAuszug.Text += ", " + umzObj.auszug.PLZ1;
            textAuszug.Text += " " + umzObj.auszug.Ort1;
            //
            textEinzug.Text = umzObj.einzug.Straße1;
            textEinzug.Text += " " + umzObj.einzug.Hausnummer1;
            textEinzug.Text += ", " + umzObj.einzug.PLZ1;
            textEinzug.Text += " " + umzObj.einzug.Ort1;
            
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
                    for (int i = 0; i < 19; i++)        //Schleife über alle Int-Datetime-Paare
                    {
                        int temp = rdrF.GetInt32(i*2 + 2);        //Versetzt um 2 weil DB mit ID´s auf 0 und 1 anfängt
                        nummern.Add(temp);

                        if (temp != 8)                  // Immer genau ein Datum add, entweder Dummy oder Korrekt
                        {
                            daten.Add(rdrF.GetDateTime(i*2 + 3));
                        }
                        else
                        {
                            daten.Add(dummy);
                        }
                    }                   

                    if (umzObj.Versicherung == 0)
                    {
                        buttonVersicherung.Enabled = false;
                    }

                    //Bemerkung
                    textNote.Text = rdrF[40].ToString();
                    
                    // Schon geschlossen?
                    if (rdrF[41].ToString() != "8")
                    {
                        //buttonKVAMail.Enabled = false;
                        //buttonKVAPost.Enabled = false;
                        //buttonKueche.Enabled = false;
                        //buttonBestaetigung.Enabled = false;
                        //buttonHVZWunder.Enabled = false;
                        //buttonHVZ.Enabled = false;
                        //buttonLKW.Enabled = false;
                        //buttonTelBuch.Enabled = false;
                        //buttonTextBuch.Enabled = false; // TextBuch <=> SchriftBuch
                        //buttonErinnerung.Enabled = false;
                        //buttonKorrektur.Enabled = false;
                        //buttonUmzugEingtragen.Enabled = false;
                        //buttonSchaden.Enabled = false;
                        //buttonRechnung.Enabled = false;
                        //buttonVersicherung.Enabled = false;

                        ////
                        //buttonAbschluss.Enabled = false;

                        //textSchließer.Text = getName(rdrF.GetInt32(41));
                    }
                    else { textSchließer.Text = ""; }

                }
                rdrF.Close();
            }
            catch (Exception exc) {
                Program.FehlerLog(exc.ToString(),"Abrufen der Fortschrittsdaten aus der DB zum Füllen");
            }


            //Check aller Stati

            checkStatus(0, textBesichtigung, dateBesichtigung, buttonScapegoat);
            checkStatus(1, textKVAMail, dateKVAMail, buttonKVAMail);
            checkStatus(2, textKVAPost, dateKVAPost, buttonKVAPost);
            checkStatus(3, textTelBuch, dateTelBuch, buttonTelBuch);
            checkStatus(4, textMailBuch, dateMailBuch, buttonMailBuch);
            checkStatus(5, textSchriftBuch, dateSchriftBuch, buttonTextBuch);
            checkStatus(6, textVersicherung, dateVersicherung, buttonVersicherung);
            checkStatus(7, textPackerin, datePackerin, buttonPackerin);
            checkStatus(8, textUmzugEintrag, dateUmzugEintrag, buttonUmzugEingtragen);
            checkStatus(9, textBestaetigung, dateBestaetigung, buttonBestaetigung);
            checkStatus(10, textLKW, dateLKW, buttonLKW);
            checkStatus(11, textHVZWunder, dateHVZWunder, buttonHVZWunder);
            checkStatus(12, textHVZ, dateHVZ, buttonHVZ);
            checkStatus(13, textKueche, dateKueche, buttonKueche);
            checkStatus(14, textKorrektur, dateKorrektur, buttonKorrektur);
            checkStatus(15, textErinnerung, dateErinnerung, buttonErinnerung);
            checkStatus(16, textSchaden, dateSchaden, buttonSchaden);
            checkStatus(17, textRechnung, dateRechnung, buttonRechnung);
            checkStatus(18, textVersicherungAb, dateVersicherungAb, buttonVersicherungAb);
        }

        private void checkStatus(int Stelle, TextBox name, DateTimePicker datum, Button butt) {

            if (nummern[Stelle] != 8)
            {
                name.AppendText(getName(nummern[Stelle]));
                datum.Value = daten[Stelle].Date;
                butt.Enabled = false;
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

                case 4:
                    return "Nora";

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
            textVersicherungAb.ResetText();
            textPackerin.ResetText();
            textMailBuch.ResetText();

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
            String k = "UPDATE Umzugsfortschritt SET VersicherungWunder = " + idBearbeitend + ", datVersicherungWunder = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonMailBuch_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET BuchungMail = " + idBearbeitend + ", datBuchungMail = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonPackerin_Click(object sender, EventArgs e)
        {
            String k = "UPDATE Umzugsfortschritt SET Packerin = " + idBearbeitend + ", datPackerin = '" + Program.DateMachine(DateTime.Now.Date) + "' WHERE Umzuege_idUmzuege = " + Umzugsnummer + ";";
            push(k);
            fuellen(Umzugsnummer);
        }

        private void buttonVersicherungAb_Click(object sender, EventArgs e)
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
