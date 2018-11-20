using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartonagen.Objekte
{
    public class Kunde
    {
        int id;
        string anrede;
        string vorname;
        string nachname;
        string telefon;
        string handy;
        string email;
        Adresse anschrift;
        string userChanged;
        string bemerkung;

        public int Id { get => id; set => id = value; }
        public string Anrede { get => anrede; set => anrede = value; }
        public string Vorname { get => vorname; set => vorname = value; }
        public string Nachname { get => nachname; set => nachname = value; }
        public string Telefon { get => telefon; set => telefon = value; }
        public string Handy { get => handy; set => handy = value; }
        public string Email { get => email; set => email = value; }
        public Adresse Anschrift { get => anschrift; set => anschrift = value; }
        public string UserChanged { get => userChanged; set => userChanged = value; }
        public string Bemerkung { get => bemerkung; set => bemerkung = value; }

        public Kunde(int id) {
            
            string tempStr = "";
            string tempHausnummer = "";
            string tempPLZ = "";
            string tempOrt = "";

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdReadKunde = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden = " + id + ";", Program.conn);
                MySqlDataReader rdrKunde = cmdReadKunde.ExecuteReader();

                while (rdrKunde.Read())
                {
                    id = rdrKunde.GetInt32(0);
                    anrede = rdrKunde.GetString(1);
                    vorname = rdrKunde.GetString(2);
                    nachname = rdrKunde.GetString(3);
                    telefon = rdrKunde.GetString(4);
                    handy = rdrKunde.GetString(5);
                    email = rdrKunde.GetString(6);
                    tempStr = rdrKunde.GetString(7);
                    tempHausnummer = rdrKunde.GetString(8);
                    tempPLZ = rdrKunde.GetString(9);
                    tempOrt = rdrKunde.GetString(10);
                    userChanged = rdrKunde.GetString(12);
                    bemerkung = rdrKunde.GetString(14);
                }
                rdrKunde.Close();
                Program.conn.Close();
                Console.WriteLine("Kunde "+id+" geladen, conn.close");
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen des Kunden "+id+" zur Objekterzeugung");
            }

            //anschrift = new Adresse(tempStr, tempHausnummer, tempOrt, tempPLZ, "", 0, "", "", 0, 0, 0);
        }

        public Kunde(String anrede, String Vorname, String Nachname, String Telefonnummer, String Handynummer, String Email, String Strasse, String Hausnummer, String PLZ, String Ort, String Land, String UserChanged, String Bemerkung) {

            String insert = "INSERT INTO Kunden (Anrede, Vorname, Nachname, Telefonnummer, Handynummer, Email, Straße, Hausnummer, PLZ, Ort, Land, UserChanged, Erstelldatum, Bemerkung) VALUES (";
            insert += "'" + anrede+ "', ";
            insert += "'" + vorname + "', ";
            insert += "'" + nachname + "', ";
            insert += "'" + Telefonnummer + "', ";
            insert += "'" + Handynummer + "', ";
            insert += "'" + Email + "', ";
            insert += "'" + Strasse + "', ";
            insert += "'" + Hausnummer + "', ";
            insert += PLZ + ", ";                 // Ohne '' da Int in der DB, Plz ist immer Int
            insert += "'" + Ort + "', ";
            insert += "'" + Land + "', ";
            insert += "'" + UserChanged + "', ";
            insert += "'" + Program.DateMachine(DateTime.Now) + "', ";
            insert += "'" + Bemerkung + "'); ";


            // String fertig, absenden
            Program.absender(insert, "Einfügen des Kunden in die DB");

            //Abfrage und Bestädtigungsmeldung.
            if (Program.conn.State != ConnectionState.Open)
            {
                Program.conn.Open();
            }
            try
            {
                MySqlCommand cmdShow = new MySqlCommand("SELECT idKunden FROM Kunden ORDER BY idKunden DESC LIMIT 1;", Program.conn);
                MySqlDataReader rdr = cmdShow.ExecuteReader();
                while (rdr.Read())
                {
                    id = rdr.GetInt32(0);
                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Hinzugefügten Kunden nicht gefunden \r\n Bereits dokumentiert.");
            }

        }


        internal string getVollerName()
        {
            return Anrede + " " + Vorname + " " + Nachname;
        }
    }
}
