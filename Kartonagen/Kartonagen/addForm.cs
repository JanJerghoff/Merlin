using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Kartonagen
{
    public partial class addForm : Form
    {
        public addForm()
        {
            InitializeComponent();
        }

        private void buttonKundeFertig_Click(object sender, EventArgs e)
        {
            //Datum extrahieren, nullen auffüllen
            // C# gibt ohne führende Null, Sql will yyyy-mm-dd mit führenden nullen 
            String yyyy = dateKunde.Value.Year.ToString();
            String mm = dateKunde.Value.Month.ToString();
            if (mm.Length == 1)
            {
                mm = "0" + mm;
            }
            String dd = dateKunde.Value.Day.ToString();
            if (dd.Length == 1)
            {
                dd = "0" + dd;
            }
            // Eingaben verifizieren

            if (textKundeName.Text.Length == 0)
            {
                textLog.Text += "Kunde hinzufügen gescheitert, Name ist leer \r\n";
                return;
            }

            if (textKundeTelefon.Text.Length == 0)
            {
                textLog.Text += "Kunde hinzufügen gescheitert, Telefonnummer ist leer \r\n";
                return;
            }

            if (Regex.IsMatch(textKundeTelefon.Text, @"^\d+$")==false)
            {
                textLog.Text += "Kunde hinzufügen gescheitert, Telefonnummer enthält nicht-Zahlen \r\n";
                return;
            };

            // String konstruieren
            string sqlKundeStub = "INSERT INTO Kunde (Name, Umzugstag, Telefonnummer) VALUES (";
            sqlKundeStub += "'"+textKundeName.Text + "', ";
            sqlKundeStub += "'" + yyyy +"-"+mm+"-"+dd+"', ";
            sqlKundeStub += textKundeTelefon.Text+ ");";

            // Fertigen Befehl absenden                       
            MySqlCommand cmdAdd = new MySqlCommand(sqlKundeStub, Program.conn);
            try
            {
                cmdAdd.ExecuteNonQuery();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }
            
            //Abfrage und Bestädtigungsmeldung.
            MySqlCommand cmdShow = new MySqlCommand("SELECT * FROM Kunde ORDER BY idKunde DESC LIMIT 1;", Program.conn);
            MySqlDataReader rdr;
            try
            {
                rdr = cmdShow.ExecuteReader();
                textLog.Text += "Erfolgreich hinzugefügter Kunde: ";
                while (rdr.Read())
                {
                    textLog.Text += rdr[0] + ", " + rdr[1] + ", "+ rdr.GetDateTime(2).ToShortDateString().ToString() +", " + rdr.GetInt32(3).ToString();
                    textLog.Text += "\r\n";
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
            }
        }

        private void buttonTransaktionBestätigen_Click(object sender, EventArgs e)
        {
            // Verifikation: Mögliche Eingabe?
            if (numericTransaktionKundenNr.Value <= 0)
            {
                textLog.Text += "Kundennummer ausserhalb des gültigen Bereichs, bitte korrigieren \r\n";
                return;
            }

            if (numericTransaktionAnzahl.Value == 0)
            {
                textLog.Text += "Buchung ohne Kartonanzahl, bitte Korrigieren \r\n";
                return;
            }

            // Zwischenspeichern Kundendaten

            MySqlCommand cmdShow = new MySqlCommand("SELECT * FROM Kunde WHERE idKunde="+numericTransaktionKundenNr.Value+";", Program.conn);
            MySqlDataReader rdr;

            string tempName ="fail";
            string tempDate ="fail" ; //Dies ist das Umzugsdatum des Kunden, Teil des Kundendatensatzes!
            string tempTelefon= "fail";

            try
            {
                rdr = cmdShow.ExecuteReader();
                while (rdr.Read())
                {
                    tempName = rdr[1].ToString();
                    tempDate = rdr.GetDateTime(2).ToShortDateString().ToString();
                    tempTelefon = rdr.GetInt32(3).ToString();
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }

            // Verifikation möglicher Kunde / Kunde existiert

            if (tempName == "fail")
            {
                textLog.Text += "Kunde nicht gefunden, bitte Kundennummer überprüfen";
                return;
            }

            //         !!!                 Absenden der Daten               !!!

            //Datum vorbereiten / parsen
            // C# gibt ohne führende Null, Sql will yyyy-mm-dd mit führenden nullen 
            String yyyy = dateTransaktionsdatum.Value.Year.ToString();
            String mm = dateTransaktionsdatum.Value.Month.ToString();
            if (mm.Length == 1)
            {
                mm = "0" + mm;
            }
            String dd = dateTransaktionsdatum.Value.Day.ToString();
            if (dd.Length == 1)
            {
                dd = "0" + dd;
            }
            
            // String konstruieren
            string sqlTransaktionStub = "INSERT INTO Transaktion (Kartons, Datum, Kunde_idKunde) VALUES (";
            sqlTransaktionStub += numericTransaktionAnzahl.Value+", ";
            sqlTransaktionStub += "'" + yyyy + "-" + mm + "-" + dd + "', ";
            sqlTransaktionStub += numericTransaktionKundenNr.Value + ");";

            //Absenden
            MySqlCommand cmdAdd = new MySqlCommand(sqlTransaktionStub, Program.conn);
            try
            {
                cmdAdd.ExecuteNonQuery();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
            }

            //Bestätigung vorbereiten
            //Generierung "gebracht / abgeholt" 
            String Buchung = "fail";
            if (numericTransaktionAnzahl.Value > 0)
            {
                Buchung = numericTransaktionAnzahl.Value + " Kartons ausgeliefert / abgeholt am " + dateTransaktionsdatum.Value.Day + "." + dateTransaktionsdatum.Value.Month + "." + dateTransaktionsdatum.Value.Year;
            }
            else
            {
                Buchung = numericTransaktionAnzahl.Value + " Kartons zurückgebracht am " + dateTransaktionsdatum.Value.Day + "." + dateTransaktionsdatum.Value.Month + "." + dateTransaktionsdatum.Value.Year;
            }

            //Bestätigung bauen / zeigen
            string confirm = "Transaktion erfolgreich angelegt. " + Buchung +"an " + tempName + ", Kundennummer " + numericTransaktionKundenNr.Value+".";
            textLog.Text += confirm;



                    

            // Generierung Bestätigungsfrage
            //String frage = "Soll für den Kunden Nr." + numericKundeNr.Value + ", Name " + tempName + ", \r\n"+"umgezogen am " + tempDate + " folgende Transaktion gebucht werden? \r\n"
            //+ Buchung;
            
            // Bestätigung der Transaktion



            //TransaktionBestätigung temp = new TransaktionBestätigung();
            //temp.Show();
            //temp.AnzeigeAendern(frage);



        }
    }
}
