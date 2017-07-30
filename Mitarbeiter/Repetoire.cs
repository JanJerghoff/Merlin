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
    public partial class Repetoire : Form
    {
        Dictionary<int, String> Tourensammlung = new Dictionary<int, String>(); // Zwischenspeicher Touren
        Dictionary<int, String> Mitarbeitersammlung = new Dictionary<int, String>(); // Zwischenspeicher Mitarbeiter


        public Repetoire()
        {
            InitializeComponent();
            
            // Autocomplete vorlegen
            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteMitarbeiter();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;
                        
            textSucheTour.AutoCompleteCustomSource = Program.getAutocompleteTour();
            textSucheTour.AutoCompleteMode = AutoCompleteMode.Suggest;

        }

        public void anzeigeMitarbeiter(int ID) {

            labelSingle.Text = "Mitarbeiter";
            labelMulti.Text = "Touren";
            textMitarbeitername.Clear();
            textTourAnzahl.Clear();
            textAnzahl.Clear();

            textMitarbeitername.AppendText(textSucheName.Text);

            // Touren füllen
            String query = "SELECT Tour_idTour, COUNT(*) FROM Fahrt WHERE Mitarbeiter_idMitarbeiter = " + ID + " GROUP BY Tour_idTour ORDER BY COUNT(*) DESC;";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn2); // Anzahl der gefahrenen Touren für den Mitarbeiter, absteigend nach Häufigkeit
            MySqlDataReader rdr;
            try
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textTourAnzahl.AppendText(Tourensammlung[rdr.GetInt32(0)]+ "\r\n");
                    textAnzahl.AppendText(rdr[1].ToString() + "\r\n");
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                var bestätigung = MessageBox.Show(sqlEx.ToString(), "Fehlermeldung");
                return;
            }

        }

        public void anzeigeTour(int ID) {

            labelSingle.Text = "Tour";
            labelMulti.Text = "Mitarbeiter";
            textMitarbeitername.Clear();
            textTourAnzahl.Clear();
            textAnzahl.Clear();

            textMitarbeitername.AppendText(textSucheTour.Text);

            // Mitarbeiter füllen
            String query = "SELECT Mitarbeiter_idMitarbeiter, COUNT(*) FROM Fahrt WHERE Tour_idTour = " + ID + " GROUP BY Mitarbeiter_idMitarbeiter ORDER BY COUNT(*) DESC;";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn2); // Anzahl der Fahrten pro Mitarbeiter für die Tour, absteigend nach Häufigkeit
            MySqlDataReader rdr;
            try
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textTourAnzahl.AppendText(Mitarbeitersammlung[rdr.GetInt32(0)] + "\r\n");
                    textAnzahl.AppendText(rdr[1].ToString() + "\r\n");
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                var bestätigung = MessageBox.Show(sqlEx.ToString(), "Fehlermeldung");
                return;
            }
        }

        public void anzeigeKombination(int Mitarbeiter, int Tour) {

            labelSingle.Text = "Tour";
            labelMulti.Text = "Mitarbeiter";
            textMitarbeitername.Clear();
            textTourAnzahl.Clear();
            textAnzahl.Clear();
                        
            textMitarbeitername.AppendText(textSucheName.Text);

            // Tour füllen
            String query = "SELECT COUNT(*) FROM Fahrt WHERE Mitarbeiter_idMitarbeiter = " + Mitarbeiter + " AND Tour_idTour = "+Tour+" GROUP BY Tour_idTour";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn2); // Anzahl der gefahrenen Touren für die Kombination
            MySqlDataReader rdr;
            try
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textTourAnzahl.AppendText(textSucheTour.Text);
                    textAnzahl.AppendText(rdr[0].ToString() + "\r\n");
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                var bestätigung = MessageBox.Show(sqlEx.ToString(), "Fehlermeldung");
                return;
            }
        }

        private void buttonSuche_Click(object sender, EventArgs e)
        {
            // Beide leer -> Fehler
            if (textSucheName.Text == "" && textSucheTour.Text == "")
            {
                var bestätigung = MessageBox.Show("Es müssen entweder Tour oder Mitarbeiter gegeben sein", "Fehlermeldung");
                return;
            }

            // Beide gefüllt -> Beide legitim -> Kombinationsanzeige
            else if (textSucheName.Text != "" && textSucheTour.Text != "")
            {
                if (Program.getAutocompleteTour().Contains(textSucheTour.Text) == false) {
                    var bestätigung = MessageBox.Show("Die gesuchte Tour existiert nicht, Eingabe bitte prüfen", "Fehlermeldung");
                    return;
                }
                if (Program.getAutocompleteMitarbeiter().Contains(textSucheName.Text) == false) {
                    var bestätigung = MessageBox.Show("Der gesuchte Mitarbeiter existiert nicht, Eingabe bitte prüfen", "Fehlermeldung");
                    return;
                }
                anzeigeKombination(Program.getMitarbeiter(textSucheName.Text), (Program.getTour(textSucheTour.Text)));
            }

            // Name gefüllt -> legitim? -> Mitarbeiter Anzeige
            else if (textSucheName.Text != "")
            {
                if (Program.getAutocompleteMitarbeiter().Contains(textSucheName.Text))
                {
                    anzeigeMitarbeiter(Program.getMitarbeiter(textSucheName.Text));
                }
                else {
                    var bestätigung = MessageBox.Show("Der gesuchte Mitarbeiter existiert nicht, Eingabe bitte prüfen", "Fehlermeldung");
                    return;
                }
            }

            // Tour anzeigen
            else {
                if (Program.getAutocompleteTour().Contains(textSucheTour.Text))
                {
                    anzeigeTour(Program.getTour(textSucheTour.Text));
                }
                else
                {
                    var bestätigung = MessageBox.Show("Die gesuchte Tour existiert nicht, Eingabe bitte prüfen", "Fehlermeldung");
                    return;
                }
            }
        }
    }
}
