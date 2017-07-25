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

        public LEA_Fahrtentabelle()
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

        }


        public void setBearbeitend(int code)
        {
            idBearbeitend = code;
        }
    }
}
