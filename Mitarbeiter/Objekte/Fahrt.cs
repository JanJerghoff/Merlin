using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiter.Objekte
{
    class Fahrt
    {
        int Bearbeitend;
        // Daten
        int id;
        int Mitarbeiter;
        int Tour;
        int Fahrzeug;
        int Umzug;

        DateTime start;
        DateTime ende;
        int Pause;
        int Dauer;
        int StartKM;
        int EndKM;

        int Kunden;
        int Stückzahl;
        int Beilagen;

        string Bemerkung;
        string UserChanged;

        public int Bearbeitend1 { get => Bearbeitend; set => Bearbeitend = value; }
        public int Id { get => id; set => id = value; }
        public int Mitarbeiter1 { get => Mitarbeiter; set => Mitarbeiter = value; }
        public int Tour1 { get => Tour; set => Tour = value; }
        public int Fahrzeug1 { get => Fahrzeug; set => Fahrzeug = value; }
        public int Umzug1 { get => Umzug; set => Umzug = value; }
        public DateTime Start { get => start; set => start = value; }
        public DateTime Ende { get => ende; set => ende = value; }
        public int Pause1 { get => Pause; set => Pause = value; }
        public int Dauer1 { get => Dauer; set => Dauer = value; }
        public int StartKM1 { get => StartKM; set => StartKM = value; }
        public int EndKM1 { get => EndKM; set => EndKM = value; }
        public int Kunden1 { get => Kunden; set => Kunden = value; }
        public int Stückzahl1 { get => Stückzahl; set => Stückzahl = value; }
        public int Beilagen1 { get => Beilagen; set => Beilagen = value; }
        public string Bemerkung1 { get => Bemerkung; set => Bemerkung = value; }
        public string UserChanged1 { get => UserChanged; set => UserChanged = value; }

        // Konstruktoren

        //Aus DB
        public Fahrt (int Nummer) {

            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Fahrt WHERE idFahrt = " + Nummer + ";", Program.conn2);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    
                    id = rdr.GetInt32(0);
                    Start = rdr.GetDateTime(1);
                    Ende = rdr.GetDateTime(2);
                    Pause1 = rdr.GetInt32(3);
                    StartKM1 = rdr.GetInt32(4);
                    EndKM1 = rdr.GetInt32(5);
                    Kunden1 = rdr.GetInt32(6);
                    Stückzahl1 = rdr.GetInt32(7);
                    Beilagen1 = rdr.GetInt32(8);
                    Bemerkung1 = rdr.GetString(9);
                    UserChanged1 = rdr.GetString(10);
                    Umzug1 = SQLReaderExtension.getIntOrMinusEins(rdr, 11);
                    Tour1 = rdr.GetInt32(12);           
                    Fahrzeug1 = SQLReaderExtension.getIntOrMinusEins(rdr, 13);
                    Mitarbeiter1 = rdr.GetInt32(14);
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der Umzugsdaten zur Objekterstellung");
            }
        }

        public Fahrt (int mitarbeiter, int tour, int fahrzeug, int umzug, DateTime start, DateTime ende, int pause, int dauer, int startKM, int endKM, int kunden, int stückzahl, int beilagen, string bemerkung, string userChanged)
        {
            Mitarbeiter = mitarbeiter;
            Tour = tour;
            Fahrzeug = fahrzeug;
            Umzug = umzug;
            this.start = start;
            this.ende = ende;
            Pause = pause;
            Dauer = dauer;
            StartKM = startKM;
            EndKM = endKM;
            Kunden = kunden;
            Stückzahl = stückzahl;
            Beilagen = beilagen;
            Bemerkung = bemerkung;
            UserChanged = userChanged;
        }

        // Update

        public String update() {

            String result = "";

            // Nicht updaten wenn Kollisionen auftreten
            if (checkKollision().Count != 0) {

                result = "Kollision mit den folgenden Fahrten gefunden, bitte überprüfen: ";
                foreach (var item in checkKollision())
                {
                    result += item + " ";
                }
                return result;
            }

            //Updaten

            return result;
        }


        // Methoden

        public static void loeschen(int nr) {

            string loe = "DELETE from Fahrt WHERE idFahrt = " + nr + ";";
            Program.absender(loe,"Löschen der Fahrt mit der Nummer " + nr);

        }

        // Prüft Kollision mit anderen Einträgen (ausser sich selbst) und gibt Liste kollidierender Fahrten
        public  List <int> checkKollision() {

            List<int> result = new List<int>();

            ////Fahrzeugkollision Kilometer
            //result.AddRange(checkFahrzeugKollisionKM());

            ////Fahrzeugkollision Zeit
            //result.AddRange(checkFahrzeugKollisionZeit());

            //Personalkollision
            result.AddRange(checkMitarbeiterKollisionZeit());
            
            return result;

        }

        private List<int> checkFahrzeugKollisionKM() {
            
            List<int> result = new List<int>();

            // Check ob Fahrzeugkollision möglich
            if (Fahrzeug1 <= 0) {
                return result;
            }

            String bedingung = "SELECT idFahrt FROM Fahrt WHERE Fahrzeug_idFahrzeug = " + Fahrzeug1+ " And ("; // Selbes Fahrzeug, Klammer auf für 4 OR-Verknüpfte Optionen für Kilometerkollision
            bedingung += "(AnfangsKM < "+StartKM1+" AND EndKM > "+StartKM+") OR ";                              // Überschneidung um den Startpunkt
            bedingung += "(AnfangsKM < " + EndKM1 + " AND EndKM > " + EndKM1 + ") OR ";                      // Überschneidung um den Endpunkt
            bedingung += "(AnfangsKM < " + StartKM1 + " AND EndKM > " + EndKM1 + ") OR ";
            bedingung += "(AnfangsKM > " + StartKM1 + " AND EndKM < " + EndKM1 + ")); ";                      // Objekt innerhalb dieser Fahrt

            MySqlCommand cmdRead = new MySqlCommand(bedingung, Program.conn2);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    result.Add(SQLReaderExtension.getIntOrMinusEins(rdr, 0));
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der Daten für Fahrzeugkollision bei Kilometern");
            }

            return result;
        }

        private List<int> checkFahrzeugKollisionZeit()
        {

            List<int> result = new List<int>();

            // Check ob Fahrzeugkollision möglich
            if (Fahrzeug1 <= 0)
            {
                return result;
            }

            String bedingung = "SELECT idFahrt FROM Fahrt WHERE Fahrzeug_idFahrzeug = " + Fahrzeug1 + " And ("; // Selbes Fahrzeug, Klammer auf für 4 OR-Verknüpfte Optionen für Kilometerkollision
            bedingung += "(Start < '" + Start + "' AND Ende > '" + Ende + "') OR ";                              // Überschneidung um den Startpunkt
            bedingung += "(Start < '" + Ende + "' AND Ende > '" + Ende + "') OR ";                      // Überschneidung um den Endpunkt
            bedingung += "(Start > '" + Start + "' AND Ende < '" + Ende + "'));";                      // Objekt innerhalb dieser Fahrt

            MySqlCommand cmdRead = new MySqlCommand(bedingung, Program.conn2);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    result.Add(SQLReaderExtension.getIntOrMinusEins(rdr, 0));
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der Daten für Fahrzeugkollision bei Zeit");
            }

            return result;
        }

        private List<int> checkMitarbeiterKollisionZeit()
        {

            List<int> result = new List<int>();
            
            String bedingungX = "SELECT * FROM Fahrt WHERE Mitarbeiter_idMitarbeiter = " + Mitarbeiter1 + " And ("; // Selber Mitarbeiter, Klammer auf für 4 OR-Verknüpfte Optionen für Kilometerkollision
            bedingungX += "(Start < '" + Program.DateTimeMachine(Start,Start) + "' AND Ende > '" + Program.DateTimeMachine(Start, Start) + "') OR ";                              // Überschneidung um den Startpunkt
            bedingungX += "(Start < '" + Program.DateTimeMachine(Ende, Ende) + "' AND Ende > '" + Program.DateTimeMachine(Ende, Ende) + "') OR ";                      // Überschneidung um den Endpunkt
            bedingungX += "(Start > '" + Program.DateTimeMachine(Start, Start) + "' AND Ende < '" + Program.DateTimeMachine(Ende, Ende) + "') OR ";                      // Objekt innerhalb dieser Fahrt
            bedingungX += "(Start < '" + Program.DateTimeMachine(Start, Start) + "' AND Ende > '" + Program.DateTimeMachine(Ende, Ende) + "'));";                      // Fahrt eingeschlossen in Objekt

            MySqlCommand cmdRead = new MySqlCommand(bedingungX, Program.conn2);
            MySqlDataReader rdrX;

            try
            {
                rdrX = cmdRead.ExecuteReader();
                while (rdrX.Read())
                {
                    result.Add(SQLReaderExtension.getIntOrMinusEins(rdrX, 0));
                }
                rdrX.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der Daten für Mitarbeiterkollision bei Zeit");
            }
            
            return result;
        }

    }
}
