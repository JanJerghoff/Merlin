using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartonagen
{
    class Umzug
    {
        // Felder
        int id;
        int idKunden;

        DateTime datBesichtigung;
        DateTime datUmzug;
        DateTime datEinraeumen;
        DateTime datAusraeumen;
        DateTime datRuempeln;
        int statUmzug;
        int statAus;
        int statEin;
        int statRuempeln;
        DateTime zeitUmzug;

        int AufzugA;
        int AufzugB;
        int HVZA;
        int HVZB;
        string GeschossA;
        string GeschossB;
        string HaustypA;
        string HaustypB;
        int LaufmeterA;
        int LaufmeterB;
        int AussenAufzugA;
        int AussenAufzugB;
        int Einpacken;
        int Auspacken;
        int Einpacker;
        int Auspacker;
        int EinStunden;
        int AusStunden;
        int Karton;         // Benötigt?
        int Kleiderkartons;
        int Mann;
        int Stunden;
        Boolean Schilder;
        DateTime SchilderZeit;
        int KucheAuf;
        int KuecheAb;
        int KuecheBau;
        int KuechePausch;
        int Umzugsdauer;
        string Autos;       // Beizeiten ersetzen durch kodierten Int?

        string StrasseA;    // Adressobjekt einführen?
        string HausnummerA;
        string OrtA;
        string PLZA;
        string LandA;

        string StrasseB;    // Adressobjekt einführen?
        string HausnummerB;
        string OrtB;
        string PLZB;
        string LandB;

        string NotizTitel;
        string NotizBuero;
        string NotizFahrer;

        string UserChanged;
        DateTime erstelldatum;

        // Konstruktor
        public Umzug (int ID)
        {
            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Umzuege WHERE idUmzuege = "+ID+";", Program.conn);
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
                
            }
        }



        


        // Ausgabemethoden



    }
}
