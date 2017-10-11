using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartonagen.Objekte
{
    class Kunde
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

            MySqlCommand cmdReadKunde = new MySqlCommand("SELECT * FROM Kunden WHERE idKunden = "+id+";", Program.conn);
            MySqlDataReader rdrKunde;

            string tempStr = "";
            string tempHausnummer = "";
            string tempPLZ = "";
            string tempOrt = "";

            try
            {
                rdrKunde = cmdReadKunde.ExecuteReader();
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
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen des Kunden zur Objekterzeugung");
            }

            anschrift = new Adresse(tempStr, tempHausnummer, tempOrt, tempPLZ, "", 0, "", "", 0, 0, 0);
        }
    }
}
