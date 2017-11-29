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

namespace Mitarbeiter.LEA_Einträge
{
    public partial class LeaAenderung : Form
    {
        // Speicher des Fahrtobjekts
        Objekte.Fahrt obj;

        public LeaAenderung()
        {
            InitializeComponent();

            //Nullen aus den Kilometern entfernen
            numericKMAnfang.Text = "";
            numericKMEnde.Text = "";
            numericKunden.Text = "";
            numericStueck.Text = "";
            numericHandbeilagen.Text = "";
            numericPause.Text = "";

            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteMitarbeiter();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            textFahrzeug.AutoCompleteCustomSource = Program.getAutocompleteFahrzeug();
            textFahrzeug.AutoCompleteMode = AutoCompleteMode.Suggest;

            textTourNeu.AutoCompleteCustomSource = Program.getAutocompleteTour();
            textTourNeu.AutoCompleteMode = AutoCompleteMode.Suggest;

            textKundenname.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenname.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        public void fuellen (int Nummer) {

            // Objekt erzeugt
            obj = new Objekte.Fahrt(Nummer);

            //Pflichtfelder Füllen
            textSucheName.AppendText(Program.getMitarbeiterName(obj.Mitarbeiter1));

            timeStart.Value = obj.Start;
            timeEnd.Value = obj.Ende;
            numericPause.Value = obj.Pause1;

            monthFahrtDatum.SelectionStart = obj.Start.Date;
            monthFahrtDatum.SelectionEnd = obj.Ende.Date;
            monthFahrtDatum.SetDate(obj.Ende.Date);

            // Sichtbarkeit der Groupboxen und Felder anpassen
            switch (Program.getTourCode(obj.Tour1))
            {
                case 0:
                    SchaltenUmzug();
                    break;

                case 1:
                    SchaltenKunden();
                    break;

                case 2:
                    SchaltenStueck();
                    break;

                case 3:
                    SchaltenBuero();
                    break;

                default:
                    break;
            }

            // Fahrzeugdaten / KM auflösen
            if (obj.Fahrzeug1 <= 0) {
                checkBeifahrer.Checked = true;
                // Ausschalten Fahrzeug
                textFahrzeug.Enabled = false;
                textFahrzeug.Text = "";
            }
            else {
                numericKMAnfang.Value = obj.StartKM1;
                numericKMEnde.Value = obj.EndKM1;
                textFahrzeug.Text = Program.getFahrzeugName(obj.Fahrzeug1);

                Differenzlable.Text = decimal.ToInt32(numericKMEnde.Value - numericKMAnfang.Value) + " KM"; 
            }
            //Stückzahl / Handbeilagen
            if (obj.Stückzahl1 != 0)
            {
                numericStueck.Value = obj.Stückzahl1;
                
            }
            if (obj.Kunden1 != 0)
            {
                numericKunden.Value = obj.Kunden1;

            }
            if (obj.Beilagen1 != 0)
            {
                numericHandbeilagen.Value = obj.Beilagen1;
            }

            //Bemerkung
            textBemerkung.Text = obj.Bemerkung1;

            //Tour setzen
            textTourAlt.AppendText(Program.getTourName(obj.Tour1));

        }

        // Formularanpassung je nach Typ der Tour
        private void SchaltenUmzug()
        {
            numericKunden.Visible = false;
            numericStueck.Visible = false;
            numericHandbeilagen.Visible = false;
            
            MySqlCommand cmdRead = new MySqlCommand("SELECT k.Nachname FROM Kunden k, Umzuege u WHERE u.idUmzuege = " + obj.Umzug1 + " AND u.Kunden_idKunden = k.idKunden;", Program.conn);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    textKundeAlt.AppendText(rdr.GetString(0));
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen des Kundennamen zur Anzeige in Lea-Änderung");
            }                      
            
        }

        private void SchaltenKunden()
        {
            groupUmzug.Visible = false;
            numericStueck.Visible = false;
            numericHandbeilagen.Visible = false;
        }

        private void SchaltenStueck()
        {
            groupUmzug.Visible = false;
            numericKunden.Visible = false;
        }

        private void SchaltenBuero()
        {
            groupUmzug.Visible = false;
        }

        private void checkBeifahrer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBeifahrer.Checked) {
                numericKMAnfang.Value = 0;
                numericKMEnde.Value = 0;
                textFahrzeug.Clear();
            }
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
            else
            {
                //textLog.AppendText("Entweder Kundenname oder Umzugsnummer eingeben, dann Suchen");
            }
        }

        private void fuellenKundennummer(int Kundennummer)
        {
            MySqlCommand cmdKundenSuche = new MySqlCommand("SELECT k.Nachname, k.Vorname FROM Kunden k, Umzuege u WHERE u.Kunden_idKunden = k.idKunden AND u.idUmzuege = " + (decimal.ToInt32(numericUmzugsnummer.Value)) + ";", Program.conn);
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

        private void fuellenKundenname(String Kundenname)
        {


            MySqlCommand cmdKundenSuche = new MySqlCommand("SELECT u.idUmzuege FROM Kunden k, Umzuege u WHERE u.Kunden_idKunden = k.idKunden AND u.datUmzug = '" + Program.DateMachine(monthFahrtDatum.SelectionStart) + "' AND k.Nachname ='" + Kundenname + "';", Program.conn);
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
    }
}
