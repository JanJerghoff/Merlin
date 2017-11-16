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
    public partial class LEA_Umzug : Form
    {
        public LEA_Umzug()
        {
            InitializeComponent();
                        
            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteMitarbeiter();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            textKundenname.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenname.AutoCompleteMode = AutoCompleteMode.Suggest;

            textFahrzeug.AutoCompleteCustomSource = Program.getAutocompleteFahrzeug();
            textFahrzeug.AutoCompleteMode = AutoCompleteMode.Suggest;

            // Numerics vorbereiten
            numericKMAnfang.Text = "";
            numericKMEnde.Text = "";
            numericPause.Text = "";

            //Standarddaten setzen
            leeren();
        }


        int idBearbeitend;

        public void setBearbeitend(int code)
        {
            idBearbeitend = code;
        }

        //Beifahrer haben weder Fahrzeug noch KM
        private void checkBeifahrer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBeifahrer.Checked)
            {
                textFahrzeug.Text = "";
                textFahrzeug.Enabled = false;
                numericKMEnde.Enabled = false;
                numericKMAnfang.Enabled = false;
            }
            else {
                textFahrzeug.Enabled = true;
                numericKMEnde.Enabled = true;
                numericKMAnfang.Enabled = true;
            }
        }

        private void buttonKundenLeeren_Click(object sender, EventArgs e)
        {
            numericUmzugsnummer.Enabled = true;
            numericUmzugsnummer.Value = 0;
            textKundenname.Text = "";
            textKundenname.Enabled = true;
            buttonSuchen.Enabled = true;
        }

        private void buttonSuchen_Click(object sender, EventArgs e)
        {
            if (numericUmzugsnummer.Value != 0)
            {
                fuellenKundennummer(decimal.ToInt32(numericUmzugsnummer.Value));
            }
            else if (textKundenname.Text != "")
            {
                fuellenKundenname(textKundenname.Text);
            }
            else { textLog.AppendText("Entweder Kundenname oder Umzugsnummer eingeben, dann Suchen");
            }
        }

        private void fuellenKundennummer(int Kundennummer) {
            MySqlCommand cmdKundenSuche = new MySqlCommand("SELECT k.Nachname, k.Vorname FROM Kunden k, Umzuege u WHERE u.Kunden_idKunden = k.idKunden AND u.idUmzuege = "+(decimal.ToInt32(numericUmzugsnummer.Value))+";", Program.conn);
            MySqlDataReader rdrKundenSuche;
            try
            {
                rdrKundenSuche = cmdKundenSuche.ExecuteReader();
                while (rdrKundenSuche.Read())
                {
                    if (textKundenname.Text == "")
                    {
                        textKundenname.Text = rdrKundenSuche[0].ToString() + ", " + rdrKundenSuche[1].ToString();
                    }
                }
                rdrKundenSuche.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }

            textKundenname.Enabled = false;
            numericUmzugsnummer.Enabled = false;
                       
        }

        private void fuellenKundenname(String Kundenname) {

            
            MySqlCommand cmdKundenSuche = new MySqlCommand("SELECT u.idUmzuege FROM Kunden k, Umzuege u WHERE u.Kunden_idKunden = k.idKunden AND u.datUmzug = '"+Program.DateMachine(monthFahrtDatum.SelectionStart)+"' AND k.Nachname ='"+ Kundenname +"';", Program.conn);
            MySqlDataReader rdrKundenSuche;
            int count = 0;
            try
            {
                rdrKundenSuche = cmdKundenSuche.ExecuteReader();
                while (rdrKundenSuche.Read())
                {
                    count++;
                    numericUmzugsnummer.Value = rdrKundenSuche.GetInt32(0);
                }
                rdrKundenSuche.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }

            try
            {
                if (count == 0)
                {
                    textLog.AppendText(Kundenname.Split(' ')[0].Split(',')[0] + Kundenname.Split(' ')[1]);
                }

                if (count > 1)
                {
                    textLog.AppendText("Entweder mehr als ein Kunde mit dem Namen \r\n oder mehr als ein Umzug zum Kunden gefunden. \r\n Bitte Umzugsnummer angeben \r\n ");
                }
            }
            catch (Exception)
            {
                var bestätigung = MessageBox.Show("Mehrere Kunden desselben Namens (oder anderes Problem) gefunden, bitte Umzugsnummer verwenden", "Erinnerung");
            }            

            textKundenname.Enabled = false;
            numericUmzugsnummer.Enabled = false;

        }

        // Formular erneuern, da nicht widerverwendbar (?)
        private void leeren() {
            monthFahrtDatum.SelectionEnd = DateTime.Now;
            monthFahrtDatum.SelectionStart = DateTime.Now;
            timeEnd.Value = new DateTime(2017, 1, 1, 12, 0, 0);
            timeStart.Value = new DateTime(2017, 1, 1, 12, 0, 0);

            checkBeifahrer.Checked = false;
            numericKMAnfang.Enabled = true;
            numericKMEnde.Enabled = true;
            textFahrzeug.Enabled = true;

            numericPause.Value = 0;
            numericKMEnde.Value = 0;
            numericKMAnfang.Value = 0;
            textSucheName.Text = "";
            textFahrzeug.Text = "";

            textKundenname.Text = "";
            textKundenname.Enabled = true;
            numericUmzugsnummer.Enabled = true;
            numericUmzugsnummer.Value = 0 ;
            buttonSuchen.Enabled = true;

            // Numerics vorbereiten
            numericKMAnfang.Text = "";
            numericKMEnde.Text = "";
            numericPause.Text = "";
        }

        private void buttonSenden_Click(object sender, EventArgs e)
        {
            int TourNummer = -1;

            if (radioVorbereitung.Checked) { TourNummer = 58; }
            else if (radioBesichtigung.Checked) { TourNummer = 59; }
            else { TourNummer = 8; }

            // Checks auf korrekte Ausführung
            if (numericKMEnde.Value < numericKMAnfang.Value && checkBeifahrer.Checked == false) {
                textLog.AppendText("Endkilometer können nicht kleiner sein als Startkilometer, wenn Beifahrer nicht gesetzt. \r\n");
                return;
            }

            if (textFahrzeug.Text == "" && checkBeifahrer.Checked == false) {
                textLog.AppendText("Es muss ein Fahrzeug ausgewählt werden, wenn Beifahrer nicht gesetzt. \r\n");
                return;
            }

            if (monthFahrtDatum.SelectionStart.Date != monthFahrtDatum.SelectionEnd.Date) {
                textLog.AppendText("Mehrtägige Umzüge bitte seperat pro Tag eintragen. Umzügler arbeiten nicht über Mitternacht hinweg. \r\n");
                return;
            }

            if (textSucheName.Text == "") {
                textLog.AppendText("Es muss ein (bestenfalls gültiger) Mitarbeiter angegeben werden. \r\n");
                return;
            }

            if (numericUmzugsnummer.Value == 0)
            {
                textLog.AppendText("Es muss ein gültiger Umzug angegeben werden angegeben werden. \r\n");
                return;
            }

            // if ((timeEnd.Value-timeStart.Value).CompareTo(new TimeSpan(decimal.ToInt32(numericPause.Value))) < 0)

            

            int min = Program.ArbeitsZeitBlock(timeStart.Value, timeEnd.Value, decimal.ToInt32(numericPause.Value));
            if (min < 0 && numericPause.Value >= 1) {
                textLog.AppendText("Die Pause kann nicht länger sein als die angegebene Arbeitszeit \r\n");
                return;
            }

            // Beschaffen der ID´s zum Text
            int fahrzeugID = Program.getFahrzeug(textFahrzeug.Text);
            if (checkBeifahrer.Checked != true)
            {
                
                if (fahrzeugID == 0) { textLog.AppendText("Kein Fahrzeug dieses Namens gefunden, bitte prüfen \r\n"); return; }
                if (fahrzeugID == -2) { textLog.AppendText("Mehrere gleichnamige Fahrzeuge gefunden, bitte prüfen \r\n"); return; }
            }

            int MitarbeiterID = Program.getMitarbeiter (textSucheName.Text);
            if (MitarbeiterID == 0) { textLog.AppendText("Kein Mitarbeiter dieses Namens gefunden, bitte prüfen \r\n"); return; }
            if (MitarbeiterID == -2) { textLog.AppendText("Mehrere gleichnamige Mitarbeiter gefunden, bitte prüfen \r\n"); return; }

            // Get Fahrzeug ID, Mitarbeiter ID zu Fahrzeugnamen, Fehlermeldung wenn nicht eindeutig

            String insert = "";
            DateTime endtag = monthFahrtDatum.SelectionStart;
            if (timeStart.Value.TimeOfDay > timeEnd.Value.TimeOfDay)
            {
                endtag = endtag.AddDays(1);
            }
            


            if (checkBeifahrer.Checked == false)
            {

                insert += "INSERT INTO Fahrt (Mitarbeiter_idMitarbeiter, Start, Ende, Pause, AnfangsKM, EndKM, Bemerkung, UserChanged, Umzuege_idUmzuege, Tour_idTour, Fahrzeug_idFahrzeug) VALUES (";

                insert += MitarbeiterID + ", ";
                insert += "'" + Program.DateTimeMachine(timeStart.Value, monthFahrtDatum.SelectionStart) + "', ";
                insert += "'" + Program.DateTimeMachine(timeEnd.Value, endtag) + "', ";
                insert += decimal.ToInt32(numericPause.Value) + ", ";
                insert += decimal.ToInt32(numericKMAnfang.Value) + ", ";
                insert += decimal.ToInt32(numericKMEnde.Value) + ", ";
                insert += "'" + textBemerkung.Text + "', ";
                insert += idBearbeitend + ", ";
                insert += numericUmzugsnummer.Value + ", ";
                insert += TourNummer+", ";
                insert += fahrzeugID + ");";
            }
            else {

                insert += "INSERT INTO Fahrt (Mitarbeiter_idMitarbeiter, Start, Ende, Pause, Bemerkung, UserChanged, Umzuege_idUmzuege, Tour_idTour) VALUES (";

                insert += MitarbeiterID + ", ";
                insert += "'" + Program.DateTimeMachine(timeStart.Value, monthFahrtDatum.SelectionStart) + "', ";
                insert += "'" + Program.DateTimeMachine(timeEnd.Value, endtag) + "', ";
                insert += decimal.ToInt32(numericPause.Value) + ", ";
                insert += "'" + textBemerkung.Text + "', ";
                insert += idBearbeitend + ", ";
                insert += numericUmzugsnummer.Value + ", ";
                insert += TourNummer+");";

            }

            // String fertig, absenden
            MySqlCommand cmdAdd = new MySqlCommand(insert, Program.conn2);
            try
            {
                cmdAdd.ExecuteNonQuery();
                textLog.AppendText("Fahrt erfolgreich gebucht \r\n ");

            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }

            // Update Kilometer wenn nicht Beifahrer
            if (checkBeifahrer.Checked == false)
            {
                textLog.AppendText(Program.FahrzeugUpdate(fahrzeugID, decimal.ToInt32(numericKMEnde.Value)));
            }

            leeren();
        }
    }
}
