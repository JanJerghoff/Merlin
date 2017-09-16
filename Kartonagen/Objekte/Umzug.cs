using Kartonagen.Objekte;
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
        // Zeitblock
        DateTime datBesichtigung;   // Nur Datumskomponenten
        DateTime datUmzug;
        DateTime datEinraeumen;
        DateTime datAusraeumen;
        DateTime datRuempeln;
        DateTime zeitUmzug;     //´Nur Zeitkomponente
        int statBesichtigung;
        int statUmzug;
        int statAus;
        int statEin;
        int statRuempeln;
        int umzugsdauer;        // In Tagen

        // Kerndaten
        string autos;       // Beizeiten ersetzen durch kodierten String?
        int mann;
        int stunden;
        Boolean versicherung;

        //Packen
        int Einpacken;
        int Einpacker;
        int EinStunden;
        int Karton;         // Benötigt?

        int Auspacken;        
        int Auspacker;        
        int AusStunden;
        
        int Kleiderkartons;

        //Küche
        int KucheAuf;
        int KuecheAb;
        int KuecheBau;
        int KuechePausch;

        // Adressobjekte

        Adresse auszug;
        Adresse einzug;
        
        // Schilder
        Boolean Schilder;
        DateTime SchilderZeit;
                
        // Notizen
        string NotizTitel;
        string NotizBuero;
        string NotizFahrer;

        //Metadata
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

                    // Zeitblock
                    datBesichtigung = rdr.GetDateTime(2).Date;
                    datUmzug = rdr.GetDateTime(3).Date;
                    datRuempeln = rdr.GetDateTime(4).Date;
                    datEinraeumen = rdr.GetDateTime(5).Date;
                    datAusraeumen = rdr.GetDateTime(6).Date;
                    zeitUmzug = rdr.GetDateTime(7);

                    statBesichtigung = rdr.GetInt32(8);
                    statUmzug = rdr.GetInt32(9);
                    statRuempeln = rdr.GetInt32(10);
                    statEin = rdr.GetInt32(11);
                    statAus = rdr.GetInt32(12);
                    
                    umzugsdauer = rdr.GetInt32(13);

                    //Kerndaten
                    autos = rdr.GetString(14);       // Beizeiten ersetzen durch kodierten String?
                    mann = rdr.GetInt32(15);
                    stunden = rdr.GetInt32(16);

                    if (rdr.GetInt32(17) == 1)
                    {
                        versicherung = true;
                    }
                    else {
                        versicherung = false;
                         }

                    //Packen
                    Einpacken = rdr.GetInt32(18);
                    Einpacker = rdr.GetInt32(19);
                    EinStunden = rdr.GetInt32(20);
                    Karton = rdr.GetInt32(21);         // Benötigt?

                    Auspacken = rdr.GetInt32(22);
                    Auspacker = rdr.GetInt32(23);
                    AusStunden = rdr.GetInt32(24);

                    Kleiderkartons = rdr.GetInt32(25);

                    //Schilder
                    if (rdr.GetInt32(26) == 1)
                    {
                        Schilder = true;
                    }
                    else
                    {
                        Schilder = false;
                    }
                    SchilderZeit = rdr.GetDateTime(rdr.GetString(56)); // Hinterfragt, ans Ende gesetzt

                    // Küche
                    KuecheAb = rdr.GetInt32(27);
                    KucheAuf = rdr.GetInt32(28);                    
                    KuecheBau = rdr.GetInt32(29);
                    KuechePausch = rdr.GetInt32(30);

                    // Adressen

                    auszug = new Adresse(rdr.GetString(31), rdr.GetString(32), rdr.GetString(33), rdr.GetString(34), rdr.GetString(35), rdr.GetInt32(36), rdr.GetString(37), rdr.GetString(38), rdr.GetInt32(39), rdr.GetInt32(40), rdr.GetInt32(41));

                    einzug = new Adresse(rdr.GetString(42), rdr.GetString(43), rdr.GetString(44), rdr.GetString(45), rdr.GetString(46), rdr.GetInt32(47), rdr.GetString(48), rdr.GetString(49), rdr.GetInt32(50), rdr.GetInt32(51), rdr.GetInt32(52));
                                        
                    
                    NotizBuero = rdr.GetString(53);
                    NotizFahrer = rdr.GetString(54);
                    NotizTitel = rdr.GetString(55);

                    UserChanged = rdr.GetString(57);
                    erstelldatum = rdr.GetDateTime(58);
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der Umzugsdaten zur Objekterstellung");
            }
        }
        

        // Ausgabemethoden

        public String Geschosse(int EinAusZug) {
            
            string ret = string.Empty;
            string Geschoss;
            string Haustyp;

            if (EinAusZug == 0) {
                Geschoss = GeschossA;
                Haustyp = HaustypA;
            }
            else
            {
                Geschoss = GeschossB;
                Haustyp = HaustypB;
            }

            ret += Haustyp;
            ret += "\r\n " + Geschoss;

            return ret;

        }

        //Updatemechanik
        public void Push ()
        {

            String longInsert = "INSERT INTO Umzuege (Kunden_idKunden, datBesichtigung, datUmzug, datRuempelung, datEinpacken, datAuspacken, " +
                "StatUmz, StatBes, StatAus, StatEin, StatEnt, umzugsZeit, Autos, Mann, Stunden, Versicherung" +
                "Einpacken, EinPackerZahl, EinPackStunden, Kartons, Auspacken, AusPackerZahl, AusPackStunden, Kleiderkisten, SchilderZurueck, " +
                "KuecheAb, KuecheAuf, KuecheBau, KuechePausch, "+
                "StraßeA, HausnummerA, PLZA, OrtA, LandA, AufzugA, StockwerkeA, HausTypA, HVZA, LaufmeterA, AussenAufzugA, " +
                "StraßeB, HausnummerB, PLZB, OrtB, LandB, AufzugB, StockwerkeB, HausTypB, HVZB, LaufmeterB, AussenAufzugB, " +

                "AufzugA, AufzugB, HVZA, HVZB, StockwerkeA, StockwerkeB, LaufmeterA, LaufmeterB, Einpacken, Auspacken, Packerzahl, Kartons, Kleiderkisten, " +
                "Mann, Stunden, SchilderBool, SchilderZeit, KuecheAb, KuecheAuf, KuecheBau, KuechePausch, UmzugsDauer, Autos, " +
                "StraßeA, HausnummerA, PLZA, OrtA, LandA, AussenAufzugA, " +
                "StraßeB, HausnummerB, PLZB, OrtB, LandB, AussenaufzugB, NotizBuero, NotizFahrer, BemerkungTitel, UserChanged, Erstelldatum, PackerStunden, " +
                "StatUmz, StatBes, StatAus, StatEin, StatEnt, HausTypA, HausTypB, umzugsZeit) VALUES (";


        }
    }
}
