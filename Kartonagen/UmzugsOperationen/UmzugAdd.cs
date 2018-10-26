using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
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
    public partial class UmzugAdd : Form
    {
        static Umzug umzObj;

        public UmzugAdd()
        {
            
            InitializeComponent();
            
            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            // Vorlegen der ListBoxen
            listBoxA.SelectedIndex = 3;
            listBoxB.SelectedIndex = 3;

            dateAuspack.Value = DateTime.Now;
            dateBesicht.Value = DateTime.Now;
            dateEinpack.Value = DateTime.Now;
            dateEntruempel.Value = DateTime.Now;
            dateUmzug.Value = DateTime.Now;


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
                    daten[tempCounter]= rdr.GetDateTime(2).ToShortDateString();                                        //   Fix steht aus
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
                Program.FehlerLog(sqlEx.ToString(),"Pushen des neuen Umzugs mit String | "+push);
                //return "Fehlgeschlagen \r\n";
                return sqlEx.ToString()+"\r\n"+push;
            }
            
        }

        private Umzug umzugBauer()
        {
            int aufzugtemp;
            int hvztemp;
            int aussenaufzugtemp;
            Adresse ruempelAdresse = null;
            int ruempelMann = 0;
            int ruempelStunde = 0;

            // Belegung der Temps für die Adresserstellung.
            if (radioAufzugAJa.Checked) { aufzugtemp = 1; }
            else if (radioAufzugANein.Checked) { aufzugtemp = 0; }
            else { aufzugtemp = 8; }

            if (radioAussenAufzugAJa.Checked) { aussenaufzugtemp = 1; }
            else if (radioAussenAufzugANein.Checked) { aussenaufzugtemp = 0; }
            else { aussenaufzugtemp = 8; }

            if (radioHVZAJa.Checked) { hvztemp = 1; }
            else if (radioHVZAV.Checked) { hvztemp = 2; }
            else if (radioHVZANein.Checked) { hvztemp = 0; }
            else { hvztemp = 8; }

            Adresse aus = new Adresse(textStraßeA.Text, textHausnummerA.Text, textOrtA.Text, textPLZA.Text, textLandA.Text, aufzugtemp, StockwerkString(0), listBoxA.SelectedItem.ToString(), hvztemp, int.Parse(textLaufMeterA.Text), aussenaufzugtemp);

            // Belegung der Temps für die Adresserstellung.
            if (radioAufzugBJa.Checked) { aufzugtemp = 1; }
            else if (radioAufzugBNein.Checked) { aufzugtemp = 0; }
            else { aufzugtemp = 8; }

            if (radioAussenAufzugBJa.Checked) { aussenaufzugtemp = 1; }
            else if (radioAussenAufzugBNein.Checked) { aussenaufzugtemp = 0; }
            else { aussenaufzugtemp = 8; }

            if (radioHVZBJa.Checked) { hvztemp = 1; }
            else if (radioHVZBV.Checked) { hvztemp = 2; }
            else if (radioHVZBNein.Checked) { hvztemp = 0; }
            else { hvztemp = 8; }

            Adresse ein = new Adresse(textStraßeB.Text, textHausnummerB.Text, textOrtB.Text, textPLZB.Text, textLandB.Text, aufzugtemp, StockwerkString(1), listBoxB.SelectedItem.ToString(), hvztemp, int.Parse(textLaufMeterB.Text), aussenaufzugtemp);

            // Temps für die Umzugserstellung
            // Status des Datums. 0 = nicht festgelegt, 1 = festgelegt, 2 = vorläufig (wenn möglich), 3 = Vorläufig gebucht (bei Umzügen)
            // Reihenfolge ist Umz, Bes, Aus, Ein, Ent
            List<int> stat = new List<int>();

            if (radioUmzJa.Checked) { stat.Add(1); }
            else if (radioUmzVllt.Checked) { stat.Add(2); }
            else if (radioUmzVorlaeufig.Checked) { stat.Add(3); }
            else { stat.Add(0); }

            //
            if (radioBesJa.Checked) { stat.Add(1); }
            else { stat.Add(0); }

            //
            if (radioAusJa.Checked) { stat.Add(1); }
            else if (radioAusVllt.Checked) { stat.Add(2); }
            else { stat.Add(0); }
            //
            if (radioEinJa.Checked) { stat.Add(1); }
            else if (radioEinVllt.Checked) { stat.Add(2); }
            else { stat.Add(0); }
            //
            if (radioEntJa.Checked) { stat.Add(1); }
            else if (radioEntVllt.Checked) { stat.Add(2); }
            else { stat.Add(0); }

            // String Autos
            String tempAuto = "";
            tempAuto = tempAuto + decimal.ToInt32(numericSprinterMit.Value).ToString();
            tempAuto = tempAuto + decimal.ToInt32(numericSprinterOhne.Value).ToString();
            tempAuto = tempAuto + decimal.ToInt32(numericLKW.Value).ToString();
            tempAuto = tempAuto + decimal.ToInt32(numericLKWGroß.Value).ToString();

            int einpacktemp = 0;
            int auspacktemp = 0;

            if (radioEinpackenJa.Checked) { einpacktemp = 1; }
            else if (radioEinpackenV.Checked) { einpacktemp = 2; }
            else if (radioEinpackenNein.Checked) { einpacktemp = 0; }
            else { einpacktemp = 8; }

            if (radioAuspackenJa.Checked) { auspacktemp = 1; }
            else if (radioAuspackenV.Checked) { auspacktemp = 2; }
            else if (radioAuspackenNein.Checked) { auspacktemp = 0; }
            else { auspacktemp = 8; }

            List<int> kueche = new List<int>();
            if (radioKuecheAufJa.Checked) { kueche.Add(1); } else if (radioKuecheAufV.Checked) { kueche.Add(2); } else if (radioKuecheAufNein.Checked) { kueche.Add(0); } else { kueche.Add(8); }
            if (radioKuecheAbJa.Checked) { kueche.Add(1); } else if (radioKuecheAbV.Checked) { kueche.Add(2); } else if (radioKuecheAbNein.Checked) { kueche.Add(0); } else { kueche.Add(8); }
            if (radioKuecheIntern.Checked) { kueche.Add(1); } else if (radioKuecheExtern.Checked) { kueche.Add(0); } else { kueche.Add(8); }

            int versicherungtemp = 8;
            int Schildertemp = 8;

            if (radioVersicherungJa.Checked) { versicherungtemp = 1; }
            else if (radioVersicherungNein.Checked) { versicherungtemp = 0; }

            if (radioSchilderJa.Checked) { Schildertemp = 1; }
            else if (radioSchilderNein.Checked) { Schildertemp = 0; }

            if (stat[4] != 0) {
                ruempelAdresse = new Adresse(textStrasseEnt.Text, textHausnummerEnt.Text, textOrtEnt.Text, textPLZEnt.Text, "Deutschland", 0, "", "", 0, 0, 0);
                ruempelMann = decimal.ToInt32(numericPackerEnt.Value);
                ruempelStunde = decimal.ToInt32(numericStundenEnt.Value);
            }

            int kundennummerTemp = int.Parse(textKundennummer.Text.Trim());

            if (kundennummerTemp == 0)
            {
                var bestätigung = MessageBox.Show("Fehler", "Kundennummer als 0 gelesen");
            }

            return umzObj = new Umzug(kundennummerTemp, dateBesicht.Value, dateUmzug.Value, dateEinpack.Value, dateAuspack.Value, dateEntruempel.Value, timeBesichtigung.Value, stat[1], stat[0], stat[2], stat[3], stat[4],
                decimal.ToInt32(numericUmzugsDauer.Value), tempAuto, decimal.ToInt32(numericMannZahl.Value), decimal.ToInt32(numericArbeitszeit.Value), versicherungtemp, einpacktemp, decimal.ToInt32(numericEinPacker.Value), decimal.ToInt32(numericEinPackStunden.Value), decimal.ToInt32(numericEinPackKartons.Value),
                auspacktemp, decimal.ToInt32(numericAusPacker.Value), decimal.ToInt32(numericAusPackStunden.Value), decimal.ToInt32(numericKleiderkisten.Value), kueche[1], kueche[0], kueche[2], int.Parse(textKuechenPreis.Text), aus, ein, Schildertemp, dateSchilderVerweildauer.Value,
                textNoteKalender.Text, textNoteBuero.Text, textNoteFahrer.Text, idBearbeitend.ToString(), DateTime.Now, ruempelAdresse, ruempelMann, ruempelStunde);
        }

        private string StockwerkString(int x) {

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

        private void buttonSchnellSpeichern_Click(object sender, EventArgs e)
        {
            // Wirklich?
            var bestätigung = MessageBox.Show("Den Umzug wirklich hinzufügen?", "Hinzufügen bestätigen", MessageBoxButtons.YesNo);
            if (bestätigung == DialogResult.No)
            {
                textUmzugLog.AppendText("Vorgang abgebrochen\r\n");
                return;
            }

            //Sicherheitsabfrage für int.parse() Methoden: Laufweg

            if (textLaufMeterA.Text.Length == 0 || textLaufMeterB.Text.Length == 0) {
                textUmzugLog.AppendText("Laufmeter dürfen nicht leer sein");
                return;
            }

            // String-Monstter in DB pushen und Termine anlegen

            Umzug aktUmzug = umzugBauer();

            MySqlCommand cmdShow = new MySqlCommand("SELECT * FROM Umzuege ORDER BY idUmzuege DESC LIMIT 1;", Program.conn);
            MySqlDataReader rdr;
            try
            {
                rdr = cmdShow.ExecuteReader();
                while (rdr.Read())
                {
                    textUmzugsNummer.Text += rdr.GetInt32(0);
                    aktUmzug.Id = rdr.GetInt32(0);
                    textUmzugsNummer.AppendText(aktUmzug.Id.ToString());
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Ergebnis-Umzugsnummer Anzeigen");
            }

            umzObj = aktUmzug;

            // Ergebnis - Umzugsnummer anzeigen
            textUmzugsNummer.Text = "";
            
            
            // Laufzettel anlegen
            laufzettelBau();
            textUmzugLog.AppendText("Umzug vollständig angelegt \r\n");
        }

        private void buttonAendern_Click(object sender, EventArgs e)
        {
            UmzuegeSearch umzAendern = new UmzuegeSearch();
            umzAendern.setBearbeiter(idBearbeitend);
            umzAendern.SetUmzugObjekt( umzObj);
            umzAendern.umzugAenderungFuellem();
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

            Body += umzObj.auszug.KalenderStringEtageHaustyp();

            if (radioAufzugAJa.Checked)
            {
                Body += "mit Aufzug \r\n";
            }
            else { Body += "ohne Aufzug \r\n"; }

            //Einzugsadresse
            Body += "\r\n Nach: " + textStraßeB.Text + " " + textHausnummerB.Text + ", " + textPLZB.Text + " " + textOrtB.Text+ "\r\n";

            Body += umzObj.einzug.KalenderStringEtageHaustyp();

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
      
        private void buttonDruck_Click(object sender, EventArgs e)
        {
            if (umzObj == null)
            {
                //TODO FEHLERMELDUNG
            }
            else {
                umzObj.druck(1);
            }
            
            textUmzugLog.AppendText("Erfolgreich gedruckt");
        }

        private void laufzettelBau()
        {
            DateTime test = DateTime.Now;
            String go = "INSERT INTO Umzugsfortschritt (Umzuege_idUmzuege, Besichtigung, datBesichtigung) VALUES (" + umzObj.Id + "," + idBearbeitend + ",'" + Program.DateMachine(test.Date) + "');";
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
