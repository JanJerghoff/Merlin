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
    public partial class LEA_Fahrtentabelle : Form
    {

        // Speicherlisten

        int idBearbeitend;
        
        private List<TextBox> Touren = new List<TextBox>();
        private List<TextBox> Fahrzeuge = new List<TextBox>();

        private List<NumericUpDown> KundenStueck = new List<NumericUpDown>();
        private List<NumericUpDown> Handbeilagen = new List<NumericUpDown>();

        private List<DateTimePicker> Datum = new List<DateTimePicker>();
        private List<DateTimePicker> Start = new List<DateTimePicker>();
        private List<DateTimePicker> Ende = new List<DateTimePicker>();

        private List<NumericUpDown> Pause = new List<NumericUpDown>();
        private List<NumericUpDown> KMStart = new List<NumericUpDown>();
        private List<NumericUpDown> KMEnde = new List<NumericUpDown>();

        private List<TextBox> Bemerkungen = new List<TextBox>();
                

        public LEA_Fahrtentabelle()
        {
            InitializeComponent();
            setUp();            

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
            textMitarbeiter.AutoCompleteCustomSource = autocomplete0;
            textMitarbeiter.AutoCompleteMode = AutoCompleteMode.Suggest;

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

            foreach (var item in Touren)
            {
                item.AutoCompleteCustomSource = autocomplete1;
                item.AutoCompleteMode = AutoCompleteMode.Suggest;
            }

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

            foreach (var item in Fahrzeuge)
            {
                item.AutoCompleteCustomSource = autocomplete2;
                item.AutoCompleteMode = AutoCompleteMode.Suggest;
            }
        }

        private void setUp() {

            Fahrzeuge.Add(textFahrzeug1);
            Fahrzeuge.Add(textFahrzeug2);
            Fahrzeuge.Add(textFahrzeug3);
            Fahrzeuge.Add(textFahrzeug4);
            Fahrzeuge.Add(textFahrzeug5);
            Fahrzeuge.Add(textFahrzeug6);
            Fahrzeuge.Add(textFahrzeug7);
            Fahrzeuge.Add(textFahrzeug8);
            Fahrzeuge.Add(textFahrzeug9);
            Fahrzeuge.Add(textFahrzeug10);

            Touren.Add(textTour1);
            Touren.Add(textTour2);
            Touren.Add(textTour3);
            Touren.Add(textTour4);
            Touren.Add(textTour5);
            Touren.Add(textTour6);
            Touren.Add(textTour7);
            Touren.Add(textTour8);
            Touren.Add(textTour9);
            Touren.Add(textTour10);

            Datum.Add(dateTag1);
            Datum.Add(dateTag2);
            Datum.Add(dateTag3);
            Datum.Add(dateTag4);
            Datum.Add(dateTag5);
            Datum.Add(dateTag6);
            Datum.Add(dateTag7);
            Datum.Add(dateTag8);
            Datum.Add(dateTag9);
            Datum.Add(dateTag10);

            Start.Add(timeStart1);
            Start.Add(timeStart2);
            Start.Add(timeStart3);
            Start.Add(timeStart4);
            Start.Add(timeStart5);
            Start.Add(timeStart6);
            Start.Add(timeStart7);
            Start.Add(timeStart8);
            Start.Add(timeStart9);
            Start.Add(timeStart10);

            Ende.Add(timeEnde1);
            Ende.Add(timeEnde2);
            Ende.Add(timeEnde3);
            Ende.Add(timeEnde4);
            Ende.Add(timeEnde5);
            Ende.Add(timeEnde6);
            Ende.Add(timeEnde7);
            Ende.Add(timeEnde8);
            Ende.Add(timeEnde9);
            Ende.Add(timeEnde10);

            Pause.Add(numericPause1);
            Pause.Add(numericPause2);
            Pause.Add(numericPause3);
            Pause.Add(numericPause4);
            Pause.Add(numericPause5);
            Pause.Add(numericPause6);
            Pause.Add(numericPause7);
            Pause.Add(numericPause8);
            Pause.Add(numericPause9);
            Pause.Add(numericPause10);

            KMStart.Add(numericKMStart1);
            KMStart.Add(numericKMStart2);
            KMStart.Add(numericKMStart3);
            KMStart.Add(numericKMStart4);
            KMStart.Add(numericKMStart5);
            KMStart.Add(numericKMStart6);
            KMStart.Add(numericKMStart7);
            KMStart.Add(numericKMStart8);
            KMStart.Add(numericKMStart9);
            KMStart.Add(numericKMStart10);

            KMEnde.Add(numericKMEnde1);
            KMEnde.Add(numericKMEnde2);
            KMEnde.Add(numericKMEnde3);
            KMEnde.Add(numericKMEnde4);
            KMEnde.Add(numericKMEnde5);
            KMEnde.Add(numericKMEnde6);
            KMEnde.Add(numericKMEnde7);
            KMEnde.Add(numericKMEnde8);
            KMEnde.Add(numericKMEnde9);
            KMEnde.Add(numericKMEnde10);

            KundenStueck.Add(numericKundenStueck1);
            KundenStueck.Add(numericKundenStueck2);
            KundenStueck.Add(numericKundenStueck3);
            KundenStueck.Add(numericKundenStueck4);
            KundenStueck.Add(numericKundenStueck5);
            KundenStueck.Add(numericKundenStueck6);
            KundenStueck.Add(numericKundenStueck7);
            KundenStueck.Add(numericKundenStueck8);
            KundenStueck.Add(numericKundenStueck9);
            KundenStueck.Add(numericKundenStueck10);

            Handbeilagen.Add(numericHand1);
            Handbeilagen.Add(numericHand2);
            Handbeilagen.Add(numericHand3);
            Handbeilagen.Add(numericHand4);
            Handbeilagen.Add(numericHand5);
            Handbeilagen.Add(numericHand6);
            Handbeilagen.Add(numericHand7);
            Handbeilagen.Add(numericHand8);
            Handbeilagen.Add(numericHand9);
            Handbeilagen.Add(numericHand10);

            Bemerkungen.Add(textBemerkung1);
            Bemerkungen.Add(textBemerkung2);
            Bemerkungen.Add(textBemerkung3);
            Bemerkungen.Add(textBemerkung4);
            Bemerkungen.Add(textBemerkung5);
            Bemerkungen.Add(textBemerkung6);
            Bemerkungen.Add(textBemerkung7);
            Bemerkungen.Add(textBemerkung8);
            Bemerkungen.Add(textBemerkung9);
            Bemerkungen.Add(textBemerkung10);

        }


        public void setBearbeitend(int code)
        {
            idBearbeitend = code;
        }

        // Passende Felder / Überschriften je nach ausgewählter Tour
        public void zeileAnpassen(int Zeilennummer, int Type) {
            // Umzug, wegleiten
            if (Type == 0)
            {
                // Rauswurf in anderes Formular
            }

            // Kundenzahl benötigt
            if (Type == 1) {
                numericEins(Zeilennummer);
            }

            // Stückzahl / Handbeilagen benötigt
            if (Type == 2)
            {
                numericZwei(Zeilennummer);
            }

            // Weder noch
            if (Type == 3)
            {
                numericNull(Zeilennummer);
            }
        }

        // Beide numerics abschalten
        public void numericNull (int Zeile) {
            Datum[Zeile].Enabled = true;
            Start[Zeile].Enabled = true;
            Ende[Zeile].Enabled = true;

            Pause[Zeile].Enabled = true;
            KMStart[Zeile].Enabled = true;
            KMEnde[Zeile].Enabled = true;

            Bemerkungen[Zeile].Enabled = true;
        }

        // Ein numerics abschalten
        public void numericEins(int Zeile)
        {
            numericNull(Zeile);
            KundenStueck[Zeile].Enabled = true;
        }

        // Beinde Numerics aktiv
        public void numericZwei(int Zeile)
        {
            numericEins(Zeile);
            Handbeilagen[Zeile].Enabled = true;
        }

        //Zeile reset
        public void resetZeile(int Zeile) {

            //Zeilen Sperren
            Datum[Zeile].Enabled = false;
            Start[Zeile].Enabled = false;
            Ende[Zeile].Enabled = false;

            Pause[Zeile].Enabled = false;
            KMStart[Zeile].Enabled = false;
            KMEnde[Zeile].Enabled = false;
            
            KundenStueck[Zeile].Enabled = false;
            Handbeilagen[Zeile].Enabled = false;

            Bemerkungen[Zeile].Enabled = false;

            leereZeile(Zeile);           
        }

        private void leereZeile(int Zeile) {

            DateTime reference = new DateTime (DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12,0,0);
            DateTime referenceStart = new DateTime(2017,1,1, 12, 0, 0);
            DateTime referenceEnd = new DateTime(2017, 1, 1, 13, 0, 0);

            Datum[Zeile].Value = reference.Date;
            Start[Zeile].Value = referenceStart;
            Ende[Zeile].Value = referenceEnd;

            Pause[Zeile].Value = 0;
            KMStart[Zeile].Value = 0;
            KMEnde[Zeile].Value = 0;

            KundenStueck[Zeile].Value = 0;
            Handbeilagen[Zeile].Value = 0;

            Bemerkungen[Zeile].Clear();
        }

        // ON_CLICKS

        private void textTour1_TextChanged(object sender, EventArgs e)
        {
            resetZeile(0);
            zeileAnpassen(0, Program.getTourCode(textTour1.Text));
        }

        private void textTour2_TextChanged(object sender, EventArgs e)
        {
            resetZeile(1);
            zeileAnpassen(1, Program.getTourCode(textTour2.Text));
        }

        private void textTour3_TextChanged(object sender, EventArgs e)
        {
            resetZeile(2);
            zeileAnpassen(2, Program.getTourCode(textTour3.Text));
        }

        private void textTour4_TextChanged(object sender, EventArgs e)
        {
            resetZeile(3);
            zeileAnpassen(3, Program.getTourCode(textTour4.Text));
        }

        private void textTour5_TextChanged(object sender, EventArgs e)
        {
            resetZeile(4);
            zeileAnpassen(4, Program.getTourCode(textTour5.Text));
        }

        private void textTour6_TextChanged(object sender, EventArgs e)
        {
            resetZeile(5);
            zeileAnpassen(5, Program.getTourCode(textTour6.Text));
        }

        private void textTour7_TextChanged(object sender, EventArgs e)
        {
            resetZeile(6);
            zeileAnpassen(6, Program.getTourCode(textTour7.Text));
        }

        private void textTour8_TextChanged(object sender, EventArgs e)
        {
            resetZeile(7);
            zeileAnpassen(7, Program.getTourCode(textTour8.Text));
        }

        private void textTour9_TextChanged(object sender, EventArgs e)
        {
            resetZeile(8);
            zeileAnpassen(8, Program.getTourCode(textTour9.Text));
        }

        private void textTour10_TextChanged(object sender, EventArgs e)
        {
            resetZeile(9);
            zeileAnpassen(9, Program.getTourCode(textTour10.Text));
        }

        private void buttonLoeschen1_Click(object sender, EventArgs e)
        {
            leereZeile(0);
        }

        private void buttonLoeschen2_Click(object sender, EventArgs e)
        {
            leereZeile(1);
        }

        private void buttonLoeschen3_Click(object sender, EventArgs e)
        {
            leereZeile(2);
        }

        private void buttonLoeschen4_Click(object sender, EventArgs e)
        {
            leereZeile(3);
        }

        private void buttonLoeschen5_Click(object sender, EventArgs e)
        {
            leereZeile(4);
        }

        private void buttonLoeschen6_Click(object sender, EventArgs e)
        {
            leereZeile(5);
        }

        private void buttonLoeschen7_Click(object sender, EventArgs e)
        {
            leereZeile(6);
        }

        private void buttonLoeschen8_Click(object sender, EventArgs e)
        {
            leereZeile(7);
        }

        private void buttonLoeschen9_Click(object sender, EventArgs e)
        {
            leereZeile(8);
        }

        private void buttonLoeschen10_Click(object sender, EventArgs e)
        {
            leereZeile(9);
        }

        private void buttonAbsenden_Click(object sender, EventArgs e)
        {
            int count = 0;

            foreach (var item in Touren)
            {
                if (Program.validTour(item.Text))
                {
                    // Gültige Tour, abschicken und Fehlermeldung zurückgeben
                    String temp = "Fehler";

                    if (Program.getTourCode(item.Text) == 1)
                    {
                        temp = Program.pushFahrt(textMitarbeiter.Text, item.Text, Fahrzeuge[count].Text, Datum[count].Value, Start[count].Value, Ende[count].Value, decimal.ToInt32(Pause[count].Value), decimal.ToInt32(KMStart[count].Value), decimal.ToInt32(KMEnde[count].Value), decimal.ToInt32(KundenStueck[count].Value), 0, 0, Bemerkungen[count].Text, idBearbeitend);
                    }
                    else if (Program.getTourCode(item.Text) == 2)
                    {
                        temp = Program.pushFahrt(textMitarbeiter.Text, item.Text, Fahrzeuge[count].Text, Datum[count].Value, Start[count].Value, Ende[count].Value, decimal.ToInt32(Pause[count].Value), decimal.ToInt32(KMStart[count].Value), decimal.ToInt32(KMEnde[count].Value), 0, decimal.ToInt32(KundenStueck[count].Value), decimal.ToInt32(Handbeilagen[count].Value), Bemerkungen[count].Text, idBearbeitend);
                    }
                    else if (Program.getTourCode(item.Text) == 3)
                    {
                        temp = Program.pushFahrt(textMitarbeiter.Text, item.Text, Fahrzeuge[count].Text, Datum[count].Value, Start[count].Value, Ende[count].Value, decimal.ToInt32(Pause[count].Value), decimal.ToInt32(KMStart[count].Value), decimal.ToInt32(KMEnde[count].Value), 0, 0, 0, Bemerkungen[count].Text, idBearbeitend);
                    }

                    if (temp != "")
                    {
                        var warnung = MessageBox.Show(temp, "Fehlermeldung");
                    }
                    else {
                        textLog.AppendText("Fahrt aus Zeile " + (count + 1) + " erfolgreich gespeichert \r\n");
                    }

                }
                else if (item.Text != "") {
                    var warnung = MessageBox.Show(item.Text + " ist keine gültige Tour", "Fehlermeldung");
                }
            
                count++;
            }
        }
    }

}
