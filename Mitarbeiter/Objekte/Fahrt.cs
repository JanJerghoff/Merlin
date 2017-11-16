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

            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Umzuege WHERE idUmzuege = " + ID + ";", Program.conn);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    
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
