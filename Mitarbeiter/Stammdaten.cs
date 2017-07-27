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

namespace Mitarbeiter
{
    public partial class Stammdaten : Form
    {
        public Stammdaten()
        {

            InitializeComponent();

            // String-collection anlegen
            AutoCompleteStringCollection autocomplete = new AutoCompleteStringCollection();

            //Abfrage aller Namen
            MySqlCommand cmdRead = new MySqlCommand("SELECT Nachname, Vorname FROM Mitarbeiter", Program.conn2);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    autocomplete.Add(rdr[0].ToString() + ", " + rdr[1].ToString());
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textStammdatenLog.Text += sqlEx.ToString();
                return;
            }
            // Autocomplete vorlegen
            textSucheName.AutoCompleteCustomSource = autocomplete;
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        int idBearbeitend;

        String bearbeitet = "";

        public void setBearbeitend(int code)
        {
            idBearbeitend = code;
        }

        public int fuellen(int mitarbeiterNummer) {

            leeren();

            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Mitarbeiter WHERE idMitarbeiter = '" + mitarbeiterNummer + "';", Program.conn2);
            MySqlDataReader rdr;
            int count = 0;

            try
            {

                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    count++;            
                    textMitarbeiterID.Text = rdr[0].ToString();
                    textNachname.Text = rdr[1].ToString();
                    textVorname.Text = rdr[2].ToString();
                    numericStundenlohn.Value = rdr.GetDecimal(3);
                    textMitarbeiterNr.Text = rdr[4].ToString();
                    textStraße.Text = rdr[5].ToString();
                    textOrt.Text = rdr[6].ToString();
                    textHandy.Text = rdr[7].ToString();
                    textTelefon.Text = rdr[8].ToString();
                    textReligion.Text = rdr[9].ToString();
                    textKrankenkasse.Text = rdr[10].ToString();
                    numericSteuerklasse.Value = rdr.GetDecimal(11);
                    textFamilienstand.Text = rdr[12].ToString();
                    numericKinder.Value = rdr.GetDecimal(13);
                    textSozialversicherung.Text = rdr[14].ToString();
                    textSteuerID.Text = rdr[15].ToString();
                    textFuehrerscheinNr.Text = rdr[16].ToString();
                    textPersoNr.Text = rdr[17].ToString();
                    dateGeburtstag.Value = rdr.GetDateTime(18);
                    textGeburtsort.Text = rdr[19].ToString();
                    textHauptarbeitgeber.Text = rdr[20].ToString();
                    numericStundenanteil.Value = Convert.ToDecimal((rdr.GetInt32(21))/60.0);
                    textStaatsangehoerigkeit.Text = rdr[22].ToString();
                    textIBAN.Text = rdr[23].ToString();
                    textBIC.Text = rdr[24].ToString();
                    textKontoinhaber.Text = rdr[25].ToString();
                    dateEinstellung.Value = rdr.GetDateTime(26);
                    parseKategorie(rdr.GetString(27));
                    bearbeitet = rdr[28].ToString();
                    dateAusschied.Value = rdr.GetDateTime(29);
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textStammdatenLog.AppendText(sqlEx.ToString());
                return 0;
            }

            return count;

        }

