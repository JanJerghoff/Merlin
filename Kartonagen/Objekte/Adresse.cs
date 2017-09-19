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

        string Straße;
        string Hausnummer;
        string Ort;
        string PLZ;
        string Land;
        int Aufzug;
        string Stockwerke;
        string Haustyp;
        int HVZ;
        int Laufmeter;
        int AussenAufzug;

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


        public string KalenderStringEtageHaustyp()
        {

            string temp = "";

            if (Haustyp == "Wohnung") {
                temp += "Wohnung im ";
                temp += GeschosseListe();
                temp += "\r\n";
            }
            else if (Haustyp == "EFH")
            {
                temp += "Einfamilienhaus mit ";
                temp += GeschosseListe();
                temp += "\r\n";
            }
            else if (Haustyp == "RH")
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

        private string GeschosseListe() {

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
