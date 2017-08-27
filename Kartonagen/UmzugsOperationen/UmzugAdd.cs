using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
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
    public partial class UmzugAdd : Form
    {
        public UmzugAdd()
        {
            InitializeComponent();
            
            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            // Vorlegen der ListBoxen
            listBoxA.SelectedIndex = 3;
            listBoxB.SelectedIndex = 3;
        }

        private int idBearbeitend = 0; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.

        public void setBearbeiter(int wer)
        {
            idBearbeitend = wer;
        }

        public void umzugFuellen(int KundenNR) {
            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden = '" + KundenNR + "';", Program.conn);
            MySqlDataReader rdr;

            try
            {

                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    textKundennummer.Text = KundenNR + "";                       //Impliziter Cast weil Typ des SQL-Attributs bekannt
                    textVorNachname.Text = rdr[1] + " " + rdr[2] + " " + rdr[3];
                    textTelefonnummer.Text = rdr[4] + "";
                    textHandynummer.Text = rdr[5] + "";
                    textEmail.Text = rdr[6] + "";
                    textStraßeA.Text = rdr[7] + "";
                    textHausnummerA.Text = rdr[8] + "";
                    textPLZA.Text = rdr[9] + "";
                    textOrtA.Text = rdr[10] + "";
                    textLandA.Text = rdr[11] + "";
                    textNoteBuero.AppendText(rdr[14].ToString());
                    textUmzugLog.AppendText("Kunde eingefüllt \r\n");
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.AppendText( sqlEx.ToString());
                return;
            }
        }

        private void buttonKundenSearchNrSuche_Click(object sender, EventArgs e)
        {
            // Abfrage Umzuege nach passenden
            //String frage = "SELECT * FROM Umzuege WHERE Kunden_idKunden = '" + numericSucheKundennr.Value + "';";
            int KundenNRkanidat = decimal.ToInt32(numericSucheKundennr.Value);

            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden = '" + KundenNRkanidat + "';", Program.conn);
            MySqlDataReader rdr;
            int tempCounter = 0;
           
            try
            {

                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    tempCounter++;
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.Text += sqlEx.ToString();
                return;
            }

            if (tempCounter == 0)
            {         //Kein Match
                textUmzugLog.AppendText("Kunde mit der Nummer " + KundenNRkanidat + " nicht gefunden.");
            }
            else
            {
                umzugFuellen(KundenNRkanidat);                
            }
            
        }

        private void buttonNameSuche_Click(object sender, EventArgs e)
        {
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
                    // test = rdr.GetDateTime(2).ToShortDateString();                                           Fix steht aus
                    tempCounter += 1;
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.AppendText(sqlEx.ToString());
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
                umzugFuellen(nummern[0]);
            }
            else
            {                          // Mehrere Treffer
                for (int i = 0; i < tempCounter; i++)
                {
                    textSuchBox.AppendText(vornamen[i] + " " + textSucheName.Text + " hinzugefügt am" + daten[i] + ": " + nummern[i] + " \r\n");
                }
            }



        }
        
        private String push(String push) {

            MySqlCommand cmdAdd = new MySqlCommand(push, Program.conn);
            try
            {
                cmdAdd.ExecuteNonQuery();
                return "Erfolgreich gespeichert \r\n";
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.AppendText(sqlEx.ToString());
                //return "Fehlgeschlagen \r\n";
                return sqlEx.ToString()+"\r\n"+push;
            }
            
        }

        private String stringbauer() {
            String longInsert = "INSERT INTO Umzuege (Kunden_idKunden, datBesichtigung, datUmzug, datRuempelung, datEinpacken, datAuspacken, " +
                "AufzugA, AufzugB, HVZA, HVZB, StockwerkeA, StockwerkeB, LaufmeterA, LaufmeterB, Einpacken, Auspacken, Packerzahl, Kartons, Kleiderkisten, " +
                "Mann, Stunden, SchilderBool, SchilderZeit, KuecheAb, KuecheAuf, KuecheBau, KuechePausch, UmzugsDauer, Autos, " +
                "StraßeA, HausnummerA, PLZA, OrtA, LandA, AussenAufzugA, " +
                "StraßeB, HausnummerB, PLZB, OrtB, LandB, AussenaufzugB, NotizBuero, NotizFahrer, BemerkungTitel, UserChanged, Erstelldatum, PackerStunden, " +
                "StatUmz, StatBes, StatAus, StatEin, StatEnt, HausTypA, HausTypB, umzugsZeit) VALUES (";

            //Anfügen
            longInsert += textKundennummer.Text + ", ";
            longInsert += "'" + Program.DateMachine(dateBesicht.Value) + "', ";
            longInsert += "'" + Program.DateMachine(dateUmzug.Value) + "', ";
            longInsert += "'" + Program.DateMachine(dateEntruempel.Value) + "', ";
            longInsert += "'" + Program.DateMachine(dateEinpack.Value) + "', ";
            longInsert += "'" + Program.DateMachine(dateAuspack.Value) + "', ";
            //Checks der Radiobuttons 1=Ja, 0=Nein, 2=Vllt

            if (radioAufzugAJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else { longInsert += 0 + ", "; }


            if (radioAufzugBJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else { longInsert += 0 + ", "; }


            if (radioHVZAJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else if (radioHVZANein.Checked)
            {
                longInsert += 0 + ", ";
            }
            else { longInsert += 2 + ", "; }


            if (radioHVZBJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else if (radioHVZBNein.Checked)
            {
                longInsert += 0 + ", ";
            }
            else { longInsert += 2 + ", "; }


            longInsert += numericStockwerkeA.Value + ", ";

            longInsert += numericStockwerkeB.Value + ", ";

            longInsert += int.Parse(textLaufMeterA.Text) + ", ";

            longInsert += int.Parse(textLaufMeterB.Text) + ", ";

            if (radioEinpackenJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else if (radioEinpackenNein.Checked)
            {
                longInsert += 0 + ", ";
            }
            else { longInsert += 2 + ", "; }


            if (radioAuspackenJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else if (radioAuspackenNein.Checked)
            {
                longInsert += 0 + ", ";
            }
            else { longInsert += 2 + ", "; }


            longInsert += numericPacker.Value + ", ";

            longInsert += numericPackKartons.Value + ", ";

            longInsert += numericKleiderkisten.Value + ", ";

            longInsert += numericMannZahl.Value + ", ";

            longInsert += numericArbeitszeit.Value +", ";

            if (radioSchilderJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else { longInsert += 0 + ", "; }


            longInsert += "'" + Program.DateMachine(dateSchilderVerweildauer.Value) + "', ";
            
            if (radioKuecheAbJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else if (radioKuecheAbNein.Checked)
            {
                longInsert += 0 + ", ";
            }
            else { longInsert += 2 + ", "; }

            if (radioKuecheAufJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else if (radioKuecheAufNein.Checked)
            {
                longInsert += 0 + ", ";
            }
            else { longInsert += 2 + ", "; }

            if (radioKuecheIntern.Checked)          // Küchenbau Intern = 1, Extern = 0;
            {
                longInsert += 1 + ", ";
            }
            else { longInsert += 0 + ", "; }

            longInsert += "'" + textKuechenPreis.Text + "', ";

            longInsert += numericUmzugsDauer.Value + ", ";

            // Auto Coding => "SprinterMit-SprinterOhne-7,5-12"
            String temp = "";
            temp = temp + decimal.ToInt32(numericSprinterMit.Value).ToString();
            temp = temp + decimal.ToInt32(numericSprinterOhne.Value).ToString();
            temp = temp + decimal.ToInt32(numericLKW.Value).ToString();
            temp = temp + decimal.ToInt32(numericLKWGroß.Value).ToString();

            longInsert += "'" + temp + "', ";

            longInsert += "'" + textStraßeA.Text + "', ";

            longInsert += "'" + textHausnummerA.Text + "', ";

            longInsert += textPLZA.Text + ", ";                 // Ohne '' da Int in der DB, Plz ist immer Int

            longInsert += "'" + textOrtA.Text + "', ";

            longInsert += "'" + textLandA.Text + "', ";

            if (radioAussenAufzugAJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else { longInsert += 0 + ", "; }

            longInsert += "'" + textStraßeB.Text + "', ";

            longInsert += "'" + textHausnummerB.Text + "', ";

            longInsert += textPLZB.Text + ", ";                 // Ohne '' da Int in der DB, Plz ist immer Int

            longInsert += "'" + textOrtB.Text + "', ";

            longInsert += "'" + textLandB.Text + "', ";

            if (radioAussenAufzugBJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else { longInsert += 0 + ", "; }

            longInsert += "'" + textNoteBuero.Text + "', ";

            longInsert += "'" + textNoteFahrer.Text + "', ";

            longInsert += "'" + textNoteKalender.Text + "', ";

            longInsert += "'"+ idBearbeitend + "', ";

            longInsert += "'" + Program.DateMachine(DateTime.Now) + "', ";

            longInsert += numericPackStunden.Value + ", ";

            // Status des Datums. 0 = nicht festgelegt, 1 = festgelegt, 2 = vorläufig (wenn möglich), 3 = Vorläufig gebucht (bei Umzügen)
            // Reihenfolge ist Umz, Bes, Aus, Ein, Ent

            if (radioUmzJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else if (radioUmzVllt.Checked)
            {
                longInsert += 2 + ", ";
            }
            else if (radioUmzVorlaeufig.Checked)
            {
                longInsert += 3 + ", ";
            }
            else { longInsert += 0 + ", "; }
            //
            if (radioBesJa.Checked)
            {
                longInsert += 1 + ", ";
            }            
            else { longInsert += 0 + ", "; }
            //
            if (radioAusJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else if (radioAusVllt.Checked)
            {
                longInsert += 2 + ", ";
            }
            else { longInsert += 0 + ", "; }
            //
            if (radioEinJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else if (radioEinVllt.Checked)
            {
                longInsert += 2 + ", ";
            }
            else { longInsert += 0 + ", "; }
            //
            if (radioEntJa.Checked)
            {
                longInsert += 1 + ", ";
            }
            else if (radioEntVllt.Checked)
            {
                longInsert += 2 + ", ";
            }
            else {
                longInsert += 0 + ", ";
            }
            //
            longInsert += "'" + listBoxA.SelectedItem + "', ";
            longInsert += "'" + listBoxB.SelectedItem + "', ";
            //
            longInsert += "'" + Program.ZeitMachine(timeBesichtigung.Value)+"');";
            //
            return longInsert;

        }

        private void buttonUmzugSpeichern_Click(object sender, EventArgs e)
        {
            // Wirklich?

            var bestätigung = MessageBox.Show("Den Umzug wirklich hinzufügen?", "Hinzufügen bestätigen", MessageBoxButtons.YesNo);
            if (bestätigung == DialogResult.Yes)
            {
                
            }
            else
            {
                textUmzugLog.AppendText("Vorgang abgebrochen\r\n");
                return;
            }


            // Validieren das alles In Ordnung
            if (textKundennummer.Text.Length == 0)
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Bitte einen Kunden auswählen \r\n");
                return;
            }
            // Check Adressen
            if (textStraßeB.Text.Length == 0 || textHausnummerB.Text.Length == 0 || textPLZB.Text.Length == 0 || textOrtB.Text.Length == 0 || textLandB.Text.Length == 0)
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Einzugsadresse Unvollständig \r\n");
                return;
            }

            if (textStraßeA.Text.Length == 0 || textHausnummerA.Text.Length == 0 || textPLZA.Text.Length == 0 || textOrtA.Text.Length == 0 || textLandA.Text.Length == 0)
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Auszugsadresse Unvollständig \r\n");
                return;
            }

            // Check Radiobuttons
            if (radioAufzugAJa.Checked == false && radioAufzugANein.Checked == false) {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Bitte Auswahl Aufzug Auszugsadresse Treffen \r\n");
                return;
            }

            if (radioAufzugBJa.Checked == false && radioAufzugBNein.Checked == false)
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Bitte Auswahl Aufzug Einzugsadresse Treffen \r\n");
                return;
            }

            if (radioHVZAJa.Checked == false && radioHVZANein.Checked == false && radioHVZAV.Checked == false)
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Bitte Auswahl HVZ Auszugsadresse Treffen \r\n");
                return;
            }

            if (radioHVZBJa.Checked == false && radioHVZBNein.Checked == false && radioHVZBV.Checked == false)
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Bitte Auswahl HVZ Einzugsadresse Treffen \r\n");
                return;
            }

            if (radioEinpackenJa.Checked == false && radioEinpackenNein.Checked == false && radioEinpackenV.Checked == false)
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Bitte Auswahl Einpacken Treffen \r\n");
                return;
            }

            if (radioAuspackenJa.Checked == false && radioAuspackenNein.Checked == false && radioAuspackenV.Checked == false)
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Bitte Auswahl Auspacken Treffen \r\n");
                return;
            }

            if (radioKuecheAbJa.Checked == false && radioKuecheAbNein.Checked == false && radioKuecheAbV.Checked == false)
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Bitte Auswahl Küchenabbau Treffen \r\n");
                return;
            }

            if (radioKuecheAufJa.Checked == false && radioKuecheAufNein.Checked == false && radioKuecheAufV.Checked == false)
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Bitte Auswahl Küchenaufbau Treffen \r\n");
                return;
            }

            if (radioKuecheExtern.Checked == false && radioKuecheIntern.Checked == false)
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Bitte Auswahl Küchenbau Treffen \r\n");
                return;
            }

            if (radioKuecheIntern.Checked && textKuechenPreis.Text == "")
            {
                textUmzugLog.AppendText("Umzug hinzufügen gescheitert, Wenn Kueche Intern, dann muss Pauschale größer 0 sein. \r\n");
                return;
            }
            
            // String-Monstter in DB pushen

            textUmzugLog.AppendText(push(stringbauer()));

            // Ergebnis - Umzugsnummer anzeigen

            textUmzugsNummer.Text = "";

            MySqlCommand cmdShow = new MySqlCommand("SELECT * FROM Umzuege ORDER BY idUmzuege DESC LIMIT 1;", Program.conn);
            MySqlDataReader rdr;
            try
            {
                rdr = cmdShow.ExecuteReader();
                while (rdr.Read())
                {
                    textUmzugsNummer.Text += rdr.GetInt32(0);
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.AppendText(sqlEx.ToString());
            }

            // Daten in Kalender Speichern

            textUmzugLog.AppendText(datumInKalender());


        }

        private void buttonSchnellSpeichern_Click(object sender, EventArgs e)
        {
            // Wirklich?

            var bestätigung = MessageBox.Show("Den Umzug wirklich hinzufügen?", "Hinzufügen bestätigen", MessageBoxButtons.YesNo);
            if (bestätigung == DialogResult.Yes)
            {

            }
            else
            {
                textUmzugLog.AppendText("Vorgang abgebrochen\r\n");
                return;
            }


            //Sicherheitsabfrage für int.parse() Methoden: Laufweg

            if (textLaufMeterA.Text.Length == 0 || textLaufMeterB.Text.Length == 0) {
                textUmzugLog.AppendText("Laufmeter dürfen nicht leer sein");
                return;
            }

            // String-Monstter in DB pushen

            textUmzugLog.AppendText(push(stringbauer()));

            // Ergebnis - Umzugsnummer anzeigen

            textUmzugsNummer.Text = "";

            MySqlCommand cmdShow = new MySqlCommand("SELECT * FROM Umzuege ORDER BY idUmzuege DESC LIMIT 1;", Program.conn);
            MySqlDataReader rdr;
            try
            {
                rdr = cmdShow.ExecuteReader();
                while (rdr.Read())
                {
                    textUmzugsNummer.Text += rdr.GetInt32(0);
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textUmzugLog.AppendText(sqlEx.ToString());
            }
            // Daten in Kalender Speichern

            textUmzugLog.AppendText(datumInKalender());

            // Laufzettel anlegen

            laufzettelBau();

        }

        private void buttonAendern_Click(object sender, EventArgs e)
        {
            UmzuegeSearch umzAendern = new UmzuegeSearch();
            umzAendern.setBearbeiter(idBearbeitend);
            umzAendern.umzugAenderungFuellem(int.Parse(textUmzugsNummer.Text));
            umzAendern.Show();
        }

        private void buttonTransaktion_Click(object sender, EventArgs e)
        {
            TransaktionAdd TranHinzufuegen = new TransaktionAdd();
            TranHinzufuegen.setBearbeiter(idBearbeitend);
            TranHinzufuegen.fuellen(int.Parse(textUmzugsNummer.Text));
            TranHinzufuegen.Show();
        }

        // Präparation Googlekalenderdaten

        private String KalenderString() {

            //Konstruktion String Kalerndereintragsinhalt
            // Name + Auszugsadresse
            String Body = textVorNachname.Text + "\r\n Aus: " + textStraßeA.Text + " " + textHausnummerA.Text + ", " + textPLZA.Text + " " + textOrtA.Text +"\r\n";
            if (listBoxA.SelectedItem.ToString() == "EFH")
            {
                if (numericStockwerkeA.Value == 1)
                {
                    Body += " EFH Eingeschossig ";
                }
                else if (numericStockwerkeA.Value == 2)
                {
                    Body += " EFH Zweigeschossig ";
                }
                else if (numericStockwerkeA.Value == 3)
                {
                    Body += " EFH Dreigeschossig ";
                }
                else if (numericStockwerkeA.Value == 4)
                {
                    Body += " EFH Viergeschossig ";
                }
            }
            else
            {
                if (numericStockwerkeA.Value == 0)
                {
                    Body += " Erdgeschoss " + listBoxA.SelectedItem + ",";
                }
                else {
                    Body += numericStockwerkeA.Value + " ET, " + listBoxA.SelectedItem + ", ";
                }
                
            }

            if (radioAufzugAJa.Checked)
            {
                Body += "mit Aufzug \r\n";
            }
            else { Body += "ohne Aufzug \r\n"; }

            //Einzugsadresse
            Body += "\r\n Nach: " + textStraßeB.Text + " " + textHausnummerB.Text + ", " + textPLZB.Text + " " + textOrtB.Text+ "\r\n";
            if (listBoxB.SelectedItem.ToString() == "EFH")
            {
                if (numericStockwerkeB.Value == 1)
                {
                    Body += " EFH Eingeschossig ";
                }
                else if (numericStockwerkeB.Value == 2)
                {
                    Body += " EFH Zweigeschossig ";
                }
                else if (numericStockwerkeB.Value == 3)
                {
                    Body += " EFH Dreigeschossig ";
                }
                else if (numericStockwerkeB.Value == 4)
                {
                    Body += " EFH Viergeschossig ";
                }
            }
            else
            {
                if (numericStockwerkeB.Value == 0)
                {
                    Body += " Erdgeschoss " + listBoxB.SelectedItem + ",";
                }
                else
                {
                    Body += numericStockwerkeB.Value + " ET, " + listBoxB.SelectedItem + ", ";
                }
            }
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

        private String hvzString() {
            if (radioHVZAJa.Checked && radioHVZBJa.Checked)
            {
                return "2 X HVZ";
            }
            else if (radioHVZAJa.Checked || radioHVZBJa.Checked)
            {
                return "1 X HVZ";
            }
            else {
                return "keine HVZ";
            }
        }

        private String AutoString() {

            String temp = "";
            if (numericSprinterMit.Value != 0) {
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
        // Eingetragene und relevante Zeiten zu Google pushen
        // Daten für Google präparieren

        private String datumInKalender() {

            String Header = textKundennummer.Text + " " + textVorNachname.Text + ", " + numericMannZahl.Value + " Mann, " + numericArbeitszeit.Value + " Stunden, " + AutoString() + " " + textNoteKalender.Text;
            String SchilderHeader = textKundennummer.Text + " " + textVorNachname.Text + ", Schilder stellen";
            String RaeumHeader = textKundennummer.Text + " " + textVorNachname.Text + ", " + numericPacker.Value + " Mann, " + numericPackStunden.Value + " Stunden";

            // Einzeln die Termine Abfragen und absenden

            // Besichtigungen sind immer fix
            if (radioBesJa.Checked)
            {
                DateTime date = dateBesicht.Value.Date.Add(timeBesichtigung.Value.TimeOfDay);
                DateTime schluss = date.AddMinutes(60);
                Program.kalenderEintrag(textKundennummer.Text +" "+ textVorNachname.Text+" "+textNoteKalender.Text, KalenderString(), 9, date, schluss);
            }

            // Bestätigte Umzüge in Rot, inklusive Schilder stellen weit genug im vorraus (in Lila / 3)
            // Bestätigte Umzüge bekommen konditional ein Schilder Stellen dazu
            if (radioUmzJa.Checked)
            {
                if (numericUmzugsDauer.Value == 1)
                {
                    Program.kalenderEintragGanz(Header, KalenderString(), hvzString(), 11, dateUmzug.Value.Date, dateUmzug.Value.Date);
                }
                else {
                    Program.kalenderEintragGanz(Header, KalenderString(), hvzString(), 11, dateUmzug.Value.Date, dateUmzug.Value.Date.AddDays(decimal.ToInt32(numericUmzugsDauer.Value)));
                   
                }
                // Schilder stellen
                if (radioHVZAJa.Checked) {
                    String Body = textStraßeA.Text + " " + textHausnummerA.Text + ", " + textPLZA.Text + " " + textOrtA.Text;
                    Program.kalenderEintragGanz(SchilderHeader, Body,"", 3, dateUmzug.Value.Date.AddDays(-6), dateUmzug.Value.Date.AddDays(-6));
                }

                if (radioHVZBJa.Checked) {
                    String Body = textStraßeB.Text + " " + textHausnummerB.Text + ", " + textPLZB.Text + " " + textOrtB.Text;
                    Program.kalenderEintragGanz(SchilderHeader, Body,"", 3, dateUmzug.Value.Date.AddDays(-6), dateUmzug.Value.Date.AddDays(-6));
                }

            }

            // Wie Umzug, aber Grasgrün / 10
            if (radioUmzVllt.Checked)   
            {
                if (numericUmzugsDauer.Value == 1)
                {
                    Program.kalenderEintragGanz(Header, KalenderString(), hvzString(), 10, dateUmzug.Value.Date, dateUmzug.Value.Date);
                }
                else {
                    Program.kalenderEintragGanz(Header, KalenderString(), hvzString(), 10, dateUmzug.Value.Date, dateUmzug.Value.Date.AddDays(decimal.ToInt32(numericUmzugsDauer.Value)));
                }
            }

            // Wie Umzug, aber Türkis
            if (radioUmzVorlaeufig.Checked)
            {
                if (numericUmzugsDauer.Value == 1)
                {
                    Program.kalenderEintragGanz(Header, KalenderString(), hvzString(), 2, dateUmzug.Value.Date, dateUmzug.Value.Date);
                }
                else
                {
                    Program.kalenderEintragGanz(Header, KalenderString(), hvzString(), 2, dateUmzug.Value.Date, dateUmzug.Value.Date.AddDays(decimal.ToInt32(numericUmzugsDauer.Value)));
                }
            }


            // Räum-Tage in Gelb / 5 

            if (radioEinJa.Checked)
            {                
               Program.kalenderEintragGanz(RaeumHeader, KalenderString(),"", 5, dateEinpack.Value.Date, dateEinpack.Value.Date);               
            }

            if (radioAusJa.Checked)
            {
                Program.kalenderEintragGanz(RaeumHeader, KalenderString(),"", 5, dateAuspack.Value.Date, dateAuspack.Value.Date);                
            }

            // Eventuelle Räum-Tage in Orange = 6

            if(radioEinVllt.Checked)
            {
                Program.kalenderEintragGanz(RaeumHeader, KalenderString(),"", 6, dateEinpack.Value.Date, dateEinpack.Value.Date);
            }

            if (radioAusVllt.Checked)
            {
                Program.kalenderEintragGanz(RaeumHeader, KalenderString(),"", 6, dateAuspack.Value.Date, dateAuspack.Value.Date);
            }

            // Entrümpelungen
            if (radioEntJa.Checked)
            {
                Program.kalenderEintragGanz(RaeumHeader + " ENTRÜMPELN", KalenderString(),"", 11, dateEntruempel.Value.Date, dateEntruempel.Value.Date);
            }
            if (radioEntVllt.Checked)
            {
                Program.kalenderEintragGanz(RaeumHeader + " ENTRÜMPELN", KalenderString(), "", 10, dateEntruempel.Value.Date, dateEntruempel.Value.Date);
            }


            return "Speichern nach Google erfolgreich!";
        }

        // Datum von Standard auf aktuell setzen, wenn "Festgelegt" geklickt wird.

        private void radioBesJa_CheckedChanged(object sender, EventArgs e)
        {
            dateBesicht.Value = DateTime.Now;
        }

        private void radioUmzJa_CheckedChanged(object sender, EventArgs e)
        {
            dateUmzug.Value = DateTime.Now;
        }

        private void radioEntJa_CheckedChanged(object sender, EventArgs e)
        {
            dateEntruempel.Value = DateTime.Now;
        }

        private void radioEinJa_CheckedChanged(object sender, EventArgs e)
        {
            dateEinpack.Value = DateTime.Now;
        }

        private void radioAusJa_CheckedChanged(object sender, EventArgs e)
        {
            dateAuspack.Value = DateTime.Now;
        }

        // Daten wieder auf Standard Zurücksetzen, wenn abgewählt

        private void radioBesNein_CheckedChanged(object sender, EventArgs e)
        {
            dateBesicht.Value = new DateTime(2017, 1, 1);
        }

        private void radioUmzNein_CheckedChanged(object sender, EventArgs e)
        {
            dateUmzug.Value = new DateTime(2017, 1, 1);
        }

        private void radioEntNein_CheckedChanged(object sender, EventArgs e)
        {
            dateEntruempel.Value = new DateTime(2017, 1, 1);
        }

        private void radioEinNein_CheckedChanged(object sender, EventArgs e)
        {
            dateEinpack.Value = new DateTime(2017, 1, 1);
        }

        private void radioAusNein_CheckedChanged(object sender, EventArgs e)
        {
            dateAuspack.Value = new DateTime(2017, 1, 1);
        }

        private void buttonDruck_Click(object sender, EventArgs e)
        {
            PdfDocument pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Besichtigungs Vordruck.pdf")), new PdfWriter(Program.druckPfad));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;

            String Name = ""; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.
            switch (idBearbeitend)
            {
                case 0:
                    Name = "Rita";
                    break;

                case 1:
                    Name = "Jonas";
                    break;

                case 2:
                    Name = "Eva";
                    break;

                case 3:
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

                fields.TryGetValue("Telefonnummer", out toSet);
                toSet.SetValue(textTelefonnummer.Text + " / " + textHandynummer.Text);

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
                if (listBoxA.SelectedItem.ToString() == "EFH") {
                    if (numericStockwerkeA.Value == 0) {
                        fields.TryGetValue("StockwerkeA", out toSet);
                        toSet.SetValue("Bungalow");
                    }
                    if (numericStockwerkeA.Value == 1)
                    {
                        fields.TryGetValue("StockwerkeA", out toSet);
                        toSet.SetValue("Eingeschossig");
                    }
                    if (numericStockwerkeA.Value == 2)
                    {
                        fields.TryGetValue("StockwerkeA", out toSet);
                        toSet.SetValue("Zweigeschossig");
                    }
                    if (numericStockwerkeA.Value == 3)
                    {
                        fields.TryGetValue("StockwerkeA", out toSet);
                        toSet.SetValue("Dreigeschossig");
                    }
                    if (numericStockwerkeA.Value == 4)
                    {
                        fields.TryGetValue("StockwerkeA", out toSet);
                        toSet.SetValue("Viergeschossig");
                    }
                }

                else
                {
                    if (numericStockwerkeA.Value != 0)
                    {
                        fields.TryGetValue("StockwerkeA", out toSet);
                        toSet.SetValue(numericStockwerkeA.Value.ToString());
                    }
                    else
                    {
                        fields.TryGetValue("StockwerkeA", out toSet);
                        toSet.SetValue("EG");
                    }
                }

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
                if (radioHVZANein.Checked)
                {
                    fields.TryGetValue("HVZANein", out toSet);
                    toSet.SetValue("Yes");
                }

                if (radioAufzugAJa.Checked)
                {
                    fields.TryGetValue("AufzugAJa", out toSet);
                    toSet.SetValue("Yes");
                }
                if (radioAufzugANein.Checked)
                {
                    fields.TryGetValue("AufzugANein", out toSet);
                    toSet.SetValue("Yes");
                }
                //
                if (radioAussenAufzugAJa.Checked)
                {
                    fields.TryGetValue("AussenAufzugAJa", out toSet);
                    toSet.SetValue("Yes");
                }
                if (radioAussenAufzugANein.Checked)
                {
                    fields.TryGetValue("AussenAufzugANein", out toSet);
                    toSet.SetValue("Yes");
                }

                // Adresse Einzug
                fields.TryGetValue("StrasseB", out toSet);
                toSet.SetValue(textStraßeB.Text + " " + textHausnummerB.Text);

                fields.TryGetValue("OrtB", out toSet);
                toSet.SetValue(textPLZB.Text + " " + textOrtB.Text);

                //Geschossigkeit
                if (listBoxB.SelectedItem.ToString() == "EFH")
                {
                    if (numericStockwerkeB.Value == 0)
                    {
                        fields.TryGetValue("StockwerkeB", out toSet);
                        toSet.SetValue("Bungalow");
                    }
                    if (numericStockwerkeB.Value == 1)
                    {
                        fields.TryGetValue("StockwerkeB", out toSet);
                        toSet.SetValue("Eingeschossig");
                    }
                    if (numericStockwerkeB.Value == 2)
                    {
                        fields.TryGetValue("StockwerkeB", out toSet);
                        toSet.SetValue("Zweigeschossig");
                    }
                    if (numericStockwerkeB.Value == 3)
                    {
                        fields.TryGetValue("StockwerkeB", out toSet);
                        toSet.SetValue("Dreigeschossig");
                    }
                    if (numericStockwerkeB.Value == 4)
                    {
                        fields.TryGetValue("StockwerkeB", out toSet);
                        toSet.SetValue("Viergeschossig");
                    }
                }

                else
                {
                    if (numericStockwerkeB.Value != 0)
                    {
                        fields.TryGetValue("StockwerkeB", out toSet);
                        toSet.SetValue(numericStockwerkeB.Value.ToString());
                    }
                    else
                    {
                        fields.TryGetValue("StockwerkeB", out toSet);
                        toSet.SetValue("EG");
                    }
                }

                if (textLaufMeterB.Text != "0")
                {
                    fields.TryGetValue("TragwegB", out toSet);
                    toSet.SetValue(textLaufMeterB.Text);
                }

                fields.TryGetValue("HausStatusB", out toSet);
                toSet.SetValue(listBoxB.SelectedItem.ToString());

                if (radioHVZBJa.Checked)
                {
                    fields.TryGetValue("HVZBJa", out toSet);
                    toSet.SetValue("Yes");
                }
                //if (radioHVZBV.Checked)
                //{
                //    fields.TryGetValue("HVZBVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioHVZBNein.Checked)
                {
                    fields.TryGetValue("HVZBNein", out toSet);
                    toSet.SetValue("Yes");
                }
                //
                if (radioAufzugBJa.Checked)
                {
                    fields.TryGetValue("AufzugBJa", out toSet);
                    toSet.SetValue("Yes");
                }
                if (radioAufzugBNein.Checked)
                {
                    fields.TryGetValue("AufzugBNein", out toSet);
                    toSet.SetValue("Yes");
                }
                //
                if (radioAussenAufzugBJa.Checked)
                {
                    fields.TryGetValue("AussenAufzugBJa", out toSet);
                    toSet.SetValue("Yes");
                }
                if (radioAussenAufzugBNein.Checked)
                {
                    fields.TryGetValue("AussenAufzugBNein", out toSet);
                    toSet.SetValue("Yes");
                }

                // Packen
                if (numericPacker.Value != 0)
                {
                    fields.TryGetValue("MannPacken", out toSet);
                    toSet.SetValue(numericPacker.Value.ToString());
                }

                if (numericPackStunden.Value != 0)
                {
                    fields.TryGetValue("StundenPacken", out toSet);
                    toSet.SetValue(numericPackStunden.Value.ToString());
                }

                //
                if (radioEinpackenJa.Checked)
                {
                    fields.TryGetValue("EinJa", out toSet);
                    toSet.SetValue("Yes");
                }
                //if (radioEinpackenV.Checked)
                //{
                //    fields.TryGetValue("EinVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioEinpackenNein.Checked)
                {
                    fields.TryGetValue("EinNein", out toSet);
                    toSet.SetValue("Yes");
                }
                //
                if (radioAuspackenJa.Checked)
                {
                    fields.TryGetValue("AusJa", out toSet);
                    toSet.SetValue("Yes");
                }
                //if (radioAuspackenV.Checked)
                //{
                //    fields.TryGetValue("AusVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioAuspackenNein.Checked)
                {
                    fields.TryGetValue("AusNein", out toSet);
                    toSet.SetValue("Yes");
                }
                //Küche
                if (radioKuecheAbJa.Checked)
                {
                    fields.TryGetValue("KuecheAbJa", out toSet);
                    toSet.SetValue("Yes");
                }
                //if (radioKuecheAbV.Checked)
                //{
                //    fields.TryGetValue("KuecheAbVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioKuecheAbNein.Checked)
                {
                    fields.TryGetValue("KuecheAbNein", out toSet);
                    toSet.SetValue("Yes");
                }
                //
                if (radioKuecheAufJa.Checked)
                {
                    fields.TryGetValue("KuecheAufJa", out toSet);
                    toSet.SetValue("Yes");
                }
                //if (radioKuecheAufV.Checked)
                //{
                //    fields.TryGetValue("KuecheAufVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioKuecheAufNein.Checked)
                {
                    fields.TryGetValue("KuecheAufNein", out toSet);
                    toSet.SetValue("Yes");
                }
                //
                //if (radioKuecheExtern.Checked)
                //{
                //    fields.TryGetValue("KuecheExtern", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioKuecheIntern.Checked)
                {
                    fields.TryGetValue("KuecheIntern", out toSet);
                    toSet.SetValue("Yes");
                }
                //
                if (textKuechenPreis.Text != "0")
                {
                    fields.TryGetValue("KuechePreis", out toSet);
                    toSet.SetValue(textKuechenPreis.Text);
                }

                // Restdaten
                if (numericMannZahl.Value != 0)
                {
                    fields.TryGetValue("Mann", out toSet);
                    toSet.SetValue(numericMannZahl.Value.ToString());
                }

                if (numericArbeitszeit.Value != 0)
                {
                    fields.TryGetValue("Stunden", out toSet);
                    toSet.SetValue(numericArbeitszeit.Value.ToString());
                }

                fields.TryGetValue("Autos", out toSet);
                toSet.SetValue(AutoString());

                if (numericKleiderkisten.Value != 0)
                {
                    fields.TryGetValue("Kleiderkisten", out toSet);
                    toSet.SetValue(numericKleiderkisten.Value.ToString());
                }
                //Bemerkungen
                fields.TryGetValue("NoteBuero", out toSet);
                toSet.SetValue(textNoteBuero.Text);

                fields.TryGetValue("NoteFahrer", out toSet);
                toSet.SetValue(textNoteFahrer.Text);

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

        private void laufzettelBau()
        {
            DateTime test = DateTime.Now;
            String go = "INSERT INTO Umzugsfortschritt (Umzuege_idUmzuege, Besichtigung, datBesichtigung) VALUES (" + textUmzugsNummer.Text + "," + idBearbeitend + ",'" + Program.DateMachine(test.Date) + "');";
            push(go);
        }

        private void buttonLaufzettel_Click(object sender, EventArgs e)
        {
            UmzugFortschritt laufzettel = new UmzugFortschritt();
            laufzettel.fuellen(int.Parse(textUmzugsNummer.Text));
            laufzettel.setBearbeitend(idBearbeitend);
            laufzettel.Show();
        }
    }
}
