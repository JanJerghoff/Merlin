using MySql.Data.MySqlClient;
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

        public KundenSearch()
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
            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden=" + nummer + ";", Program.conn);
            MySqlDataReader rdr;

            int worked = 0;
            String letzteAenderung = "";

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    textShowAnrede.Text = rdr[1].ToString();                                                                                                // TODO Fixing Bool to String
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
                    letzteAenderung = rdr[12].ToString();
                    textShowBemerkung.Text = rdr[14].ToString();
                    worked = 1;
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textKundenSearchLog.Text += sqlEx.ToString();
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
            String temp = letzteAenderung[letzteAenderung.Length-1].ToString();

            if (temp == "0")
            {
                textGeändertVon.Text = "Rita";
            }

            if (temp == "1")
            {
                textGeändertVon.Text = "Jonas";
            }

            if (temp == "2")
            {
                textGeändertVon.Text = "Eva";
            }

            if (temp == "3")
            {
                textGeändertVon.Text = "Jan";
            }
            
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
            MySqlCommand cmdRead = new MySqlCommand("SELECT idKunden,Vorname,Erstelldatum FROM Kunden WHERE Nachname = '"+textSucheName.Text+"';", Program.conn);
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
                    // test = rdr.GetDateTime(2).ToShortDateString();                                           Fix steht aus
                    tempCounter += 1;
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textKundenSearchLog.Text += sqlEx.ToString();
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

        private void pushChange(String command) {
            MySqlCommand change = new MySqlCommand(command, Program.conn);
            try
            {
                change.ExecuteNonQuery();
            }
            catch (Exception sqlEx)
            {
                textKundenSearchLog.Text += sqlEx.ToString();
                return;
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
            String change = "UPDATE Kunden SET Anrede = '" + textAendernAnrede.Text + "' WHERE idKunden = " + numericAendernNummer.Value + ";";
            pushChange(change);
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernVorname_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Vorname = '" + textAendernVorname.Text + "' WHERE idKunden = " + numericAendernNummer.Value + ";";
            pushChange(change);
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernNachname_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Nachname = '" + textAendernNachname.Text + "' WHERE idKunden = " + numericAendernNummer.Value + ";";
            pushChange(change);
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernTelefonnummer_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Telefonnummer = '" + textAendernTelefonnummer.Text + "' WHERE idKunden = " + numericAendernNummer.Value + ";";
            pushChange(change);
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernHandynummer_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Handynummer = '" + textAendernHandynummer.Text + "' WHERE idKunden = " + numericAendernNummer.Value + ";";
            pushChange(change);
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernEmail_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Email = '" + textAendernEmail.Text + "' WHERE idKunden = " + numericAendernNummer.Value + ";";
            pushChange(change);
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernStraße_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Straße = '" + textAendernStraße.Text + "', Hausnummer = '"+textAendernHausnummer.Text+"' WHERE idKunden = " + numericAendernNummer.Value + ";";
            pushChange(change);
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
            UmzugsAdresseAendern();
        }

        private void buttonKundeAendernPLZ_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET PLZ = " + textAendernPLZ.Text + ", Ort = '" + textAendernOrt + "' WHERE idKunden = " + numericAendernNummer.Value + ";";
            pushChange(change);
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
            UmzugsAdresseAendern();
        }

        private void buttonKundeAendernLand_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Land = '" + textAendernLand.Text + "' WHERE idKunden = " + numericAendernNummer.Value + ";";
            pushChange(change);
            textKundenSearchLog.AppendText("Änderung vorgenommen \r\n");
            auffüllen(decimal.ToInt32(numericAendernNummer.Value));
        }

        private void buttonKundeAendernBemerkung_Click(object sender, EventArgs e)
        {
            String change = "UPDATE Kunden SET Bemerkung = '" + textAendernBemerkung.Text + "' WHERE idKunden = " + numericAendernNummer.Value + ";";
            pushChange(change);
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
            MySqlCommand cmdRead = new MySqlCommand("SELECT u.idUmzuege, u.datUmzug FROM Umzuege u, Umzugsfortschritt f WHERE u.Kunden_idKunden = "+numericAendernNummer.Value+" AND u.idUmzuege = f.Umzuege_idUmzuege AND f.abgeschlossen = 8;", Program.conn);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();

                while (rdr.Read())
                {
                    umzNr.Add(rdr.GetInt32(0));
                    umzDat.Add(rdr.GetDateTime(1).ToShortDateString());                    
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textKundenSearchLog.Text += sqlEx.ToString();
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
                        String push = "UPDATE Umzuege SET StraßeA = '"+Straße+ "', HausnummerA= '" + Hausnummer + "', OrtA= '" + Ort + "', PLZA= '" + PLZ + "' WHERE idUmzuege = " + item;
                        Program.absender(push);
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
                MySqlCommand cmdSend = new MySqlCommand(delete, Program.conn);
                try
                {
                    cmdSend.ExecuteNonQuery();
                    textKundenSearchLog.AppendText("Kunde erfolgreich gelöscht\r\n");
                }
                catch (Exception sqlEx)
                {
                    textKundenSearchLog.Text += sqlEx.ToString();
                }
            }
            else
            {
                textKundenSearchLog.AppendText("Löschvorgang abgebrochen\r\n");
            }
        }

        
    }
}
