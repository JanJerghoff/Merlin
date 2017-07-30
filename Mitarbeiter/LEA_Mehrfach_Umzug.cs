using Google.Apis.Calendar.v3.Data;
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
    public partial class LEA_Mehrfach_Umzug : Form
    {

        

        // String-collection anlegen
        AutoCompleteStringCollection autocomplete2 = new AutoCompleteStringCollection(); // Fahrzeuge

        static List<TextBox> textboxen = new List<TextBox>();
        static List<NumericUpDown> zahlboxen = new List<NumericUpDown>();
        static List<Label> labels = new List<Label>();

        public LEA_Mehrfach_Umzug()
        {
            InitializeComponent();

            listenfuellen();
            
            // Autocomplete vorlegen
            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteMitarbeiter();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            textKundenname1.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenname1.AutoCompleteMode = AutoCompleteMode.Suggest;

            textKundenname2.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenname2.AutoCompleteMode = AutoCompleteMode.Suggest;

            textKundenname3.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenname3.AutoCompleteMode = AutoCompleteMode.Suggest;

            textKundenname4.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenname4.AutoCompleteMode = AutoCompleteMode.Suggest;

            textKundenname5.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenname5.AutoCompleteMode = AutoCompleteMode.Suggest;

            textKundenname6.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenname6.AutoCompleteMode = AutoCompleteMode.Suggest;

            textKundenname7.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenname7.AutoCompleteMode = AutoCompleteMode.Suggest;

            textKundenname8.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenname8.AutoCompleteMode = AutoCompleteMode.Suggest;

            textKundenname9.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenname9.AutoCompleteMode = AutoCompleteMode.Suggest;
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

            //Standarddaten setzen
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

            // Kundenfelder nullen

            textKundenname1.Text = "";
            textKundenname1.Enabled = true;
            numericUmzugsnummer1.Enabled = true;
            numericUmzugsnummer1.Value = 0;

            textKundenname2.Text = "";
            textKundenname2.Enabled = true;
            numericUmzugsnummer2.Enabled = true;
            numericUmzugsnummer2.Value = 0;

            textKundenname3.Text = "";
            textKundenname3.Enabled = true;
            numericUmzugsnummer3.Enabled = true;
            numericUmzugsnummer3.Value = 0;

            textKundenname4.Text = "";
            textKundenname4.Enabled = true;
            numericUmzugsnummer4.Enabled = true;
            numericUmzugsnummer4.Value = 0;

            textKundenname5.Text = "";
            textKundenname5.Enabled = true;
            numericUmzugsnummer5.Enabled = true;
            numericUmzugsnummer5.Value = 0;

            textKundenname6.Text = "";
            textKundenname6.Enabled = true;
            numericUmzugsnummer6.Enabled = true;
            numericUmzugsnummer6.Value = 0;

            textKundenname7.Text = "";
            textKundenname7.Enabled = true;
            numericUmzugsnummer7.Enabled = true;
            numericUmzugsnummer7.Value = 0;

            textKundenname8.Text = "";
            textKundenname8.Enabled = true;
            numericUmzugsnummer8.Enabled = true;
            numericUmzugsnummer8.Value = 0;

            textKundenname9.Text = "";
            textKundenname9.Enabled = true;
            numericUmzugsnummer9.Enabled = true;
            numericUmzugsnummer9.Value = 0;

            buttonSuchen.Enabled = true;
            buttonKundenLeeren.Enabled = false;
        }

        int idBearbeitend;

        public void setBearbeitend(int code)
        {
            idBearbeitend = code;
        }

        private void listenfuellen() {

            //Listen füllen

            textboxen.Add(textKundenname1);
            textboxen.Add(textKundenname2);
            textboxen.Add(textKundenname3);
            textboxen.Add(textKundenname4);
            textboxen.Add(textKundenname5);
            textboxen.Add(textKundenname6);
            textboxen.Add(textKundenname7);
            textboxen.Add(textKundenname8);
            textboxen.Add(textKundenname9);

            zahlboxen.Add(numericUmzugsnummer1);
            zahlboxen.Add(numericUmzugsnummer2);
            zahlboxen.Add(numericUmzugsnummer3);
            zahlboxen.Add(numericUmzugsnummer4);
            zahlboxen.Add(numericUmzugsnummer5);
            zahlboxen.Add(numericUmzugsnummer6);
            zahlboxen.Add(numericUmzugsnummer7);
            zahlboxen.Add(numericUmzugsnummer8);
            zahlboxen.Add(numericUmzugsnummer9);

            labels.Add(NG1);
            labels.Add(NG2);
            labels.Add(NG3);
            labels.Add(NG4);
            labels.Add(NG5);
            labels.Add(NG6);
            labels.Add(NG7);
            labels.Add(NG8);
            labels.Add(NG9);

        }

        private void fuellenKundenname(TextBox name, NumericUpDown nummer, Label meldung)
        {
            if (Program.getAutocompleteKunden().Contains(name.Text) == false){
                meldung.Visible = true;
                return;
            }

            MySqlCommand cmdKundenSuche = new MySqlCommand("SELECT u.idUmzuege FROM Kunden k, Umzuege u WHERE u.Kunden_idKunden = k.idKunden AND k.Nachname ='" + name.Text.Split(',')[0] + "' AND k.Vorname ='" + name.Text.Split(',')[1].Split(' ')[1] + "';", Program.conn);
            MySqlDataReader rdrKundenSuche;
            int count = 0;
            try
            {
                rdrKundenSuche = cmdKundenSuche.ExecuteReader();
                while (rdrKundenSuche.Read())
                {
                    count++;
                    nummer.Value = rdrKundenSuche.GetInt32(0);
                }
                rdrKundenSuche.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }
           
            if (count > 1)
            {
                textLog.AppendText("Entweder mehr als ein Kunde mit dem Namen \r\n oder mehr als ein Umzug zum Kunden gefunden. \r\n Bitte Umzugsnummer angeben \r\n ");
                return;
            }
            
            meldung.Visible = false;
            name.Enabled = false;
            nummer.Enabled = false;

        }

        private void fuellenKundennummer(TextBox name, NumericUpDown nummer, Label meldung)
        {
            //Vortest
            if (nummer.Value == 0) {
                meldung.Visible = true;
                return;
            }


            MySqlCommand cmdKundenSuche = new MySqlCommand("SELECT k.Nachname, k.Vorname FROM Kunden k, Umzuege u WHERE u.Kunden_idKunden = k.idKunden AND u.idUmzuege = " + (decimal.ToInt32(nummer.Value)) + ";", Program.conn);
            MySqlDataReader rdrKundenSuche;
            int count = 0;
            try
            {
                rdrKundenSuche = cmdKundenSuche.ExecuteReader();
                while (rdrKundenSuche.Read())
                {
                    name.Text = rdrKundenSuche[0].ToString() + ", " + rdrKundenSuche[1].ToString();
                    count++;            
                }
                rdrKundenSuche.Close();
                
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                meldung.Visible = true;
                return;
            }

            if (count > 1 || count == 0 ) {
                meldung.Visible = true;
                return;
            }
            meldung.Visible = false;
            name.Enabled = false;
            nummer.Enabled = false;


        }

        private void ZeilenCheck(TextBox T, NumericUpDown N, Label L) {

            // Zeilenweise 
            if (N.Value == Decimal.Zero)
            {
                if (T.Text != "")
                {
                    fuellenKundenname(T, N, L);
                }
            }
            else
            {
                fuellenKundennummer(T, N, L);
            }
        }


        private void buttonSuchen_Click(object sender, EventArgs e)
        {
            ZeilenCheck(textKundenname1, numericUmzugsnummer1, NG1);
            ZeilenCheck(textKundenname2, numericUmzugsnummer2, NG2);
            ZeilenCheck(textKundenname3, numericUmzugsnummer3, NG3);
            ZeilenCheck(textKundenname4, numericUmzugsnummer4, NG4);
            ZeilenCheck(textKundenname5, numericUmzugsnummer5, NG5);
            ZeilenCheck(textKundenname6, numericUmzugsnummer6, NG6);
            ZeilenCheck(textKundenname7, numericUmzugsnummer7, NG7);
            ZeilenCheck(textKundenname8, numericUmzugsnummer8, NG8);
            ZeilenCheck(textKundenname9, numericUmzugsnummer9, NG9);

            if ((textKundenname1.Enabled || textKundenname2.Enabled || textKundenname3.Enabled || textKundenname4.Enabled || textKundenname5.Enabled || textKundenname6.Enabled || textKundenname7.Enabled || textKundenname8.Enabled || textKundenname9.Enabled) == false) {
                buttonSuchen.Enabled = false;
            }

            if((textKundenname1.Enabled && textKundenname2.Enabled && textKundenname3.Enabled && textKundenname4.Enabled && textKundenname5.Enabled && textKundenname6.Enabled && textKundenname7.Enabled && textKundenname8.Enabled && textKundenname9.Enabled) == false) {
                buttonKundenLeeren.Enabled = true;
            }

        }

        private void buttonKundenLeeren_Click(object sender, EventArgs e)
        {
            textKundenname1.Text = "";
            textKundenname2.Text = "";
            textKundenname3.Text = "";
            textKundenname4.Text = "";
            textKundenname5.Text = "";
            textKundenname6.Text = "";
            textKundenname7.Text = "";
            textKundenname8.Text = "";
            textKundenname9.Text = "";

            textKundenname1.Enabled = true;
            textKundenname2.Enabled = true;
            textKundenname3.Enabled = true;
            textKundenname4.Enabled = true;
            textKundenname5.Enabled = true;
            textKundenname6.Enabled = true;
            textKundenname7.Enabled = true;
            textKundenname8.Enabled = true;
            textKundenname9.Enabled = true;

            numericUmzugsnummer1.Value = 0;
            numericUmzugsnummer2.Value = 0;
            numericUmzugsnummer3.Value = 0;
            numericUmzugsnummer4.Value = 0;
            numericUmzugsnummer5.Value = 0;
            numericUmzugsnummer6.Value = 0;
            numericUmzugsnummer7.Value = 0;
            numericUmzugsnummer8.Value = 0;
            numericUmzugsnummer9.Value = 0;

            numericUmzugsnummer1.Enabled = true;
            numericUmzugsnummer2.Enabled = true;
            numericUmzugsnummer3.Enabled = true;
            numericUmzugsnummer4.Enabled = true;
            numericUmzugsnummer5.Enabled = true;
            numericUmzugsnummer6.Enabled = true;
            numericUmzugsnummer7.Enabled = true;
            numericUmzugsnummer8.Enabled = true;
            numericUmzugsnummer9.Enabled = true;

            buttonKundenLeeren.Enabled = false;
            buttonSuchen.Enabled = true;

        }

        

        private void Eintraegespeichern() {

            //Checks gegen Falscheingabe
            
            if (Program.ArbeitsZeitBlock(timeStart.Value, timeEnd.Value, decimal.ToInt32(numericPause.Value)) < 0)
            {
                textLog.AppendText("Die Pause kann nicht länger sein als die angegebene Arbeitszeit \r\n");
                return;
            }

            if (textFahrzeug.Text == "" && checkBeifahrer.Checked == false)
            {
                textLog.AppendText("Es muss ein Fahrzeug ausgewählt werden, wenn Beifahrer nicht gesetzt. \r\n");
                return;
            }

            if (monthFahrtDatum.SelectionStart.Date != monthFahrtDatum.SelectionEnd.Date)
            {
                textLog.AppendText("Mehrtägige Umzüge bitte seperat pro Tag eintragen. Umzügler arbeiten nicht über Mitternacht hinweg. \r\n");
                return;
            }
                        
            if (textSucheName.Text == "")
            {
                textLog.AppendText("Es muss ein (bestenfalls gültiger) Mitarbeiter angegeben werden. \r\n");
                return;
            }

            if (numericKMEnde.Value < numericKMAnfang.Value && checkBeifahrer.Checked == false)
            {
                textLog.AppendText("Endkilometer können nicht kleiner sein als Startkilometer, wenn Beifahrer nicht gesetzt. \r\n");
                return;
            }

            double Kundenzahl = 0;
            Double Teilminuten = 0;
            int Fahrzeit = 0;
            double Strecke = 0;
            double Teilstrecke = 0;
            double DifferenzStück = 0; // Rundungsfehler wird auf die letzte Teilstrecke addiert
            int Pause = 0;

            int MitarbeiterID = Program.getMitarbeiter(textSucheName.Text);
            int FahrzeugID = Program.getFahrzeug(textFahrzeug.Text);         
            

            //Feststellung Kundenzahl für Teilung d. Zeit
            foreach (var item in textboxen)
            {
                if (item.Text != "") {
                    Kundenzahl++;
                }
            }

            // Berechnung Zeit pro Kunde

            TimeSpan x = (timeEnd.Value.TimeOfDay - timeStart.Value.TimeOfDay);

            int min = Convert.ToInt32(x.TotalMinutes);

            double doubleMin = Convert.ToDouble(min);

            Teilminuten = Math.Ceiling(doubleMin / Kundenzahl);

            Fahrzeit = Convert.ToInt32(Teilminuten);

            // Pause

            Pause = Convert.ToInt32(Math.Floor(decimal.ToDouble(numericPause.Value) / Kundenzahl));

            // Berechnung Kilometerstücke.

            Strecke = Convert.ToDouble(numericKMEnde.Value - numericKMAnfang.Value);
            Teilstrecke = Math.Floor (Strecke / Kundenzahl);
            DifferenzStück = Strecke - (Teilstrecke * Kundenzahl);
            

            //Absenden der einzelnen LEA-Einträge

            for (int i = 0; i < Kundenzahl; i++)
            {
                if (checkBeifahrer.Checked == false)
                {
                    if (i < Kundenzahl - 1)
                    {
                        EinzelSpeichern(Convert.ToInt32(zahlboxen[i].Value), (timeStart.Value + TimeSpan.FromMinutes(Fahrzeit * i)), Fahrzeit, Pause, MitarbeiterID, FahrzeugID, Convert.ToInt32(Convert.ToDouble(numericKMAnfang.Value) + (Teilstrecke * i)), Convert.ToInt32((Convert.ToDouble(numericKMAnfang.Value)) + (Teilstrecke * (i + 1))));
                    }
                    else
                    {
                        EinzelSpeichern(Convert.ToInt32(zahlboxen[i].Value), (timeStart.Value + TimeSpan.FromMinutes(Fahrzeit * i)), Fahrzeit, Pause,  MitarbeiterID, FahrzeugID, Convert.ToInt32(Convert.ToDouble(numericKMAnfang.Value) + (Teilstrecke * i)), Convert.ToInt32(Convert.ToDouble(numericKMAnfang.Value) + (Teilstrecke * (i + 1)) + DifferenzStück));
                    }
                }
                else {
                    if (i < Kundenzahl - 1)
                    {
                        EinzelspeichernBeifahrer(Convert.ToInt32(zahlboxen[i].Value), (timeStart.Value + TimeSpan.FromMinutes(Fahrzeit * i)), Fahrzeit, Pause, MitarbeiterID);
                    }
                    else
                    {
                        EinzelspeichernBeifahrer(Convert.ToInt32(zahlboxen[i].Value), (timeStart.Value + TimeSpan.FromMinutes(Fahrzeit * i)), Fahrzeit, Pause, MitarbeiterID);
                    }
                }
            }           

        }

        private void EinzelSpeichern(int Umzug, DateTime Startzeit, int Minuten, int Pause, int Mitarbeiter, int Fahrzeug, int StartKM, int EndKM) {

            String insert = "";
            
            insert += "INSERT INTO Fahrt (Mitarbeiter_idMitarbeiter, Start, Ende, Pause, AnfangsKM, EndKM, Bemerkung, UserChanged, Umzuege_idUmzuege, Tour_idTour, Fahrzeug_idFahrzeug) VALUES (";

            insert += Mitarbeiter + ", ";
            insert += "'" + Program.DateTimeMachine(Startzeit, monthFahrtDatum.SelectionStart) + "', ";
            insert += "'" + Program.DateTimeMachine(Startzeit.AddMinutes(Minuten), monthFahrtDatum.SelectionEnd) + "', ";
            insert += Pause + ", ";
            insert += StartKM + ", ";
            insert += EndKM + ", ";
            insert += "'" + textBemerkung.Text + "', ";
            insert += idBearbeitend + ", ";
            insert += Umzug + ", ";
            insert += "1, ";
            insert += Fahrzeug + ");";

            push(insert);

        }

        private void EinzelspeichernBeifahrer(int Umzug, DateTime Startzeit, int Minuten, int Pause, int Mitarbeiter) {

            String insert = "";

            insert += "INSERT INTO Fahrt (Mitarbeiter_idMitarbeiter, Start, Ende, Pause, Bemerkung, UserChanged, Umzuege_idUmzuege, Tour_idTour) VALUES (";

            insert += Mitarbeiter + ", ";
            insert += "'" + Program.DateTimeMachine(Startzeit, monthFahrtDatum.SelectionStart) + "', ";
            insert += "'" + Program.DateTimeMachine(Startzeit.AddMinutes(Minuten), monthFahrtDatum.SelectionEnd) + "', ";
            insert += Pause + ", ";
            insert += "'" + textBemerkung.Text + "', ";
            insert += idBearbeitend + ", ";
            insert += Umzug + ", ";
            insert += "1); ";

            push(insert);

        }

        private void buttonSenden_Click(object sender, EventArgs e)
        {
            Eintraegespeichern();
        }

        private void push(String k) {

            // String fertig, absenden
            MySqlCommand cmdAdd = new MySqlCommand(k, Program.conn2);
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

        private void FahrzeugSchließen() {

            textFahrzeug.Clear();
            textFahrzeug.Enabled = false;

            numericKMAnfang.Value = 0;
            numericKMAnfang.Enabled = false;

            numericKMEnde.Value = 0;
            numericKMEnde.Enabled = false;
        }

        private void FahrzeugOeffnen() {

            textFahrzeug.Enabled = true;
            numericKMAnfang.Enabled = true;
            numericKMEnde.Enabled = true;
        }

        private void checkBeifahrer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBeifahrer.Checked)
            {
                FahrzeugSchließen();
            }
            else {
                FahrzeugOeffnen();
            }
        }

        private void TerminFuellen(int Code) {

            Events k = Program.kalenderKundenFinder(monthFahrtDatum.SelectionStart.Date);
            int counter = 0;

            foreach (var eventor in k.Items)
            {
                if (eventor.ColorId == Code.ToString() && radioBesichtigen.Checked)
                {
                    zahlboxen[counter].Value = int.Parse(eventor.Summary.Split(' ')[0]);
                    fuellenKundennummer(textboxen[counter], zahlboxen[counter], labels[counter]);
                    counter++;
                }
            }
        }

        private void buttonTermineFinden_Click(object sender, EventArgs e)
        {
            if (radioBesichtigen.Checked)
            {
                TerminFuellen(9);
            }
            else if (radioKartons.Checked)
            {
                TerminFuellen(0);
            }
            else if (radioSchilder.Checked)
            {
                TerminFuellen(3);
            }
            else {
                textLog.AppendText("Es muss ein Typ Fahrt sowie das gesuchte Datum ausgewählt sein \r\n ");
            }
        }
    }

        //private void button1_Click(object sender, EventArgs e)
        //{
        

        //    Events k = Program.kalenderKundenFinder(monthFahrtDatum.SelectionStart.Date);
        //    int counter = 0;
            
        //    foreach (var eventor in k.Items)
        //    {
        //        if (eventor.ColorId == "9" && radioBesichtigen.Checked) {
        //            zahlboxen[counter].Value = int.Parse(eventor.Summary.Split(' ')[0]);
        //            fuellenKundennummer(textboxen[counter], zahlboxen[counter], labels[counter]);
        //            counter++;
        //        }
        //    }
            
        //    textLog.AppendText("Done?!");
        //}
    //}
}
