﻿using Google.Apis.Calendar.v3.Data;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kartonagen
{
    public partial class UmzuegeSearch : Form
    {

        //Primärobjekt, der geladene Umzug:
        Umzug umzObj;

        private int idBearbeitend = 0; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.

        String UserSpeicher = "";
        int maxKundennummer;

        DateTime Umzug = new DateTime(2017, 1, 1);
        DateTime Besichtigung = new DateTime(2017, 1, 1);
        DateTime Einpacken = new DateTime(2017, 1, 1);
        DateTime Auspacken = new DateTime(2017, 1, 1);
        DateTime Entruempelung = new DateTime(2017, 1, 1);
        
        int UmzugSet = 0;
        int BesichtigungSet = 0;
        int EntruempelungSet = 0;
        int EinpackenSet = 0;
        int AuspackenSet = 0;

        public UmzuegeSearch()
        {
            this.Icon = Properties.Resources.icon_Fnb_icon;
            InitializeComponent();

            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            //Maximale Kundennummer um OutOfBounds vorzubeugen                     

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdReadKunde = new MySqlCommand("SELECT * FROM Kunden ORDER BY idKunden DESC LIMIT 1;", Program.conn);
                MySqlDataReader rdrKunde = cmdReadKunde.ExecuteReader();
                while (rdrKunde.Read())
                {
                    maxKundennummer = rdrKunde.GetInt32(0);
                }
                rdrKunde.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.Text += sqlEx.ToString();
                return;
            }

            // Daten auf heute setzen

            dateAuspack.Value = DateTime.Now;
            dateBesicht.Value = DateTime.Now;
            dateEinpack.Value = DateTime.Now;
            dateEntruempel.Value = DateTime.Now;
            dateUmzug.Value = DateTime.Now;

            numericUmzugsnummer.Text = "";
            numericSucheKundennr.Text = "";

        }

        public void SetUmzugObjekt(Umzug umz)
        {
            umzObj = umz;
        }

        public void setBearbeiter(int wer)
        {
            idBearbeitend = wer;
        }

        private void buttonNameSuche_Click(object sender, EventArgs e)
        {

            textSuchBox.Text = "";
            int tempCounter = 0;
            // Bricht, wenn mehr als 30 gleichnamige Kunden
            int[] nummern = new int[20];
            String[] daten = new String[20];
            String[] vornamen = new String[20];

            try
            {

                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdRead = new MySqlCommand("SELECT k.idKunden, k.Vorname, k.Erstelldatum FROM Kunden k, Umzuege u WHERE k.Nachname = '" + textSucheName.Text + "' AND u.Kunden_idKunden = k.idKunden;", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    Boolean alreadyExist = false;

                    //Nummer bereits vorhanden?
                    for (int i = 0; i < nummern.Length; i++) {
                        if (nummern[i] == rdr.GetInt32(0)) {
                            alreadyExist = true;
                            break;
                        }
                    }

                    if (!alreadyExist)
                    {
                        nummern[tempCounter] = rdr.GetInt32(0);
                        vornamen[tempCounter] = rdr[1].ToString();
                        daten[tempCounter] = rdr.GetDateTime(2).ToShortDateString();                                        //   Fix steht aus
                        tempCounter += 1;
                    }
                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.Text += sqlEx.ToString();
                return;
            }

            // Entscheidung einzelner / mehrere Treffer

            if (tempCounter == 0)           // Kein Treffer
            {
                textUmzugLog.AppendText("Fehler: Kein Kunde zum Namen gefunden \r\n");
                return;
            }
            else if (tempCounter == 1)      // Ein treffer
            {
                umzugKundennummerFuellen(nummern[0]);
            }
            else
            {                          // Mehrere Treffer 
                for (int i = 0; i < tempCounter; i++)
                {
                    textSuchBox.AppendText(vornamen[i] + " " + textSucheName.Text + " hinzugefügt am " + daten[i] + ": " + nummern[i] + " \r\n");
                }
            }

        }

        public void umzugKundennummerFuellen(int Nummer)
        {
            String such = "";

            if (checkGeschlossenAnzeigen.Checked)
            {
                such = "SELECT idUmzuege, datUmzug FROM Umzuege WHERE Kunden_idKunden = " + Nummer + ";";
            }
            else {
                such = "SELECT u.idUmzuege, u.datUmzug FROM Umzuege u, Umzugsfortschritt f WHERE u.Kunden_idKunden = " + Nummer + " AND f.Umzuege_idUmzuege = u.idUmzuege AND f.abgeschlossen = 8;";
            }



            ;
            int tempCounter = 0;
            // Bricht, wenn mehr als 30 gleichnamige Kunden
            int[] nummern = new int[30];
            String[] umzDaten = new String[30];

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdRead = new MySqlCommand(such, Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();

                while (rdr.Read())
                {
                    nummern[tempCounter] = rdr.GetInt32(0);
                    tempCounter += 1;
                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.Text += sqlEx.ToString();
                return;
            }

            // Entscheidung einzelner / mehrere Treffer

            if (tempCounter == 0)           // Kein Treffer
            {
                textUmzugLog.AppendText("Fehler: Kein gültiger Umzug zum Kunden mit der Nummer " + Nummer + " gefunden \r\n Wenn der Umzug bereits abgeschlossen ist, \r\n den Haken neben dem Namensfeld setzen und erneut versuchen.");
                return;
            }
            else if (tempCounter == 1)      // Ein treffer
            {
                try
                {
                    umzObj = new Umzug(nummern[0]);
                }
                catch (MySqlException ex)
                {
                    Program.FehlerLog("", "Umzug nicht gefunden");
                    return;
                }
                umzugAenderungFuellem();
            }
            else
            {                          // Mehrere Treffer + umzDaten[i]
                for (int i = 0; i < tempCounter; i++)
                {
                    textSuchBox.AppendText("Umzug Nummer " + nummern[i] + " vom Datum " + " \r\n");
                }
            }

        }

        public void umzugAenderungFuellem()
        {         // FulleM. Sic!           

            if (umzObj == null)
            {
                Program.FehlerLog("", "Fehler kein Umzugsobjekt in UmzugAenderungFülleM");
                return;
            }
           
            //UserSpeicher = umzObj.UserChanged1;

            textUmzNummerBlock.Text = umzObj.Id.ToString();
            textKundennummer.Text = umzObj.IdKunden.ToString();
            dateBesicht.Value = umzObj.DatBesichtigung;
            dateUmzug.Value = umzObj.DatUmzug;
            dateEntruempel.Value = umzObj.DatRuempeln;
            dateEinpack.Value = umzObj.DatEinraeumen;
            dateAuspack.Value = umzObj.DatAusraeumen;
            //
            UmzugSet = umzObj.StatUmzug;
            BesichtigungSet = umzObj.StatBesichtigung;
            EntruempelungSet = umzObj.StatRuempeln;
            AuspackenSet = umzObj.StatAus;
            EinpackenSet = umzObj.StatEin;
            //
            Umzug = umzObj.DatUmzug;
            Besichtigung = umzObj.DatBesichtigung;
            Einpacken = umzObj.DatEinraeumen;
            Auspacken = umzObj.DatAusraeumen;
            Entruempelung = umzObj.DatRuempeln;
            //
            switch (umzObj.auszug.Aufzug1)
            {
                case 1:
                    radioAufzugAJa.Checked = true;
                    break;
                case 0:
                    radioAufzugANein.Checked = true;
                    break;
                default:
                    break;
            }
            //
            switch (umzObj.einzug.Aufzug1)
            {
                case 1:
                    radioAufzugBJa.Checked = true;
                    break;
                case 0:
                    radioAufzugBNein.Checked = true;
                    break;
                default:
                    break;
            }
            //
            switch (umzObj.auszug.HVZ1)
            {
                case 2:
                    radioHVZAV.Checked = true;
                    break;
                case 1:
                    radioHVZAJa.Checked = true;
                    break;
                case 0:
                    radioHVZANein.Checked = true;
                    break;
                default:
                    break;
            }
            //
            switch (umzObj.einzug.HVZ1)
            {
                case 2:
                    radioHVZBV.Checked = true;
                    break;
                case 1:
                    radioHVZBJa.Checked = true;
                    break;
                case 0:
                    radioHVZBNein.Checked = true;
                    break;
                default:
                    break;
            }
            //
            textLaufMeterA.Text = umzObj.auszug.Laufmeter1.ToString();
            textLaufMeterB.Text = umzObj.einzug.Laufmeter1.ToString();

            //
            numericKleiderkisten.Value = umzObj.Kleiderkartons1;
            numericMannZahl.Value = umzObj.Mann;
            numericArbeitszeit.Value = umzObj.Stunden;
            //
            switch (umzObj.Schilder1)
            {
                case 1:
                    radioSchilderJa.Checked = true;
                    break;
                case 0:
                    radioSchilderNein.Checked = true;
                    break;
                default:
                    break;
            }
            //
            switch (umzObj.KuecheAb1)
            {
                case 2:
                    radioKuecheAbV.Checked = true;
                    break;
                case 1:
                    radioKuecheAbJa.Checked = true;
                    break;
                case 0:
                    radioKuecheAbNein.Checked = true;
                    break;
                default:
                    break;
            }
            //
            switch (umzObj.KuecheAuf1)
            {
                case 2:
                    radioKuecheAufV.Checked = true;
                    break;
                case 1:
                    radioKuecheAufJa.Checked = true;
                    break;
                case 0:
                    radioKuecheAufNein.Checked = true;
                    break;
                default:
                    break;
            }
            //
            switch (umzObj.KuecheBau1)
            {
                case 1:
                    radioKuecheIntern.Checked = true;
                    break;
                case 0:
                    radioKuecheExtern.Checked = true;
                    break;
                default:
                    break;
            }
            //
            textKuechenPreis.Text = umzObj.KuechePausch1.ToString();
            numericUmzugsDauer.Value = umzObj.Umzugsdauer;
            string tempAutos = umzObj.Autos;
            //
            textStraßeA.Text = umzObj.auszug.Straße1;
            textHausnummerA.Text = umzObj.auszug.Hausnummer1;
            textPLZA.Text = umzObj.auszug.PLZ1;
            textOrtA.Text = umzObj.auszug.Ort1;
            textLandA.Text = umzObj.auszug.Land1;
            //
            textStraßeB.Text = umzObj.einzug.Straße1;
            textHausnummerB.Text = umzObj.einzug.Hausnummer1;
            textPLZB.Text = umzObj.einzug.PLZ1;
            textOrtB.Text = umzObj.einzug.Ort1;
            textLandB.Text = umzObj.einzug.Land1;
            //
            textStrasseEnt.Clear();
            textHausnummerEnt.Clear();
            textPLZEnt.Clear();
            textOrtEnt.Clear();
            if (umzObj.AdresseRuempel1 != 0)
            {
                textStrasseEnt.Text = (umzObj.entruempeln.Straße1);
                textHausnummerEnt.Text = (umzObj.entruempeln.Hausnummer1);
                textOrtEnt.Text = (umzObj.entruempeln.Ort1);
                textPLZEnt.Text = (umzObj.entruempeln.PLZ1);
                numericPackerEnt.Value = umzObj.RuempelMann1;
                numericStundenEnt.Value = umzObj.RuempelStunden1;
            }
            //
            textNoteBuero.Text = umzObj.NotizBuero1;
            textNoteFahrer.Text = umzObj.NotizFahrer1;
            textNoteKalender.Text = umzObj.NotizTitel1;

            //
            //Setzen der Terminzustände
            // Umzug
            switch (umzObj.StatUmzug)
            {
                case 3:
                    radioUmzVorlaeufig.Checked = true;
                    break;
                case 2:
                    radioUmzVllt.Checked = true;
                    break;
                case 1:
                    radioUmzJa.Checked = true;
                    break;
                case 0:
                    radioUmzNein.Checked = true;
                    break;
                default:
                    break;
            }
            // Besichtigung
            switch (umzObj.StatBesichtigung)
            {
                case 1:
                    radioBesJa.Checked = true;
                    break;
                case 0:
                    radioBesNein.Checked = true;
                    break;
                default:
                    break;
            }
            // Auspacken
            switch (umzObj.StatAus)
            {
                case 2:
                    radioAusVllt.Checked = true;
                    break;
                case 1:
                    radioAusJa.Checked = true;
                    break;
                case 0:
                    radioAusNein.Checked = true;
                    break;
                default:
                    break;
            }
            // Einpacken
            switch (umzObj.StatEin)
            {
                case 2:
                    radioEinVllt.Checked = true;
                    break;
                case 1:
                    radioEinJa.Checked = true;
                    break;
                case 0:
                    radioEinNein.Checked = true;
                    break;
                default:
                    break;
            }
            // Entrümpeln
            switch (umzObj.StatRuempeln)
            {
                case 2:
                    radioEntVllt.Checked = true;
                    break;
                case 1:
                    radioEntJa.Checked = true;
                    break;
                case 0:
                    radioEntNein.Checked = true;
                    break;
                default:
                    break;
            }
            // umzugsZeit (Besichtigungszeit!)
            //String temp = rdr[51].ToString();
            DateTime transplant = new DateTime(2000, 1, 1, 0, 0, 0);

            TimeSpan t = umzObj.ZeitUmzug.TimeOfDay;
            timeBesichtigung.Value = transplant.Add(t);

            // Setzen der Listboxen 

            switch (umzObj.auszug.Haustyp1)
            {
                case "EFH":
                    listBoxA.SelectedIndex = 0;
                    break;

                case "DHH":
                    listBoxA.SelectedIndex = 1;
                    break;

                case "RH":
                    listBoxA.SelectedIndex = 2;
                    break;

                default:
                    listBoxA.SelectedIndex = 3;
                    break;
            }
            //
            switch (umzObj.einzug.Haustyp1)
            {
                case "EFH":
                    listBoxB.SelectedIndex = 0;
                    break;

                case "DHH":
                    listBoxB.SelectedIndex = 1;
                    break;

                case "RH":
                    listBoxB.SelectedIndex = 2;
                    break;

                default:
                    listBoxB.SelectedIndex = 3;
                    break;
            }
            // Aussenaufzüge
            switch (umzObj.auszug.AussenAufzug1)
            {
                case 1:
                    radioAussenAufzugAJa.Checked = true;
                    break;
                case 0:
                    radioAussenAufzugANein.Checked = true;
                    break;
                default:
                    break;
            }
            //
            switch (umzObj.einzug.AussenAufzug1)
            {
                case 1:
                    radioAussenAufzugBJa.Checked = true;
                    break;
                case 0:
                    radioAussenAufzugBNein.Checked = true;
                    break;
                default:
                    break;
            }
            // Geändertes UI

            readBitstringEtagenA(umzObj.auszug.Stockwerke1);
            readBitstringEtagenB(umzObj.einzug.Stockwerke1);

            switch (umzObj.Versicherung)
            {
                case 1:
                    radioVersicherungJa.Checked = true;
                    break;
                case 0:
                    radioVersicherungNein.Checked = true;
                    break;
                default:
                    break;
            }
            //
            switch (umzObj.Einpacken1)
            {
                case 2:
                    radioEinpackenV.Checked = true;
                    break;
                case 1:
                    radioEinpackenJa.Checked = true;
                    break;
                case 0:
                    radioEinpackenNein.Checked = true;
                    break;
                default:
                    break;
            }
            //
            switch (umzObj.Auspacken1)
            {
                case 2:
                    radioAuspackenV.Checked = true;
                    break;
                case 1:
                    radioAuspackenJa.Checked = true;
                    break;
                case 0:
                    radioAuspackenNein.Checked = true;
                    break;
                default:
                    break;
            }

            numericEinPacker.Value = umzObj.Einpacker1;
            numericAusPacker.Value = umzObj.Auspacker1;
            numericEinPackStunden.Value = umzObj.EinStunden1;
            numericAusPackStunden.Value = umzObj.AusStunden1;
            numericEinPackKartons.Value = umzObj.Karton1;

            // Autos dekodieren
            Regex regex = new Regex(@"^\d+$");

            if (tempAutos.Length == 4 && regex.IsMatch(tempAutos))
            {
                if (tempAutos[0] != 0)
                {
                    numericSprinterMit.Value = int.Parse(tempAutos[0].ToString());
                }

                if (tempAutos[1] != 0)
                {
                    numericSprinterOhne.Value = int.Parse(tempAutos[1].ToString());
                }

                if (tempAutos[2] != 0)
                {
                    numericLKW.Value = int.Parse(tempAutos[2].ToString());
                }

                if (tempAutos[3] != 0)
                {
                    numericLKWGroß.Value = int.Parse(tempAutos[3].ToString());
                }
            }

            //Entrümpelblock füllen
            if (umzObj.StatRuempeln != 0 && umzObj.entruempeln != null) {
                textStrasseEnt.AppendText(umzObj.entruempeln.Straße1);
                textHausnummerEnt.AppendText(umzObj.entruempeln.Hausnummer1);
                textOrtEnt.AppendText(umzObj.entruempeln.Ort1);
                textPLZEnt.AppendText(umzObj.entruempeln.PLZ1);
            }



            // Zweite Umzugsnummer füllen

            textUmzugsNummer.Text = textUmzNummerBlock.Text;
            
            // Personendaten aus dem Kunden ziehen

            ;

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdReadKunde = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden=" + textKundennummer.Text + " ;", Program.conn);
                MySqlDataReader rdrKunde = cmdReadKunde.ExecuteReader();
                while (rdrKunde.Read())
                {

                    textVorNachname.Text = rdrKunde[1] + " " + rdrKunde[2] + " " + rdrKunde[3];
                    textTelefonnummer.Text = rdrKunde[4] + "";
                    textHandynummer.Text = rdrKunde[5] + "";
                    textEmail.Text = rdrKunde[6] + "";
                    textNoteBuero.AppendText(rdrKunde[14].ToString());
                }
                rdrKunde.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.Text += sqlEx.ToString();
                return;
            }

        }
        
        private void buttonNrSuche_Click(object sender, EventArgs e)
        {
            if (maxKundennummer >= numericSucheKundennr.Value)
            {
                umzugKundennummerFuellen(decimal.ToInt32(numericSucheKundennr.Value));
            }
            else
            {
                textUmzugLog.AppendText("Kein Kunde mit dieser Nummer vorhanden \r\n");
            }

        }

        private void buttonUmzugsnummer_Click(object sender, EventArgs e)
        {
            try
            {
                umzObj = new Umzug(decimal.ToInt32(numericUmzugsnummer.Value));
                Console.WriteLine("Umzugsobjekt geladen, hat die nummer "+umzObj.DatAusraeumen.ToShortDateString());

                Console.WriteLine("Auszug " + umzObj.AdresseAuszug1);
                Console.WriteLine("Einzug " + umzObj.AdresseEinzug1);
                Console.WriteLine("Entrümpelung " + umzObj.AdresseRuempel1);
            }
            catch (MySqlException ex)
            {
                textUmzugLog.AppendText("Umzug nicht gefunden");
                return;
            }

            if (umzObj.auszug != null)
            {
                Console.WriteLine("Auszug check" + umzObj.AdresseAuszug1);
            }
            if (umzObj.einzug != null)
            {
                Console.WriteLine("Einzug check" + umzObj.AdresseEinzug1);

            }
            if (umzObj.entruempeln != null)
            {
                Console.WriteLine("Ruempel check" + umzObj.AdresseRuempel1);
            }



            if (umzObj != null)
            {
                umzugAenderungFuellem();
            }
            
        }

        //public void absender(String befehl)
        //{
        //    MySqlCommand cmdAdd = new MySqlCommand(befehl, Program.conn);
        //    try
        //    {
        //        cmdAdd.ExecuteNonQuery();
        //        textUmzugLog.AppendText("Änderung ausgeführt \r\n");
        //        umzugAenderungFuellem(); // Neuladen der Ansicht
        //    }
        //    catch (Exception sqlEx)
        //    {
        //        textUmzugLog.AppendText(sqlEx.ToString());
        //        return;
        //    }
        //}

        //
        //  Benötigte Strings für Anlegen von Terminen
        //
        //private String KalenderString()
        //{
        //    //Konstruktion String Kalerndereintragsinhalt
        //    // Name + Auszugsadresse
        //    String Body = textVorNachname.Text + "\r\n Aus: " + textStraßeA.Text + " " + textHausnummerA.Text + ", " + textPLZA.Text + " " + textOrtA.Text + "\r\n";

        //    // Geschoss + HausTyp
        //    Body += umzObj.auszug.KalenderStringEtageHaustyp();

        //    if (radioAufzugAJa.Checked)
        //    {
        //        Body += "mit Aufzug \r\n";
        //    }
        //    else { Body += "ohne Aufzug \r\n"; }

        //    //Einzugsadresse
        //    Body += "\r\n Nach: " + textStraßeB.Text + " " + textHausnummerB.Text + ", " + textPLZB.Text + " " + textOrtB.Text + "\r\n";

        //    // Geschoss + HausTyp
        //    Body += umzObj.einzug.KalenderStringEtageHaustyp();

        //    if (radioAufzugBJa.Checked)
        //    {
        //        Body += "mit Aufzug \r\n";
        //    }
        //    else { Body += "ohne Aufzug \r\n"; }

        //    // Kontaktdaten
        //    if (textTelefonnummer.Text != "0")
        //    {
        //        Body += "\r\n " + textTelefonnummer.Text;
        //    }
        //    if (textHandynummer.Text != "0")
        //    {
        //        Body += "\r\n " + textHandynummer.Text;
        //    }
        //    if (textEmail.Text != "x@y.z")
        //    {
        //        Body += "\r\n " + textEmail.Text;
        //    }
        //    Body += "\r\n Am: " + dateUmzug.Value.ToShortDateString() + "\r\n";

        //    // Büronotiz

        //    Body += textNoteBuero.Text;

        //    // Rückgabe fertiger Body
        //    return Body;
        //}

        private String hvzString()
        {
            if (radioHVZAJa.Checked && radioHVZBJa.Checked)
            {
                return "2 X HVZ";
            }
            else if (radioHVZAJa.Checked || radioHVZBJa.Checked)
            {
                return "1 X HVZ";
            }
            else
            {
                return "keine HVZ";
            }
        }

        private String AutoString()
        {

            String temp = "";
            if (numericSprinterMit.Value != 0)
            {
                temp = temp + numericSprinterMit.Value.ToString() + " Sprinter Mit, ";
            }
            if (numericSprinterOhne.Value != 0)
            {
                temp = temp + numericSprinterOhne.Value.ToString() + " Sprinter Ohne, ";
            }
            if (numericLKW.Value != 0)
            {
                temp = temp + numericLKW.Value.ToString() + " LKW, ";
            }
            if (numericLKWGroß.Value != 0)
            {
                temp = temp + numericLKWGroß.Value.ToString() + " 12-Tonner";
            }

            return temp;
        }
           

        //
        //
        // ALLE Änderungen auf einmal übernehmen

        private void aenderungSpeichern() {

            umzObj.killAll(textUmzugLog);

            //--------------------Datumsblock

            Boolean pop = false;

            // STATI SETZEN
            // Besichtigung
            if (radioBesJa.Checked)
            {
                umzObj.StatBesichtigung = 1;
            }
            else { umzObj.StatBesichtigung = 0; }

            //Umzug
            if (radioUmzJa.Checked)
            {
                umzObj.StatUmzug = 1;
            }
            else if (radioUmzVllt.Checked)
            {
                umzObj.StatUmzug = 2;
            }
            else if (radioUmzVorlaeufig.Checked)
            {
                umzObj.StatUmzug = 3;
            }
            else { umzObj.StatUmzug = 0; }

            //Einräumen
            if (radioEinJa.Checked)
            {
                umzObj.StatEin = 1;
            }
            else if (radioEinVllt.Checked)
            {
                umzObj.StatEin = 2;
            }
            else { umzObj.StatEin = 0; }

            //Ausräumen
            if (radioAusJa.Checked)
            {
                umzObj.StatAus = 1;
            }
            else if (radioAusVllt.Checked)
            {
                umzObj.StatAus = 2;
            }
            else { umzObj.StatAus = 0; }

            //Entrümpeln
            if (radioEntJa.Checked)
            {
                umzObj.StatRuempeln = 1;
            }
            else if (radioEntVllt.Checked)
            {
                umzObj.StatRuempeln = 2;
            }
            else { umzObj.StatRuempeln = 0; }

            //ÜBERSCHREIBEN DER DATEN

            umzObj.DatBesichtigung = dateBesicht.Value;
            umzObj.DatUmzug = dateUmzug.Value;
            umzObj.DatEinraeumen = dateEinpack.Value;
            umzObj.DatAusraeumen = dateAuspack.Value;
            umzObj.DatRuempeln = dateEntruempel.Value;
            umzObj.ZeitUmzug = timeBesichtigung.Value;
            umzObj.Umzugsdauer = decimal.ToInt32(numericUmzugsDauer.Value);


            // --------------Küche
            int kuecheab = 8;
            int kuecheauf = 8;
            int kuechebau = 8;
            String kuechepausch = "0";

            if (radioKuecheAbJa.Checked) { kuecheab = 1; }
            else if (radioKuecheAbNein.Checked) { kuecheab = 0; }
            else if (radioKuecheAbV.Checked) { kuecheab = 2; }
            //
            if (radioKuecheAufJa.Checked) { kuecheauf = 1; }
            else if (radioKuecheAufNein.Checked) { kuecheauf = 0; }
            else if (radioKuecheAufV.Checked) { kuecheauf = 2; }
            //
            if (radioKuecheIntern.Checked) { kuechebau = 1; } // Küchenbau Intern = 1, Extern = 0;
            else if (radioKuecheExtern.Checked) { kuechebau = 0; }

            kuechepausch = textKuechenPreis.Text;

            // Setzen
            umzObj.KuecheAb1 = kuecheab;
            umzObj.KuecheAuf1 = kuecheauf;
            umzObj.KuecheBau1 = kuechebau;
            umzObj.KuechePausch1 = int.Parse(textKuechenPreis.Text);

            //--------------Packen


            int einPacken = 8;
            int ausPacken = 8;

            if (radioEinpackenJa.Checked) { einPacken = 1; }
            else if (radioEinpackenNein.Checked) { einPacken = 0; }
            else if (radioEinpackenV.Checked) { einPacken = 2; }
            //
            if (radioAuspackenJa.Checked) { ausPacken = 1; }
            else if (radioAuspackenNein.Checked) { ausPacken = 0; }
            else if (radioAuspackenV.Checked) { ausPacken = 2; }

            // Setzen
            umzObj.Einpacken1 = einPacken;
            umzObj.Einpacker1 = decimal.ToInt32(numericEinPacker.Value);
            umzObj.EinStunden1 = decimal.ToInt32(numericEinPackStunden.Value);
            umzObj.Karton1 = decimal.ToInt32(numericEinPackKartons.Value);
            umzObj.Auspacken1 = ausPacken;
            umzObj.Auspacker1 = decimal.ToInt32(numericAusPacker.Value);
            umzObj.AusStunden1 = decimal.ToInt32(numericAusPackStunden.Value);

            //-------------- Entrümpeln
            if (radioEntJa.Checked || radioEntVllt.Checked) {
                umzObj.getEntruempeln().Straße1 = textStrasseEnt.Text;
                umzObj.getEntruempeln().Hausnummer1 = textHausnummerEnt.Text;
                umzObj.getEntruempeln().Ort1 = textOrtEnt.Text;
                umzObj.getEntruempeln().PLZ1 = textPLZEnt.Text;
                umzObj.RuempelMann1 = decimal.ToInt32(numericPackerEnt.Value);
                umzObj.RuempelStunden1 = decimal.ToInt32(numericStundenEnt.Value);
            }
            // ------- Daten

            int schilder = 0;

            //
            if (radioSchilderJa.Checked)
            {
                schilder = 1;
            }
            else if (radioSchilderNein.Checked)
            {
                schilder = 0;
            }
            else { schilder = 8; }

            String temp = "";
            temp = temp + decimal.ToInt32(numericSprinterMit.Value).ToString();
            temp = temp + decimal.ToInt32(numericSprinterOhne.Value).ToString();
            temp = temp + decimal.ToInt32(numericLKW.Value).ToString();
            temp = temp + decimal.ToInt32(numericLKWGroß.Value).ToString();

            // Setzen
            umzObj.Schilder1 = schilder;
            umzObj.Kleiderkartons1 = decimal.ToInt32(numericKleiderkisten.Value);
            umzObj.Mann = decimal.ToInt32(numericMannZahl.Value);
            umzObj.Autos = temp;
            umzObj.Stunden = decimal.ToInt32(numericArbeitszeit.Value);
            umzObj.SchilderZeit1 = dateSchilderVerweildauer.Value;

            // ---------------- Bemerkungen
            umzObj.NotizFahrer1 = textNoteFahrer.Text;
            umzObj.NotizBuero1 = textNoteBuero.Text;
            umzObj.NotizTitel1 = textNoteKalender.Text;

            // ---------------- Auszug
            int aufzug = 8;
            int hvz = 8;
            int aussenAuf = 8;

            if (radioAussenAufzugAJa.Checked) { aussenAuf = 1; }
            else if (radioAussenAufzugANein.Checked) { aussenAuf = 0; }
            //
            if (radioAufzugAJa.Checked) { aufzug = 1; }
            else if (radioAufzugANein.Checked) { aufzug = 0; }
            //
            if (radioHVZAJa.Checked) { hvz = 1; }
            else if (radioHVZANein.Checked) { hvz = 0; }
            else if (radioHVZAV.Checked) { hvz = 2; }

            // Daten ins Objekt
            umzObj.auszug.Straße1 = textStraßeA.Text;
            umzObj.auszug.Hausnummer1 = textHausnummerA.Text;
            umzObj.auszug.PLZ1 = textPLZA.Text;
            umzObj.auszug.Ort1 = textOrtA.Text;
            umzObj.auszug.Land1 = textLandA.Text;
            umzObj.auszug.HVZ1 = hvz;
            umzObj.auszug.Aufzug1 = aufzug;
            umzObj.auszug.AussenAufzug1 = aussenAuf;
            umzObj.auszug.Haustyp1 = listBoxA.SelectedItem.ToString();
            umzObj.auszug.Laufmeter1 = int.Parse(textLaufMeterA.Text);
            umzObj.auszug.Stockwerke1 = buildBitstringEtagen(true);

            // ----------------- Einzug

            aufzug = 8;
            hvz = 8;
            aussenAuf = 8;

            if (radioAussenAufzugBJa.Checked) { aussenAuf = 1; }
            else if (radioAussenAufzugBNein.Checked) { aussenAuf = 0; }
            //
            if (radioAufzugBJa.Checked) { aufzug = 1; }
            else if (radioAufzugBNein.Checked) { aufzug = 0; }
            //
            if (radioHVZBJa.Checked) { hvz = 1; }
            else if (radioHVZBNein.Checked) { hvz = 0; }
            else if (radioHVZBV.Checked) { hvz = 2; }

            umzObj.einzug.Straße1 = textStraßeB.Text;
            umzObj.einzug.Hausnummer1 = textHausnummerB.Text;
            umzObj.einzug.PLZ1 = textPLZB.Text;
            umzObj.einzug.Ort1 = textOrtB.Text;
            umzObj.einzug.Land1 = textLandB.Text;
            umzObj.einzug.HVZ1 = hvz;
            umzObj.einzug.Aufzug1 = aufzug;
            umzObj.einzug.AussenAufzug1 = aussenAuf;
            umzObj.einzug.Haustyp1 = listBoxB.SelectedItem.ToString();
            umzObj.einzug.Laufmeter1 = int.Parse(textLaufMeterB.Text);
            umzObj.einzug.Stockwerke1 = buildBitstringEtagen(false);

            // --------------- Versicherung
            int VersTemp = 8;
            if (radioVersicherungJa.Checked) { VersTemp = 1; }
            else if (radioVersicherungNein.Checked) { VersTemp = 0; }
            umzObj.Versicherung = VersTemp;

            //----------
            umzObj.UpdateDB(idBearbeitend.ToString());

            //Kalender aktualisieren
            //refreshAll();
            umzObj.addAll();

            textUmzugLog.AppendText("Alle Termine hinzugefügt, Umzug erfolgreich aktualisiert "+Environment.NewLine);

            if (pop)
            {
                popUp();
            }
        }

        private void buttonTransaktion_Click(object sender, EventArgs e)
        {

            TransaktionAdd TranHinzufuegen = new TransaktionAdd();
            TranHinzufuegen.setBearbeiter(idBearbeitend);
            TranHinzufuegen.fuellen(int.Parse(textUmzugsNummer.Text));
            TranHinzufuegen.Show();
        }

        private void buttonLöschen_Click(object sender, EventArgs e)
        {
            var bestätigung = MessageBox.Show("Den Umzug wirklich entgültig löschen? \r\n Dies löscht auch alle Transaktionen und den Laufzettel zu diesem Umzug!", "Löschen bestätigen", MessageBoxButtons.YesNo);
            if (bestätigung == DialogResult.Yes)

            //Erst Transaktionen & Laufzettel zum Umzug, dann Umzüge selbst löschen.
            {
                String deleteL = "DELETE FROM Umzugsfortschritt WHERE Umzuege_idUmzuege = " + textUmzNummerBlock.Text + " ;";
                Program.absender(deleteL, "Löschen des Umzugsfortschritts " + umzObj.Id);
                // TODO hier fehlt das Löschen der Transaktions-Termine
                String deleteT = "DELETE FROM Transaktionen WHERE Umzuege_idUmzuege = " + textUmzNummerBlock.Text + " ;";
                Program.absender(deleteT, "Löschen der Transaktionen zum Umzug");
                //
                String delete = "DELETE FROM Umzuege WHERE idUmzuege = " + textUmzNummerBlock.Text + " ;";
                Program.absender(delete, "Löschen des Umzugs mit der nummer " + umzObj.Id);

                //Termine löschen
                umzObj.killAll(textUmzugLog);
            }
            else
            {
                textUmzugLog.AppendText("Löschvorgang abgebrochen\r\n");
            }

            textUmzugLog.AppendText("Umzug gelöscht!");
        }

        private void buttonDruk_Click(object sender, EventArgs e)
        {
            umzObj.druck(1);
            return;
        }
        

        private void buttonLaufzettel_Click(object sender, EventArgs e)
        {
            popUp();
        }

        private void popUp() {

            UmzugFortschritt laufzettel = new UmzugFortschritt();
            if (textUmzNummerBlock.Text != "")
            {
                laufzettel.fuellen(int.Parse(textUmzNummerBlock.Text));
            }
            laufzettel.fuellen(int.Parse(textUmzugsNummer.Text));
            laufzettel.setBearbeitend(idBearbeitend);
            laufzettel.Show();
        }

        private void erinnerungsPopup() {
            var bestätigung = MessageBox.Show("Umzugsbearbeitung öffnen?", "Erinnerung", MessageBoxButtons.YesNo);
            if (bestätigung == DialogResult.Yes)
            {
                popUp();
            }
            else
            {
                return;
            }
        }

        private void radioEntruempelAus_CheckedChanged(object sender, EventArgs e)
        {
            textStrasseEnt.Clear();
            textHausnummerEnt.Clear();
            textOrtEnt.Clear();
            textPLZEnt.Clear();

            textStrasseEnt.AppendText(textStraßeA.Text);
            textHausnummerEnt.AppendText(textHausnummerA.Text);
            textOrtEnt.AppendText(textOrtA.Text);
            textPLZEnt.AppendText(textPLZA.Text);

            textStrasseEnt.ReadOnly = true;
            textHausnummerEnt.ReadOnly = true;
            textOrtEnt.ReadOnly = true;
            textPLZEnt.ReadOnly = true;

        }

        private void radioEntruempelnAndere_CheckedChanged(object sender, EventArgs e)
        {
            textStrasseEnt.Clear();
            textHausnummerEnt.Clear();
            textOrtEnt.Clear();
            textPLZEnt.Clear();

            textStrasseEnt.ReadOnly = false;
            textHausnummerEnt.ReadOnly = false;
            textOrtEnt.ReadOnly = false;
            textPLZEnt.ReadOnly = false;
        }

        private void toggleEntruempelAdresse() {

            //Alle clearen
            textStrasseEnt.Clear();
            textHausnummerEnt.Clear();
            textPLZEnt.Clear();
            textOrtEnt.Clear();

            //Kopieren der Auszugsadresse aus dem Umzugsobjekt
            if (radioEntruempelAus.Checked)
            {
                textStrasseEnt.AppendText(umzObj.auszug.Straße1);
                textHausnummerEnt.AppendText(umzObj.auszug.Hausnummer1);
                textPLZEnt.AppendText(umzObj.auszug.PLZ1);
                textOrtEnt.AppendText(umzObj.auszug.Ort1);
            }
            else if (radioEntruempelnAndere.Checked) {

                textStrasseEnt.AppendText(umzObj.entruempeln.Straße1);
                textHausnummerEnt.AppendText(umzObj.entruempeln.Hausnummer1);
                textPLZEnt.AppendText(umzObj.entruempeln.PLZ1);
                textOrtEnt.AppendText(umzObj.entruempeln.Ort1);

            }
        }

        // Reihenfolge im Bitstring
        // 0) Keller 1) Erdgeschoss 2) Hochpaterre 3) Souterrain
        // 4) Maisonette 5-9) 1 bis 5 OG  
        // 10) Dachboden
        // "-" als Trenner, dann eingabe als Klartext

        private void readBitstringEtagenA(String Bitstring)
        {
            Console.WriteLine("Read Bitstring " + Bitstring);

            if (Bitstring.Length == 11)
            {
                Bitstring = Bitstring + "-";
            }

            checkKellerA.Checked = false;
            checkEGA.Checked = false;
            checkDBA.Checked = false;
            checkMAA.Checked = false;
            checkSTA.Checked = false;
            checkHPA.Checked = false;
            checkOG1A.Checked = false;
            checkOG2A.Checked = false;
            checkOG3A.Checked = false;
            checkOG4A.Checked = false;
            checkOG5A.Checked = false;
            textSonderEtageA.Clear();

            if (Bitstring[0] == '1') 
            {
                checkKellerA.Checked = true;
            }
            if (Bitstring[1] == '1')
            {
                checkEGA.Checked = true;
            }
            if (Bitstring[2] == '1')
            {
                checkHPA.Checked = true;
            }
            if (Bitstring[3] == '1')
            {
                checkSTA.Checked = true;
            }
            if (Bitstring[4] == '1')
            {
                checkMAA.Checked = true;
            }
            if (Bitstring[5] == '1')
            {
                checkOG1A.Checked = true;
            }
            if (Bitstring[6] == '1')
            {
                checkOG2A.Checked = true;
            }
            if (Bitstring[7] == '1')
            {
                checkOG3A.Checked = true;
            }
            if (Bitstring[8] == '1')
            {
                checkOG4A.Checked = true;
            }
            if (Bitstring[9] == '1')
            {
                checkOG5A.Checked = true;
            }
            if (Bitstring[10] == '1')
            {
                checkDBA.Checked = true;
            }
            if (Bitstring.Split('-').Length != 1)
            {
                if (!Bitstring.Split('-')[1].Equals(String.Empty))
                {
                    textSonderEtageA.Text = Bitstring.Split('-')[1];
                }
            }

        }

        private void readBitstringEtagenB(String Bitstring) {

            if (Bitstring.Length == 11) {
                Bitstring = Bitstring + "-";
            }

            checkKellerB.Checked = false;
            checkEGB.Checked = false;
            checkDBB.Checked = false;
            checkMAB.Checked = false;
            checkSTB.Checked = false;
            checkHPB.Checked = false;
            checkOG1B.Checked = false;
            checkOG2B.Checked = false;
            checkOG3B.Checked = false;
            checkOG4B.Checked = false;
            checkOG5B.Checked = false;
            textSonderEtageB.Clear();

            if (Bitstring[0] == '1')
            {
                checkKellerB.Checked = true;
            }
            if (Bitstring[1] == '1')
            {
                checkEGB.Checked = true;
            }
            if (Bitstring[2] == '1')
            {
                checkHPB.Checked = true;
            }
            if (Bitstring[3] == '1')
            {
                checkSTB.Checked = true;
            }
            if (Bitstring[4] == '1')
            {
                checkMAB.Checked = true;
            }
            if (Bitstring[5] == '1')
            {
                checkOG1B.Checked = true;
            }
            if (Bitstring[6] == '1')
            {
                checkOG2B.Checked = true;
            }
            if (Bitstring[7] == '1')
            {
                checkOG3B.Checked = true;
            }
            if (Bitstring[8] == '1')
            {
                checkOG4B.Checked = true;
            }
            if (Bitstring[9] == '1')
            {
                checkOG5B.Checked = true;
            }
            if (Bitstring[10] == '1')
            {
                checkDBB.Checked = true;
            }

            if (!Bitstring.Split('-')[1].Equals(String.Empty))
            {
                textSonderEtageB.Text = Bitstring.Split('-')[1];
            }

        }

        private String buildBitstringEtagen(Boolean Selector) {

            String[] ret = new String[11];
            CheckBox[] boxes;

            if (Selector)       // Selector ist True wenn A gemeint ist, false bei B
            {
                boxes = new CheckBox[] {
                checkKellerA,checkEGA,checkHPA,checkSTA,checkMAA,
                checkOG1A,checkOG2A,checkOG3A,checkOG4A,checkOG5A,checkDBA
                };
            }
            else
            {
                boxes = new CheckBox[] {
                checkKellerB,checkEGB,checkHPB,checkSTB,checkMAB,
                checkOG1B,checkOG2B,checkOG3B,checkOG4B,checkOG5B,checkDBB
                };
            }

            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i].Checked)
                {
                    ret[i] = "1";
                }
                else {
                    ret[i] = "0";
                }
            }

            String Etagen = String.Join("", ret);
            if (Selector)
            {
                if (!textSonderEtageA.Text.Equals(String.Empty))
                {
                   Etagen = Etagen.Insert(Etagen.Length, ("-" + textSonderEtageA.Text));
                }
                else
                {
                    Etagen = Etagen.Insert(Etagen.Length, ("-"));
                }
            }
            else
            {
                if (!textSonderEtageB.Text.Equals(String.Empty))
                {
                    Etagen = Etagen.Insert(Etagen.Length, ("-" + textSonderEtageB.Text));
                }
                else
                {
                    Etagen = Etagen.Insert(Etagen.Length, ("-"));
                }
            }
            return Etagen;
        }

        private void buttonBlockAlleAenderungen_Click(object sender, EventArgs e)
        {
            aenderungSpeichern();
        }
    }
}