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
    public partial class LEA_Mitarbeiter_Details : Form
    {
        // String-collection anlegen
        AutoCompleteStringCollection autocomplete0 = new AutoCompleteStringCollection(); // Mitarbeiter
        AutoCompleteStringCollection autocomplete1 = new AutoCompleteStringCollection(); // Touren
        Dictionary<int, String> Fahrzeugsammlung = new Dictionary<int, String>(); // Zwischenspeicher Fahrzeuge für LAdegeschwindigkeit
        Dictionary<int, String> Tourensammlung = new Dictionary<int, String>(); // Zwischenspeicher Touren
        Dictionary<int, String> Mitarbeitersammlung = new Dictionary<int, String>(); // Zwischenspeicher Mitarbeiter

        public LEA_Mitarbeiter_Details()
        {
            InitializeComponent();
            

            //Abfrage aller Mitarbeiternamen
            MySqlCommand cmdMitarbeiter = new MySqlCommand("SELECT Nachname, Vorname, idMitarbeiter FROM Mitarbeiter", Program.conn2);
            MySqlDataReader rdrMitarbeiter;
            try
            {
                rdrMitarbeiter = cmdMitarbeiter.ExecuteReader();
                while (rdrMitarbeiter.Read())
                {
                    autocomplete0.Add(rdrMitarbeiter[0].ToString() + ", " + rdrMitarbeiter[1].ToString());
                    Mitarbeitersammlung.Add(rdrMitarbeiter.GetInt32(2), (rdrMitarbeiter[0].ToString() + ", " + rdrMitarbeiter[1].ToString()));
                }
                rdrMitarbeiter.Close();
            }
            catch (Exception sqlEx)
            {
                var bestätigung = MessageBox.Show(sqlEx.ToString(), "Fehlermeldung");
                return;
            }
            // Autocomplete vorlegen
            textSucheName.AutoCompleteCustomSource = autocomplete0;
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;


            //Abfrage aller Tourennamen
            MySqlCommand cmdTour = new MySqlCommand("SELECT Name, idTour FROM Tour WHERE TYPE >= 0 AND TYPE <=3;", Program.conn2); // Zulässige Touren finden / definieren
            MySqlDataReader rdrTour;
            try
            {
                rdrTour = cmdTour.ExecuteReader();
                while (rdrTour.Read())
                {
                    autocomplete1.Add(rdrTour[0].ToString());
                    Tourensammlung.Add(rdrTour.GetInt32(1), rdrTour.GetString(0));
                }
                rdrTour.Close();
            }
            catch (Exception sqlEx)
            {
                var bestätigung = MessageBox.Show(sqlEx.ToString(), "Fehlermeldung");
                return;
            }
            // Autocomplete vorlegen
            textSucheTour.AutoCompleteCustomSource = autocomplete1;
            textSucheTour.AutoCompleteMode = AutoCompleteMode.Suggest;

            //Fahrzeug-Dictionary anlegen


            MySqlCommand cmdFahrzeug = new MySqlCommand("SELECT idFahrzeug, Name FROM Fahrzeug;" , Program.conn2); // Liste aller Fahrzeuge
            MySqlDataReader rdrFahrzeug;
            try
            {
                rdrFahrzeug = cmdFahrzeug.ExecuteReader();
                while (rdrFahrzeug.Read())
                {
                    Fahrzeugsammlung.Add(rdrFahrzeug.GetInt32(0), rdrFahrzeug.GetString(1));
                }
                rdrFahrzeug.Close();
            }
            catch (Exception sqlEx)
            {
                var bestätigung = MessageBox.Show(sqlEx.ToString(), "Fehlermeldung");
                return;
            }
        }

        private void leeren() {
            textMitarbeitername.Clear();
            textDatum.Clear();
            textStartzeit.Clear();
            textEndzeit.Clear();
            textBemerkung.Clear();
            textTourname.Clear();
            textFahrzeug.Clear();
            textKMSumme.Clear();
        }

        private void buttonSuche_Click(object sender, EventArgs e)
        {
            int IDMitarbeiter = 0;
            int IDTour = 0;

            if (textSucheName.Text != "")
            {
                IDMitarbeiter = Program.getMitarbeiter(textSucheName.Text);
            }

            if (textSucheTour.Text != "")
            {
                IDTour = Program.getTour(textSucheTour.Text);
            }

            List<int> Fahrzeuge = new List<int>();
            List<int> Mitarbeiter = new List<int>();
            List<int> Tour = new List<int>();

            leeren();

            //Checks
            if (textSucheName.Text != "" && IDMitarbeiter == 0) {
                var bestätigung = MessageBox.Show("Der angegebene Mitarbeiter ist ungültig \r\n Wenn alle Mitarbeiter gezeigt werden sollen, Feld leer lassen", "Fehlermeldung");
                return;
            }

            if (textSucheTour.Text != "" && IDTour == 0)
            {
                var bestätigung = MessageBox.Show("Die angegebene Tour ist ungültig \r\n Wenn alle Touren gezeigt werden sollen, Feld leer lassen", "Fehlermeldung");
                return;
            }

            if (dateStart.Value.Date.CompareTo(dateEnd.Value.Date) > 0 && checkEndzeit.Checked && checkStartzeit.Checked) {
                var bestätigung = MessageBox.Show("Die Startzeit darf nicht später als die Endzeit sein", "Fehlermeldung");
                return;
            }

            //String zusammenbasteln aus Kriterien

            String start = "SELECT * FROM Fahrt";

            if (IDMitarbeiter > 0 || IDTour > 0 || checkEndzeit.Checked || checkStartzeit.Checked ) {
                start += " WHERE";
            }

            if (IDMitarbeiter > 0) {
                start += " Mitarbeiter_idMitarbeiter ="+IDMitarbeiter+" AND ";
            }

            if (IDTour > 0)
            {
                start += " Tour_idTour =" + IDTour + " AND ";
            }

            if (checkStartzeit.Checked)
            {
                start += " Start > '" + Program.DateMachine(dateStart.Value) + "' AND ";
            }

            if (checkEndzeit.Checked)
            {
                start += " Start < '" + Program.DateMachine(dateEnd.Value) + "' AND ";
            }

            // Letztes AND wegschneiden, wenn min. ein Kriterium angelegt war.

            String fin = "";

            if (IDMitarbeiter > 0 || IDTour > 0 || checkEndzeit.Checked || checkStartzeit.Checked)
            {
                fin = start.Substring(0, start.Length - 5);
            }
            else {
                fin = start;
            }

            fin += " ORDER BY Start DESC LIMIT 40;";

            // Abfrage Daten

            MySqlCommand cmd = new MySqlCommand(fin, Program.conn2);
            MySqlDataReader rdr;
            try
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textDatum.AppendText(rdr.GetDateTime(1).Date.ToShortDateString() + "\r\n");
                    textStartzeit.AppendText(rdr.GetDateTime(1).ToShortTimeString() + "\r\n");
                    textEndzeit.AppendText(rdr.GetDateTime(2).ToShortTimeString() + "\r\n");                    
                    textKMSumme.AppendText((rdr.GetInt32(5) - rdr.GetInt32(4)) + "\r\n");
                    // Listen zum später auflösen der ID´s
                    if ((rdr.GetInt32(5) - rdr.GetInt32(4)) != 0)
                    {
                        Fahrzeuge.Add(rdr.GetInt32(13));
                    }
                    else
                    {
                        Fahrzeuge.Add(0);
                    }
                    //
                    Mitarbeiter.Add(rdr.GetInt32(14));
                    Tour.Add(rdr.GetInt32(12));
                    textBemerkung.AppendText(rdr.GetString(9) + "\r\n");
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                var bestätigung = MessageBox.Show(sqlEx.ToString(), "Fehlermeldung");
                return;
            }

            // Mitarbeiter, Touren und Fahrzeuge auflösen

            if (textSucheName.Text != "")
            {
                for (int i = 0; i < Mitarbeiter.Count; i++)
                {
                    textMitarbeitername.AppendText(textSucheName.Text + "\r\n");
                }
            }
            else
            {
                foreach (var item in Mitarbeiter)
                {
                    textMitarbeitername.AppendText(Mitarbeitersammlung[item] + "\r\n");
                }
            }
            //
            if (textSucheTour.Text != "")
            {
                for (int i = 0; i < Mitarbeiter.Count; i++)
                {
                    textTourname.AppendText(textSucheTour.Text + "\r\n");
                }
            }
            else
            {
                foreach (var item in Tour)
                {
                    textTourname.AppendText(Tourensammlung[item] + "\r\n");
                }
            }
            //
            foreach (var item in Fahrzeuge)
            {
                if (item != 0)
                {
                    textFahrzeug.AppendText(Fahrzeugsammlung[item] + "\r\n");
                }
                else {
                    textFahrzeug.AppendText("\r\n");
                }
            }

        }
    }
}
