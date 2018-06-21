using Google.Apis.Calendar.v3.Data;
using Kartonagen.CalendarAPIUtil;
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
        Transaktion transObj;

        int maxKundennummer;

        public TransaktionAdd()
        {
            InitializeComponent();

            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            dateTimeTransaktion.Value = DateTime.Now;

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

            // Umzugsdaten aus dem Umzug ziehen

            MySqlCommand cmdReadUmzug = new MySqlCommand("SELECT Kunden_idKunden, datUmzug FROM Umzuege WHERE idUmzuege=" + umzNummer + ";", Program.conn);
            MySqlDataReader rdrUmzug;
            int kundennummer = 0;

            try
            {
                rdrUmzug = cmdReadUmzug.ExecuteReader();
                while (rdrUmzug.Read())
                {
                    textKundennummer.Text = rdrUmzug[0] + "";
                    kundennummer = rdrUmzug.GetInt32(0);
                    textUmzugsdatum.Text = rdrUmzug.GetDateTime(1).ToShortDateString();
                    textUmzugsnummer.Text = umzNummer + "";
                }
                rdrUmzug.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Umzugsdaten \r\n Bereits dokumentiert.");
                return;
            }
            
            // Personendaten aus dem Kunden ziehen

            MySqlCommand cmdReadKunde = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden=" + textKundennummer.Text + ";", Program.conn);
            MySqlDataReader rdrKunde;

            try
            {
                rdrKunde = cmdReadKunde.ExecuteReader();
                while (rdrKunde.Read())
                {

                    textVorNachname.Text = rdrKunde[1] + " " + rdrKunde[2] + " " + rdrKunde[3];
                }
                rdrKunde.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Personendaten \r\n Bereits dokumentiert.");
                return;
            }

            // Kartonagenkonto Berechnen

            MySqlCommand cmdReadKonto = new MySqlCommand("SELECT Kartons, GlaeserKartons, FlaschenKartons, KleiderKartons FROM Transaktionen WHERE Umzuege_Kunden_idKunden=" + textKundennummer.Text + " AND unbenutzt != 2;", Program.conn);
            MySqlDataReader rdrKonto;

            int Kartons = 0;
            int GlaeserKartons = 0;
            int FlaschenKartons = 0;
            int KleiderKartons = 0;

            try
            {
                rdrKonto = cmdReadKonto.ExecuteReader();
                while (rdrKonto.Read())
                {
                    Kartons += rdrKonto.GetInt32(0);
                    GlaeserKartons += rdrKonto.GetInt32(1);
                    FlaschenKartons += rdrKonto.GetInt32(2);
                    KleiderKartons += rdrKonto.GetInt32(3);

                }
                rdrKonto.Close();
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
            textAltDatum.Text = "";
            textAltKarton.Text = "";
            textAltFlaschen.Text = "";
            textAltGlaeser.Text = "";
            textAltKleider.Text = "";
            textTransNummer.Text = "";
            textAltRechnungsnr.Text = "";
            textUnbenutzt.Text = "";

            // Bisherige Buchungen Parsen

            MySqlCommand cmdReadAlt = new MySqlCommand("SELECT * FROM Transaktionen WHERE Umzuege_idUmzuege=" + textUmzugsnummer.Text + ";", Program.conn);
            MySqlDataReader rdrAlt;

            try
            {
                rdrAlt = cmdReadAlt.ExecuteReader();
                while (rdrAlt.Read())
                {
                    textAltRechnungsnr.AppendText(rdrAlt[12] + "\r\n");
                    textAltDatum.AppendText(rdrAlt.GetDateTime(1).ToShortDateString() + "\r\n");
                    textAltKarton.AppendText(rdrAlt[2] + "\r\n");
                    textAltFlaschen.AppendText(rdrAlt[3] + "\r\n");
                    textAltGlaeser.AppendText(rdrAlt[4] + "\r\n");
                    textAltKleider.AppendText(rdrAlt[5] + "\r\n");
                    textTransNummer.AppendText(rdrAlt[0] + "\r\n");
                    if (rdrAlt.GetInt32(11) == 2)
                    {
                        textUnbenutzt.AppendText("Kauf \r\n");
                    }
                    else if (rdrAlt.GetInt32(11) == 1){
                        textUnbenutzt.AppendText("x \r\n");
                    }
                    else {
                        textUnbenutzt.AppendText("\r\n");
                    }
                    ;
                }
                rdrAlt.Close();
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

            // Neue Sektion, Hinzufügen über das Transaktionsobjekt. Multiplikator -1 für Auslieferungen

            int multi = 1;
            int adressid = 0;


            if (radioAusgang.Checked)
            {
                multi = -1;
            }

            //check welche Adresse gewählt ist

            var adressselect = new AdressSelect3(umzObj.auszug,umzObj.einzug);
            adressselect.ShowDialog();

            if (adressselect.DialogResult == DialogResult.Yes) {
                adressid = umzObj.auszug.IDAdresse1;
            } else if (adressselect.DialogResult == DialogResult.Yes) {
                adressid = umzObj.einzug.IDAdresse1;
            }

            transObj = new Transaktion(decimal.ToInt32(numericKarton.Value * multi), decimal.ToInt32(numericGlaeserkarton.Value * multi), decimal.ToInt32(numericFlaschenKarton.Value * multi), decimal.ToInt32(numericKleiderKarton.Value * multi), radioKaufJa.Checked, radioUnbenutzt.Checked, textBemerkung.Text, textRechnungsnr.Text, dateTimeTransaktion.Value.Date, adressid, umzObj.Id,umzObj.IdKunden, "8");




            ////String bauen
            //String push = "INSERT INTO Transaktionen (datTransaktion, timeTransaktion, Kartons, FlaschenKartons, GlaeserKartons, KleiderKartons, Umzuege_idUmzuege, Umzuege_Kunden_idKunden, Bemerkungen, UserChanged, Erstelldatum, unbenutzt, final, RechnungsNr) VALUES (";

            //push += "'" + Program.DateMachine(dateTimeTransaktion.Value) + "', ";

            //// Zeitkomponente pushen wenn Termin in der Zukunft
            //if (checkTermin.Checked)
            //{
            //    push += "'" + Program.DateMachine(dateTimeTransaktion.Value) + " " + Program.ZeitMachine(timeLieferzeit.Value) + "', ";
            //}
            //else { push += "'" + Program.DateMachine(dateTimeTransaktion.Value) + " 00-00-00', "; }

            //if (radioAusgang.Checked)
            //{
            //    push += numericKarton.Value + ", ";
            //    push += numericFlaschenKarton.Value + ", ";
            //    push += numericGlaeserkarton.Value + ", ";
            //    push += numericKleiderKarton.Value + ", ";
            //}
            //else {
            //    push += "-" + numericKarton.Value + ", ";
            //    push += "-" + numericFlaschenKarton.Value + ", ";
            //    push += "-" + numericGlaeserkarton.Value + ", ";
            //    push += "-" + numericKleiderKarton.Value + ", ";
            //}


            //push +=  textUmzugsnummer.Text + ", ";
            //push +=  textKundennummer.Text + ", ";
            //push += "'" + textBemerkung.Text + " ', ";
            //push += "'" + idBearbeitend + "', ";
            //push += "'" + Program.DateMachine(DateTime.Now) + "', ";

            //// Kartons unbenútzt zurück?
            //if (radioUnbenutzt.Checked)
            //{
            //    push += 1 + ",";
            //}
            //else if (radioKaufJa.Checked) {
            //    push += 2 + ",";
            //}
            //else {
            //    push += 0 + ",";
            //}

            //// Buchung final oder vorläufig? (0 ist vorläufig, 1 final)

            //if (checkTermin.Checked || (dateTimeTransaktion.Value.Date.CompareTo(DateTime.Now.Date) > 0))
            //{
            //    push += 0 + ",";
            //}
            //else {
            //    push += 1 + ",";
            //}

            //// Rechnungsnummer
            //push += "'"+textRechnungsnr.Text+"');";

            //Program.absender(push, "Fehler beim Speichern der Transaktion in die DB");

            

            // Ergebnis - Transaktionssnummer anzeigen

            MySqlCommand cmdShow = new MySqlCommand("SELECT * FROM Transaktionen ORDER BY idTransaktionen DESC LIMIT 1;", Program.conn);
            MySqlDataReader rdr;
            try
            {
                rdr = cmdShow.ExecuteReader();
                while (rdr.Read())
                {
                    textResultatsNummer.Text += rdr.GetInt32(0);
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim finden der Ergebnis-Transaktion \r\n Bereits dokumentiert.");
            }


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

            var bestätigung = MessageBox.Show("Auszugsadresse (Ja auswählen) oder Einzugsadresse (Nein auswählen) für den Kalendereintrag nutzen? /r/n Auszug: "+ umzObj.auszug.Straße1 + " " + umzObj.auszug.Hausnummer1 +" "+ umzObj.auszug.PLZ1 + " " + umzObj.auszug.Ort1 + " /r/n Einzug "+
                 umzObj.einzug.Straße1 + " " + umzObj.einzug.Hausnummer1 + " " + umzObj.einzug.PLZ1 + " " + umzObj.einzug.Ort1 + " ", "Adresswahl", MessageBoxButtons.YesNo);
            if (bestätigung == DialogResult.Yes)
            { Body += umzObj.auszug.Straße1 + " " + umzObj.auszug.Hausnummer1 + "\r\n" + umzObj.auszug.PLZ1 + " " + umzObj.auszug.Ort1 + "\r\n"; }
            else { Body += umzObj.einzug.Straße1 + " " + umzObj.einzug.Hausnummer1 + "\r\n" + umzObj.einzug.PLZ1 + " " + umzObj.einzug.Ort1 + "\r\n"; }

            // Kontaktdaten
            MySqlCommand cmdRead = new MySqlCommand("SELECT Handynummer, Telefonnummer FROM Kunden WHERE idKunden = "+textKundennummer.Text+";", Program.conn);
            MySqlDataReader rdr;
            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr.GetString(0) == "0")
                    {
                        Body += "Telefon = " + rdr.GetString(1)+"\r\n";
                    }
                    else { Body += "Handy = " + rdr.GetString(0) + "\r\n"; }
                }
                rdr.Close();
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
            MySqlCommand cmdRead = new MySqlCommand("SELECT idUmzuege,datUmzug FROM Umzuege WHERE Kunden_idKunden = " + kundenNr + ";", Program.conn);
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
                    // test = rdr.GetDateTime(2).ToShortDateString();                                           Fix steht aus
                    tempCounter += 1;
                }
                rdr.Close();
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
            }
            else {
                timeLieferzeit.Enabled = false;
            }
        }
    }
    
}
