﻿using Google.Apis.Calendar.v3.Data;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

        Events events;

        int UmzugSet = 0;
        int BesichtigungSet = 0;
        int EntruempelungSet = 0;
        int EinpackenSet = 0;
        int AuspackenSet = 0;
        

        public UmzuegeSearch()
        {
            InitializeComponent();

            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            //Maximale Kundennummer um OutOfBounds vorzubeugen

            MySqlCommand cmdReadKunde = new MySqlCommand("SELECT * FROM Kunden ORDER BY idKunden DESC LIMIT 1;", Program.conn);
            MySqlDataReader rdrKunde;

            try
            {
                rdrKunde = cmdReadKunde.ExecuteReader();
                while (rdrKunde.Read())
                {
                    maxKundennummer = rdrKunde.GetInt32(0);
                }
                rdrKunde.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.Text += sqlEx.ToString();
                return;
            }

            // Daten auf heute setzen

            dateAuspack.Value = DateTime.Now;
            dateEinpack.Value = DateTime.Now;
            dateUmzug.Value = DateTime.Now;
            dateBesicht.Value = DateTime.Now;
            dateEntruempel.Value = DateTime.Now;

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
            MySqlCommand cmdRead = new MySqlCommand("SELECT idKunden,Vorname,Erstelldatum FROM Kunden WHERE Nachname = '" + textSucheName.Text + "';", Program.conn);
            MySqlDataReader rdr;
            int tempCounter = 0;
            // Bricht, wenn mehr als 30 gleichnamige Kunden
            int[] nummern = new int[30];
            String[] daten = new String[30];
            String[] vornamen = new String[30];

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    nummern[tempCounter] = rdr.GetInt32(0);
                    vornamen[tempCounter] = rdr[1].ToString();
                    daten[tempCounter] = rdr.GetDateTime(2).ToShortDateString();                                        //   Fix steht aus
                    tempCounter += 1;
                }
                rdr.Close();
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



            MySqlCommand cmdRead = new MySqlCommand(such, Program.conn);
            MySqlDataReader rdr;
            int tempCounter = 0;
            // Bricht, wenn mehr als 30 gleichnamige Kunden
            int[] nummern = new int[30];
            String[] umzDaten = new String[30];

            try
            {

                rdr = cmdRead.ExecuteReader();

                while (rdr.Read())
                {
                    nummern[tempCounter] = rdr.GetInt32(0);
                    tempCounter += 1;
                }
                rdr.Close();
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
                umzugAenderungFuellem(nummern[0]);
            }
            else
            {                          // Mehrere Treffer
                for (int i = 0; i < tempCounter; i++)
                {
                    textSuchBox.AppendText("Umzug Nummer " + nummern[i] + " vom Datum " + umzDaten[i] + " \r\n");
                }
            }

        }

        public void umzugAenderungFuellem(int umzNum)
        {         // FulleM. Sic!           

            umzObj = new Umzug(umzNum);

            UserSpeicher = umzObj.UserChanged1;

            textUmzNummerBlock.Text = umzNum.ToString();
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
            if (umzObj.auszug.Aufzug1 == 1)
            {
                radioAufzugAJa.Checked = true;
            }
            else { radioAufzugANein.Checked = true; }
            //
            if (umzObj.einzug.Aufzug1 == 1)
            {
                radioAufzugBJa.Checked = true;
            }
            else { radioAufzugBNein.Checked = true; }
            //
            if (umzObj.auszug.HVZ1 == 1)
            {
                radioHVZAJa.Checked = true;
            }
            else if (umzObj.auszug.HVZ1 == 0)
            {
                radioHVZANein.Checked = true;
            }
            else { radioHVZAV.Checked = true; }
            //
            if (umzObj.einzug.HVZ1 == 1)
            {
                radioHVZBJa.Checked = true;
            }
            else if (umzObj.einzug.HVZ1 == 0)
            {
                radioHVZBNein.Checked = true;
            }
            else { radioHVZBV.Checked = true; }
            //
            //
            textLaufMeterA.Text = umzObj.auszug.Laufmeter1.ToString();
            textLaufMeterB.Text = umzObj.einzug.Laufmeter1.ToString();
            
            //
            numericKleiderkisten.Value = umzObj.Kleiderkartons1;
            numericMannZahl.Value = umzObj.Mann;
            numericArbeitszeit.Value = umzObj.Stunden;
            //
            if (umzObj.Schilder1 == 1)
            {
                radioSchilderJa.Checked = true;
            }
            else { radioSchilderNein.Checked = true; }
            //
            dateSchilderVerweildauer.Value = umzObj.SchilderZeit1;
            //
            if (umzObj.KuecheAb1 == 1)
            {
                radioKuecheAbJa.Checked = true;
            }
            else if (umzObj.KuecheAb1 == 0)
            {
                radioKuecheAbNein.Checked = true;
            }
            else { radioKuecheAbV.Checked = true; }
            //
            if (umzObj.KuecheAuf1 == 1)
            {
                radioKuecheAufJa.Checked = true;
            }
            else if (umzObj.KuecheAuf1 == 0)
            {
                radioKuecheAufNein.Checked = true;
            }
            else { radioKuecheAufV.Checked = true; }
            //
            if (umzObj.KuecheBau1 == 1)
            {
                radioKuecheIntern.Checked = true;
            }
            else { radioKuecheExtern.Checked = true; }
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
            textNoteBuero.Text = umzObj.NotizBuero1;
            textNoteFahrer.Text = umzObj.NotizFahrer1;
            textNoteKalender.Text = umzObj.NotizTitel1;
            
            //
            //Setzen der Terminzustände, elend umständlich.
            // Umzug
            if (umzObj.StatUmzug == 0)
            {
                radioUmzNein.Checked = true;
            }
            else if (umzObj.StatUmzug == 1)
            {
                radioUmzJa.Checked = true;
            }
            else if (umzObj.StatUmzug == 3)
            {
                radioUmzVorlaeufig.Checked = true;
            }
            else
            {
                radioUmzVllt.Checked = true;
            }
            // Besichtigung
            if (umzObj.StatBesichtigung == 0)
            {
                radioBesNein.Checked = true;
            }
            else
            {
                radioBesJa.Checked = true;
            }
            // Auspacken
            if (umzObj.StatAus == 0)
            {
                radioAusNein.Checked = true;
            }
            else if (umzObj.StatAus == 1)
            {
                radioAusJa.Checked = true;
            }
            else
            {
                radioAusVllt.Checked = true;
            }
            // Einpacken
            if (umzObj.StatEin == 0)
            {
                radioEinNein.Checked = true;
            }
            else if (umzObj.StatEin == 1)
            {
                radioEinJa.Checked = true;
            }
            else
            {
                radioEinVllt.Checked = true;
            }
            // Entrümpeln
            if (umzObj.StatRuempeln == 0)
            {
                radioEntNein.Checked = true;
            }
            else if (umzObj.StatRuempeln == 1)
            {
                radioEntJa.Checked = true;
            }
            else
            {
                radioEntVllt.Checked = true;
            }
            // umzugsZeit (Besichtigungszeit!)
            //String temp = rdr[51].ToString();
            DateTime transplant = new DateTime(2000, 1, 1, 0, 0, 0);

            TimeSpan t = umzObj.ZeitUmzug.TimeOfDay;
            timeBesichtigung.Value = transplant.Add(t);
            //
            // Setzen der Listboxen         CHANGE SWITHCASE?

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
            if (umzObj.auszug.AussenAufzug1 == 1)
            {
                radioAussenAufzugAJa.Checked = true;
            }
            else
            {
                radioAussenAufzugANein.Checked = true;
            }
            //
            if (umzObj.einzug.AussenAufzug1 == 1)
            {
                radioAussenAufzugBJa.Checked = true;
            }
            else
            {
                radioAussenAufzugBNein.Checked = true;
            }

            // Geändertes UI

            parseEtagen();

            if (umzObj.Versicherung == 1)
            {
                radioVersicherungJa.Checked = true;
            }
            else { radioVersicherungJa.Checked = false; }

            //
            if (umzObj.Einpacken1 == 1)
            {
                radioEinpackenJa.Checked = true;
            }
            else if (umzObj.Einpacken1 == 0)
            {
                radioEinpackenNein.Checked = true;
            }
            else { radioEinpackenV.Checked = true; }
            //
            if (umzObj.Auspacken1 == 1)
            {
                radioAuspackenJa.Checked = true;
            }
            else if (umzObj.Auspacken1 == 0)
            {
                radioAuspackenNein.Checked = true;
            }
            else { radioAuspackenV.Checked = true; }

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
            // Zweite Umzugsnummer füllen

            textUmzugsNummer.Text = textUmzNummerBlock.Text;

            // Google-Get

            events = Program.kalenderKundenFinder(textKundennummer.Text);
            // Personendaten aus dem Kunden ziehen

            MySqlCommand cmdReadKunde = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden=" + textKundennummer.Text + " ;", Program.conn);
            MySqlDataReader rdrKunde;

            try
            {
                rdrKunde = cmdReadKunde.ExecuteReader();
                while (rdrKunde.Read())
                {

                    textVorNachname.Text = rdrKunde[1] + " " + rdrKunde[2] + " " + rdrKunde[3];
                    textTelefonnummer.Text = rdrKunde[4] + "";
                    textHandynummer.Text = rdrKunde[5] + "";
                    textEmail.Text = rdrKunde[6] + "";
                    textNoteBuero.AppendText(rdrKunde[14].ToString());
                }
                rdrKunde.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.Text += sqlEx.ToString();
                return;
            }

        }

        private void parseEtagen()
        {

            if (umzObj.auszug.Stockwerke1.Length != 0)
            {

                string[] temp = umzObj.auszug.Stockwerke1.Split(',');

                foreach (var item in temp)
                {
                    if (item.Contains("K"))
                    {
                        checkKellerA.Checked = true;
                    }
                    else if (item.Contains("EG"))
                    {
                        checkEGA.Checked = true;
                    }
                    else if (item.Contains("DB"))
                    {
                        checkDBA.Checked = true;
                    }
                    else if (item.Contains("MA"))
                    {
                        checkMAA.Checked = true;
                    }
                    else if (item.Contains("ST"))
                    {
                        checkSTA.Checked = true;
                    }
                    else if (item.Contains("HP"))
                    {
                        checkHPA.Checked = true;
                    }
                    else if (item.Contains("1"))
                    {
                        checkOG1A.Checked = true;
                    }
                    else if (item.Contains("2"))
                    {
                        checkOG2A.Checked = true;
                    }
                    else if (item.Contains("3"))
                    {
                        checkOG3A.Checked = true;
                    }
                    else if (item.Contains("4"))
                    {
                        checkOG4A.Checked = true;
                    }
                    else if (item.Contains("5"))
                    {
                        checkOG5A.Checked = true;
                    }
                    else
                    {
                        textSonderEtageA.AppendText(item);
                    }
                }

            }

            if (umzObj.einzug.Stockwerke1.Length != 0)
            {

                string[] temp = umzObj.einzug.Stockwerke1.Split(',');

                foreach (var item in temp)
                {
                    if (item.Contains("K"))
                    {
                        checkKellerB.Checked = true;
                    }
                    else if (item.Contains("EG"))
                    {
                        checkEGB.Checked = true;
                    }
                    else if (item.Contains("DB"))
                    {
                        checkDBB.Checked = true;
                    }
                    else if (item.Contains("MA"))
                    {
                        checkMAB.Checked = true;
                    }
                    else if (item.Contains("ST"))
                    {
                        checkSTB.Checked = true;
                    }
                    else if (item.Contains("HP"))
                    {
                        checkHPB.Checked = true;
                    }
                    else if (item.Contains("1"))
                    {
                        checkOG1B.Checked = true;
                    }
                    else if (item.Contains("2"))
                    {
                        checkOG2B.Checked = true;
                    }
                    else if (item.Contains("3"))
                    {
                        checkOG3B.Checked = true;
                    }
                    else if (item.Contains("4"))
                    {
                        checkOG4B.Checked = true;
                    }
                    else if (temp.Contains("5"))
                    {
                        checkOG5B.Checked = true;
                    }
                    else
                    {
                        textSonderEtageB.AppendText(item);
                    }
                }


            }

        }

        private string StockwerkString(int x)
        {

            string stockwerketemp = "";
            if (x == 0)
            {
                if (checkKellerA.Checked) { stockwerketemp += "K,"; }
                if (checkEGA.Checked) { stockwerketemp += "EG,"; }
                if (checkDBA.Checked) { stockwerketemp += "DB,"; }
                if (checkMAA.Checked) { stockwerketemp += "MA,"; }
                if (checkSTA.Checked) { stockwerketemp += "ST,"; }
                if (checkHPA.Checked) { stockwerketemp += "HP,"; }
                if (checkOG1A.Checked) { stockwerketemp += "1,"; }
                if (checkOG2A.Checked) { stockwerketemp += "2,"; }
                if (checkOG3A.Checked) { stockwerketemp += "3,"; }
                if (checkOG4A.Checked) { stockwerketemp += "4,"; }
                if (checkOG5A.Checked) { stockwerketemp += "5,"; }
                if (textSonderEtageA.TextLength != 0)
                {
                    stockwerketemp += textSonderEtageA.Text;
                }
            }
            else if (x == 1)
            {
                if (checkKellerB.Checked) { stockwerketemp += "K,"; }
                if (checkEGB.Checked) { stockwerketemp += "EG,"; }
                if (checkDBB.Checked) { stockwerketemp += "DB,"; }
                if (checkMAB.Checked) { stockwerketemp += "MA,"; }
                if (checkSTB.Checked) { stockwerketemp += "ST,"; }
                if (checkHPB.Checked) { stockwerketemp += "HP,"; }
                if (checkOG1B.Checked) { stockwerketemp += "1,"; }
                if (checkOG2B.Checked) { stockwerketemp += "2,"; }
                if (checkOG3B.Checked) { stockwerketemp += "3,"; }
                if (checkOG4B.Checked) { stockwerketemp += "4,"; }
                if (checkOG5B.Checked) { stockwerketemp += "5,"; }
                if (textSonderEtageB.TextLength != 0)
                {
                    stockwerketemp += textSonderEtageB.Text;
                }
            }

            return stockwerketemp;

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
            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Umzuege WHERE idUmzuege=" + numericUmzugsnummer.Value + ";", Program.conn);
            MySqlDataReader rdr;

            int worked = 0;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    worked = 1;
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.Text += sqlEx.ToString();
                return;
            }

            if (worked == 0)
            {
                textUmzugLog.AppendText("Umzug nicht gefunden");
            }

            umzugAenderungFuellem(decimal.ToInt32(numericUmzugsnummer.Value));
        }

        public void absender(String befehl)
        {
            MySqlCommand cmdAdd = new MySqlCommand(befehl, Program.conn);
            try
            {
                cmdAdd.ExecuteNonQuery();
                textUmzugLog.AppendText("Änderung ausgeführt \r\n");
                umzugAenderungFuellem(int.Parse(textUmzNummerBlock.Text)); // Neuladen der Ansicht
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.AppendText(sqlEx.ToString());
                return;
            }
        }

        //
        //  Benötigte Strings für Anlegen von Terminen
        //
        private String KalenderString()
        {
            //Konstruktion String Kalerndereintragsinhalt
            // Name + Auszugsadresse
            String Body = textVorNachname.Text + "\r\n Aus: " + textStraßeA.Text + " " + textHausnummerA.Text + ", " + textPLZA.Text + " " + textOrtA.Text + "\r\n";

            // Geschoss + HausTyp

            if (radioAufzugAJa.Checked)
            {
                Body += "mit Aufzug \r\n";
            }
            else { Body += "ohne Aufzug \r\n"; }

            //Einzugsadresse
            Body += "\r\n Nach: " + textStraßeB.Text + " " + textHausnummerB.Text + ", " + textPLZB.Text + " " + textOrtB.Text + "\r\n";

            // Geschoss + HausTyp

            if (radioAufzugBJa.Checked)
            {
                Body += "mit Aufzug \r\n";
            }
            else { Body += "ohne Aufzug \r\n"; }

            // Kontaktdaten
            if (textTelefonnummer.Text != "0")
            {
                Body += "\r\n " + textTelefonnummer.Text;
            }
            if (textHandynummer.Text != "0")
            {
                Body += "\r\n " + textHandynummer.Text;
            }
            if (textEmail.Text != "x@y.z")
            {
                Body += "\r\n " + textEmail.Text;
            }
            Body += "\r\n Am: " + dateUmzug.Value.ToShortDateString() + "\r\n";

            // Büronotiz

            Body += textNoteBuero.Text;

            // Rückgabe fertiger Body
            return Body;
        }

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

        private String Header() {
            return textKundennummer.Text + " " + textVorNachname.Text + ", " + numericMannZahl.Value + " Mann, " + numericArbeitszeit.Value + " Stunden, " + AutoString() + " " + textNoteKalender.Text;
        }

        private String SchilderHeader()
        {
            return textKundennummer.Text + " " + textVorNachname.Text + ", Schilder stellen";
        }

        private String RaeumHeader()
        {
            return textKundennummer.Text + " " + textVorNachname.Text + ", ";//+ numericPacker.Value + " Mann, " + numericPackStunden.Value + " Stunden";
        }

        private void UmzugEinfuegen(int code) {
            if (code == 1) {
                //Umzug
                Program.kalenderEintragGanz(Header(), KalenderString(), hvzString(), 11, dateUmzug.Value.Date, dateUmzug.Value.Date.AddDays(decimal.ToInt32(numericUmzugsDauer.Value)));
                textUmzugLog.AppendText("Umzug hinzugefügt \r\n");
                Schilderstellen();              
            }
            else if (code == 2) {
                // Umzug
                Program.kalenderEintragGanz(Header(), KalenderString(), hvzString(), 10, dateUmzug.Value.Date, dateUmzug.Value.Date.AddDays(decimal.ToInt32(numericUmzugsDauer.Value)));
                textUmzugLog.AppendText("Vorläufiger Umzug hinzugefügt \r\n");
            }
            else if (code == 3) {
                // Umzug
                Program.kalenderEintragGanz(Header(), KalenderString(), hvzString(), 2, dateUmzug.Value.Date, dateUmzug.Value.Date.AddDays(decimal.ToInt32(numericUmzugsDauer.Value)));
                textUmzugLog.AppendText("Vorläufig gebuchter Umzug hinzugefügt \r\n");
                Schilderstellen();
            }
        }

        private void Schilderstellen() {
            //Schilder
            if (radioHVZAJa.Checked)
            {
                String Body = textStraßeA.Text + " " + textHausnummerA.Text + ", " + textPLZA.Text + " " + textOrtA.Text;
                Program.kalenderEintragGanz(SchilderHeader(), Body, "Auszug", 3, dateUmzug.Value.Date.AddDays(-6), dateUmzug.Value.Date.AddDays(-6));
                textUmzugLog.AppendText("Schilderstellen Auszug hinzugefügt \r\n");
            }

            if (radioHVZBJa.Checked)
            {
                String Body = textStraßeB.Text + " " + textHausnummerB.Text + ", " + textPLZB.Text + " " + textOrtB.Text;
                Program.kalenderEintragGanz(SchilderHeader(), Body, "Einzug", 3, dateUmzug.Value.Date.AddDays(-6), dateUmzug.Value.Date.AddDays(-6));
                textUmzugLog.AppendText("Schilderstellen Einzug hinzugefügt \r\n");
            }
        }

        private void Schilderloeschen() {

            foreach (var EventItem in events.Items) {
                if (EventItem.ColorId == "3") {
                    Program.EventDeleteId(EventItem.Id);
                }
            }            
        }

        private void UmzugLoeschen (int code) {
            if (code == 1)
            {
                Program.EventDeleteId(Program.EventListMatch(events, Umzug, "11"));
                textUmzugLog.AppendText("Umzug entfernt \r\n");
                Schilderloeschen();
            }
            else if (code == 2)
            {
                Program.EventDeleteId(Program.EventListMatch(events, Umzug, "10"));
                textUmzugLog.AppendText("Vorläufigen Umzug entfernt \r\n");
            }
            else if (code == 3) {
                Program.EventDeleteId(Program.EventListMatch(events, Umzug, "2"));
                textUmzugLog.AppendText("Vorläufig Gebuchten Umzug entfernt \r\n");
                Schilderloeschen();
            }

        }

        private void PackenHinzufuegen(int code) {

            switch (code) // 1 Einräumen, 2 Vllt Einräumen, 3 Ausräumen, 4 Vllt Ausräumen
            {
                case 1:
                    Program.kalenderEintragGanz(RaeumHeader(), KalenderString(), "", 5, dateEinpack.Value.Date, dateEinpack.Value.Date);
                    textUmzugLog.AppendText("Einpacken hinzugefügt \r\n");
                    break;

                case 2:
                    Program.kalenderEintragGanz(RaeumHeader(), KalenderString(), "", 6, dateEinpack.Value.Date, dateEinpack.Value.Date);
                    textUmzugLog.AppendText("Vorläufiges Einpacken hinzugefügt \r\n");
                    break;
                
                case 3:
                    Program.kalenderEintragGanz(RaeumHeader(), KalenderString(), "", 5, dateAuspack.Value.Date, dateAuspack.Value.Date);
                    textUmzugLog.AppendText("Auspacken hinzugefügt \r\n");
                    break;

                case 4:
                    Program.kalenderEintragGanz(RaeumHeader(), KalenderString(), "", 6, dateAuspack.Value.Date, dateAuspack.Value.Date);
                    textUmzugLog.AppendText("Vorläufiges Auspacken hinzugefügt \r\n");
                    break;
                
                default:
                    break;
            }
        }

        private void PackenLoeschen(int code) {
            switch (code) // 1 Einräumen, 2 Vllt Einräumen, 3 Ausräumen, 4 Vllt Ausräumen
            {
                case 1:
                    Program.EventDeleteId(Program.EventListMatch(events, Einpacken, "5"));
                    textUmzugLog.AppendText("Einpacken entfernt \r\n");
                    break;

                case 2:
                    Program.EventDeleteId(Program.EventListMatch(events, Einpacken, "6"));
                    textUmzugLog.AppendText("Vorläufiges Einpacken entfernt \r\n");
                    break;

                case 3:
                    Program.EventDeleteId(Program.EventListMatch(events, Auspacken, "5"));
                    textUmzugLog.AppendText("Auspacken entfernt \r\n");
                    break;

                case 4:
                    Program.EventDeleteId(Program.EventListMatch(events, Auspacken, "6"));
                    textUmzugLog.AppendText("Vorläufiges Auspacken entfernt \r\n");
                    break;

                default:
                    break;
            }
        }        
        
        //
        //
        //
        private void buttonBlockTermine_Click(object sender, EventArgs e)
        {

            Boolean pop = false;

            String InsertDaten = "UPDATE Umzuege SET datBesichtigung = " + "'" + Program.DateMachine(dateBesicht.Value) + "', " +
                "UserChanged = '" + UserSpeicher.ToString() + idBearbeitend.ToString() + "', " +
                "datUmzug = " + "'" + Program.DateMachine(dateUmzug.Value) + "', " +
                "datRuempelung = " + "'" + Program.DateMachine(dateEntruempel.Value) + "', " +
                "datEinpacken = " + "'" + Program.DateMachine(dateEinpack.Value) + "', " +
                "datAuspacken = " + "'" + Program.DateMachine(dateAuspack.Value) + "', " +
                "UmzugsDauer = " + numericUmzugsDauer.Value + ", " +
                "umzugsZeit = " + "'" + Program.ZeitMachine(timeBesichtigung.Value) + "', ";

            // Check und abgleich der Termine und TerminStats ... Google änderungscode!
            if (radioBesJa.Checked && BesichtigungSet != 1)
            {
                // Anlegen Besichtigung
                DateTime date = dateBesicht.Value.Date.Add(timeBesichtigung.Value.TimeOfDay);
                DateTime schluss = date.AddMinutes(60);
                Program.kalenderEintrag(textKundennummer.Text + " " + textVorNachname.Text, KalenderString(), 9, date, schluss);
                textUmzugLog.AppendText("Besichtigung hinzugefügt \r\n");
                // Merken
                InsertDaten += "StatBes = " + 1 + ", ";
            }
            else if (radioBesNein.Checked && BesichtigungSet != 0)
            {
                //find&kill Besichtigung           
                Program.EventDeleteId(Program.EventListMatch(events, Besichtigung, "9"));
                textUmzugLog.AppendText("Besichtigung gelöscht \r\n");
                //Merken
                InsertDaten += "StatBes = " + 0 + ", ";
            }

            else { refreshBesichtigung(); }
            //
            // UMZÜGE
            //


            if (radioUmzNein.Checked && UmzugSet != 0)
            {
                UmzugLoeschen(UmzugSet);
                InsertDaten += "StatUmz = " + 0 + ", ";
                pop = true;
            }

            else if (radioUmzJa.Checked && UmzugSet != 1)
            {
                UmzugLoeschen(UmzugSet);
                UmzugEinfuegen(1);
                InsertDaten += "StatUmz = " + 1 + ", ";
            }

            else if (radioUmzVllt.Checked && UmzugSet != 2)
            {
                UmzugLoeschen(UmzugSet);
                UmzugEinfuegen(2);
                InsertDaten += "StatUmz = " + 2 + ", ";
            }


            else if (radioUmzVorlaeufig.Checked && UmzugSet != 3)
            {
                UmzugLoeschen(UmzugSet);
                UmzugEinfuegen(3);
                InsertDaten += "StatUmz = " + 3 + ", ";
                pop = true;
            }
            else { refreshUmzug(); }

            //
            // EINPACKEN
            //

            if (radioEinJa.Checked && EinpackenSet != 1)
            {
                PackenLoeschen(EinpackenSet);
                PackenHinzufuegen(1);
                InsertDaten += "StatEin = " + 1 + ", ";
            }

            else if (radioEinVllt.Checked && EinpackenSet != 2)
            {
                PackenLoeschen(EinpackenSet);
                PackenHinzufuegen(2);
                InsertDaten += "StatEin = " + 2 + ", ";
            }

            else if (radioEinNein.Checked && EinpackenSet != 0)
            {
                PackenLoeschen(EinpackenSet);
                InsertDaten += "StatEin = " + 0 + ", ";
            }
            else { refreshEinraeumen(); }

            // Auspacken

            if (radioAusJa.Checked && AuspackenSet != 1)
            {
                if (AuspackenSet != 0)
                {
                    PackenLoeschen(4);
                }
                PackenHinzufuegen(3);
                InsertDaten += "StatAus = " + 1 + ", ";
            }

            else if (radioAusVllt.Checked && AuspackenSet != 2)
            {
                if (AuspackenSet != 0)
                {
                    PackenLoeschen(3);
                }
                PackenHinzufuegen(4);
                InsertDaten += "StatAus = " + 2 + ", ";
            }

            else if (radioAusNein.Checked && AuspackenSet != 0)
            {
                PackenLoeschen(AuspackenSet);
                InsertDaten += "StatAus = " + 0 + ", ";
            }
            else { refreshAusraeumen(); }
            //
            // ENTRÜMPELN
            //

            if (radioEntJa.Checked && EntruempelungSet != 1)
            {
                // Vorläufige Entrümpelung entfernen
                if (EntruempelungSet == 2)
                {
                    Program.EventDeleteId(Program.EventListMatch(events, Entruempelung, "10"));
                    textUmzugLog.AppendText("Vorläufiges Entrümpeln entfernt \r\n");
                }
                // Entrümpelung hinzufügen
                String Header = textKundennummer.Text + " " + textVorNachname.Text + ", " + numericEinPacker.Value + " Mann, " + numericEinPackStunden.Value + " Stunden ENTRÜMPELN, ";
                Program.kalenderEintragGanz(Header, KalenderString(), "", 11, dateEntruempel.Value.Date, dateEntruempel.Value.Date);
                textUmzugLog.AppendText("Entrümpeln hinzugefügt \r\n");

                InsertDaten += "StatEnt = " + 1 + ", ";
            }

            else if (radioEntVllt.Checked && EntruempelungSet != 2)
            {
                // Entrümpeln entfernen
                if (EntruempelungSet == 1)
                {
                    Program.EventDeleteId(Program.EventListMatch(events, Entruempelung, "11"));
                    textUmzugLog.AppendText("Entrümpeln entfernt \r\n");
                }
                // Vorläufiges Entrümpeln hinzufügen
                String Header = textKundennummer.Text + " " + textVorNachname.Text + ", " + numericEinPacker.Value + " Mann, " + numericEinPackStunden.Value + " Stunden ENTRÜMPELN, ";
                Program.kalenderEintragGanz(Header, KalenderString(), "", 10, dateEntruempel.Value.Date, dateEntruempel.Value.Date);
                textUmzugLog.AppendText("Vorläufiges Entrümpeln hinzugefügt \r\n");

                InsertDaten += "StatEnt = " + 2 + ", ";
            }
            else if (radioEntNein.Checked && EntruempelungSet != 0)
            {
                // Entrümpeln entfernen
                if (EntruempelungSet == 1)
                {
                    Program.EventDeleteId(Program.EventListMatch(events, Entruempelung, "11"));
                    textUmzugLog.AppendText("Entrümpeln entfernt \r\n");
                }

                // Vorläufige Entrümpelung entfernen
                if (EntruempelungSet == 2)
                {
                    Program.EventDeleteId(Program.EventListMatch(events, Entruempelung, "10"));
                    textUmzugLog.AppendText("Vorläufiges Entrümpeln entfernt \r\n");
                }

                InsertDaten += "StatEnt = " + 0 + ", ";
            }

            // Letzes Komma wegnehmen
            InsertDaten = InsertDaten.Remove(InsertDaten.Length - 2);
            InsertDaten += " WHERE idUmzuege = " + textUmzNummerBlock.Text + ";";
            absender(InsertDaten);

            if (pop)
            {
                popUp();
            }
            erinnerungsPopup();
        }

        private void buttonBlockKueche_Click_1(object sender, EventArgs e)
        {
            //Deklaration und setzen der Werte

            int kuecheab = 0; ;
            int kuecheauf = 0;
            int kuechebau = 0;
            String kuechepausch = "0";

            if (radioKuecheAbJa.Checked)
            {
                kuecheab = 1;
            }
            else if (radioKuecheAbNein.Checked)
            {
                kuecheab = 0;
            }
            else { kuecheab = 2; }

            if (radioKuecheAufJa.Checked)
            {
                kuecheauf = 1;
            }
            else if (radioKuecheAufNein.Checked)
            {
                kuecheauf = 0;
            }
            else { kuecheauf = 2; }

            if (radioKuecheIntern.Checked)          // Küchenbau Intern = 1, Extern = 0;
            {
                kuechebau = 1;
            }
            else { kuechebau = 0; }

            kuechepausch = textKuechenPreis.Text;
            
            // Setzen
            umzObj.KuecheAb1 = kuecheab;
            umzObj.KuecheAuf1 = kuecheauf;
            umzObj.KuecheBau1 = kuechebau;
            umzObj.KuechePausch1 = int.Parse(textKuechenPreis.Text);

            // Absenden
            umzObj.UpdateDB(idBearbeitend.ToString());
            umzugAenderungFuellem(int.Parse(textUmzNummerBlock.Text)); // Neuladen der Ansicht

            erinnerungsPopup();
        }

        private void buttonBlockPacken_Click_1(object sender, EventArgs e)
        {
            int einPacken = 0;
            int ausPacken = 0;

            if (radioEinpackenJa.Checked)
            {
                einPacken = 1;
            }
            else if (radioEinpackenNein.Checked)
            {
                einPacken = 0;
            }
            else { einPacken = 2; }
            //
            if (radioAuspackenJa.Checked)
            {
                ausPacken = 1;
            }
            else if (radioAuspackenNein.Checked)
            {
                ausPacken = 0;
            }
            else { ausPacken = 2; }
            
            // Setzen
            umzObj.Einpacken1 = einPacken;
            umzObj.Einpacker1 = decimal.ToInt32(numericEinPacker.Value);
            umzObj.EinStunden1 = decimal.ToInt32(numericEinPackStunden.Value);
            umzObj.Karton1 = decimal.ToInt32(numericEinPackKartons.Value);
            umzObj.Auspacken1 = ausPacken;
            umzObj.Auspacker1 = decimal.ToInt32(numericAusPacker.Value);
            umzObj.AusStunden1 = decimal.ToInt32(numericAusPackStunden.Value);

            // Absenden
            umzObj.UpdateDB(idBearbeitend.ToString());

            umzugAenderungFuellem(int.Parse(textUmzNummerBlock.Text)); // Neuladen der Ansicht
            //Kalender aktualisieren
            refreshAusraeumen();
            refreshEinraeumen();
            erinnerungsPopup();
        }

        private void buttonBlockDaten_Click(object sender, EventArgs e)
        {
            int schilder = 0;

            //
            if (radioSchilderJa.Checked)
            {
                schilder = 1;
            }
            else
            {
                schilder = 0;
            }

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

            // Absenden
            umzObj.UpdateDB(idBearbeitend.ToString());

            umzugAenderungFuellem(int.Parse(textUmzNummerBlock.Text)); // Neuladen der Ansicht
            //Kalender aktualisieren
            refreshUmzug();
            refreshBesichtigung();
            refreshEntruempeln();
            erinnerungsPopup();
        }

        private void buttonBlockBemerkungen_Click(object sender, EventArgs e)
        {            
            // Setzen
            umzObj.NotizFahrer1 = textNoteFahrer.Text;
            umzObj.NotizBuero1 = textNoteBuero.Text;
            umzObj.NotizTitel1 = textNoteKalender.Text;

            // Absenden
            umzObj.UpdateDB(idBearbeitend.ToString());

            umzugAenderungFuellem(int.Parse(textUmzNummerBlock.Text)); // Neuladen der Ansicht
            // Kalenderaktualisierung
            refreshAll();
            erinnerungsPopup();
        }

        private void buttonBlockEinzug_Click(object sender, EventArgs e)
        {
            int aufzug = 0;
            int hvz = 0;
            int aussenAuf = 0;

            if (radioAussenAufzugBJa.Checked)
            {
                aussenAuf = 1;
            }
            else
            {
                aussenAuf = 0;
            }
            //
            if (radioAufzugBJa.Checked)
            {
                aufzug = 1;
            }
            else
            {
                aufzug = 0;
            }
            //
            if (radioHVZBJa.Checked)
            {
                hvz = 1;
            }
            else if (radioHVZBNein.Checked)
            {
                hvz = 0;
            }
            else { hvz = 2; }

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
            umzObj.einzug.Stockwerke1 = StockwerkString(1);

            //Absenden
            umzObj.UpdateDB(idBearbeitend.ToString());

            umzugAenderungFuellem(int.Parse(textUmzNummerBlock.Text)); // Neuladen der Ansicht
            //Kalender aktualisieren
            refreshAll();
            erinnerungsPopup();
        }

        private void ButtonBlockAuszug_Click_1(object sender, EventArgs e)
        {
            int aufzug = 0;
            int hvz = 0;
            int aussenAuf = 0;

            if (radioAussenAufzugAJa.Checked)
            {
                aussenAuf = 1;
            }
            else
            {
                aussenAuf = 0;
            }
            //

            if (radioAufzugAJa.Checked)
            {
                aufzug = 1;
            }
            else
            {
                aufzug = 0;
            }
            //
            if (radioHVZAJa.Checked)
            {
                hvz = 1;
            }
            else if (radioHVZANein.Checked)
            {
                hvz = 0;
            }
            else { hvz = 2; }

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
            umzObj.auszug.Stockwerke1 = StockwerkString(0);

            //Absenden
            umzObj.UpdateDB(idBearbeitend.ToString());

            umzugAenderungFuellem(int.Parse(textUmzNummerBlock.Text)); // Neuladen der Ansicht

            //Kalender aktualisieren
            refreshAll();
            erinnerungsPopup();
        }

        private void buttonBlockVersicherung_Click(object sender, EventArgs e)
        {
            int VersTemp = 0;
            if (radioVersicherungJa.Checked) { VersTemp = 1; }

            //Absenden
            umzObj.UpdateDB(idBearbeitend.ToString());

            umzugAenderungFuellem(int.Parse(textUmzNummerBlock.Text)); // Neuladen der Ansicht

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
                MySqlCommand cmdSendL = new MySqlCommand(deleteL, Program.conn);
                try
                {
                    cmdSendL.ExecuteNonQuery();
                    textUmzugLog.AppendText("Laufzettel erfolgreich gelöscht\r\n");
                }
                catch (Exception sqlEx)
                {
                    textUmzugLog.AppendText(sqlEx.ToString());
                }
                //
                String deleteT = "DELETE FROM Transaktionen WHERE Umzuege_idUmzuege = " + textUmzNummerBlock.Text + " ;";
                MySqlCommand cmdSendT = new MySqlCommand(deleteT, Program.conn);
                try
                {
                    cmdSendT.ExecuteNonQuery();
                    textUmzugLog.AppendText("Transaktionen erfolgreich gelöscht\r\n");
                }
                catch (Exception sqlEx)
                {
                    textUmzugLog.AppendText(sqlEx.ToString());
                }
                //
                String delete = "DELETE FROM Umzuege WHERE idUmzuege = " + textUmzNummerBlock.Text + " ;";
                MySqlCommand cmdSend = new MySqlCommand(delete, Program.conn);
                try
                {
                    cmdSend.ExecuteNonQuery();
                    textUmzugLog.AppendText("Umzug erfolgreich gelöscht\r\n");
                }
                catch (Exception sqlEx)
                {
                    textUmzugLog.AppendText(sqlEx.ToString());
                }

                UmzugLoeschen(umzObj.StatUmzug);
                PackenLoeschen(umzObj.StatEin);
                if (umzObj.StatAus == 1)
                {
                    PackenLoeschen(3);
                }
                else if (umzObj.StatAus == 2) {
                    PackenLoeschen(4);
                }
            }
            else
            {
                textUmzugLog.AppendText("Löschvorgang abgebrochen\r\n");
            }
        }

        private void buttonDruk_Click(object sender, EventArgs e)
        {
            PdfDocument pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Besichtigungs Vordruck.pdf")), new PdfWriter(Program.druckPfad));          

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;

            String Name = ""; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.
            switch (UserSpeicher[0])
            {
                case '0':
                    Name = "Rita";
                    break;

                case '1':
                    Name = "Jonas";
                    break;

                case '2':
                    Name = "Eva";
                    break;

                case '3':
                    Name = "Jan";
                    break;

                default:
                    break;
            }
            
            // Vergleihstermin
            DateTime stand = new DateTime(2017, 1, 1);

            try
            {
                fields.TryGetValue("NameKundennummer", out toSet);
                toSet.SetValue(textKundennummer.Text + " " + textVorNachname.Text);

                // Telefonnummern sauber auflösen
                if (textTelefonnummer.Text != "0" && textHandynummer.Text != "0")
                {
                    fields.TryGetValue("Telefonnummer", out toSet);
                    toSet.SetValue(textTelefonnummer.Text + " / " + textHandynummer.Text);
                }
                else if (textTelefonnummer.Text == "0" && textHandynummer.Text != "0")
                {
                    fields.TryGetValue("Telefonnummer", out toSet);
                    toSet.SetValue(textHandynummer.Text);
                }
                else if (textTelefonnummer.Text != "0" && textHandynummer.Text == "0")
                {
                    fields.TryGetValue("Telefonnummer", out toSet);
                    toSet.SetValue(textTelefonnummer.Text);
                }
                else
                {
                    fields.TryGetValue("Telefonnummer", out toSet);
                    toSet.SetValue("Keine Nummer!");
                }

                fields.TryGetValue("Email", out toSet);
                toSet.SetValue(textEmail.Text);

                fields.TryGetValue("DatumZeichen", out toSet);
                toSet.SetValue(DateTime.Now.Date.ToShortDateString() + " " + Name);

                if (dateBesicht.Value.ToShortDateString() != stand.ToShortDateString())
                {
                    fields.TryGetValue("TerminBes", out toSet);
                    toSet.SetValue(dateBesicht.Value.ToShortDateString());
                }

                if (dateUmzug.Value.ToShortDateString() != stand.ToShortDateString())
                {
                    fields.TryGetValue("TerminUmz", out toSet);
                    toSet.SetValue(dateUmzug.Value.ToShortDateString());
                }

                if (dateEntruempel.Value.ToShortDateString() != stand.ToShortDateString())
                {
                    fields.TryGetValue("TerminEnt", out toSet);
                    toSet.SetValue(dateEntruempel.Value.ToShortDateString());
                }

                fields.TryGetValue("Umzugsnummer", out toSet);
                toSet.SetValue(textUmzugsNummer.Text);

                // Auszugsadresse
                fields.TryGetValue("StrasseA", out toSet);
                toSet.SetValue(textStraßeA.Text + " " + textHausnummerA.Text);

                fields.TryGetValue("OrtA", out toSet);
                toSet.SetValue(textPLZA.Text + " " + textOrtA.Text);

                //Geschossigkeit

                fields.TryGetValue("StockwerkA", out toSet);
                toSet.SetValue(umzObj.auszug.KalenderStringEtageHaustyp());

                if (textLaufMeterA.Text != "0")
                {
                    fields.TryGetValue("TragwegA", out toSet);
                    toSet.SetValue(textLaufMeterA.Text);
                }

                fields.TryGetValue("HausStatusA", out toSet);
                toSet.SetValue(listBoxA.SelectedItem.ToString());

                if (radioHVZAJa.Checked)
                {
                    fields.TryGetValue("HVZAJa", out toSet);
                    toSet.SetValue("Yes");
                }
                //if (radioHVZAV.Checked)
                //{
                //    fields.TryGetValue("HVZAVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                //if (radioHVZANein.Checked)
                //{
                //    fields.TryGetValue("HVZANein", out toSet);
                //    toSet.SetValue("Yes");
                //}

                //    if (radioAufzugAJa.Checked)
                //    {
                //        fields.TryGetValue("AufzugBJa", out toSet);
                //        toSet.SetValue("Yes");
                //    }

                //    if (radioAufzugANein.Checked)
                //    {
                //        fields.TryGetValue("AufzugANein", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //
                //    if (radioAussenAufzugAJa.Checked)
                //    {
                //        fields.TryGetValue("AussenAufzugAJa", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    if (radioAussenAufzugANein.Checked)
                //    {
                //        fields.TryGetValue("AussenAufzugANein", out toSet);
                //        toSet.SetValue("Yes");
                //    }

                //    // Adresse Einzug
                //    fields.TryGetValue("StrasseB", out toSet);
                //    toSet.SetValue(textStraßeB.Text + " " + textHausnummerB.Text);

                //    fields.TryGetValue("OrtB", out toSet);
                //    toSet.SetValue(textPLZB.Text + " " + textOrtB.Text);

                //    //Geschossigkeit

                //    //fields.TryGetValue("StockwerkB", out toSet);
                //    //toSet.SetValue(umzObj.einzug.KalenderStringEtageHaustyp());

                //    if (textLaufMeterB.Text != "0")
                //    {
                //        fields.TryGetValue("TragwegB", out toSet);
                //        toSet.SetValue(textLaufMeterB.Text);
                //    }

                //    //fields.TryGetValue("HausStatusB", out toSet);
                //    //toSet.SetValue(listBoxB.SelectedItem.ToString());

                //    if (radioHVZBJa.Checked)
                //    {
                //        fields.TryGetValue("HVZBJa", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //if (radioHVZBV.Checked)
                //    //{
                //    //    fields.TryGetValue("HVZBVllt", out toSet);
                //    //    toSet.SetValue("Yes");
                //    //}
                //    if (radioHVZBNein.Checked)
                //    {
                //        fields.TryGetValue("HVZBNein", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //
                //    if (radioAufzugBJa.Checked)
                //    {
                //        fields.TryGetValue("AufzugBJa", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    if (radioAufzugBNein.Checked)
                //    {
                //        fields.TryGetValue("AufzugBNein", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //
                //    if (radioAussenAufzugBJa.Checked)
                //    {
                //        fields.TryGetValue("AussenAufzugBJa", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    if (radioAussenAufzugBNein.Checked)
                //    {
                //        fields.TryGetValue("AussenAufzugBNein", out toSet);
                //        toSet.SetValue("Yes");
                //    }

                //    // Packen

                //    //
                //    if (radioEinpackenJa.Checked)
                //    {
                //        fields.TryGetValue("EinJa", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //if (radioEinpackenV.Checked)
                //    //{
                //    //    fields.TryGetValue("EinVllt", out toSet);
                //    //    toSet.SetValue("Yes");
                //    //}
                //    if (radioEinpackenNein.Checked)
                //    {
                //        fields.TryGetValue("EinNein", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //
                //    if (radioAuspackenJa.Checked)
                //    {
                //        fields.TryGetValue("AusJa", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //if (radioAuspackenV.Checked)
                //    //{
                //    //    fields.TryGetValue("AusVllt", out toSet);
                //    //    toSet.SetValue("Yes");
                //    //}
                //    if (radioAuspackenNein.Checked)
                //    {
                //        fields.TryGetValue("AusNein", out toSet);
                //        toSet.SetValue("Yes");
                //    }

                //    //Küche
                //    if (radioKuecheAbJa.Checked)
                //    {
                //        fields.TryGetValue("KuecheAbJa", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //if (radioKuecheAbV.Checked)
                //    //{
                //    //    fields.TryGetValue("KuecheAbVllt", out toSet);
                //    //    toSet.SetValue("Yes");
                //    //}
                //    if (radioKuecheAbNein.Checked)
                //    {
                //        fields.TryGetValue("KuecheAbNein", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //
                //    if (radioKuecheAufJa.Checked)
                //    {
                //        fields.TryGetValue("KuecheAufJa", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //if (radioKuecheAufV.Checked)
                //    //{
                //    //    fields.TryGetValue("KuecheAufVllt", out toSet);
                //    //    toSet.SetValue("Yes");
                //    //}
                //    if (radioKuecheAufNein.Checked)
                //    {
                //        fields.TryGetValue("KuecheAufNein", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //
                //    //if (radioKuecheExtern.Checked)
                //    //{
                //    //    fields.TryGetValue("KuecheExtern", out toSet);
                //    //    toSet.SetValue("Yes");
                //    //}
                //    if (radioKuecheIntern.Checked)
                //    {
                //        fields.TryGetValue("KuecheIntern", out toSet);
                //        toSet.SetValue("Yes");
                //    }
                //    //
                //    if (textKuechenPreis.Text != "0")
                //    {
                //        fields.TryGetValue("KuechePreis", out toSet);
                //        toSet.SetValue(textKuechenPreis.Text);
                //    }

                //    // Restdaten
                //    if (numericMannZahl.Value != 0)
                //    {
                //        fields.TryGetValue("Mann", out toSet);
                //        toSet.SetValue(numericMannZahl.Value.ToString());
                //    }

                //    if (numericArbeitszeit.Value != 0)
                //    {
                //        fields.TryGetValue("Stunden", out toSet);
                //        toSet.SetValue(numericArbeitszeit.Value.ToString());
                //    }

                //    fields.TryGetValue("Autos", out toSet);
                //    toSet.SetValue(AutoString());

                //    if (numericKleiderkisten.Value != 0)
                //    {
                //        fields.TryGetValue("Kleiderkisten", out toSet);
                //        toSet.SetValue(numericKleiderkisten.Value.ToString());
                //    }

                //    //Bemerkungen
                //    fields.TryGetValue("NoteBuero", out toSet);
                //    toSet.SetValue(textNoteBuero.Text);

                //    fields.TryGetValue("NoteFahrer", out toSet);
                //    toSet.SetValue(textNoteFahrer.Text);

                form.FlattenFields();
                pdf.Close();
                Program.SendToPrinter();
                textUmzugLog.AppendText("Erfolgreich gedruckt");
            }
            catch (Exception ex)
            {
                textUmzugLog.AppendText(ex.ToString());
            }
        }

        void refreshUmzug() {               // Hier waren vorher zig Zeilen.
            UmzugLoeschen(UmzugSet);
            UmzugEinfuegen(UmzugSet);

            // Eventliste refreshen
            events = Program.kalenderKundenFinder(textKundennummer.Text);
        }

        void refreshBesichtigung() {
            if (umzObj.StatBesichtigung == 1)
            {
                //find&kill Besichtigung           
                Program.EventDeleteId(Program.EventListMatch(events, Besichtigung, "9"));
                textUmzugLog.AppendText("Besichtigung gelöscht \r\n");
                // Anlegen Besichtigung
                DateTime date = dateBesicht.Value.Date.Add(timeBesichtigung.Value.TimeOfDay);
                DateTime schluss = date.AddMinutes(60);
                Program.kalenderEintrag(textKundennummer.Text + " " + textVorNachname.Text + " " + textNoteKalender.Text, KalenderString(), 9, date, schluss);
                textUmzugLog.AppendText("Besichtigung hinzugefügt \r\n");
            }

            // Eventliste refreshen
            events = Program.kalenderKundenFinder(textKundennummer.Text);
        }

        void refreshAusraeumen () {
            
            if (umzObj.StatAus == 1)
            {
                PackenLoeschen(3);
                PackenHinzufuegen(3);
            }
            else if (umzObj.StatAus == 2)
            {
                PackenLoeschen(4);
                PackenHinzufuegen(4);
            }

            // Eventliste refreshen
            events = Program.kalenderKundenFinder(textKundennummer.Text);
        }

        void refreshEinraeumen ()
        {
            if (umzObj.StatEin == 1)
            {
                PackenLoeschen(1);
                PackenHinzufuegen(1);
            }
            else if (umzObj.StatEin == 2)
            {
                PackenLoeschen(2);
                PackenHinzufuegen(2);
            }

            // Eventliste refreshen
            events = Program.kalenderKundenFinder(textKundennummer.Text);
        }

        void refreshEntruempeln() {
            if (umzObj.StatRuempeln == 2)
            {
                // Entruempeln entfernen
                Program.EventDeleteId(Program.EventListMatch(events, Entruempelung, "10"));
                textUmzugLog.AppendText("Vorläufiges Entrümpeln entfernt \r\n");

                // Vorläufiges Entrümpeln hinzufügen
                String Header = textKundennummer.Text + " " + textVorNachname.Text + ", " + numericEinPacker.Value + " Mann, " + numericEinPackStunden.Value + " Stunden ENTRÜMPELN, ";
                Program.kalenderEintragGanz(Header, KalenderString(), "", 10, dateEntruempel.Value.Date, dateEntruempel.Value.Date);
                textUmzugLog.AppendText("Vorläufiges Entrümpeln hinzugefügt \r\n");
            }
            else if (umzObj.StatRuempeln == 1) {
                // Entrümpeln entfernen
                Program.EventDeleteId(Program.EventListMatch(events, Entruempelung, "11"));
                textUmzugLog.AppendText("Entrümpeln entfernt \r\n");

                // Entrümpelung hinzufügen
                String Header = textKundennummer.Text + " " + textVorNachname.Text + ", " + numericEinPacker.Value + " Mann, " + numericEinPackStunden.Value + " Stunden ENTRÜMPELN, ";
                Program.kalenderEintragGanz(Header, KalenderString(), "", 11, dateEntruempel.Value.Date, dateEntruempel.Value.Date);
                textUmzugLog.AppendText("Entrümpeln hinzugefügt \r\n");
            }
        }

        void refreshAll() {
            refreshUmzug();
            refreshBesichtigung();
            refreshAusraeumen();
            refreshEinraeumen();
            refreshEntruempeln();
            // Eventliste refreshen
            events = Program.kalenderKundenFinder(textKundennummer.Text);
            return;
        }

        void resetEvents() {
            events = Program.kalenderKundenFinder(textKundennummer.Text);
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

        private void erinnerungsPopup () {
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

    }
}