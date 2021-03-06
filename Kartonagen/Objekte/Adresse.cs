﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            
            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdId = new MySqlCommand(select, Program.conn);
                MySqlDataReader rdrId = cmdId.ExecuteReader();
                while (rdrId.Read())
                {
                    IDAdresse = rdrId.GetInt32(0);
                }
                rdrId.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der ID der neu angelegten Adresse "+ Straße );
            }
        }

        public Adresse(int id) {

            String select = "SELECT * FROM Adresse WHERE id = "+id+";";

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdRead = new MySqlCommand(select, Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();

                while (rdr.Read())
                {
                    IDAdresse = rdr.GetInt32(0);
                    Straße = rdr.GetString(1);
                    Hausnummer = rdr.GetString(2);
                    Ort = rdr.GetString(3);
                    PLZ = rdr.GetString(4);
                    Land = rdr.GetString(5);
                    Aufzug = rdr.GetInt32(6);
                    Stockwerke = rdr.GetString(7);
                    Haustyp = rdr.GetString(8);
                    HVZ = rdr.GetInt32(9);
                    Laufmeter = rdr.GetInt32(10);
                    AussenAufzug = rdr.GetInt32(11);
                }
                rdr.Close();
                Program.conn.Close();
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
            
            Program.absender(dbInsert, "Updaten der Adresse " + Straße1);
        }

        public int findAdresse() {

            int idDb = 0;

            String select = "SELECT id FROM Adresse WHERE strasse = '" + Straße1 + "' AND hausnummer = '" + Hausnummer1 + "' AND ort = '" + Ort1 + "' AND PLZ = '" + PLZ1 + "';";
            
            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdRead = new MySqlCommand(select, Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    idDb = rdr.GetInt32(0);
                }
                rdr.Close();
                Program.conn.Close();
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

            String Bitstring = Stockwerke;
            String temp = "  ";

            if (Bitstring[0] == '1')
            {
                temp += "Keller, ";
            }
            if (Bitstring[1] == '1')
            {
                temp += "Erdgeschoss, ";
            }
            if (Bitstring[2] == '1')
            {
                temp += "Hochpaterre, ";
            }
            if (Bitstring[3] == '1')
            {
                temp += "Souterrain, ";
            }
            if (Bitstring[4] == '1')
            {
                temp += "Maisonette, ";
            }
            if (Bitstring[5] == '1')
            {
                temp += "1.OG, ";
            }
            if (Bitstring[6] == '1')
            {
                temp += "2.OG, ";
            }
            if (Bitstring[7] == '1')
            {
                temp += "3.OG, ";
            }
            if (Bitstring[8] == '1')
            {
                temp += "4.OG, ";
            }
            if (Bitstring[9] == '1')
            {
                temp += "5.OG, ";
            }
            if (Bitstring[10] == '1')
            {
                temp += "Dachboden, ";
            }
            if (Bitstring.Split('-').Length != 1)
            {
                if (!Bitstring.Split('-')[1].Equals(String.Empty))
                {
                    temp += Bitstring.Split('-')[1]+"  ";
                }
            }

            string ret = temp.Remove(temp.Length - 2);

           return ret;

        }
    }
}
