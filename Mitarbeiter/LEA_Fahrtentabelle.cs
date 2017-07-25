﻿using MySql.Data.MySqlClient;
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
            KMStart.Add(numericKMStart1);
            KMStart.Add(numericKMStart1);
            KMStart.Add(numericKMStart1);
            KMStart.Add(numericKMStart1);
            KMStart.Add(numericKMStart1);
            KMStart.Add(numericKMStart1);
            KMStart.Add(numericKMStart1);
            KMStart.Add(numericKMStart1);
            KMStart.Add(numericKMStart1);

            KMEnde.Add(numericKMEnde1);
          

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
    }
}
