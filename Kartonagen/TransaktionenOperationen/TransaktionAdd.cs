using Google.Apis.Calendar.v3.Data;
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
    public partial class TransaktionAdd : Form
    {

        Umzug umzObj;
        Kunde kundenObj;

        int maxKundennummer;
        public TransaktionAdd()
        {
            InitializeComponent();

            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            dateTimeTransaktion.Value = DateTime.Now;

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
            }
            catch (Exception sqlEx)
            {
                textTransaktionLog.Text += sqlEx.ToString();
                return;
            }

        }

        private int idBearbeitend = 0; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.

        public void setBearbeiter(int wer)
        {
            idBearbeitend = wer;
        }

        public void fuellen(int umzNummer) {

            labelLieferung.Visible = false;
            labelAbholung.Visible = false;

            umzObj = new Umzug(umzNummer);
            kundenObj = new Kunde(umzObj.IdKunden);

            // Umzugsdaten einfüllen
            textKundennummer.Text = umzObj.IdKunden+"";
            textUmzugsdatum.Text = umzObj.DatUmzug.ToShortDateString();
            textUmzugsnummer.Text = umzObj.Id+"";

            textVorNachname.Text = kundenObj.getVollerName();
        
            // Kartonagenkonto Berechnen
            

            int Kartons = 0;
            int GlaeserKartons = 0;
            int FlaschenKartons = 0;
            int KleiderKartons = 0;

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }
                MySqlCommand cmdReadKonto = new MySqlCommand("SELECT Kartons, GlaeserKartons, FlaschenKartons, KleiderKartons FROM Transaktionen WHERE Umzuege_Kunden_idKunden=" + textKundennummer.Text + " AND unbenutzt != 2;", Program.conn);
                MySqlDataReader rdrKonto = cmdReadKonto.ExecuteReader();
                while (rdrKonto.Read())
                {
                    Kartons += rdrKonto.GetInt32(0);
                    GlaeserKartons += rdrKonto.GetInt32(1);
                    FlaschenKartons += rdrKonto.GetInt32(2);
                    KleiderKartons += rdrKonto.GetInt32(3);

                }
                rdrKonto.Close();
                Program.conn.Close();

                textKartonAusstehend.Text = Kartons.ToString();
                textGlaeserAusstehend.Text = GlaeserKartons.ToString();
                textFlaschenAusstehend.Text = FlaschenKartons.ToString();
                textKleiderAusstehend.Text = KleiderKartons.ToString();
                
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Berechnen des Kartonagenkontos \r\n Bereits dokumentiert.");
                return;
            }

            // Überblick Rechts nullen
            dataGridAlteTransaktionen.Rows.Clear();
            dataGridAlteTransaktionen.Refresh();

            // Bisherige Buchungen Parsen
            
            string textUnbenutzt = "";

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }
                MySqlCommand cmdReadAlt = new MySqlCommand("SELECT * FROM Transaktionen WHERE Umzuege_idUmzuege=" + textUmzugsnummer.Text + ";", Program.conn);
                MySqlDataReader rdrAlt = cmdReadAlt.ExecuteReader();

                while (rdrAlt.Read())
                {
                    if (rdrAlt.GetInt32(11) == 2)
                    {
                        textUnbenutzt = "Kauf";
                    }
                    else if (rdrAlt.GetInt32(11) == 1)
                    {
                        textUnbenutzt = "x";
                    }
                    else
                    {
                        textUnbenutzt = "";
                    }

                    object[] toSave = { rdrAlt.GetDateTime(1), rdrAlt[0], rdrAlt.GetString(12), rdrAlt.GetInt32(2), rdrAlt.GetInt32(3), rdrAlt.GetInt32(4), rdrAlt.GetInt32(5), textUnbenutzt,null};
         
                    dataGridAlteTransaktionen.Rows.Add(toSave);
                }
                rdrAlt.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Finden bisheriger Buchungen \r\n Bereits dokumentiert.");
                return;
            }

            // Kalender auf bisherige Lieferung auslesen

            Events check = Program.getUtil().kalenderKundenFinder(textKundennummer.Text);
            try
            {
                foreach (var item in check.Items)
                {
                    if (item.Description.Contains("Kartonlieferung"))
                    {
                        labelLieferung.Visible = true;
                    }
                    else if (item.Description.Contains("Kartonabholung"))
                    {
                        labelAbholung.Visible = true;
                    }
                }
            }
            catch (Exception test)
            {
                // Ignorieren des unausweichlichen Fehlers, wenn zu einem Kunden kein Kalendereintrag existiert
                textTransaktionLog.AppendText("Keine Termine zum Kunden im Kalender \r\n");
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Alte Transaktionsnummer löchen

            textResultatsNummer.Text = "";

            // Häkchen gesetzt?

            if (radioAusgang.Checked == false && radioEingang.Checked == false)
            {
                textTransaktionLog.AppendText(" Bitte Ausgang oder Eingang auswählen!\r\n");
                return;
            }

            Boolean kaufkartons = false;
            if (radioKaufJa.Checked) { kaufkartons = true; }

            Boolean unbenutzt = false;
            if (radioUnbenutzt.Checked) { unbenutzt = true; }

            Adresse Adressobj = new Adresse(textStraße.Text,textHausnummer.Text, textOrt.Text, textPLZ.Text, "Deutschland", 0,"","",0,0,0);

            Transaktion transObj = new Transaktion(Decimal.ToInt32(numericKarton.Value), Decimal.ToInt32(numericGlaeserkarton.Value), Decimal.ToInt32(numericFlaschenKarton.Value), Decimal.ToInt32(numericKleiderKarton.Value), kaufkartons,unbenutzt,textBemerkung.Text,textRechnungsnr.Text, Program.getUtil().mergeDatetime(dateTimeTransaktion.Value,timeLieferzeit.Value),Adressobj.IDAdresse1,umzObj.Id,kundenObj.Id,"0");
            
            //Ergebnis-Transaktionsnummer anzeigen
            textResultatsNummer.Text = transObj.getId()+"";


            // Termine in Kalender pushen wenn relevant
            if (checkTermin.Checked)
            {
                Kalendereintrag();
            }


            //Neu Fuellen, um den Kontostand aktuell zu halten
            fuellen(int.Parse(textUmzugsnummer.Text));
            // Felder auf Null setzen
            numericKarton.Value = 0;
            numericFlaschenKarton.Value = 0;
            numericGlaeserkarton.Value = 0;
            numericKleiderKarton.Value = 0;
            textRechnungsnr.Text = "";



            //

            if (radioAusgang.Checked)
            {
                radioAusgang.Checked = false;
            }
            if (radioEingang.Checked)
            {
                radioEingang.Checked = false;
            }

        }

        private void Kalendereintrag() {

            String Titel = textKundennummer.Text + " " + textVorNachname.Text;
            if (radioAusgang.Checked)
            {
                Titel += " " + "Lieferung";
            }
            else {
                Titel += " " + "Abholung";
            }

            String Body = "";

            //Adresse in den Body

            // Kontaktdaten
            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }
                MySqlCommand cmdRead = new MySqlCommand("SELECT Handynummer, Telefonnummer FROM Kunden WHERE idKunden = " + textKundennummer.Text + ";", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetString(0) == "0")
                    {
                        Body += "Telefon = " + rdr.GetString(1)+"\r\n";
                    }
                    else { Body += "Handy = " + rdr.GetString(0) + "\r\n"; }
                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Kundenadresse \r\n Bereits dokumentiert.");
                return;
            }

            if (radioAusgang.Checked)
            {
                Body +="Kartonlieferung";
            }
            else
            {
                Body += "Kartonabholung";
            }

            if (radioAusgang.Checked&&labelLieferung.Visible)
            {
                Body += " Kostenpflichtig!";
            }
            else if (radioEingang.Checked && labelAbholung.Visible)
            {
                Body += " Kostenpflichtig!";
            }

            Body += " über ";

            if (numericKarton.Value != 0) {
                Body += numericKarton.Value.ToString()+" Kartons ";
            }
            if (numericGlaeserkarton.Value != 0)
            {
                Body += numericGlaeserkarton.Value.ToString() + " Gläserkartons ";
            }
            if ( numericFlaschenKarton.Value != 0)
            {
                Body += numericFlaschenKarton.Value.ToString() + " Flaschenkartons ";
            }
            if (numericKleiderKarton.Value != 0)
            {
                Body += numericKleiderKarton.Value.ToString() + " Kleiderkartons ";
            }

            Body += "\r\n Transaktionsnummer =" + textResultatsNummer.Text;

            Body += ", Zeichen =" + Program.getBearbeitender(idBearbeitend);

            if (radioEingang.Checked) { Body += " tatsächliche Kartonzahl nachkorrigieren"; }

            DateTime start = new DateTime(dateTimeTransaktion.Value.Year, dateTimeTransaktion.Value.Month, dateTimeTransaktion.Value.Day, timeLieferzeit.Value.Hour, timeLieferzeit.Value.Minute, 0);

            Program.getUtil().kalenderEventEintrag(Titel, Body, 8, start, start.AddHours(1));

            textTransaktionLog.AppendText(" Kalendereintrag erfolgreich!\r\n");

        }

        private void buttonKundenSearchNrSuche_Click(object sender, EventArgs e)
        {
            int worked = 0;

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }
                MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Umzuege WHERE idUmzuege=" + numericUmzugsnummer.Value + ";", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();

                while (rdr.Read())
                {
                    worked = 1;
                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                textTransaktionLog.Text += sqlEx.ToString();
                return;
            }

            if (worked == 0)
            {
                textTransaktionLog.AppendText("Umzug nicht gefunden");
            }

            fuellen(Decimal.ToInt32(numericUmzugsnummer.Value));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maxKundennummer >= numericKundennummer.Value)
            {
                transKundennummerFuellen(decimal.ToInt32(numericKundennummer.Value));
            }
            else {
                textTransaktionLog.AppendText("Kein Kunde mit dieser Nummer vorhanden \r\n");
            }
        }

        private void buttonNameSuche_Click(object sender, EventArgs e)
        {
            int tempCounter = 0;
            // Bricht, wenn mehr als 30 gleichnamige Kunden
            int[] nummern = new int[30];
            String[] daten = new String[30];
            String[] vornamen = new String[30];

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }
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
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen des Kunden \r\n Bereits dokumentiert.");
                return;
            }

            // Entscheidung einzelner / mehrere Treffer

            if (tempCounter == 0)           // Kein Treffer
            {
                textTransaktionLog.AppendText("Fehler: Kein Kunde zum Namen gefunden \r\n");
                return;
            }
            else if (tempCounter == 1)      // Ein treffer
            {
                transKundennummerFuellen(nummern[0]);
            }
            else
            {                          // Mehrere Treffer
                for (int i = 0; i < tempCounter; i++)
                {
                    textSuchBox.Text += vornamen[i] + " " + textSucheName.Text + " hinzugefügt am" + daten[i] + ": " + nummern[i] + " \r\n";
                }
            }
        }
        

        public void transKundennummerFuellen(int kundenNr) {

            // Bricht, wenn mehr als 30 gleichnamige Kunden
            int[] nummern = new int[30];
            String[] umzDaten = new String[30];
            int tempCounter = 0;

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }
                MySqlCommand cmdRead = new MySqlCommand("SELECT idUmzuege,datUmzug FROM Umzuege WHERE Kunden_idKunden = " + kundenNr + ";", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();

                while (rdr.Read())
                {
                    nummern[tempCounter] = rdr.GetInt32(0);
                    // test = rdr.GetDateTime(2).ToShortDateString();                                           Fix steht aus
                    tempCounter += 1;
                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auffüllen der Umzugsdaten \r\n Bereits dokumentiert.");
                return;
            }

            // Entscheidung einzelner / mehrere Treffer

            if (tempCounter == 0)           // Kein Treffer
            {
                textTransaktionLog.AppendText("Fehler: Kein Umzug zum Kunden mit der Nummer " + kundenNr + " gefunden \r\n");
                return;
            }
            else if (tempCounter == 1)      // Ein treffer
            {
                fuellen(nummern[0]);
            }
            else
            {                          // Mehrere Treffer
                for (int i = 0; i < tempCounter; i++)
                {
                    textSuchBox.AppendText("Umzug Nummer " + nummern[i] + " vom Datum " + umzDaten[i] + " \r\n");
                }
            }
        }

        private void radioEingang_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            groupBox4.Enabled = false;
            radioKaufJa.Checked = false;
        }

        private void radioAusgang_CheckedChanged(object sender, EventArgs e)
        {
            radioBenutzt.Checked = true;
            groupBox2.Enabled = false;
            groupBox4.Enabled = true;
        }

        private void radioKaufJa_CheckedChanged(object sender, EventArgs e)
        {
            radioAusgang.Checked = true;
            radioEingang.Enabled = false;
            radioUnbenutzt.Checked = false;
        }

        private void radioKaufNein_CheckedChanged(object sender, EventArgs e)
        {
            radioEingang.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTermin.Checked)
            {
                timeLieferzeit.Enabled = true;
                groupBoxAdresse.Enabled = true;
                radioAuszugsadresse.Checked = true;
            }
            else {
                timeLieferzeit.Enabled = false;
                groupBoxAdresse.Enabled = false;
            }
        }

        private void buttonTransaktionAendern_Click(object sender, EventArgs e)
        {
            TransaktionenSearch transaktionenSuche = new TransaktionenSearch();
            transaktionenSuche.setBearbeiter(idBearbeitend);
            transaktionenSuche.Show();
        }

        private void radioAuszugsadresse_CheckedChanged(object sender, EventArgs e)
        {
            if (radioAuszugsadresse.Checked) {
                textStraße.Text = umzObj.auszug.Straße1;
                textHausnummer.Text = umzObj.auszug.Hausnummer1;
                textOrt.Text = umzObj.auszug.Ort1;
                textPLZ.Text = umzObj.auszug.PLZ1;
            }
        }

        private void radioEinzugsadresse_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEinzugsadresse.Checked)
            {
                textStraße.Text = umzObj.einzug.Straße1;
                textHausnummer.Text = umzObj.einzug.Hausnummer1;
                textOrt.Text = umzObj.einzug.Ort1;
                textPLZ.Text = umzObj.einzug.PLZ1;
            }
            
        }
    }
    
}
