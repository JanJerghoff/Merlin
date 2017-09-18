using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartonagen.Objekte
{
    class Adresse
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
    }
}
