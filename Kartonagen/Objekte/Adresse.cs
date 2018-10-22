using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartonagen.Objekte
{
    public class Adresse
    {
        // Felder
        int IDAdresse;
        string Straße = "";
        string Hausnummer = "";
        string Ort = "";
        string PLZ = "";
        string Land = "";
        int Aufzug = 0;
        string Stockwerke = "";
        string Haustyp = "";
        int HVZ =0;
        int Laufmeter=0;
        int AussenAufzug=0;
        string Bemerkung = "";

        public Adresse(string straße, string hausnummer, string ort, string pLZ, string land, int aufzug, string stockwerke, string haustyp, int hVZ, int laufmeter, int aussenAufzug)
        {
            Straße = straße;
            Hausnummer = hausnummer;
            Ort = ort;
            PLZ = pLZ;
            Land = land;
            Aufzug = aufzug;
            Stockwerke = stockwerke;
            Haustyp = haustyp;
            HVZ = hVZ;
            Laufmeter = laufmeter;
            AussenAufzug = aussenAufzug;

            saveNew();

            String select = "SELECT id FROM Adresse ORDER BY id DESC LIMIT 1;";

            MySqlCommand cmdId = new MySqlCommand(select, Program.conn);
            MySqlDataReader rdrId;

            try
            {

                rdrId = cmdId.ExecuteReader();
                while (rdrId.Read())
                {
                    IDAdresse = rdrId.GetInt32(0);
                }
                rdrId.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der ID der neu angelegten Adresse "+ Straße );
            }
        }

        public Adresse(int id) {

            String select = "SELECT * FROM Adresse WHERE id = "+id+";";

            MySqlCommand cmdRead = new MySqlCommand(select, Program.conn);
            MySqlDataReader rdr;

            try
            {

                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    Straße = rdr.GetString(1);
                    Hausnummer = rdr.GetString(2);
                    Ort = rdr.GetString(3);
                    PLZ = rdr.GetString(4);
                    Land = rdr.GetString(5);
                    Aufzug = rdr.GetInt32(6);
                    Stockwerke = rdr.GetString(7);
                    Haustyp = rdr.GetString(8);
                    Laufmeter = rdr.GetInt32(10);
                    AussenAufzug = rdr.GetInt32(11);
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der Adresse Nummer " + id);
            }

        }

        public string Straße1 { get => Straße; set => Straße = value; }
        public string Hausnummer1 { get => Hausnummer; set => Hausnummer = value; }
        public string Ort1 { get => Ort; set => Ort = value; }
        public string PLZ1 { get => PLZ; set => PLZ = value; }
        public string Land1 { get => Land; set => Land = value; }
        public int Aufzug1 { get => Aufzug; set => Aufzug = value; }
        public string Stockwerke1 { get => Stockwerke; set => Stockwerke = value; }
        public string Haustyp1 { get => Haustyp; set => Haustyp = value; }
        public int HVZ1 { get => HVZ; set => HVZ = value; }
        public int Laufmeter1 { get => Laufmeter; set => Laufmeter = value; }
        public int AussenAufzug1 { get => AussenAufzug; set => AussenAufzug = value; }
        public string Bemerkung1 { get => Bemerkung; set => Bemerkung = value; }
        public int IDAdresse1 { get => IDAdresse; set => IDAdresse = value; }

        public void saveNew() {
            String dbInsert = "INSERT INTO Adresse (strasse, hausnummer, ort, PLZ, land, aufzug, stockwerke, haustyp, aussenaufzug, laufmeter) Values (";

            dbInsert += "'"+ Straße1 + "', ";
            dbInsert += "'" + Hausnummer1 + "', ";
            dbInsert += "'" + Ort1 + "', ";
            dbInsert += "'" + PLZ1 + "', ";
            dbInsert += "'" + Land1 + "', ";
            dbInsert += Aufzug1 + ", ";
            dbInsert += "'" + Stockwerke1 + "', ";
            dbInsert += "'" + Haustyp1 + "', ";
            dbInsert += AussenAufzug1 + ", ";
            dbInsert += Laufmeter1 + ");";

            Program.QueryLog(dbInsert);
            Program.absender(dbInsert, "Einfügen der Adresse "+Straße1);
        }

        public void updateDB() {


            String dbInsert = "UPDATE Adresse SET ";
            dbInsert += "strasse = '" + Straße1 + "',";
            dbInsert += "hausnummer = '" + Hausnummer1 + "',";
            dbInsert += "ort = '" + Ort1 + "',";
            dbInsert += "PLZ = '" + PLZ1 + "',";
            dbInsert += "land = '" + Land1 + "',";
            dbInsert += "aufzug = " + Aufzug1 + ",";
            dbInsert += "stockwerke = '" + Stockwerke1 + "',";
            dbInsert += "haustyp = '" + Haustyp1 + "',";
            dbInsert += "HVZ = " + HVZ1 + ",";
            dbInsert += "laufmeter = " + Laufmeter1 + ",";
            dbInsert += "aussenaufzug = " + AussenAufzug1 + ",";
            dbInsert += "bemerkung = '" + Bemerkung1 + "'";

            dbInsert += " WHERE id =" + IDAdresse1 +";";

            Program.QueryLog(dbInsert);
            Program.absender(dbInsert, "Updaten der Adresse " + Straße1);
        }

        public int findAdresse() {
            int idDb = 0;

            String select = "SELECT id FROM Adresse WHERE strasse = '" + Straße1 + "' AND hausnummer = '" + Hausnummer1 + "' AND ort = '" + Ort1 + "' AND PLZ = '" + PLZ1 + "';";

            MySqlCommand cmdRead = new MySqlCommand(select, Program.conn);
            MySqlDataReader rdr;

            try
            {

                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    idDb = rdr.GetInt32(0);
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Finden der Adresse : "+Straße1+ " "+Hausnummer1+"");
            }

            return idDb;
        }

        public String beschreibungsText() {

            String ret = KalenderStringEtageHaustyp() + " ";

            if (Aufzug == 1)
            {
                ret += "mit Aufzug \r\n";
            }
            else {
                ret += "ohne Aufzug \r\n";
            }

            return ret;
        }

            public string KalenderStringEtageHaustyp()
        {

            string temp = "";

            if (Haustyp.Contains("Wohnung")) {
                temp += "Wohnung im ";
                temp += GeschosseListe();
                temp += "\r\n";
            }
            else if (Haustyp.Contains("EFH"))
            {
                temp += "Einfamilienhaus mit ";
                temp += GeschosseListe();
                temp += "\r\n";
            }
            else if (Haustyp.Contains("RH"))
            {
                temp += "Reihenhaus mit ";
                temp += GeschosseListe();
                temp += "\r\n";
            }
            else
            {
                temp += "Doppelhaushälfte mit ";
                temp += GeschosseListe();
                temp += "\r\n";
            }
            return temp;

        }

        public string GeschosseListe() {

            string temp = "";

            string[] tempList = Stockwerke.Split(',');

            foreach (var item in tempList)
            {
                if (item.Contains("K"))
                {
                    temp += "Keller, ";
                }
                else if (item.Contains("EG"))
                {
                    temp += "Erdgeschoss, ";
                }
                else if (item.Contains("DB"))
                {
                    temp += "Dachboden, ";
                }
                else if (item.Contains("MA"))
                {
                    temp += "Maisonette, ";
                }
                else if (item.Contains("ST"))
                {
                    temp += "Souterrain, ";
                }
                else if (item.Contains("HP"))
                {
                    temp += "Hochpaterre, ";
                }
                else if (item.Contains("1"))
                {
                    temp += "1.OG, ";
                }
                else if (item.Contains("2"))
                {
                    temp += "2.OG, ";
                }
                else if (item.Contains("3"))
                {
                    temp += "3.OG, ";
                }
                else if (item.Contains("4"))
                {
                    temp += "4.OG, ";
                }
                else if (temp.Contains("5"))
                {
                    temp += "5.OG, ";
                }
                else
                {
                    temp += item + ", ";
                }
            }

            string ret = temp.Remove(temp.Length - 2);

           return ret;

        }
    }
}
