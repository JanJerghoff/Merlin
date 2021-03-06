﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kartonagen
{
    public partial class KundenSearch : Form
    {
        int maxKundennummer;
        string UserChanged;

        public KundenSearch()
        {
            this.Icon = Properties.Resources.icon_Fnb_icon;
            InitializeComponent();

            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            //Maximale Kundennummer um OutOfBounds vorzubeugen

            if (Program.conn.State != ConnectionState.Open)
            {
                Program.conn.Open();
            }
            try
            {
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
                textKundenSearchLog.Text += sqlEx.ToString();
                return;
            }
        }

        private int Bearbeiter = 0;

        public void setBearbeiter(int wer) {
            Bearbeiter = wer;
        }

        public void auffüllen(int nummer)
        {

            // Ausführung Abfrage
            

            int worked = 0;

            if (Program.conn.State != ConnectionState.Open)
            {
                Program.conn.Open();
            }
            try
            {
                MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden=" + nummer + ";", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    textShowAnrede.Text = rdr[1].ToString();                                                                                                
                    textShowVorname.Text = rdr[2].ToString();
                    textShowNachname.Text = rdr[3].ToString();
                    textShowTelefonnummer.Text = rdr[4].ToString();
                    textShowHandynummer.Text = rdr[5].ToString();
                    textShowEmail.Text = rdr[6].ToString();
                    textShowStraße.Text = rdr[7].ToString();
                    textShowHausnummer.Text = rdr[8].ToString();
                    textShowPLZ.Text = rdr[9].ToString();
                    textShowOrt.Text = rdr[10].ToString();
                    textShowLand.Text = rdr[11].ToString();
                    UserChanged = rdr[12].ToString();
                    textShowBemerkung.Text = rdr[14].ToString();
                    worked = 1;
                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auffüllen der Kundendaten \r\n Bereits dokumentiert.");
                return;
            }

            if (worked == 0)
            {
                textKundenSearchLog.AppendText("Kein Kunde mit dieser Nummer gefunden \r\n");
            }
            // Anzeigen der gefundenen Kundennummer an allen möglichen Stellen
            numericSucheKundennr.Value = nummer;
            numericAendernNummer.Value = nummer;

            // Letzte Änderung auslesen
            String temp = UserChanged[UserChanged.Length-1].ToString();
            int wer = int.Parse(temp);
            textGeändertVon.Text = Program.getBearbeitender(wer);
                        
        }

        private void buttonKundenSearchNrSuche_Click(object sender, EventArgs e)
        {
            //Validierung Eingabefeld
            if (numericSucheKundennr.Value == 1000) {
                textKundenSearchLog.AppendText("Bitte eine Kundennummer eingeben, nach der gesucht werden soll. 1000 ist ungültig \r\n");
                return;
            }

            if (maxKundennummer >= numericSucheKundennr.Value)
            {
                auffüllen(decimal.ToInt32(numericSucheKundennr.Value));
            }
            else
            {
                textKundenSearchLog.AppendText("Kein Kunde mit dieser Nummer vorhanden \r\n");
            }
                       
        }
        
        private void buttonKundenSearchNameSuche_Click(object sender, EventArgs e)
        {
            
            int tempCounter = 0;
            // Bricht, wenn mehr als 30 gleichnamige Kunden
            int[] nummern = new int[30];
            String[] daten = new String[30];
            String[] vornamen = new String[30];

            if (Program.conn.State != ConnectionState.Open)
            {
                Program.conn.Open();
            }
            try
            {

                MySqlCommand cmdRead = new MySqlCommand("SELECT idKunden,Vorname,Erstelldatum FROM Kunden WHERE Nachname = '" + textSucheName.Text + "';", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();

                while (rdr.Read())
                {
                    nummern[tempCounter] = rdr.GetInt32(0);
                    vornamen[tempCounter] = rdr[1].ToString();
                    // test = rdr.GetDateTime(2).ToShortDateString();                                           Fix steht aus
                    tempCounter += 1;
                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim finden des Kunden in der DB \r\n Bereits dokumentiert.");
                return;
            }

            // Entscheidung einzelner / mehrere Treffer

            if (tempCounter == 0)           // Kein Treffer
            {
                textKundenSearchLog.AppendText("Fehler: Kein Kunde zum Namen gefunden \r\n");
                return;
            }
            else if (tempCounter == 1)      // Ein treffer
            {
                auffüllen(nummern[0]);
            }
            else {                          // Mehrere Treffer
                for (int i = 0; i < tempCounter; i++) {
                    textKundenSearchResult.AppendText(vornamen[i] + " " + textSucheName.Text + " hinzugefügt am" + daten[i] + ": "+nummern[i]+" \r\n"); 
                }
            }

        }        

        //  Einzelne Updatefunktionen
        //  für die jeweiligen Felder

        private void buttonKundeAendernAnrede_Click(object sender, EventArgs e)
        {
            if (textAendernAnrede.Text != "Herr" && textAendernAnrede.Text != "Frau" && textAendernAnrede.Text != "Familie" && textAendernAnrede.Text != "Firma")
            {
                textKundenSearchLog.AppendText("Kunde ändern gescheitert, Anrede muss Herr oder Frau sein \r\n");
                return;
            }
            String change = "UPDATE Kunden SET Anrede = '" + textAendernAnrede.Text + "', UserChanged = "+UserChanged+Bearbeiter+" WHERE idKunden = " + numericAendernNummer.Value + ";";
            Program.absender(change,"Kunden ändern: Anrede");
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernVorname_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Vorname = '" + textAendernVorname.Text + "', UserChanged = " + UserChanged + Bearbeiter + " WHERE idKunden = " + numericAendernNummer.Value + ";";
            Program.absender(change, "Kunden ändern: Vorname");
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernNachname_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Nachname = '" + textAendernNachname.Text + "', UserChanged = " + UserChanged + Bearbeiter + " WHERE idKunden = " + numericAendernNummer.Value + ";";
            Program.absender(change, "Kunden ändern: Nachname");
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernTelefonnummer_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Telefonnummer = '" + textAendernTelefonnummer.Text + "', UserChanged = " + UserChanged + Bearbeiter + " WHERE idKunden = " + numericAendernNummer.Value + ";";
            Program.absender(change, "Kunden ändern: Telefonnummer");
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernHandynummer_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Handynummer = '" + textAendernHandynummer.Text + "', UserChanged = " + UserChanged + Bearbeiter + " WHERE idKunden = " + numericAendernNummer.Value + ";";
            Program.absender(change, "Kunden ändern: Handynummer");
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernEmail_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Email = '" + textAendernEmail.Text + "', UserChanged = " + UserChanged + Bearbeiter + " WHERE idKunden = " + numericAendernNummer.Value + ";";
            Program.absender(change, "Kunden ändern: Email");
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernStraße_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Straße = '" + textAendernStraße.Text + "', Hausnummer = '"+textAendernHausnummer.Text+ "', UserChanged = " + UserChanged + Bearbeiter + " WHERE idKunden = " + numericAendernNummer.Value + ";";
            Program.absender(change, "Kunden ändern: Straße");
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
            UmzugsAdresseAendern();
        }

        private void buttonKundeAendernPLZ_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET PLZ = " + textAendernPLZ.Text + ", Ort = '" + textAendernOrt + "', UserChanged = " + UserChanged + Bearbeiter + " WHERE idKunden = " + numericAendernNummer.Value + ";";
            Program.absender(change, "Kunden ändern: PLZ");
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
            UmzugsAdresseAendern();
        }

        private void buttonKundeAendernLand_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Land = '" + textAendernLand.Text + "', UserChanged = " + UserChanged + Bearbeiter + " WHERE idKunden = " + numericAendernNummer.Value + ";";
            Program.absender(change, "Kunden ändern: Land");
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernBemerkung_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Bemerkung = '" + textAendernBemerkung.Text + "', UserChanged = " + UserChanged + Bearbeiter + " WHERE idKunden = " + numericAendernNummer.Value + ";";
            Program.absender(change, "Kunden ändern: Bemerkung");
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void UmzugsAdresseAendern ()
        {
            // Merken der Umzugsnummern und Daten
            List<int> umzNr = new List<int>();
            List<string> umzDat = new List<string>();
            String aufzaehlung = "";

            // Checken ob aktiver Umzug
            
            if (Program.conn.State != ConnectionState.Open)
            {
                Program.conn.Open();
            }
            try
            {
                MySqlCommand cmdRead = new MySqlCommand("SELECT u.idUmzuege, u.datUmzug FROM Umzuege u, Umzugsfortschritt f WHERE u.Kunden_idKunden = " + numericAendernNummer.Value + " AND u.idUmzuege = f.Umzuege_idUmzuege AND f.abgeschlossen = 8;", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();

                while (rdr.Read())
                {
                    umzNr.Add(rdr.GetInt32(0));
                    umzDat.Add(rdr.GetDateTime(1).ToShortDateString());                    
                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Umzugsadresse \r\n Bereits dokumentiert.");
                return;
            }

            int count = 0;
            foreach (var item in umzNr)
            {
                aufzaehlung += "Umzug " + item + " am " + umzDat[count]+", \r\n ";
                count++;
            }


            // Abfrage ob Änderung übernommen werden soll.
            if (aufzaehlung.Length > 0)
            {
                var bestätigung = MessageBox.Show("Sollen die Änderungen an der Adresse in die Auszugsadresse der folgenden Umzuege übernommen werden? \r\n "+aufzaehlung, "Adresse in Umzüge übernehmen?", MessageBoxButtons.YesNo);
                if (bestätigung == DialogResult.Yes)
                                   
                    
                {
                    // Präparieren der änderungen
                    string Straße = "";
                    string Hausnummer = "";
                    string Ort = "";
                    string PLZ = "";

                    if (textAendernStraße.TextLength != 0)
                    {
                        Straße = textAendernStraße.Text;
                    }
                    else { Straße = textShowStraße.Text; }

                    if (textAendernHausnummer.TextLength != 0)
                    {
                        Hausnummer = textAendernHausnummer.Text;
                    }
                    else { Hausnummer = textShowHausnummer.Text; }

                    if (textAendernOrt.TextLength != 0)
                    {
                        Ort = textAendernOrt.Text;
                    }
                    else { Ort = textShowOrt.Text; }

                    if (textAendernPLZ.TextLength != 0)
                    {
                        PLZ = textAendernPLZ.Text;
                    }
                    else { PLZ = textShowPLZ.Text; }

                    // Abschicken an alle einzelnen Umzüge
                    foreach (var item in umzNr)
                    {
                        String push = "UPDATE Umzuege SET StraßeA = '"+Straße+ "', HausnummerA= '" + Hausnummer + "', OrtA= '" + Ort + "', PLZA= '" + PLZ + "', UserChanged = " + UserChanged + Bearbeiter + " WHERE idUmzuege = " + item;
                        Program.absender(push,"Absenden Adressänderung");
                    }

                }
            }
        }

        private void buttonLöschen_Click(object sender, EventArgs e)
        {
            var bestätigung = MessageBox.Show("Den Kunden wirklich entgültig löschen? \r\n Nur wenn kein Umzug / Kartonagen vorhanden", "Löschen bestätigen", MessageBoxButtons.YesNo);
            if (bestätigung == DialogResult.Yes)
            {
                String delete = "DELETE FROM Kunden WHERE idKunden = " + numericAendernNummer.Value + " ;";
                Program.absender(delete,"Löschen des Kunden Nr:"+ numericAendernNummer.Value);
            }
            else
            {
                textKundenSearchLog.AppendText("Löschvorgang abgebrochen\r\n");
            }
        }

        
    }
}
