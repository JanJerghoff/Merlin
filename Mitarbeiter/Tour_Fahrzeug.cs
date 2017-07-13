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
    public partial class Tour_Fahrzeug : Form
    {
        public Tour_Fahrzeug()
        {
            InitializeComponent();

            // String-collection anlegen
            AutoCompleteStringCollection autocompleteFahrzeug = new AutoCompleteStringCollection();
            AutoCompleteStringCollection autocompleteTour = new AutoCompleteStringCollection();

            //Abfrage aller Namen
            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Fahrzeug", Program.conn2);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    autocompleteFahrzeug.Add(rdr[3].ToString());
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }
            // Autocomplete vorlegen
            textFahrzeugName.AutoCompleteCustomSource = autocompleteFahrzeug;
            textFahrzeugName.AutoCompleteMode = AutoCompleteMode.Suggest;

            //Abfrage aller Namen
            MySqlCommand cmdReadTour = new MySqlCommand("SELECT * FROM Tour", Program.conn2);
            MySqlDataReader rdrTour;

            try
            {
                rdrTour = cmdReadTour.ExecuteReader();
                while (rdrTour.Read())
                {
                    autocompleteTour.Add(rdrTour[1].ToString());
                }
                rdrTour.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }
            // Autocomplete vorlegen
            textTourName.AutoCompleteCustomSource = autocompleteTour;
            textTourName.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        int idBearbeitend;

        String TourBearbeitet = "";
        String FahrzeugBearbeitet = "";

        public void setBearbeitend(int code)
        {
            idBearbeitend = code;
        }

        private void FahrzeugFuellen() {

            

            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Fahrzeug WHERE Name = '" + textFahrzeugName.Text + "';", Program.conn2);
            MySqlDataReader rdr;
            int count = 0;

            try
            {

                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    textFahrzeugID.AppendText(rdr.GetString(0));
                    numericZaehlerstand.Value = rdr.GetInt32(2);
                    numericCentProKM.Value = rdr.GetInt32(4);
                    textKennzeichen.AppendText(rdr.GetString(1));
                    count++;
                }
                rdr.Close();

                if (count == 0) {
                    var warnung = MessageBox.Show("Kein Fahrzeug mit dem Namen gefunden, Eingabe überprüfen", "Fehlermeldung");
                }
            }
            catch (Exception sqlEx)
            {
                textLog.AppendText(sqlEx.ToString());
                return;
            }

        }

        private void TourFuellen() {

            

            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Tour WHERE Name = '" + textTourName.Text + "';", Program.conn2);
            MySqlDataReader rdr;
            int count = 0;

            try
            {

                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    textTourID.AppendText(rdr.GetString(0));
                    numericTourKM.Value = rdr.GetInt32(2);
                    numericTyp.Value = rdr.GetInt32(3);
                    count++;
                }
                rdr.Close();

                if (count == 0)
                {
                    var warnung = MessageBox.Show("Keine Tour mit dem Namen gefunden, Eingabe überprüfen", "Fehlermeldung");
                }
            }
            catch (Exception sqlEx)
            {
                textLog.AppendText(sqlEx.ToString());
                return;
            }


        }


        private void TourLeeren() {
            textTourID.Clear();
            textTourName.Clear();
            numericTourKM.Value = 0;
            numericTyp.Value = 0;
        }
            

        private void FahrzeugLeeren() {
            textFahrzeugID.Clear();
            textKennzeichen.Clear();
            numericCentProKM.Value = 0;
            numericZaehlerstand.Value = 0;
        }

        private void buttonFahrzeugSuchen_Click(object sender, EventArgs e)
        {
            FahrzeugFuellen();
        }

        private void buttonFahrzeugLeeren_Click(object sender, EventArgs e)
        {
            FahrzeugLeeren();
        }

        private void buttonTourSuchen_Click(object sender, EventArgs e)
        {
            TourFuellen();
        }

        private void buttonTourLeeren_Click(object sender, EventArgs e)
        {
            TourLeeren();
        }

        private void buttonFahrzeugAdd_Click(object sender, EventArgs e)
        {
            if (textFahrzeugID.Text != "") {
                var warnung = MessageBox.Show("Bitte erst Leeren, dann neues Fahrzeug hinzufügen", "Fehlermeldung");
                textLog.AppendText("Fahrzeug konnte nicht hinzugefügt werden, Formular muss vorher geleert sein (keine ID) \r\n");
                return;
            }

            if (textKennzeichen.Text == "") {
                var warnung = MessageBox.Show("Es muss ein Kennzeichen angegeben sein, bitte ergänzen", "Fehlermeldung");
                textLog.AppendText("Kennzeichen fehlt, bitte ergänzen \r\n");
                return;
            }

            if (textFahrzeugName.Text == "")
            {
                var warnung = MessageBox.Show("Es muss ein Name angegeben sein, bitte ergänzen", "Fehlermeldung");
                textLog.AppendText("Name fehlt, bitte ergänzen \r\n");
                return;
            }


            String Insert = "INSERT INTO Fahrzeug (Kennzeichen, Zaehlerstand, Name, KostenKM) VALUES (";
            Insert += "'" + textKennzeichen.Text + "', ";
            Insert += numericZaehlerstand.Value + ", ";
            Insert += "'" + textFahrzeugName.Text + "', ";
            Insert += numericCentProKM.Value + ");";

            // String fertig, absenden
            MySqlCommand cmdAdd = new MySqlCommand(Insert, Program.conn2);
            try
            {
                cmdAdd.ExecuteNonQuery();
                textLog.AppendText("Fahrzeug erfolgreich angelegt \r\n ");

            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }
        }

        private void buttonTourAdd_Click(object sender, EventArgs e)
        {
            if (textTourID.Text != "")
            {
                var warnung = MessageBox.Show("Bitte erst Leeren, dann neue Tour hinzufügen", "Fehlermeldung");
                textLog.AppendText("Tour konnte nicht hinzugefügt werden, Formular muss vorher geleert sein (keine ID) \r\n");
                return;
            }
            
            if (textTourName.Text == "")
            {
                var warnung = MessageBox.Show("Es muss ein Name angegeben sein, bitte ergänzen", "Fehlermeldung");
                textLog.AppendText("Name fehlt, bitte ergänzen \r\n");
                return;
            }

            String Insert = "INSERT INTO Tour (Name, LaengeKM, Type) VALUES (";            
            Insert += "'" + textTourName.Text + "', ";
            Insert += numericTourKM.Value + ", ";
            Insert += numericTyp.Value + ");";

            // String fertig, absenden
            MySqlCommand cmdAdd = new MySqlCommand(Insert, Program.conn2);
            try
            {
                cmdAdd.ExecuteNonQuery();
                textLog.AppendText("Tour erfolgreich angelegt \r\n ");

            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }
        }

        private void buttonFahrzeugChange_Click(object sender, EventArgs e)
        {
            if (textFahrzeugID.Text == "")
            {
                var warnung = MessageBox.Show("Es muss ein Fahrzeug geladen sein (ID-Feld) um es zu ändern", "Fehlermeldung");
                textLog.AppendText("Fahrzeug konnte nicht geändert werden, Formular darf nicht geleert sein ( ID) \r\n");
                return;
            }

            if (textKennzeichen.Text == "")
            {
                var warnung = MessageBox.Show("Es muss ein Kennzeichen angegeben sein, bitte ergänzen", "Fehlermeldung");
                textLog.AppendText("Kennzeichen fehlt, bitte ergänzen \r\n");
                return;
            }

            if (textFahrzeugName.Text == "")
            {
                var warnung = MessageBox.Show("Es muss ein Name angegeben sein, bitte ergänzen", "Fehlermeldung");
                textLog.AppendText("Name fehlt, bitte ergänzen \r\n");
                return;
            }

            String Insert = "UPDATE Fahrzeug SET ";
            Insert += "Kennzeichen = '" + textKennzeichen.Text + "', ";
            Insert += "Zaehlerstand = " + numericZaehlerstand.Value + ", ";
            Insert += "Name = '" + textFahrzeugName.Text + "', ";
            Insert += "KostenKM = "+numericCentProKM.Value+" WHERE idFahrzeug = "+textFahrzeugID.Text+";";

            // String fertig, absenden
            MySqlCommand cmdAdd = new MySqlCommand(Insert, Program.conn2);
            try
            {
                cmdAdd.ExecuteNonQuery();
                textLog.AppendText("Fahrzeug erfolgreich geändert \r\n ");

            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }

        }

        private void buttonTourChange_Click(object sender, EventArgs e)
        {
            if (textTourID.Text == "")
            {
                var warnung = MessageBox.Show("Es muss eine Tour geladen sein (ID-Feld) um sie zu ändern", "Fehlermeldung");
                textLog.AppendText("Tour konnte nicht geändert werden, Formular darf nicht geleert sein (ID) \r\n");
                return;
            }

            if (textTourName.Text == "")
            {
                var warnung = MessageBox.Show("Es muss ein Name angegeben sein, bitte ergänzen", "Fehlermeldung");
                textLog.AppendText("Name fehlt, bitte ergänzen \r\n");
                return;
            }

            String Insert = "UPDATE Tour SET ";
            Insert += "Name = '" + textTourName.Text + "', ";
            Insert += "LaengeKM = " + numericTourKM.Value + ", ";
            Insert += "Type = " + numericTyp.Value + " WHERE idTour = " + textTourID.Text + ";";

            // String fertig, absenden
            MySqlCommand cmdAdd = new MySqlCommand(Insert, Program.conn2);
            try
            {
                cmdAdd.ExecuteNonQuery();
                textLog.AppendText("Tour erfolgreich geändert \r\n ");

            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }


        }
    }
}
