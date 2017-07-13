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
    public partial class LEA_Kundenzahl : Form
    {
        // Zwischenspeicher für eingeloggte Daten, gelöscht wenn ausgeloggt wird.
        int TourID = 0;
        int KMErwartet = 0;
        int Type = -1;
        int MitarbeiterID = 0;
        int fahrzeugID = 0;

        public LEA_Kundenzahl()
        {
            InitializeComponent();

            AutoCompleteStringCollection autocomplete0 = new AutoCompleteStringCollection(); // Mitarbeiter
            AutoCompleteStringCollection autocomplete1 = new AutoCompleteStringCollection(); // Touren
            AutoCompleteStringCollection autocomplete2 = new AutoCompleteStringCollection(); // Fahrzeuge

            //Abfrage aller Mitarbeiternamen
            MySqlCommand cmdMitarbeiter = new MySqlCommand("SELECT Nachname, Vorname FROM Mitarbeiter", Program.conn2);
            MySqlDataReader rdrMitarbeiter;
            try
            {
                rdrMitarbeiter = cmdMitarbeiter.ExecuteReader();
                while (rdrMitarbeiter.Read())
                {
                    autocomplete0.Add(rdrMitarbeiter[0].ToString() + ", " + rdrMitarbeiter[1].ToString());
                }
                rdrMitarbeiter.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }
            // Autocomplete vorlegen
            textSucheName.AutoCompleteCustomSource = autocomplete0;
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            //

            //Abfrage aller Tourennamen
            MySqlCommand cmdTour = new MySqlCommand("SELECT Name FROM Tour WHERE TYPE >= 0 AND TYPE <=3;", Program.conn2); // Zulässige Touren finden / definieren
            MySqlDataReader rdrTour;
            try
            {
                rdrTour = cmdTour.ExecuteReader();
                while (rdrTour.Read())
                {
                    autocomplete1.Add(rdrTour[0].ToString());
                }
                rdrTour.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }
            // Autocomplete vorlegen
            textTour.AutoCompleteCustomSource = autocomplete1;
            textTour.AutoCompleteMode = AutoCompleteMode.Suggest;


            //

            //Abfrage aller Fahrzeuge
            MySqlCommand cmdFahrzeug = new MySqlCommand("SELECT Name FROM Fahrzeug", Program.conn2);
            MySqlDataReader rdrFahrzeug;
            try
            {
                rdrFahrzeug = cmdFahrzeug.ExecuteReader();
                while (rdrFahrzeug.Read())
                {
                    autocomplete2.Add(rdrFahrzeug[0].ToString());
                }
                rdrFahrzeug.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }
            // Autocomplete vorlegen
            textFahrzeug.AutoCompleteCustomSource = autocomplete2;
            textFahrzeug.AutoCompleteMode = AutoCompleteMode.Suggest;

            leeren();
        }

        private void leeren()
        {
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
        }

        int idBearbeitend;

        public void setBearbeitend(int code)
        {
            idBearbeitend = code;
        }

        private void buttonTourLock_Click(object sender, EventArgs e)
        {

            // Vorprüfungen Mitarbeiter, Fahrzeug

            if (textSucheName.Text == "") {
                textLog.AppendText("Bitte einen Mitarbeiter auswählen \r\n");
                return;
            }
            if (textFahrzeug.Text == "" && checkBeifahrer.Checked == false)
            {
                textLog.AppendText("Bitte ein Fahrzeug auswählen oder 'Beifahrer' ankreuzen auswählen \r\n");
                return;
            }


            // Tour auflösen
            try
            {
                TourID = Program.getTour(textTour.Text);
            }
            catch (Exception ex)
            {
                textLog.AppendText("Es ist folgender Fehler aufgetreten: \r\n" + ex.ToString());
            }

            // Abfrage Tour-Infos für Formularanpassung

            MySqlCommand cmdTour = new MySqlCommand("SELECT * FROM Tour WHERE idTour = "+TourID+";", Program.conn2); // Zulässige Touren finden / definieren
            MySqlDataReader rdrTour;
            try
            {
                rdrTour = cmdTour.ExecuteReader();
                while (rdrTour.Read())
                {
                    KMErwartet = rdrTour.GetInt32(2);
                    Type = rdrTour.GetInt32(3);
                }
                rdrTour.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }

            if (KMErwartet == 0) {
                labelPruefung.Visible = false;
                checkKMPruefung.Visible = false;
                checkKMPruefung.Checked = false;
            }

            // Abfrage eingeloggter ID´s
            if (checkBeifahrer.Checked == false)
            {
                fahrzeugID = Program.getFahrzeug(textFahrzeug.Text);
            }

            MitarbeiterID = Program.getMitarbeiter(textSucheName.Text);

            // 0 = Umzug, 1 = Kundenzahl, 2 = Stückzahl , 3 weder-noch............................... 
            switch (Type)
            {
                case -1:
                    textLog.AppendText("Die Tour konnte nicht erkannt werden, bitte überprüfen \r\n");
                    break;

                case 0:
                    LEA_Umzug umzug = new LEA_Umzug();
                    umzug.setBearbeitend(idBearbeitend);
                    umzug.Show();
                    this.Close();
                    break;

                case 1:
                    labelChange.Text = "Kunden";
                    labelChange.Visible = true;
                    numericKundenStueck.Visible = true;
                    break;

                case 2:
                    labelChange.Text = "Stück";
                    labelChange.Visible = true;
                    numericKundenStueck.Visible = true;

                    labelHand.Visible = true;
                    numericHandbeilagen.Visible = true;
                    break;

                case 3:
                    // Unsichtbar machen für Touren ohne Stück oder Kunden
                    labelChange.Visible = false;
                    labelHand.Visible = false;
                    numericKundenStueck.Visible = false;
                    numericHandbeilagen.Visible = false;
                    break;

                default:
                    textLog.AppendText("Unbekannte Tour, bitte überprüfen \r\n");
                    break;
            }

            // Sektion 1 sperren

            textSucheName.Enabled = false;
            textTour.Enabled = false;
            textFahrzeug.Enabled = false;
            checkBeifahrer.Enabled = false;

            buttonTourUnlock.Enabled = true;
            buttonTourLock.Enabled = false;

        }

        private void TourUnlock() {
            // Entsperren
            textSucheName.Enabled = true;
            textTour.Enabled = true;            
            checkBeifahrer.Enabled = true;

            if (checkBeifahrer.Checked == false)
            {
                textFahrzeug.Enabled = true;
            }

            buttonTourUnlock.Enabled = false;
            buttonTourLock.Enabled = true;
        }

        private void buttonTourUnlock_Click(object sender, EventArgs e)
        {
            TourUnlock();
        }

        private void buttonTourLeeren_Click(object sender, EventArgs e)
        {
            TourUnlock();

            Type = -1;

            textSucheName.Text = "";
            textTour.Text = "";
            textFahrzeug.Text = "";
            checkBeifahrer.Checked = false;
        }
                
        //Abschicken Formulardaten
        private void senden() {

            int flagKM = 0; // 1 wenn Warnung ausgelöst
            String insert = ""; // Basis für die Tourabhängigen SQL-Statements

            // Tour muss eingeloggt sein
            if (textTour.Enabled) {
                textLog.AppendText("Tour, Mitarbeiter und Fahrzeug müssen eingeloggt sein \r\n");
                return;
            }

            if (numericKMEnde.Value < numericKMAnfang.Value && checkBeifahrer.Checked == false)
            {
                textLog.AppendText("Endkilometer können nicht kleiner sein als Startkilometer, wenn Beifahrer nicht gesetzt. \r\n");
                return;
            }

            // Kilometerwarnung muss bestätigt werden, dass kein Fehler vorliegt.
            if (Program.KMCheck(TourID, decimal.ToInt32(numericKMEnde.Value - numericKMAnfang.Value)) && checkKMPruefung.Checked == false) {
                var bestätigung = MessageBox.Show("Die eingegebene Fahrt weicht von den Kilometern der Tour ab. \r\n Ein Vermerk wird automatisch vor die Bemerkung gesetzt. \r\n Soll gespeichert werden?","KilometerPrüfung", MessageBoxButtons.YesNo);
                if (bestätigung == DialogResult.Yes)
                {
                    flagKM = 1;
                }
                else {
                    return;
                }
            }

            // Weitgehend identisch, Unterschied in der ersten Zeile (Kunden / Stück / leer) und in Fall 3 entsprechend eine Zeile unten weniger
            if (Type == 1) {
                insert += "INSERT INTO Fahrt (Mitarbeiter_idMitarbeiter, Start, Ende, Pause, AnfangsKM, EndKM, Bemerkung, UserChanged, Kunden, Tour_idTour, Fahrzeug_idFahrzeug) VALUES (";

                insert += MitarbeiterID + ", ";
                insert += "'" + Program.DateTimeMachine(timeStart.Value, monthFahrtDatum.SelectionStart) + "', ";
                insert += "'" + Program.DateTimeMachine(timeEnd.Value, monthFahrtDatum.SelectionEnd) + "', ";
                insert += decimal.ToInt32(numericPause.Value) + ", ";
                insert += decimal.ToInt32(numericKMAnfang.Value) + ", ";
                insert += decimal.ToInt32(numericKMEnde.Value) + ", ";

                // Prüfung nicht bestanden -> Vermerk direkt vor der Bemerkung
                if (flagKM == 1)
                {
                    insert += "'Kilometerpruefung " + textBemerkung.Text + "', ";
                }
                else { insert += "'" + textBemerkung.Text + "', "; }
                
                insert += idBearbeitend + ", ";
                insert += numericKundenStueck.Value + ", ";
                insert += TourID+", ";
                insert += fahrzeugID + ");";
            }

            if (Type == 2) {
                insert += "INSERT INTO Fahrt (Mitarbeiter_idMitarbeiter, Start, Ende, Pause, AnfangsKM, EndKM, Bemerkung, UserChanged, Stückzahl, Tour_idTour, Fahrzeug_idFahrzeug) VALUES (";

                insert += MitarbeiterID + ", ";
                insert += "'" + Program.DateTimeMachine(timeStart.Value, monthFahrtDatum.SelectionStart) + "', ";
                insert += "'" + Program.DateTimeMachine(timeEnd.Value, monthFahrtDatum.SelectionEnd) + "', ";
                insert += decimal.ToInt32(numericPause.Value) + ", ";
                insert += decimal.ToInt32(numericKMAnfang.Value) + ", ";
                insert += decimal.ToInt32(numericKMEnde.Value) + ", ";

                // Prüfung nicht bestanden -> Vermerk direkt vor der Bemerkung
                if (flagKM == 1)
                {
                    insert += "'Kilometerpruefung " + textBemerkung.Text + "', ";
                }
                else { insert += "'" + textBemerkung.Text + "', "; }


                insert += idBearbeitend + ", ";
                insert += numericKundenStueck.Value + ", ";
                insert += TourID + ", ";
                insert += fahrzeugID + ");";
            }

            if (Type == 3)
            {
                insert += "INSERT INTO Fahrt (Mitarbeiter_idMitarbeiter, Start, Ende, Pause, AnfangsKM, EndKM, Bemerkung, UserChanged, Tour_idTour, Fahrzeug_idFahrzeug) VALUES (";

                insert += MitarbeiterID + ", ";
                insert += "'" + Program.DateTimeMachine(timeStart.Value, monthFahrtDatum.SelectionStart) + "', ";
                insert += "'" + Program.DateTimeMachine(timeEnd.Value, monthFahrtDatum.SelectionEnd) + "', ";
                insert += decimal.ToInt32(numericPause.Value) + ", ";
                insert += decimal.ToInt32(numericKMAnfang.Value) + ", ";
                insert += decimal.ToInt32(numericKMEnde.Value) + ", ";

                // Prüfung nicht bestanden -> Vermerk direkt vor der Bemerkung
                if (flagKM == 1)
                {
                    insert += "'Kilometerpruefung " + textBemerkung.Text + "', ";
                }
                else { insert += "'" + textBemerkung.Text + "', "; }


                insert += idBearbeitend + ", ";
                insert += TourID + ", ";
                insert += fahrzeugID + ");";
            }

            if (insert != "")
            {
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
            }
            else {
                textLog.AppendText("Fehler aufgetreten, Tour-Typ ist unbekannt. \r\n ");
            }

            // Update Kilometer wenn nicht Beifahrer
            if (checkBeifahrer.Checked == false) {
                textLog.AppendText(Program.FahrzeugUpdate(fahrzeugID, decimal.ToInt32(numericKMEnde.Value)));
            }

        }

        private void buttonSenden_Click(object sender, EventArgs e)
        {
            senden();
        }

        private void checkBeifahrer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBeifahrer.Checked)
            {
                textFahrzeug.Clear();
                textFahrzeug.Enabled = false;
                numericKMAnfang.Value = 0;
                numericKMAnfang.Enabled = false;
                numericKMEnde.Value = 0;
                numericKMEnde.Enabled = false;
            }
            else {
                textFahrzeug.Enabled = true;
                numericKMAnfang.Enabled = true;
                numericKMEnde.Enabled = true;
            }
        }
    }
}
