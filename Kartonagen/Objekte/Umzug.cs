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
        public int Id { get => id; }
        public string UserChanged1 { get => UserChanged; set => UserChanged = value; }

        // Konstruktor
        public Umzug(int ID)
        {
            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Umzuege WHERE idUmzuege = " + ID + ";", Program.conn);
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

                    zeitUmzug = Program.MachineTime(rdr[7].ToString());

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

            umzugsKunde = new Kunde(idKunden);
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
            longInsert += "'" + Program.DateMachine(DateTime.Now) + "');";
                        

            // Merkt den Query
            Program.QueryLog(longInsert);

            Program.absender(longInsert, "Einfügen des neuen Umzuges zum erstellen des Umzugsobjekts");
        }



        // Ausgabemethoden


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

        }

        // Druck, Parameter für Ausdruck (temp2, toggle = 1) oder mitnahme (t=2)
        public void druck(int toggle)
        {
            
            // Unschön, bessere Lösung?
            PdfDocument pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Besichtigungs Vordruck.pdf")), new PdfWriter(Program.druckPfad));

            if (toggle == 1)
            {
                pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Besichtigungs Vordruck.pdf")), new PdfWriter(Program.druckPfad));
            }
            else if (toggle == 2)
            {
                string dateiName = id + "_" + einzug.Straße1 + "_" + einzug.Hausnummer1;
                string mitnehmPfad = System.IO.Path.Combine(Program.mitnehmPfad, dateiName);
                pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Besichtigungs Vordruck.pdf")), new PdfWriter(mitnehmPfad));
            }

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;

            String Name = ""; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.
            switch (UserChanged[0])
            {
                case '0':
                    Name = "Rita";
                    break;

                case '1':
                    Name = "Jonas";
                    break;

                case '2':
                    Name = "Eva";
                    break;

                case '3':
                    Name = "Jan";
                    break;

                default:
                    break;
            }

            // Vergleihstermin
            DateTime stand = new DateTime(2017, 1, 1);

            try
            {
                fields.TryGetValue("NameKundennummer", out toSet);
                toSet.SetValue(idKunden + " " + umzugsKunde.Nachname);

                // Telefonnummern sauber auflösen
                if (umzugsKunde.Telefon != "0" && umzugsKunde.Handy != "0")
                {
                    fields.TryGetValue("Telefonnummer", out toSet);
                    toSet.SetValue(umzugsKunde.Telefon + " / " + umzugsKunde.Handy);
                }
                else if (umzugsKunde.Telefon == "0" && umzugsKunde.Handy != "0")
                {
                    fields.TryGetValue("Telefonnummer", out toSet);
                    toSet.SetValue(umzugsKunde.Telefon);
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
                    toSet.SetValue(DatBesichtigung.ToShortDateString());
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
                    toSet.SetValue(auszug.Laufmeter1.ToString());
                }

                if (auszug.HVZ1 == 1)
                {
                    fields.TryGetValue("HVZAJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioHVZAV.Checked)
                //{
                //    fields.TryGetValue("HVZAVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
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
                toSet.SetValue(umzObj.einzug.KalenderStringEtageHaustyp());

                if (textLaufMeterB.Text != "0")
                {
                    fields.TryGetValue("TragwegB", out toSet);
                    toSet.SetValue(textLaufMeterB.Text);
                }

                if (radioHVZBJa.Checked)
                {
                    fields.TryGetValue("HVZBJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioHVZBV.Checked)
                //{
                //    fields.TryGetValue("HVZBVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioHVZBNein.Checked)
                {
                    fields.TryGetValue("HVZBNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (radioAufzugBJa.Checked)
                {
                    fields.TryGetValue("AufzugBJa", out toSet);
                    toSet.SetValue("X");
                }
                if (radioAufzugBNein.Checked)
                {
                    fields.TryGetValue("AufzugBNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (radioAussenAufzugBJa.Checked)
                {
                    fields.TryGetValue("AussenAufzugBJa", out toSet);
                    toSet.SetValue("X");
                }
                if (radioAussenAufzugBNein.Checked)
                {
                    fields.TryGetValue("AussenAufzugBNein", out toSet);
                    toSet.SetValue("X");
                }

                // Packen

                //
                if (radioEinpackenJa.Checked)
                {
                    fields.TryGetValue("EinJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioEinpackenV.Checked)
                //{
                //    fields.TryGetValue("EinVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioEinpackenNein.Checked)
                {
                    fields.TryGetValue("EinNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (radioAuspackenJa.Checked)
                {
                    fields.TryGetValue("AusJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioAuspackenV.Checked)
                //{
                //    fields.TryGetValue("AusVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioAuspackenNein.Checked)
                {
                    fields.TryGetValue("AusNein", out toSet);
                    toSet.SetValue("X");
                }

                //Küche
                if (radioKuecheAbJa.Checked)
                {
                    fields.TryGetValue("KuecheAbJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioKuecheAbV.Checked)
                //{
                //    fields.TryGetValue("KuecheAbVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioKuecheAbNein.Checked)
                {
                    fields.TryGetValue("KuecheAbNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (radioKuecheAufJa.Checked)
                {
                    fields.TryGetValue("KuecheAufJa", out toSet);
                    toSet.SetValue("X");
                }
                //if (radioKuecheAufV.Checked)
                //{
                //    fields.TryGetValue("KuecheAufVllt", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioKuecheAufNein.Checked)
                {
                    fields.TryGetValue("KuecheAufNein", out toSet);
                    toSet.SetValue("X");
                }
                //
                //if (radioKuecheExtern.Checked)
                //{
                //    fields.TryGetValue("KuecheExtern", out toSet);
                //    toSet.SetValue("Yes");
                //}
                if (radioKuecheIntern.Checked)
                {
                    fields.TryGetValue("KuecheIntern", out toSet);
                    toSet.SetValue("X");
                }
                //
                if (textKuechenPreis.Text != "0")
                {
                    fields.TryGetValue("KuechePreis", out toSet);
                    toSet.SetValue(textKuechenPreis.Text);
                }

                // Restdaten
                if (numericMannZahl.Value != 0)
                {
                    fields.TryGetValue("Mann", out toSet);
                    toSet.SetValue(numericMannZahl.Value.ToString());
                }

                if (numericArbeitszeit.Value != 0)
                {
                    fields.TryGetValue("Stunden", out toSet);
                    toSet.SetValue(numericArbeitszeit.Value.ToString());
                }

                fields.TryGetValue("Autos", out toSet);
                toSet.SetValue(AutoString());

                if (numericKleiderkisten.Value != 0)
                {
                    fields.TryGetValue("Kleiderkisten", out toSet);
                    toSet.SetValue(numericKleiderkisten.Value.ToString());
                }

                //Bemerkungen
                fields.TryGetValue("NoteBuero", out toSet);
                toSet.SetValue(textNoteBuero.Text);

                fields.TryGetValue("NoteFahrer", out toSet);
                toSet.SetValue(textNoteFahrer.Text);

            }

            catch (Exception ex)
            {
                Program.FehlerLog(ex.ToString(), "Drucken des Umzugs " + id);
            }
        }

    }
}