        private void NamenSuche()
        { 

            MySqlCommand cmdRead = new MySqlCommand("SELECT idMitarbeiter, Vorname, Nachname FROM Mitarbeiter;", Program.conn2);
            MySqlDataReader rdr;
            int tempCounter = 0;
            int targetNr = -1;

            try
            {

                rdr = cmdRead.ExecuteReader();

                while (rdr.Read())
                {
                    if (textSucheName.Text == (rdr[2].ToString() + ", " + rdr[1].ToString()))
                    {
                        targetNr = (rdr.GetInt32(0));
                        tempCounter += 1;
                    }                    
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textStammdatenLog.Text += sqlEx.ToString();
                return;
            }

            if (tempCounter > 1) {
                var warnung = MessageBox.Show("Mehrere identische Mitarbeiter gefunden, Datenbank prüfen lassen", "Fehlermeldung");
                return;
            }

            fuellen(targetNr);

        }

        private void MitarbeiterAdd (){
            var bestätigung = MessageBox.Show("Den Mitarbeiter wirklich neu hinzufügen?", "Hinzufügen bestätigen", MessageBoxButtons.YesNo);
            if (bestätigung == DialogResult.Yes) {
                //Zwischenspeicher neue ID
                int ID = 0;
                // Sicherheit gegen versehendliches Doppelspeichern
                if (textMitarbeiterID.Text != "") {
                    var warnung = MessageBox.Show("Es ist bereits ein Mitarbeiter geladen, /r/n ein neuer Mitarbeiter kann nicht gespeichert werden /r/n Sollte der Mitarbeiter geändert werden, bitte Änderungsknopf drücken", "Fehlermeldung");
                    return;
                }
                if (dateAusschied.Value.Date != new DateTime(2017, 1, 1))
                {
                    var bestätigungAusschied = MessageBox.Show("Das Ausschiedsdatum ist geändert. Mitarbeiter wirklich zum Datum ausscheiden lassen?", "Ausschied bestätigen", MessageBoxButtons.YesNo);
                    if (bestätigungAusschied == DialogResult.No)
                    {
                        return;
                    }
                }

                    //String bauen
                    //Vorlage zum Anhängen der Werte
                    String insert = "INSERT INTO Mitarbeiter (Vorname, Nachname, StundenGehalt, Personalnummer, Straße, PLZOrt, Handynummer, Festnetznummer, Religion, Krankenkasse, " +
                    "Steuerklasse, Familienstand, Kinderzahl, Sozialversicherungsnummer, SteuerID, FuehrerscheinNummer, PersonalausweisNummer, "+
                    "Geburtsdatum, Geburtsort, Hauptarbeitgeber, StundenanteilMinuten, Staatsangehoerigkeit, IBAN, BIC, Kontoinhaber, Einstellungsdatum, Kategorie, UserChanged, Ausscheidedatum) VALUES (";
                insert += "'" + textVorname.Text + "', ";
                insert += "'" + textNachname.Text + "', ";
                insert += numericStundenlohn.Value.ToString().Replace(',' , '.') + ", ";
                insert += "'" + textMitarbeiterNr.Text + "', ";
                insert += "'" + textStraße.Text + "', ";
                insert += "'" + textOrt.Text + "', ";
                insert += "'" + textHandy.Text + "', ";
                insert += "'" + textTelefon.Text + "', ";
                insert += "'" + textReligion.Text + "', ";
                insert += "'" + textKrankenkasse.Text + "', ";
                insert +=  numericSteuerklasse.Value.ToString().Replace(',', '.') + ", ";
                insert += "'" + textFamilienstand.Text + "', ";
                insert +=  numericKinder.Value.ToString().Replace(',', '.') + ", ";
                insert += "'" + textSozialversicherung.Text + "', ";
                insert += "'" + textSteuerID.Text + "', ";
                insert += "'" + textFuehrerscheinNr.Text + "', ";
                insert += "'" + textPersoNr.Text + "', ";
                insert += "'" + Program.DateMachine(dateGeburtstag.Value) + "', ";
                insert += "'" + textGeburtsort.Text + "', ";
                insert += "'" + textHauptarbeitgeber.Text + "', ";
                insert += Program.toMinute(numericStundenanteil.Value) + ", ";
                insert += "'" + textStaatsangehoerigkeit.Text + "', ";
                insert += "'" + textIBAN.Text + "', ";
                insert += "'" + textBIC.Text + "', ";
                insert += "'" + textKontoinhaber.Text + "', ";
                insert += "'" + Program.DateMachine(dateEinstellung.Value) + "',";
                insert += "'" + getKategorie() + "', ";
                insert += idBearbeitend + ", ";
                insert += "'" + Program.DateMachine(dateAusschied.Value) + "');";

                // String fertig, absenden
                MySqlCommand cmdAdd = new MySqlCommand(insert, Program.conn2);
                try
                {
                    cmdAdd.ExecuteNonQuery();
                    textStammdatenLog.AppendText("Mitarbeiter erfolgreich angelegt \r\n ");

                }
                catch (Exception sqlEx)
                {
                    textStammdatenLog.Text += sqlEx.ToString();
                    return;
                }
                

                // ErgebnisID rausziehen

                MySqlCommand cmdRead = new MySqlCommand("SELECT idMitarbeiter FROM Mitarbeiter WHERE Nachname = '" + textNachname.Text + "' AND Vorname = '"+ textVorname.Text + "';", Program.conn2);
                MySqlDataReader rdr;
                int count = 0;
                try
                {

                    rdr = cmdRead.ExecuteReader();
                    while (rdr.Read())
                    {
                        count++;
                        textMitarbeiterID.Text = rdr[0].ToString();
                        ID = rdr.GetInt32(0);
                    }
                    rdr.Close();
                }
                catch (Exception sqlEx)
                {
                    textStammdatenLog.AppendText(sqlEx.ToString());
                    return;
                }

                // Erfolg prüfen
                if (count == 0)
                {
                    textStammdatenLog.AppendText("Fehler beim Hinzufügen, konnte keine MitarbeiterID finden \r\n ");
                }
                else if (count > 1) {
                    textStammdatenLog.AppendText("Hinzugefügter Mitarbeiter existiert doppelt (Vor/Nachname gleich). Bitte Prüfen! \r\n ");
                }


                // Mitarbeiter-Checkliste anlegen

                insert = "INSERT INTO MitarbeiterCheck (Mitarbeiter_idMitarbeiter) VALUES (" + ID + ");";
                MySqlCommand cmdcheck = new MySqlCommand(insert, Program.conn2);
                try
                {
                    cmdcheck.ExecuteNonQuery();
                    textStammdatenLog.AppendText("Mitarbeiter Checkliste erfolgreich angelegt \r\n ");

                }
                catch (Exception sqlEx)
                {
                    textStammdatenLog.Text += sqlEx.ToString();
                    return;
                }
                              

                //Minuten für den Monat berechnen
                int min = 0;

                if (dateEinstellung.Value.Day <= 26)
                {
                    min = Program.toMinute(numericStundenanteil.Value) * Program.getWochentage(dateEinstellung.Value.Date, new DateTime(dateEinstellung.Value.Year, dateEinstellung.Value.Month, 26));
                }
                else {
                    min = Program.toMinute(numericStundenanteil.Value) * Program.getWochentage(dateEinstellung.Value.Date, new DateTime(dateEinstellung.Value.Year, dateEinstellung.Value.Month+1, 26));
                }
                
                DateTime StartMonat = new DateTime(2017, 7, 1);

                String stdKonto = "INSERT INTO Stundenkonto (Mitarbeiter_idMitarbeiter, Monat, SollMinuten) VALUES(" + ID + ", '" + Program.DateMachine(dateEinstellung.Value) + "', " + min + ");";

                // String fertig, absenden
                MySqlCommand cmdStd = new MySqlCommand(stdKonto, Program.conn2);
                try
                {
                    cmdStd.ExecuteNonQuery();
                    textStammdatenLog.AppendText("Stundenkonto erfolgreich angelegt \r\n ");

                }
                catch (Exception sqlEx)
                {
                    textStammdatenLog.Text += sqlEx.ToString();
                    return;
                }

                // Checkliste zur Bearbeitung öffnen
                Stammdaten_Check check = new Stammdaten_Check();
                check.Show();
                check.fuellen(ID);
                check.setBearbeitend(idBearbeitend);
            }
        }

        private void MitarbeiterChange()
        {
            if (dateAusschied.Value.Date != new DateTime(2017, 1, 1))
            {
                var bestätigungAusschied = MessageBox.Show("Das Ausschiedsdatum ist geändert. Mitarbeiter wirklich zum Datum ausscheiden lassen?", "Ausschied bestätigen", MessageBoxButtons.YesNo);
                if (bestätigungAusschied == DialogResult.No)
                {
                    return;
                }
            }

            var bestätigung = MessageBox.Show("Den Mitarbeiter wirklich ändern?", "Änderung bestätigen", MessageBoxButtons.YesNo);
            if (bestätigung == DialogResult.Yes)
            {
                
                //String bauen
                //Vorlage zum Anhängen der Werte
                String insert = "UPDATE Mitarbeiter SET ";
                insert += "Vorname = '" + textVorname.Text + "', ";
                insert += "Nachname = '" + textNachname.Text + "', ";
                insert += "StundenGehalt = " + numericStundenlohn.Value.ToString() + ", ";
                insert += "Personalnummer = '" + textMitarbeiterNr.Text + "', ";
                insert += "Straße = '" + textStraße.Text + "', ";
                insert += "PLZOrt = '" + textOrt.Text + "', ";
                insert += "Handynummer = '" + textHandy.Text + "', ";
                insert += "Festnetznummer = '" + textTelefon.Text + "', ";
                insert += "Religion = '" + textReligion.Text + "', ";
                insert += "Krankenkasse = '" + textKrankenkasse.Text + "', ";
                insert += "Steuerklasse = " + numericSteuerklasse.Value.ToString() + ", ";
                insert += "Familienstand = '" + textFamilienstand.Text + "', ";
                insert += "Kinderzahl = " + numericKinder.Value.ToString() + ", ";
                insert += "Sozialversicherungsnummer = '" + textSozialversicherung.Text + "', ";
                insert += "SteuerID = '" + textSteuerID.Text + "', ";
                insert += "FuehrerscheinNummer = '" + textFuehrerscheinNr.Text + "', ";
                insert += "PersonalausweisNummer = '" + textPersoNr.Text + "', ";
                insert += "Geburtsdatum = '" + Program.DateMachine(dateGeburtstag.Value) + "', ";
                insert += "Geburtsort = '" + textGeburtsort.Text + "', ";
                insert += "Hauptarbeitgeber = '" + textHauptarbeitgeber.Text + "', ";
                insert += "StundenanteilMinuten = " + Program.toMinute(numericStundenanteil.Value) + ", ";
                insert += "Staatsangehoerigkeit = '" + textStaatsangehoerigkeit.Text + "', ";
                insert += "IBAN = '" + textIBAN.Text + "', ";
                insert += "BIC = '" + textBIC.Text + "', ";
                insert += "Kontoinhaber = '" + textKontoinhaber.Text + "', ";
                insert += "Ausscheidedatum = '" + Program.DateMachine(dateAusschied.Value) + "', ";
                insert += "UserChanged = " + int.Parse(bearbeitet + idBearbeitend.ToString()) + ", ";
                insert += "Kategorie = '"+getKategorie()+"', ";
                insert += "Einstellungsdatum = '" + Program.DateMachine(dateEinstellung.Value) + "' WHERE idMitarbeiter = " + textMitarbeiterID.Text + ";";

                // String fertig, absenden
                MySqlCommand cmdAdd = new MySqlCommand(insert, Program.conn2);
                try
                {
                    cmdAdd.ExecuteNonQuery();
                    textStammdatenLog.AppendText("Mitarbeiter erfolgreich geändert \r\n ");

                }
                catch (Exception sqlEx)
                {
                    textStammdatenLog.AppendText(sqlEx.ToString());
                    return;
                }
            }
        }
        // Kategorien zuordnen // Umzug 0, Nacht 1, Gemüse 2, Büro 3
        private string getKategorie() {
            String ret = "";
            if (checkUmzug.Checked) {
                ret += "0";
            }
            if (checkNacht.Checked)
            {
                ret += "1";
            }
            if (checkGemuese.Checked)
            {
                ret += "2";
            }
            if (checkBuero.Checked)
            {
                ret += "3";
            }
            return ret;
        }
              

        private void parseKategorie(String kat) {
            for (int i = 0; i < kat.Length; i++)
            {
                switch (kat[i])
                {
                    case '0':
                        checkUmzug.Checked = true;
                        break;
                    case '1':
                        checkNacht.Checked = true;
                        break;
                    case '2':
                        checkGemuese.Checked = true;
                        break;
                    case '3':
                        checkBuero.Checked = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private void buttonNeu_Click(object sender, EventArgs e)
        {
            MitarbeiterAdd();
        }

        private void buttonNrSuche_Click(object sender, EventArgs e)
        {
            if (fuellen(Decimal.ToInt32(numericSucheNummer.Value)) == 1)
            {
                textStammdatenLog.AppendText("Mitarbeiter eingefüllt \r\n ");
            }
            else {

                textStammdatenLog.AppendText("Kein Mitarbeiter mit dieser Nummer \r\n");
            }

        }

        private void buttonAendern_Click(object sender, EventArgs e)
        {
            MitarbeiterChange();
        }

        private void buttonNameSuche_Click(object sender, EventArgs e)
        {
            NamenSuche();
        }

        private void buttonLeeren_Click(object sender, EventArgs e)
        {
            leeren();
        }

        private void leeren () { 
            textMitarbeiterID.Clear();
            textMitarbeiterNr.Clear();
            textVorname.Clear();
            textNachname.Clear();
            textStraße.Clear();
            textOrt.Clear();
            textHandy.Clear();
            textTelefon.Clear();
            textReligion.Clear();
            textKrankenkasse.Clear();
            textFamilienstand.Clear();
            textSozialversicherung.Clear();
            textSteuerID.Clear();
            textFuehrerscheinNr.Clear();
            textPersoNr.Clear();
            textStaatsangehoerigkeit.Clear();
            textGeburtsort.Clear();
            textHauptarbeitgeber.Clear();
            textIBAN.Clear();
            textBIC.Clear();
            textKontoinhaber.Clear();
            numericKinder.Value = 0;
            numericSteuerklasse.Value = 0;
            numericStundenanteil.Value = 0;
            numericStundenlohn.Value = 0;
            numericUrlaubstage.Value = 0;
            dateEinstellung.Value = DateTime.Now;
            dateGeburtstag.Value = DateTime.Now;
            dateAusschied.Value = new DateTime(2017, 1, 1);
            bearbeitet = "";
            checkBuero.Checked = false;
            checkGemuese.Checked = false;
            checkNacht.Checked = false;
            checkUmzug.Checked = false;
        }

        private void buttonCheckliste_Click(object sender, EventArgs e)
        {
            if (textMitarbeiterID.Text == "")
            {
                textStammdatenLog.AppendText("Es muss ein Mitarbeiter ausgewählt sein, um die Checkliste anzuzeigen \r\n ");
            }
            else {
                // Checkliste zur Bearbeitung öffnen
                Stammdaten_Check check = new Stammdaten_Check();
                check.Show();
                int x;
                int.TryParse(textMitarbeiterID.Text, out x);
                check.fuellen(x);
                check.setBearbeitend(idBearbeitend);
            }
        }
        
    }
}
