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
                    Umzug1 = rdr.GetInt32(11);          //Neutral = 
                    Tour1 = rdr.GetInt32(12);           
                    Fahrzeug1 = rdr.GetInt32(13);       //Neutral = 14
                    Mitarbeiter1 = rdr.GetInt32(14);
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der Umzugsdaten zur Objekterstellung");
            }

        }

        // Methoden


    }
}
