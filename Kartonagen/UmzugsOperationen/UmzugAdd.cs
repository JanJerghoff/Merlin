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

            //aktUmzug.addAll();

            // Adresse für Entrümpeln speichern TODO


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
            PdfDocument pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Besichtigungs Vordruck.pdf")), new PdfWriter(Program.druckPfad));

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;

            

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

                if (radioHVZAJa.Checked)
                {
                    fields.TryGetValue("HVZAJa", out toSet);
                    toSet.SetValue("X");
                }



                //if (radioHVZAV.Checked)
                //{
                //    fields.TryGetValue("HVZAVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioHVZANein.Checked)
                {
                    fields.TryGetValue("HVZANein", out toSet);
                    toSet.SetValue("X");
                }

                if (radioAufzugAJa.Checked)
                {
                    fields.TryGetValue("AufzugAJa", out toSet);
                    toSet.SetValue("X");
                }

                if (radioAufzugANein.Checked)
                {
                    fields.TryGetValue("AufzugANein", out toSet);
                    toSet.SetValue("X");
                }


                if (radioAussenAufzugAJa.Checked)
                {
                    fields.TryGetValue("AussenAufzugAJa", out toSet);
                    toSet.SetValue("X");
                }
                if (radioAussenAufzugANein.Checked)
                {
                    fields.TryGetValue("AussenAufzugANein", out toSet);
                    toSet.SetValue("X");
                }

                // Adresse Einzug
                fields.TryGetValue("StrasseB", out toSet);
                toSet.SetValue(textStraßeB.Text + " " + textHausnummerB.Text);

                fields.TryGetValue("OrtB", out toSet);
                toSet.SetValue(textPLZB.Text + " " + textOrtB.Text);

                //Geschossigkeit

                fields.TryGetValue("StockwerkB", out toSet);
                toSet.SetValue(umzObj.einzug.KalenderStringEtageHaustyp());

                if (textLaufMeterB.Text != "0")
                {
                    fields.TryGetValue("TragwegB", out toSet);
                    toSet.SetValue(textLaufMeterB.Text);
                }

                if (radioHVZBJa.Checked)
                {
                    fields.TryGetValue("HVZBJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioHVZBV.Checked)
                //{
                //    fields.TryGetValue("HVZBVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioHVZBNein.Checked)
                {
                    fields.TryGetValue("HVZBNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (radioAufzugBJa.Checked)
                {
                    fields.TryGetValue("AufzugBJa", out toSet);
                    toSet.SetValue("X");
                }
                if (radioAufzugBNein.Checked)
                {
                    fields.TryGetValue("AufzugBNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (radioAussenAufzugBJa.Checked)
                {
                    fields.TryGetValue("AussenAufzugBJa", out toSet);
                    toSet.SetValue("X");
                }
                if (radioAussenAufzugBNein.Checked)
                {
                    fields.TryGetValue("AussenAufzugBNein", out toSet);
                    toSet.SetValue("X");
                }

                // Packen

                //
                if (radioEinpackenJa.Checked)
                {
                    fields.TryGetValue("EinJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioEinpackenV.Checked)
                //{
                //    fields.TryGetValue("EinVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioEinpackenNein.Checked)
                {
                    fields.TryGetValue("EinNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (radioAuspackenJa.Checked)
                {
                    fields.TryGetValue("AusJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioAuspackenV.Checked)
                //{
                //    fields.TryGetValue("AusVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioAuspackenNein.Checked)
                {
                    fields.TryGetValue("AusNein", out toSet);
                    toSet.SetValue("X");
                }

                //Küche
                if (radioKuecheAbJa.Checked)
                {
                    fields.TryGetValue("KuecheAbJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioKuecheAbV.Checked)
                //{
                //    fields.TryGetValue("KuecheAbVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioKuecheAbNein.Checked)
                {
                    fields.TryGetValue("KuecheAbNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (radioKuecheAufJa.Checked)
                {
                    fields.TryGetValue("KuecheAufJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioKuecheAufV.Checked)
                //{
                //    fields.TryGetValue("KuecheAufVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioKuecheAufNein.Checked)
                {
                    fields.TryGetValue("KuecheAufNein", out toSet);
                    toSet.SetValue("X");
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
                    toSet.SetValue("X");
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
