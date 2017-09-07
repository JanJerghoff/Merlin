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
                    id = rdr.GetInt32(0);
                    idKunden = rdr.GetInt32(1);

                    datBesichtigung = rdr.GetDateTime(2).Date;
                    datUmzug = rdr.GetDateTime(3).Date;
                    datRuempeln = rdr.GetDateTime(4).Date;
                    datEinraeumen = rdr.GetDateTime(5).Date;
                    datAusraeumen = rdr.GetDateTime(6).Date;
                    
                    statUmzug = = rdr.GetInt32(0);
                    statAus = rdr.GetInt32(0);
                    statEin = rdr.GetInt32(0);
                    statRuempeln = rdr.GetInt32(0);
                    zeitUmzug = rdr.GetInt32(0);

                    AufzugA = rdr.GetInt32(7);
                    AufzugB = rdr.GetInt32(8);
                    HVZA = rdr.GetInt32(9);
                    HVZB = rdr.GetInt32(10);
                    GeschossA = rdr.GetInt32(11);       // FIXIT!
                    GeschossB = rdr.GetInt32(12);
                    HaustypA = rdr.GetString(0);        //NR
                    HaustypB = rdr.GetString(0);
                    LaufmeterA = rdr.GetInt32(12);
                    LaufmeterB = rdr.GetInt32(13);
                    AussenAufzugA = rdr.GetInt32(0);
                    AussenAufzugB = rdr.GetInt32(0);    //NR
                    Einpacken = rdr.GetInt32(14);
                    Auspacken = rdr.GetInt32(15);
                    Einpacker = rdr.GetInt32(16);
                    Auspacker = rdr.GetInt32(17);       // Datenbank Änderung Abends

                    EinStunden;
                     AusStunden;
                     Karton;         // Benötigt?
                     Kleiderkartons;
                     Mann;
                     Stunden;
                     Schilder;
                     SchilderZeit;
                     KucheAuf;
                     KuecheAb;
                     KuecheBau;
                     KuechePausch;
                     Umzugsdauer;
                     Autos;       // Beizeiten ersetzen durch kodierten Int?

                     StrasseA;    // Adressobjekt einführen?
                     HausnummerA;
                     OrtA;
                     PLZA;
                     LandA;

                     StrasseB;    // Adressobjekt einführen?
                     HausnummerB;
                     OrtB;
                     PLZB;
                     LandB;

                     NotizTitel;
                     NotizBuero;
                     NotizFahrer;

                     UserChanged;
                     erstelldatum;
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der Umzugsdaten zur Objekterstellung");
            }
        }






        // Ausgabemethoden

        public String Geschosse() {

            string ret = string.Empty;

            ret += Haus
        }


    }
}
