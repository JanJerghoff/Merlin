using Google.Apis.Calendar.v3.Data;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using Kartonagen.Objekte;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        Kunde umzugsKunde;

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

        //Rümpeldaten
        int RuempelMann;
        int RuempelStunden;

        // Adressobjekte
        public Adresse auszug;
        public Adresse einzug;
        public Adresse entruempeln;

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
        int lfd_nr;

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
        public int Id { get => id; }
        public string UserChanged1 { get => UserChanged; set => UserChanged = value; }
        public int RuempelMann1 { get => RuempelMann; set => RuempelMann = value; }
        public int RuempelStunden1 { get => RuempelStunden; set => RuempelStunden = value; }

        // Konstruktoren

        //Bestehenden Umzug über UmzNr abrufen
        public Umzug(int ID)
        {
            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Umzuege WHERE idUmzuege = " + ID + ";", Program.conn);
            MySqlDataReader rdr;
            int AdresseRuempel = 0;

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

                    zeitUmzug = Program.MachineTime(rdr[7].ToString());

                    statBesichtigung = rdr.GetInt32(8);
                    statUmzug = rdr.GetInt32(9);
                    statRuempeln = rdr.GetInt32(10);
                    statEin = rdr.GetInt32(11);
                    statAus = rdr.GetInt32(12);

                    umzugsdauer = rdr.GetInt32(13);

                    //Kerndaten
                    autos = rdr.GetString(14);       
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


                    if (rdr.GetInt32(62) < UserChanged.Length)
                    {    //Wenn Laufnummer geringer ist als sie sollte, hochsetzen
                        lfd_nr = UserChanged.Length+1;
                    }
                    else
                    {
                        lfd_nr = rdr.GetInt32(62);
                    }

                    AdresseRuempel = rdr.GetInt32(59);
                    RuempelMann1 = rdr.GetInt32(60);
                    RuempelStunden1 = rdr.GetInt32(61);

                }
                rdr.Close();

                entruempeln = new Adresse(AdresseRuempel);
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der Umzugsdaten zur Objekterstellung");
                throw sqlEx;
            }

            umzugsKunde = new Kunde(idKunden);
        }

        // Neuen Umzug in die DB anlegen
        public Umzug(int idKunden, DateTime datBesichtigung, DateTime datUmzug, DateTime datEinraeumen, DateTime datAusraeumen, DateTime datRuempeln, DateTime zeitUmzug, int statBesichtigung, int statUmzug, int statAus, int statEin, int statRuempeln,
            int umzugsdauer, string autos, int mann, int stunden, int versicherung, int einpacken, int einpacker, int einStunden, int karton, int auspacken, int auspacker, int ausStunden, int kleiderkartons, int kuecheAuf, int kuecheAb, int kuecheBau, 
            int kuechePausch, Adresse auszug, Adresse einzug, int schilder, DateTime schilderZeit, string notizTitel, string notizBuero, string notizFahrer, string userChanged, DateTime erstelldatum, Adresse ruempeladresse, int RuempelMann, int RuempelStunden)

        {

            int ruempelNr = 0;

            if (ruempeladresse != null) {
                ruempeladresse.saveNew();
                ruempelNr = ruempeladresse.findAdresse();
            }
            

            String longInsert = "INSERT INTO Umzuege (Kunden_idKunden, datBesichtigung, datUmzug, datRuempelung, datEinpacken, datAuspacken, umzugsZeit, " +
                "StatBes, StatUmz, StatAus, StatEin, StatEnt, Umzugsdauer, Autos, Mann, Stunden, Versicherung, " +
                "Einpacken, EinPackerZahl, EinPackStunden, Kartons, Auspacken, AusPackerZahl, AusPackStunden, Kleiderkisten, SchilderZurueck, " +
                "KuecheAb, KuecheAuf, KuecheBau, KuechePausch, " +
                "StraßeA, HausnummerA, PLZA, OrtA, LandA, AufzugA, StockwerkeA, HausTypA, HVZA, LaufmeterA, AussenAufzugA, " +
                "StraßeB, HausnummerB, PLZB, OrtB, LandB, AufzugB, StockwerkeB, HausTypB, HVZB, LaufmeterB, AussenAufzugB, " +
                "NotizBuero, NotizFahrer, BemerkungTitel, SchilderZeit, UserChanged, Erstelldatum, Adresse_id, entruempelMann, entruempelStunden) VALUES (";

            longInsert += idKunden + ", ";
            longInsert += "'" + Program.DateMachine(datBesichtigung) + "', ";
            longInsert += "'" + Program.DateMachine(datUmzug) + "', ";
            longInsert += "'" + Program.DateMachine(datRuempeln) + "', ";
            longInsert += "'" + Program.DateMachine(datEinraeumen) + "', ";
            longInsert += "'" + Program.DateMachine(datAusraeumen) + "', ";
            longInsert += "'" + Program.ZeitMachine(zeitUmzug) + "', ";

            longInsert += statBesichtigung + ", ";
            longInsert += statUmzug + ", ";
            longInsert += statAus + ", ";
            longInsert += statEin + ", ";
            longInsert += statRuempeln + ", ";
            longInsert += umzugsdauer + ", ";

            longInsert += "'" + autos + "', ";
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
            longInsert += "'" + userChanged + "', ";
            longInsert += "'" + Program.DateMachine(DateTime.Now) + "', ";
            longInsert += ruempelNr + ", ";
            longInsert += RuempelMann + ", ";
            longInsert += RuempelStunden + ");";

            // Merkt den Query
            Program.QueryLog(longInsert);

            Program.absender(longInsert, "Einfügen des neuen Umzuges zum erstellen des Umzugsobjekts");

            RefreshAll();
        }

        // Ausgabemethoden
        private String AutoString()
        {
            

            String temp = "";

            if (autos.Length != 4) {
                return autos;
            }

            if (autos[0] != '0')
            {
                temp = temp + autos[0].ToString() + " Sprinter Mit, ";
            }
            if (autos[1] != '0')
            {
                temp = temp + autos[1].ToString() + " Sprinter Ohne, ";
            }
            if (autos[2] != '0')
            {
                temp = temp + autos[2].ToString() + " LKW, ";
            }
            if (autos[3] != '0')
            {
                temp = temp + autos[3].ToString() + " 12-Tonner";
            }

            return temp;
        }

        public void increaseLfdNr()
        {
            lfd_nr++;
            // In DB updaten
            String Insert = "UPDATE Umzuege SET lfd_nr = " + lfd_nr + " WHERE idUmzuege = "+id+";";
            Program.QueryLog(Insert);

            Program.absender(Insert, "Update der Laufenden Nummer");
        }

        //Updatemechanik
        public void UpdateDB(string idUser)
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
            longInsert += "umzugsZeit = '" + Program.ZeitMachine(zeitUmzug) + "', ";
            longInsert += "SchilderZeit = '" + Program.DateMachine(SchilderZeit.Date) + "', ";

            longInsert += "Autos = '" + autos + "', ";
            longInsert += "NotizBuero = '" + NotizBuero + "', ";
            longInsert += "NotizFahrer = '" + NotizFahrer + "', ";
            longInsert += "BemerkungTitel = '" + NotizTitel + "', ";
            longInsert += "UserChanged = '" + UserChanged + idUser + "', ";

            longInsert += "StraßeA = '" + auszug.Straße1 + "', ";
            longInsert += "HausnummerA = '" + auszug.Hausnummer1 + "', ";
            longInsert += "PLZA = '" + auszug.PLZ1 + "', ";
            longInsert += "OrtA = '" + auszug.Ort1 + "', ";
            longInsert += "LandA = '" + auszug.Land1 + "', ";
            longInsert += "AufzugA = " + auszug.Aufzug1 + ", ";
            longInsert += "StockwerkeA = '" + auszug.Stockwerke1 + "', ";
            longInsert += "HausTypA = '" + auszug.Haustyp1 + "', ";
            longInsert += "HVZA = " + auszug.HVZ1 + ", ";
            longInsert += "LaufmeterA = " + auszug.Laufmeter1 + ", ";
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

            //Ändern der seperaten Adressen
            entruempeln.updateDB();

            UserChanged = UserChanged + idUser;

        }

        
        // Druck, Parameter für Ausdruck (temp2, toggle = 1) oder mitnahme (t=2)
        public string druck(int toggle)
        {
            
            PdfDocument pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Besichtigungs Vordruck.pdf")));

            if (toggle == 1)
            {
                pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Besichtigungs Vordruck.pdf")), new PdfWriter(Program.druckPfad));
            }
            else if (toggle == 2)
            {
                string dateiName = id + "_" + einzug.Straße1 + "_" + einzug.Hausnummer1+".pdf";
                string mitnehmPfad = System.IO.Path.Combine(Program.getMitnehmPfad(), dateiName);
                pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Besichtigungs Vordruck.pdf")), new PdfWriter(mitnehmPfad));
            }

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;

            String Name = ""; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.
            string temp = char.ToString(UserChanged[0]);
            int tempO = int.Parse(temp);
            Name = Program.getBearbeitender(tempO);
            
            // Vergleihstermin
            DateTime stand = new DateTime(2017, 1, 1);

            try
            {
                fields.TryGetValue("NameKundennummer", out toSet);
                toSet.SetValue(idKunden + " " + umzugsKunde.Anrede + " " + umzugsKunde.Nachname);

                // Telefonnummern sauber auflösen
                if (umzugsKunde.Telefon != "0" && umzugsKunde.Handy != "0")
                {
                    fields.TryGetValue("Telefonnummer", out toSet);
                    toSet.SetValue(umzugsKunde.Telefon + " / " + umzugsKunde.Handy);
                }
                else if (umzugsKunde.Telefon == "0" && umzugsKunde.Handy != "0")
                {
                    fields.TryGetValue("Telefonnummer", out toSet);
                    toSet.SetValue(umzugsKunde.Handy);
                }
                else if (umzugsKunde.Telefon != "0" && umzugsKunde.Handy == "0")
                {
                    fields.TryGetValue("Telefonnummer", out toSet);
                    toSet.SetValue(umzugsKunde.Telefon);
                }
                else
                {
                    fields.TryGetValue("Telefonnummer", out toSet);
                    toSet.SetValue("Keine Nummer!");
                }

                fields.TryGetValue("Email", out toSet);
                toSet.SetValue(umzugsKunde.Email);

                fields.TryGetValue("DatumZeichen", out toSet);
                toSet.SetValue(DateTime.Now.Date.ToShortDateString() + " " + Name);

                if (DatBesichtigung.ToShortDateString() != stand.ToShortDateString())
                {
                    fields.TryGetValue("TerminBes", out toSet);
                    toSet.SetValue(DatBesichtigung.ToShortDateString()+ " "+ zeitUmzug.ToShortTimeString());
                }

                if (datUmzug.ToShortDateString() != stand.ToShortDateString())
                {
                    fields.TryGetValue("TerminUmz", out toSet);
                    toSet.SetValue(datUmzug.ToShortDateString());
                }

                if (datRuempeln.ToShortDateString() != stand.ToShortDateString())
                {
                    fields.TryGetValue("TerminEnt", out toSet);
                    toSet.SetValue(datRuempeln.ToShortDateString());
                }

                fields.TryGetValue("Umzugsnummer", out toSet);
                toSet.SetValue(id.ToString());

                // Auszugsadresse
                fields.TryGetValue("StrasseA", out toSet);
                toSet.SetValue(auszug.Straße1 + " " + auszug.Hausnummer1);

                fields.TryGetValue("OrtA", out toSet);
                toSet.SetValue(auszug.PLZ1 + " " + auszug.Ort1);

                //Geschossigkeit

                fields.TryGetValue("StockwerkA", out toSet);
                toSet.SetValue(auszug.KalenderStringEtageHaustyp());

                if (auszug.Laufmeter1.ToString() != "0")
                {
                    fields.TryGetValue("TragwegA", out toSet);
                    toSet.SetValue(auszug.Laufmeter1.ToString()+ "Meter");
                }

                if (auszug.HVZ1 == 1)
                {
                    fields.TryGetValue("HVZAJa", out toSet);
                    toSet.SetValue("X");
                }
                if (auszug.HVZ1 == 2)
                {
                    fields.TryGetValue("HVZAVllt", out toSet);
                    toSet.SetValue("Yes");
                }
                if (auszug.HVZ1 == 0)
                {
                    fields.TryGetValue("HVZANein", out toSet);
                    toSet.SetValue("X");
                }

                if (auszug.Aufzug1 == 1)
                {
                    fields.TryGetValue("AufzugAJa", out toSet);
                    toSet.SetValue("X");
                }

                if (auszug.Aufzug1 == 0)
                {
                    fields.TryGetValue("AufzugANein", out toSet);
                    toSet.SetValue("X");
                }


                if (auszug.AussenAufzug1 == 1)
                {
                    fields.TryGetValue("AussenAufzugAJa", out toSet);
                    toSet.SetValue("X");
                }
                if (auszug.AussenAufzug1 == 0)
                {
                    fields.TryGetValue("AussenAufzugANein", out toSet);
                    toSet.SetValue("X");
                }

                // Adresse Einzug
                fields.TryGetValue("StrasseB", out toSet);
                toSet.SetValue(einzug.Straße1 + " " + einzug.Hausnummer1);

                fields.TryGetValue("OrtB", out toSet);
                toSet.SetValue(einzug.PLZ1 + " " + einzug.Ort1);

                //Geschossigkeit

                fields.TryGetValue("StockwerkB", out toSet);
                toSet.SetValue(einzug.KalenderStringEtageHaustyp());

                if (einzug.Laufmeter1.ToString() != "0")
                {
                    fields.TryGetValue("TragwegB", out toSet);
                    toSet.SetValue(einzug.Laufmeter1.ToString()+ "Meter");
                }

                if (einzug.HVZ1 == 1)
                {
                    fields.TryGetValue("HVZBJa", out toSet);
                    toSet.SetValue("X");
                }
                if (einzug.HVZ1 == 2)
                {
                    fields.TryGetValue("HVZBVllt", out toSet);
                    toSet.SetValue("Yes");
                }
                if (einzug.HVZ1 == 0)
                {
                    fields.TryGetValue("HVZBNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (einzug.Aufzug1 == 1)
                {
                    fields.TryGetValue("AufzugBJa", out toSet);
                    toSet.SetValue("X");
                }
                if (einzug.Aufzug1 == 0)
                {
                    fields.TryGetValue("AufzugBNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (einzug.AussenAufzug1 == 1)
                {
                    fields.TryGetValue("AussenAufzugBJa", out toSet);
                    toSet.SetValue("X");
                }
                if (einzug.AussenAufzug1 == 0)
                {
                    fields.TryGetValue("AussenAufzugBNein", out toSet);
                    toSet.SetValue("X");
                }

                // Packen

                //
                if (Einpacken == 1)
                {
                    fields.TryGetValue("EinJa", out toSet);
                    toSet.SetValue("X");
                }
                if (Einpacken == 2)
                {
                    fields.TryGetValue("EinVllt", out toSet);
                    toSet.SetValue("Yes");
                }
                if (Einpacken == 0)
                {
                    fields.TryGetValue("EinNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (Auspacken == 1)
                {
                    fields.TryGetValue("AusJa", out toSet);
                    toSet.SetValue("X");
                }
                if (Auspacken == 2)
                {
                    fields.TryGetValue("AusVllt", out toSet);
                    toSet.SetValue("Yes");
                }
                if (Auspacken == 0)
                {
                    fields.TryGetValue("AusNein", out toSet);
                    toSet.SetValue("X");
                }

                if (Einpacker1 != 0) {
                    fields.TryGetValue("MannEinPacken", out toSet);
                    toSet.SetValue(Einpacker1.ToString());
                }

                if (EinStunden1 != 0)
                {
                    fields.TryGetValue("StundenEinPacken", out toSet);
                    toSet.SetValue(EinStunden1.ToString());
                }

                if (Auspacker1 != 0)
                {
                    fields.TryGetValue("MannAusPacken", out toSet);
                    toSet.SetValue(Auspacker1.ToString());
                }

                if (AusStunden1!= 0)
                {
                    fields.TryGetValue("StundenAusPacken", out toSet);
                    toSet.SetValue(AusStunden1.ToString());
                }

                //Küche
                if (KuecheAb == 1)
                {
                    fields.TryGetValue("KuecheAbJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioKuecheAbV.Checked)
                //{
                //    fields.TryGetValue("KuecheAbVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (KuecheAb == 0)
                {
                    fields.TryGetValue("KuecheAbNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (KuecheAuf == 1)
                {
                    fields.TryGetValue("KuecheAufJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioKuecheAufV.Checked)
                //{
                //    fields.TryGetValue("KuecheAufVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (KuecheAuf == 0)
                {
                    fields.TryGetValue("KuecheAufNein", out toSet);
                    toSet.SetValue("X");
                }

                if (KuecheBau == 0)
                {
                    fields.TryGetValue("KuecheExtern", out toSet);
                    toSet.SetValue("Yes");
                }
                if (KuecheBau == 1)
                {
                    fields.TryGetValue("KuecheIntern", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (KuechePausch.ToString() != "0")
                {
                    fields.TryGetValue("KuechePreis", out toSet);
                    toSet.SetValue(KuechePausch.ToString());
                }

                // Restdaten
                if (mann != 0)
                {
                    fields.TryGetValue("Mann", out toSet);
                    toSet.SetValue(mann.ToString());
                }

                if (stunden != 0)
                {
                    fields.TryGetValue("Stunden", out toSet);
                    toSet.SetValue(stunden.ToString());
                }

                fields.TryGetValue("Autos", out toSet);
                toSet.SetValue(AutoString());

                
                 fields.TryGetValue("Kleiderkisten", out toSet);
                 toSet.SetValue(Kleiderkartons.ToString());
                

                if (versicherung == 0)
                {
                    fields.TryGetValue("VersicherungJa", out toSet);
                    toSet.SetValue("X");
                }

                //Bemerkungen
                fields.TryGetValue("NoteBuero", out toSet);
                toSet.SetValue(NotizTitel1+" "+NotizBuero);

                fields.TryGetValue("NoteFahrer", out toSet);
                toSet.SetValue(NotizFahrer);

            }

            catch (Exception ex)
            {
                Program.FehlerLog(ex.ToString(), "Drucken des Umzugs " + id);
            }

            // Abschließen

            if (toggle == 1)
            {
                form.FlattenFields();
                pdf.Close();
                Program.SendToPrinter();
                return "Erfolgreich gedruckt";
            }
            else {
                pdf.Close();
                return "Abgelegt";
            }
        }

        //
        //Google Calendar funktionen
        //

        //Code-Auflösung für "welchen Termin?"
        private String resolveCode(int code) {

            string ending = "";

            switch (code)
            {
                case 7:
                    ending = "schildaus";
                    break;
                case 6:
                    ending = "schildein";
                    break;
                case 1:
                    ending = "bes";
                    break;
                case 2:
                    ending = "um";
                    break;
                case 3:
                    ending = "ein";
                    break;
                case 4:
                    ending = "aus";
                    break;
                case 5:
                    ending = "ent";
                    break;

                default:
                    break;
            }

            return ending;
        }

        private int resolveUmzugsfarbe() {

            switch (StatUmzug)
            {
                case 1:
                    return 11;
                case 2:
                    return 10;
                case 3:
                    return 2;
                default:
                    return 0;
                    break;
            }

        }

        //Löschen einzelner Termine
        public void kill( int code) {

            Events ev = Program.getUtil().kalenderUmzugFinder("merlinum" + id + "c" + resolveCode(code));
            Console.WriteLine(ev.Items.Count + "gefunden");

            foreach (var item in ev.Items)
            {
                Program.getUtil().kalenderEventRemove(item.Id);
            }
              
        }

        //Löschen aller Termine
        public void killAll() {

            kill(1);
            kill(2);
            kill(3);
            kill(4);
            kill(5);
            kill(6);
            kill(7);
        }

        //Einfügen Eizentermine
        public Boolean addEvent(int code) {

           
            try
            {

                switch (code)
                {
                    case 1:
                        DateTime date = Program.getUtil().mergeDatetime(datBesichtigung, zeitUmzug);
                        DateTime schluss = date.AddMinutes(60);
                        Program.getUtil().kalenderEventEintrag(IdKunden + " " + umzugsKunde.Vorname + " " + umzugsKunde.Nachname, KalenderString(), 9, date, schluss);
                        return true;

                    case 2:
                        Program.getUtil().kalenderEventEintragGanz(UmzHeader(), KalenderString(), hvzString(), resolveUmzugsfarbe(), DatUmzug, DatUmzug.AddDays(umzugsdauer));

                        if (statUmzug == 1 || StatUmzug == 3)

                        {
                            Schilderstellen();
                        }
                        return true;
                         
                    case 3:
                        if (StatEin == 1)
                        {
                            Program.getUtil().kalenderEventEintragGanz(EinRaeumHeader(), KalenderString(), "", 5, datEinraeumen.Date, datEinraeumen.Date);
                            return true;
                        }
                        else if (StatEin == 2)
                        {
                            Program.getUtil().kalenderEventEintragGanz(EinRaeumHeader(), KalenderString(), "", 6, datEinraeumen.Date, datEinraeumen.Date);
                            return true;
                        }
                        return false;

                    case 4:
                        if (StatAus == 1)
                        {
                            Program.getUtil().kalenderEventEintragGanz(AusRaeumHeader(), KalenderString(), "", 5, DatAusraeumen.Date, DatAusraeumen.Date);
                            return true;
                        }
                        else if (StatAus == 2)
                        {
                            Program.getUtil().kalenderEventEintragGanz(AusRaeumHeader(), KalenderString(), "", 6, DatAusraeumen.Date, DatAusraeumen.Date);
                            return true;
                        }
                        return false;

                    case 5:

                        String Header = IdKunden + " "+umzugsKunde.Anrede+" " + umzugsKunde.Vorname + " " + umzugsKunde.Nachname + ", " + Einpacker1 + " Mann, " + EinStunden1 + " Stunden ENTRÜMPELN, "; // TODO Entrümpeldaten erfassen!
                        if (StatRuempeln == 1)
                        {
                            Program.getUtil().kalenderEventEintragGanz(Header, RuempelString(), "", 11, datRuempeln.Date, datRuempeln.Date);

                            return true;
                        }
                        else if (StatRuempeln == 2)
                        {

                            Program.getUtil().kalenderEventEintragGanz(Header, RuempelString(), "", 10, datRuempeln.Date, datRuempeln.Date);

                            return true;
                        }
                        return false;

                    default:
                        
                        Program.FehlerLog("Nix hinzuzufügen".ToString(), "Einfügen des Termins in den Kalender");
                        return false;
                }
            }
            catch (Exception kalenderEx)
            {
                Program.FehlerLog(kalenderEx.ToString(), "Einfügen des Termins in den Kalender");
            }
            return false;
        }

        // Einfügen aller Termine
        public Boolean addAll() {

            increaseLfdNr();

            addEvent(2);
            addEvent(1);
            addEvent(3);
            addEvent(4);
            addEvent(5);
            return true;
        }

        //Kopletter Refresh
        public void RefreshAll() {
            killAll();
            increaseLfdNr();
            addAll();
        }

        //Partielle Refreshs

        //Textgeneration für Kalendereinträge
        private String UmzHeader()
        {
            String sMann = "";
            String sStunden = "";

            if (Mann != 0) { sMann = Mann + " Mann, "; }
            if (Stunden != 0) { sStunden = Stunden + " Stunden, "; }

            return IdKunden + " " + umzugsKunde.Anrede + " " + umzugsKunde.Vorname+" "+umzugsKunde.Nachname + ", " + sMann + sStunden + AutoString() + " " + NotizTitel1;
        }

        private String SchilderHeader()
        {
            return idKunden + " " + umzugsKunde.Anrede + " " +umzugsKunde.Vorname+ " "+umzugsKunde.Nachname+ ", Schilder stellen";
        }

        private String EinRaeumHeader()
        {
            String EinRaeumHeader = idKunden + " " + umzugsKunde.Anrede + " " + umzugsKunde.Vorname + " " + umzugsKunde.Nachname + " Einpacken, " + Einpacker + " Mann, " + EinStunden + " Stunden";

            if (StatEin == 2)
            {
                EinRaeumHeader = EinRaeumHeader + " Optional";
            }

            return EinRaeumHeader;
        }

        private String AusRaeumHeader()
        {
            String AusRaeumHeader = idKunden + " " + umzugsKunde.Anrede + " " + umzugsKunde.Vorname + " " + umzugsKunde.Nachname + "Auspacken, " + Auspacker + " Mann, " + AusStunden + " Stunden";

            if (statAus == 2)
            {
                AusRaeumHeader = AusRaeumHeader + " Optional";
            }

            return AusRaeumHeader;
        }


        private String RuempelString() {

            String body = "Umzugsnummer:" + id + "\r\n" + umzugsKunde.Anrede + " " + umzugsKunde.Vorname + " " + umzugsKunde.Nachname + "\r\n";
            body += "Adresse:  " + entruempeln.Straße1 + " " + entruempeln.Hausnummer1 + ", " + entruempeln.PLZ1 + " " + entruempeln.Ort1 + "\r\n";


            return body;
        }

        private String KalenderString()
        {
            //Konstruktion String Kalerndereintragsinhalt

            // Umzugsnummer + Name + Auszugsadresse
            String Body = "Umzugsnummer:"+id+ "\r\n"  + umzugsKunde.Anrede + " " + umzugsKunde.Vorname + " " + umzugsKunde.Nachname + "\r\n Aus: " + auszug.Straße1 + " " + auszug.Hausnummer1 + ", " + auszug.PLZ1 + " " + auszug.Ort1 + "\r\n";


            // Geschoss + HausTyp
            Body += auszug.KalenderStringEtageHaustyp();

            if (auszug.Aufzug1 == 1)
            {
                Body += "mit Aufzug \r\n";
            }
            else { Body += "ohne Aufzug \r\n"; }

            //Einzugsadresse
            Body += "\r\n Nach: " + einzug.Straße1 + " " + einzug.Hausnummer1 + ", " + einzug.PLZ1 + " " + einzug.Ort1 + "\r\n";

            // Geschoss + HausTyp
            Body += einzug.KalenderStringEtageHaustyp();

            if (einzug.Aufzug1 == 1)
            {
                Body += "mit Aufzug \r\n";
            }
            else { Body += "ohne Aufzug \r\n"; }

            // Kontaktdaten
            if (umzugsKunde.Telefon != "0")
            {
                Body += "\r\n " + umzugsKunde.Telefon;
            }
            if (umzugsKunde.Handy != "0")
            {
                Body += "\r\n " + umzugsKunde.Handy;
            }
            if (umzugsKunde.Email != "x@y.z")
            {
                Body += "\r\n " + umzugsKunde.Email;
            }
            Body += "\r\n Am: " + datUmzug.ToShortDateString() + "\r\n";

            // Büronotiz

            Body += NotizBuero;

            // Rückgabe fertiger Body
            return Body;
        }

        private String hvzString()
        {
            if (auszug.HVZ1 == 1 && einzug.HVZ1==1)
            {
                return "2 X HVZ";
            }
            else if (auszug.HVZ1 == 1 || einzug.HVZ1 == 1)
            {
                return "1 X HVZ";
            }
            else
            {
                return "keine HVZ";
            }
        }

        private void Schilderstellen()
        {
            //Schilder
            if (auszug.HVZ1 == 1)
            {
                string calId = "merlinum" + id + "c" + resolveCode(7) + "i" + UserChanged1.Length;

                String Body = auszug.Straße1 + " " + auszug.Hausnummer1 + ", " + auszug.PLZ1 + " " + auszug.Ort1 + "\r\n gehört zu Umzug-Nr: " + id;

                Program.getUtil().kalenderEventEintragGanz(SchilderHeader(), Body, "Auszug", 3, datUmzug.Date.AddDays(-6), datUmzug.Date.AddDays(-6));
            }

            if (einzug.HVZ1 == 1)
            {
                string calId = "merlinum" + id + "c" + resolveCode(6) + "i" + UserChanged1.Length;

                String Body = einzug.Straße1 + " " + einzug.Hausnummer1 + ", " + einzug.PLZ1 + " " + einzug.Ort1 + "\r\n gehört zu Umzug-Nr: "+id;

                Program.getUtil().kalenderEventEintragGanz(SchilderHeader(), Body, "Einzug", 3, datUmzug.Date.AddDays(-6), datUmzug.Date.AddDays(-6));
            }
        }
        
    }
}