using Kartonagen.Objekte;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartonagen
{
    public class Umzug
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
        int versicherung;

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
        int KuecheAuf;
        int KuecheAb;
        int KuecheBau;
        int KuechePausch;

        // Adressobjekte

        public Adresse auszug;
        public Adresse einzug;
        
        // Schilder
        int Schilder;
        DateTime SchilderZeit;
                
        // Notizen
        string NotizTitel;
        string NotizBuero;
        string NotizFahrer;

        //Metadata
        string UserChanged;
        DateTime erstelldatum;

        public DateTime DatBesichtigung { get => datBesichtigung; set => datBesichtigung = value; }
        public DateTime DatUmzug { get => datUmzug; set => datUmzug = value; }
        public DateTime DatEinraeumen { get => datEinraeumen; set => datEinraeumen = value; }
        public DateTime DatAusraeumen { get => datAusraeumen; set => datAusraeumen = value; }
        public DateTime DatRuempeln { get => datRuempeln; set => datRuempeln = value; }
        public DateTime ZeitUmzug { get => zeitUmzug; set => zeitUmzug = value; }
        public int StatBesichtigung { get => statBesichtigung; set => statBesichtigung = value; }
        public int StatUmzug { get => statUmzug; set => statUmzug = value; }
        public int StatAus { get => statAus; set => statAus = value; }
        public int StatEin { get => statEin; set => statEin = value; }
        public int StatRuempeln { get => statRuempeln; set => statRuempeln = value; }
        public int Umzugsdauer { get => umzugsdauer; set => umzugsdauer = value; }
        public string Autos { get => autos; set => autos = value; }
        public int Mann { get => mann; set => mann = value; }
        public int Stunden { get => stunden; set => stunden = value; }
        public int Versicherung { get => versicherung; set => versicherung = value; }
        public int Einpacken1 { get => Einpacken; set => Einpacken = value; }
        public int Einpacker1 { get => Einpacker; set => Einpacker = value; }
        public int EinStunden1 { get => EinStunden; set => EinStunden = value; }
        public int Karton1 { get => Karton; set => Karton = value; }
        public int Auspacken1 { get => Auspacken; set => Auspacken = value; }
        public int Auspacker1 { get => Auspacker; set => Auspacker = value; }
        public int AusStunden1 { get => AusStunden; set => AusStunden = value; }
        public int Kleiderkartons1 { get => Kleiderkartons; set => Kleiderkartons = value; }
        public int KuecheAuf1 { get => KuecheAuf; set => KuecheAuf = value; }
        public int KuecheAb1 { get => KuecheAb; set => KuecheAb = value; }
        public int KuecheBau1 { get => KuecheBau; set => KuecheBau = value; }
        public int KuechePausch1 { get => KuechePausch; set => KuechePausch = value; }
        public int Schilder1 { get => Schilder; set => Schilder = value; }
        public DateTime SchilderZeit1 { get => SchilderZeit; set => SchilderZeit = value; }
        public string NotizTitel1 { get => NotizTitel; set => NotizTitel = value; }
        public string NotizBuero1 { get => NotizBuero; set => NotizBuero = value; }
        public string NotizFahrer1 { get => NotizFahrer; set => NotizFahrer = value; }
        public int IdKunden { get => idKunden; }

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

                    TimeSpan zeittemp = rdr.GetTimeSpan(7);

                    zeitUmzug = new DateTime(2000, 1, 1);
                    zeitUmzug.Add(zeittemp);

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
                    versicherung = rdr.GetInt32(17);

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
                    Schilder = rdr.GetInt32(26);                    
                    SchilderZeit = rdr.GetDateTime(56); // Hinterfragt, ans Ende gesetzt

                    // Küche
                    KuecheAb = rdr.GetInt32(27);
                    KuecheAuf = rdr.GetInt32(28);                    
                    KuecheBau = rdr.GetInt32(29);
                    KuechePausch = rdr.GetInt32(30);

                    // Adressen

                    auszug = new Adresse(rdr.GetString(31), rdr.GetString(32), rdr.GetString(34), rdr.GetString(33), rdr.GetString(35), rdr.GetInt32(36), rdr.GetString(37), rdr.GetString(38), rdr.GetInt32(39), rdr.GetInt32(40), rdr.GetInt32(41));

                    einzug = new Adresse(rdr.GetString(42), rdr.GetString(43), rdr.GetString(45), rdr.GetString(44), rdr.GetString(46), rdr.GetInt32(47), rdr.GetString(48), rdr.GetString(49), rdr.GetInt32(50), rdr.GetInt32(51), rdr.GetInt32(52));
                                        
                    
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

        public Umzug(int idKunden, DateTime datBesichtigung, DateTime datUmzug, DateTime datEinraeumen, DateTime datAusraeumen, DateTime datRuempeln, DateTime zeitUmzug, int statBesichtigung, int statUmzug, int statAus, int statEin, int statRuempeln, int umzugsdauer, string autos, int mann, int stunden, int versicherung, int einpacken, int einpacker, int einStunden, int karton, int auspacken, int auspacker, int ausStunden, int kleiderkartons, int kuecheAuf, int kuecheAb, int kuecheBau, int kuechePausch, Adresse auszug, Adresse einzug, int schilder, DateTime schilderZeit, string notizTitel, string notizBuero, string notizFahrer, string userChanged, DateTime erstelldatum)
        {

            String longInsert = "INSERT INTO Umzuege (Kunden_idKunden, datBesichtigung, datUmzug, datRuempelung, datEinpacken, datAuspacken, umzugsZeit, " +
                "StatBes, StatUmz, StatAus, StatEin, StatEnt, Umzugsdauer, Autos, Mann, Stunden, Versicherung, " +
                "Einpacken, EinPackerZahl, EinPackStunden, Kartons, Auspacken, AusPackerZahl, AusPackStunden, Kleiderkisten, SchilderZurueck, " +
                "KuecheAb, KuecheAuf, KuecheBau, KuechePausch, " +
                "StraßeA, HausnummerA, PLZA, OrtA, LandA, AufzugA, StockwerkeA, HausTypA, HVZA, LaufmeterA, AussenAufzugA, " +
                "StraßeB, HausnummerB, PLZB, OrtB, LandB, AufzugB, StockwerkeB, HausTypB, HVZB, LaufmeterB, AussenAufzugB, " +
                "NotizBuero, NotizFahrer, BemerkungTitel, SchilderZeit, UserChanged, Erstelldatum) VALUES (";

            longInsert += idKunden + ", ";
            longInsert += "'" + Program.DateMachine(datBesichtigung) + "', ";
            longInsert += "'" + Program.DateMachine(datUmzug) + "', ";
            longInsert += "'" + Program.DateMachine(datRuempeln) + "', ";
            longInsert += "'" + Program.DateMachine(datEinraeumen) + "', ";
            longInsert += "'" + Program.DateMachine(datAusraeumen) + "', ";
            longInsert += "'" + Program.ZeitMachine(zeitUmzug) + "', ";

            longInsert +=  statBesichtigung+ ", ";
            longInsert +=  statUmzug + ", ";
            longInsert += statAus + ", ";
            longInsert += statEin + ", ";
            longInsert += statRuempeln + ", ";
            longInsert += umzugsdauer + ", ";

            longInsert += "'" + autos +"', ";
            longInsert += mann + ", ";
            longInsert += stunden + ", ";            
            longInsert += versicherung + ", ";
            

            longInsert += einpacken + ", ";
            longInsert += einpacker + ", ";
            longInsert += einStunden + ", ";
            longInsert += karton + ", ";
            longInsert += auspacken + ", ";
            longInsert += auspacker + ", ";
            longInsert += ausStunden + ", ";
            longInsert += kleiderkartons + ", ";
            longInsert += schilder + ", ";

            longInsert += kuecheAuf + ", ";
            longInsert += kuecheAb + ", ";
            longInsert += kuecheBau + ", ";
            longInsert += kuechePausch + ", ";

            longInsert += "'" + auszug.Straße1 + "', ";
            longInsert += "'" + auszug.Hausnummer1 + "', ";
            longInsert += "'" + auszug.PLZ1 + "', ";
            longInsert += "'" + auszug.Ort1 + "', ";
            longInsert += "'" + auszug.Land1 + "', ";
            longInsert += auszug.Aufzug1 + ", ";            
            longInsert += "'" + auszug.Stockwerke1 + "', ";
            longInsert += "'" + auszug.Haustyp1 + "', ";
            longInsert += auszug.HVZ1 + ", ";
            longInsert += auszug.Laufmeter1 + ", ";
            longInsert += auszug.AussenAufzug1 + ", ";

            longInsert += "'" + einzug.Straße1 + "', ";
            longInsert += "'" + einzug.Hausnummer1 + "', ";
            longInsert += "'" + einzug.PLZ1 + "', ";
            longInsert += "'" + einzug.Ort1 + "', ";
            longInsert += "'" + einzug.Land1 + "', ";
            longInsert += einzug.Aufzug1 + ", ";
            longInsert += "'" + einzug.Stockwerke1 + "', ";
            longInsert += "'" + einzug.Haustyp1 + "', ";
            longInsert += einzug.HVZ1 + ", ";
            longInsert += einzug.Laufmeter1 + ", ";
            longInsert += einzug.AussenAufzug1 + ", ";

            longInsert += "'" + notizBuero + "', ";
            longInsert += "'" + notizFahrer + "', ";
            longInsert += "'" + notizTitel + "', ";
            longInsert += "'" + Program.DateMachine(schilderZeit) + "', ";
            longInsert += "'" + userChanged+ "', ";
            longInsert += "'" + Program.DateMachine(DateTime.Now) + "');";

            // Merkt den Query
            Program.QueryLog(longInsert);
            
            Program.absender(longInsert, "Einfügen des neuen Umzuges zum erstellen des Umzugsobjekts");
            }



        // Ausgabemethoden


        //Updatemechanik
        public void UpdateDB (string idUser)
        {

            String longInsert = "UPDATE Umzuege SET ";
            
            longInsert += "Kunden_idKunden = " + idKunden + ", ";
            longInsert += "StatBes = " + statBesichtigung + ", ";
            longInsert += "StatUmz = " + statUmzug + ", ";
            longInsert += "StatAus = " + statAus + ", ";
            longInsert += "StatEin = " + statEin + ", ";
            longInsert += "StatEnt = " + statRuempeln + ", ";
            longInsert += "Mann = " + mann + ", ";
            longInsert += "Stunden = " + stunden + ", ";
            longInsert += "Versicherung = " + versicherung + ", ";
            longInsert += "Einpacken = " + Einpacken + ", ";
            longInsert += "EinPackerZahl = " + Einpacker + ", ";
            longInsert += "EinPackStunden = " + EinStunden + ", ";
            longInsert += "Kartons = " + Karton + ", ";
            longInsert += "Auspacken = " + Auspacken + ", ";
            longInsert += "AusPackerZahl = " + Auspacker + ", ";
            longInsert += "AusPackStunden = " + AusStunden + ", ";
            longInsert += "Kleiderkisten = " + Kleiderkartons + ", ";
            longInsert += "SchilderZurueck = " + Schilder + ", ";
            longInsert += "KuecheAb = " + KuecheAb + ", ";
            longInsert += "KuecheAuf = " + KuecheAuf + ", ";
            longInsert += "KuecheBau = " + KuecheBau + ", ";
            longInsert += "KuechePausch = " + KuechePausch + ", ";

            longInsert += "datBesichtigung = '" + Program.DateMachine(datBesichtigung.Date) + "', ";
            longInsert += "datUmzug = '" + Program.DateMachine(datUmzug.Date) + "', ";
            longInsert += "datRuempelung = '" + Program.DateMachine(datRuempeln.Date) + "', ";
            longInsert += "datEinpacken = '" + Program.DateMachine(datEinraeumen.Date) + "', ";
            longInsert += "datAuspacken = '" + Program.DateMachine(datAusraeumen.Date) + "', ";
            longInsert += "umzugsZeit = '" + Program.ZeitMachine(zeitUmzug.Date) + "', ";
            longInsert += "SchilderZeit = '" + Program.DateMachine(SchilderZeit.Date) + "', ";

            longInsert += "Autos = '" + autos + "', ";
            longInsert += "NotizBuero = '" + NotizBuero + "', ";
            longInsert += "NotizFahrer = '" + NotizFahrer + "', ";
            longInsert += "BemerkungTitel = '" + NotizTitel + "', ";
            longInsert += "UserChanged = '" + UserChanged + idUser + "', "; 
            
            longInsert += "StraßeA = '" + auszug.Straße1  + "', ";
            longInsert += "HausnummerA = '" + auszug.Hausnummer1 + "', ";
            longInsert += "PLZA = '" + auszug.PLZ1 + "', ";
            longInsert += "OrtA = '" + auszug.Ort1 + "', ";
            longInsert += "LandA = '" + auszug.Land1 + "', ";
            longInsert += "AufzugA = " + auszug.Aufzug1 + ", ";    
            longInsert += "StockwerkeA = '" + auszug.Stockwerke1 + "', ";
            longInsert += "HausTypA = '" + auszug.Haustyp1 + "', ";
            longInsert += "HVZA = " + auszug.HVZ1 + ", ";        
            longInsert += "LaufmeterA = " + auszug.Laufmeter1+ ", ";  
            longInsert += "AussenAufzugA = " + auszug.AussenAufzug1 + ", ";

            longInsert += "StraßeB = '" + einzug.Straße1 + "', ";
            longInsert += "HausnummerB = '" + einzug.Hausnummer1 + "', ";
            longInsert += "PLZB = '" + einzug.PLZ1 + "', ";
            longInsert += "OrtB = '" + einzug.Ort1 + "', ";
            longInsert += "LandB = '" + einzug.Land1 + "', ";
            longInsert += "AufzugB = " + einzug.Aufzug1 + ", ";
            longInsert += "StockwerkeB = '" + einzug.Stockwerke1 + "', ";
            longInsert += "HausTypB = '" + einzug.Haustyp1 + "', ";
            longInsert += "HVZB = " + einzug.HVZ1 + ", ";
            longInsert += "LaufmeterB = " + einzug.Laufmeter1 + ", ";
            longInsert += "AussenAufzugB = " + einzug.AussenAufzug1;

            longInsert += " WHERE idUmzuege = " + id + ";";

            Program.QueryLog(longInsert);

            Program.absender(longInsert, "Absenden der Änderung am Umzug");

        }
    }
}
