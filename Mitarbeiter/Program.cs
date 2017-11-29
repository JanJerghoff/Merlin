using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mitarbeiter
{
    static class Program
    {

        // Singleton-Autocomplete-Listen
        private static AutoCompleteStringCollection autocompleteKunden;
        private static AutoCompleteStringCollection autocompleteMitarbeiter;
        private static AutoCompleteStringCollection autocompleteTour;
        private static AutoCompleteStringCollection autocompleteFahrzeug;
        // Vorhalte-Listen für schnellen Lookup, Singletons

        private static Dictionary<String, int> Tour;
        private static Dictionary<String, int> Mitarbeiter;
        private static Dictionary<String, int> Fahrzeuge;

        // Google vorbereitungen

        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API";
        static CalendarService dienst;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Google-Auth

            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = System.IO.Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            dienst = service;

            // Datenbank

            try
            {
                conn.Open();
                conn2.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.Application.Exit();
            }

            //Füllen der Singletons (nicht optimal, aber einfacher)
            getFahrzeug("");
            getTour("");
            getMitarbeiter("");

            // Bei Programmstart Stundenkonto aktualisieren

            Sollminute();
            StundenkontoUpdate();

            MonatsCheck();      //Fehlen für Mitarbeiter die passenden Stundenkontos (weil wir im neuen Merlin-Monat sind?)

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Start());

           
            
        }

        // Buero-geänderte-version

        // Deployment
        //internal static MySqlConnection conn = new MySqlConnection("server = 192.168.2.102;user=root;database=Umzuege;port=3306;password=he62okv;");
        //internal static MySqlConnection conn2 = new MySqlConnection("server = 192.168.2.102;user=root;database=Mitarbeiter;port=3306;password=he62okv;");

        //Test
        internal static MySqlConnection conn = new MySqlConnection("server = 10.0.0.0;user=test;database=Umzuege;port=3306;password=he62okv;");
        internal static MySqlConnection conn2 = new MySqlConnection("server = 10.0.0.0;user=test;database=Mitarbeiter;port=3306;password=he62okv;");

        // Aufbewahrung für Sollminuten pro Mitarbeiter
        internal static Dictionary <int, int> Sollminuten = new Dictionary<int, int>();

        // Getter  Resetter für Singletopn Autocomplete

        public static AutoCompleteStringCollection getAutocompleteKunden()
        {

            if (autocompleteKunden == null)
            {
                refreshAutocompleteKunden();
            }

            return autocompleteKunden;
        }

        public static void refreshAutocompleteKunden()
        {

            autocompleteKunden = new AutoCompleteStringCollection();

            //Abfrage aller Namen
            MySqlCommand cmdRead = new MySqlCommand("SELECT Nachname FROM Kunden", Program.conn);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    autocompleteKunden.Add(rdr[0].ToString());
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                return;
            }
        }

        public static AutoCompleteStringCollection getAutocompleteMitarbeiter()
        {

            if (autocompleteMitarbeiter == null)
            {
                refreshAutocompleteMitarbeiter();
            }

            return autocompleteMitarbeiter;
        }

        public static void refreshAutocompleteMitarbeiter()
        {

            autocompleteMitarbeiter = new AutoCompleteStringCollection();

            //Abfrage aller Mitarbeiternamen
            MySqlCommand cmdMitarbeiter = new MySqlCommand("SELECT Nachname, Vorname FROM Mitarbeiter", Program.conn2);
            MySqlDataReader rdrMitarbeiter;
            try
            {
                rdrMitarbeiter = cmdMitarbeiter.ExecuteReader();
                while (rdrMitarbeiter.Read())
                {
                    autocompleteMitarbeiter.Add(rdrMitarbeiter[0].ToString() + ", " + rdrMitarbeiter[1].ToString());
                }
                rdrMitarbeiter.Close();
            }
            catch (Exception sqlEx)
            {
                return;
            }
        }

        public static AutoCompleteStringCollection getAutocompleteFahrzeug()
        {

            if (autocompleteKunden == null)
            {
                refreshAutocompleteFahrzeug();
            }

            return autocompleteFahrzeug;
        }

        public static void refreshAutocompleteFahrzeug()
        {

            autocompleteFahrzeug = new AutoCompleteStringCollection();

            //Abfrage aller Fahrzeuge
            MySqlCommand cmdFahrzeug = new MySqlCommand("SELECT Name FROM Fahrzeug", Program.conn2);
            MySqlDataReader rdrFahrzeug;
            try
            {
                rdrFahrzeug = cmdFahrzeug.ExecuteReader();
                while (rdrFahrzeug.Read())
                {
                    autocompleteFahrzeug.Add(rdrFahrzeug[0].ToString());
                }
                rdrFahrzeug.Close();
            }
            catch (Exception sqlEx)
            {
                return;
            }
        }

        public static AutoCompleteStringCollection getAutocompleteTour()
        {

            if (autocompleteTour == null)
            {
                refreshAutocompleteTour();
            }

            return autocompleteTour;
        }

        public static void refreshAutocompleteTour()
        {

            autocompleteTour = new AutoCompleteStringCollection();

            //Abfrage aller Tourennamen
            MySqlCommand cmdTour = new MySqlCommand("SELECT Name FROM Tour WHERE TYPE >= 0 AND TYPE <=3;", Program.conn2); // Zulässige Touren finden / definieren
            MySqlDataReader rdrTour;
            try
            {
                rdrTour = cmdTour.ExecuteReader();
                while (rdrTour.Read())
                {
                    autocompleteTour.Add(rdrTour[0].ToString());
                }
                rdrTour.Close();
            }
            catch (Exception sqlEx)
            {
                return;
            }
        }

        //Arbeitstage in Zeitspanne, inklusive Starttag und Endtag
        public static int ArbeitsTage(DateTime start, DateTime end) {

            int calc = 0;

            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Sunday)
                    calc++;
            }

            return calc;
        }

        // Hol die MitarbeiterID - Sollminuten in den Speicher
        public static Dictionary<int,int> Sollminute() {

            Sollminuten = new Dictionary<int, int>();

            MySqlCommand cmdMitarbeiter = new MySqlCommand("SELECT idMitarbeiter, StundenanteilMinuten FROM Mitarbeiter WHERE Ausscheidedatum = '2017-01-01' OR Ausscheidedatum > "+DateMachine(DateTime.Now.Date) +";", Program.conn2);
            MySqlDataReader rdrMitarbeiter;
            try
            {
                rdrMitarbeiter = cmdMitarbeiter.ExecuteReader();
                while (rdrMitarbeiter.Read())
                {
                    Sollminuten.Add(rdrMitarbeiter.GetInt32(0), rdrMitarbeiter.GetInt32(1));
                }
                rdrMitarbeiter.Close();
            }
            catch (Exception sqlEx)
            {

            }

            return Sollminuten;
        }

        public static void MonatsCheck() {

            DateTime comp = DateTime.Now;
            DateTime start = new DateTime(comp.Year, comp.Month - 1, 27);
            DateTime end = new DateTime(comp.Year, comp.Month, 26);

            int Tage = Program.ArbeitsTage(start, end);

            Dictionary<int, int> MitarbeiterAktiv = Sollminute();

            List<int> aktiveKontos = new List<int>(); // Liste aller Kontos für diesen Monat, wenn identisch zu aktiven Mitarbeitern, alles gut

            MySqlCommand cmdMitarbeiter = new MySqlCommand("SELECT Mitarbeiter_idMitarbeiter FROM Stundenkonto WHERE Monat = "+ DateMachine(getMonat(DateTime.Now)) +";", Program.conn2);
            MySqlDataReader rdrMitarbeiter;
            try
            {
                rdrMitarbeiter = cmdMitarbeiter.ExecuteReader();
                while (rdrMitarbeiter.Read())
                {
                    aktiveKontos.Add(rdrMitarbeiter.GetInt32(0));
                }
                rdrMitarbeiter.Close();
            }
            catch (Exception sqlEx)
            {

            }

            if (aktiveKontos.Count != MitarbeiterAktiv.Count) { // Diskrepanz zwischen aktiven Mitarbeitern und Stundenkonten für den aktuellen Monat

                List<int> TempList = new List<int>();

                foreach (var item in MitarbeiterAktiv.Keys)                 // Temp-Liste von fehlenden Stundenkonten
                {
                    if (!aktiveKontos.Contains(item))
                    {
                        TempList.Add(item);
                    }
                }

                foreach (var item in TempList)          //  Temp list abarbeiten, Stundenkonten aktualisieren
                {
                    Mitarbeiter temp = new Mitarbeiter(item);
                    temp.StundenkontoAktualisieren(Tage);
                }

            }

        }

        // Stellt eine DateTime -> SQL-String Umwandlung bereit
        // C# gibt ohne führende Null, Sql will yyyy-mm-dd mit führenden nullen
        public static String DateMachine(DateTime input)
        {
            String x = String.Empty;

            x += input.Year.ToString();
            String mm = input.Month.ToString();
            if (mm.Length == 1)
            {
                mm = "0" + mm;
            }
            x += "-" + mm;

            String dd = input.Day.ToString();
            if (dd.Length == 1)
            {
                dd = "0" + dd;
            }
            x += "-" + dd;
            return x;
        }

        public static String DateTimeMachine(DateTime input, DateTime datum)
        {
            // Datumsteil
            String x = String.Empty;

            x += datum.Year.ToString();

            String mm = datum.Month.ToString();
            if (mm.Length == 1)
            {
                mm = "0" + mm;
            }
            x += "-" + mm;

            String dd = datum.Day.ToString();
            if (dd.Length == 1)
            {
                dd = "0" + dd;
            }
            x += "-" + dd;

            // Zeitteil

            x += " ";

            String hh = input.Hour.ToString();
            if (hh.Length == 1)
            {
                hh = "0" + hh;
            }
            x += hh + ":";

            String min = input.Minute.ToString();
            if (min.Length == 1)
            {
                min = "0" + min;
            }
            x += min + ":00";
            // Sekunden immer auf 0 gesetzt

            return x;
        }

        // Daten für Merlin-Monate
        static DateTime endDez = new DateTime(1999, 12, 27);
        static DateTime endJan = new DateTime(2000, 1, 27);
        static DateTime endFeb = new DateTime(2000, 2, 27);
        static DateTime endMaer = new DateTime(2000, 3, 27);
        static DateTime endApr = new DateTime(2000, 4, 27);
        static DateTime endMai = new DateTime(2000, 5, 27);
        static DateTime endJun = new DateTime(2000, 6, 27);
        static DateTime endJul = new DateTime(2000, 7, 27);
        static DateTime endAug = new DateTime(2000, 8, 27);
        static DateTime endSep = new DateTime(2000, 9, 27);
        static DateTime endOkt = new DateTime(2000, 10, 27);
        static DateTime endNov = new DateTime(2000, 11, 27);

        // Merlin -Monate auf normale Monate münzen
        public static DateTime getMonat(DateTime x)
        {
            if (x.Day >= 27 && x.Month != 12)
            {
                DateTime temp = new DateTime(x.Year, x.Month + 1, 1);
                return temp;
            }
            else if (x.Day >= 27 && x.Month == 12)
            {
                DateTime temp = new DateTime(x.Year + 1, 1, 1);
                return temp;
            }
            else
            {
                DateTime temp = new DateTime(x.Year, x.Month, 1);
                return temp;
            }
        }

        // Stundenbruchteile in Minuten umrechnen
        public static int toMinute(decimal Stunden)
        {
            decimal minuten = 60;
            return (decimal.ToInt32(Stunden * minuten));
        }


        public static int MonatsDifferenz(DateTime alt, DateTime neu)
        {
            return Math.Abs(neu.Month - alt.Month) + 12 * Math.Abs(neu.Year - alt.Year);
        }

        // Gibt die gearbeiteten Minuten aus
        public static int ArbeitsZeitBlock(DateTime start, DateTime ende, int Pause)
        {
            TimeSpan PauseT = new TimeSpan(0, Pause, 0);

            return (int)Math.Floor(((ende - start) - PauseT).TotalMinutes);

        }

        public static int getFahrzeug(String Name)
        {
            int ret;

            if (Fahrzeuge == null) {
                Fahrzeuge = new Dictionary<String, int>();

                //Abfrage aller Fahrzeuge
                MySqlCommand cmdFahrzeug = new MySqlCommand("SELECT idFahrzeug, Name FROM Fahrzeug;", Program.conn2);
                MySqlDataReader rdrFahrzeug;
                try
                {
                    rdrFahrzeug = cmdFahrzeug.ExecuteReader();
                    while (rdrFahrzeug.Read())
                    {
                        Fahrzeuge.Add(rdrFahrzeug.GetString(1), rdrFahrzeug.GetInt32(0));
                    }
                    rdrFahrzeug.Close();
                }
                catch (Exception sqlEx)
                {
                    //throw sqlEx;
                }
            }

            Fahrzeuge.TryGetValue(Name, out ret);
            return ret;            
        }

        public static String getFahrzeugName(int ID) {

            MySqlCommand cmdFahrzeug = new MySqlCommand("SELECT Name FROM Fahrzeug WHERE idFahrzeug = " + ID + ";", Program.conn2);
            MySqlDataReader rdrFahrzeug;
            String ret = "";
            try
            {
                rdrFahrzeug = cmdFahrzeug.ExecuteReader();
                while (rdrFahrzeug.Read())
                {
                    ret =(rdrFahrzeug[0].ToString());
                }
                rdrFahrzeug.Close();
                return ret;
            }
            catch (Exception sqlEx)
            {
                return "x";
            }
            return "-";
        }

        public static int getTour(String Name)
        {

            int ret;

            if (Tour == null)
            {
                Tour = new Dictionary<String, int>();

                //Abfrage aller Tourennamen
                MySqlCommand cmdTour = new MySqlCommand("SELECT idTour, Name FROM Tour;", Program.conn2); // Zulässige Touren finden / definieren
                MySqlDataReader rdrTour;
                try
                {
                    rdrTour = cmdTour.ExecuteReader();
                    while (rdrTour.Read())
                    {
                        Tour.Add(rdrTour.GetString(1), rdrTour.GetInt32(0));
                    }
                    rdrTour.Close();
                }
                catch (Exception sqlEx)
                {
                    throw sqlEx;
                }
            }

            Tour.TryGetValue(Name, out ret);
            return ret;
        }

        public static int getTourCode(int ID)
        {

            MySqlCommand cmdTour = new MySqlCommand("SELECT Type FROM Tour WHERE idTour = " + ID + ";", Program.conn2);
            MySqlDataReader rdrTour;
            int nummer = -1; // -1 als Fehlercode
            try
            {
                rdrTour = cmdTour.ExecuteReader();
                while (rdrTour.Read())
                {
                    nummer = rdrTour.GetInt32(0);
                }
                rdrTour.Close();
            }
            catch (Exception sqlEx)
            {

            }
            return nummer;
        }

        public static int getTourCode(String Name)
        {

            MySqlCommand cmdTour = new MySqlCommand("SELECT Type FROM Tour WHERE Name = '" + Name + "';", Program.conn2);
            MySqlDataReader rdrTour;
            int nummer = -1; // -1 als Fehlercode
            try
            {
                rdrTour = cmdTour.ExecuteReader();
                while (rdrTour.Read())
                {
                    nummer = rdrTour.GetInt32(0);
                }
                rdrTour.Close();
            }
            catch (Exception sqlEx)
            {

            }
            return nummer;
        }

        public static String getTourName(int ID) {
            MySqlCommand cmdTour = new MySqlCommand("SELECT Name FROM Tour WHERE idTour = " + ID + ";", Program.conn2);
            MySqlDataReader rdrTour;
            String ret = "";
            try
            {
                rdrTour = cmdTour.ExecuteReader();
                while (rdrTour.Read())
                {
                    ret = (rdrTour.GetString(0));
                }
                rdrTour.Close();
                return ret;
            }
            catch (Exception sqlEx)
            {

            }
            return "-";

        }

        public static int getMitarbeiter(String Name)
        {

            int ret;

            if (Mitarbeiter == null)
            {
                Mitarbeiter = new Dictionary<String, int>();

                //Abfrage aller Mitarbeiternamen
                MySqlCommand cmdMitarbeiter = new MySqlCommand("SELECT idMitarbeiter, Nachname, Vorname FROM Mitarbeiter", Program.conn2);
                MySqlDataReader rdrMitarbeiter;
                try
                {
                    rdrMitarbeiter = cmdMitarbeiter.ExecuteReader();
                    while (rdrMitarbeiter.Read())
                    {
                        Mitarbeiter.Add((rdrMitarbeiter[1].ToString() + ", " + rdrMitarbeiter[2].ToString()), rdrMitarbeiter.GetInt32(0));
                    }
                    rdrMitarbeiter.Close();
                }
                catch (Exception sqlEx)
                {
                    throw sqlEx;
                }
            }

            Mitarbeiter.TryGetValue(Name, out ret);
            return ret;            
        }

        public static String getMitarbeiterName(int ID) {

            MySqlCommand cmdMitarbeiter = new MySqlCommand("SELECT Nachname, Vorname FROM Mitarbeiter WHERE idMitarbeiter = "+ID+";", Program.conn2);
            MySqlDataReader rdrMitarbeiter;
            String ret = "";
            try
            {
                rdrMitarbeiter = cmdMitarbeiter.ExecuteReader();
                while (rdrMitarbeiter.Read())
                {
                   ret = (rdrMitarbeiter[0].ToString() + ", " + rdrMitarbeiter[1].ToString());
                }
                rdrMitarbeiter.Close();
                return ret;
            }
            catch (Exception sqlEx)
            {
                return sqlEx.ToString();
            }
            return "-";
        }

        public static bool validMitarbeiter(String Name) {

            if (Mitarbeiter.ContainsKey(Name))
            {
                return true;
            }
            else { return false; }
        }

        public static bool validTour(String Name)
        {

            if (Tour.ContainsKey(Name))
            {
                return true;
            }
            else { return false; }
        }

        public static bool validFahrzeug(String Name)
        {

            if (Fahrzeuge.ContainsKey(Name))
            {
                return true;
            }
            else { return false; }
        }


        // Beschafft Urlaubstage zu einem Mitarbeiter am Datum
        public static int getUrlaub(int ID, DateTime datum) {
            DateTime vollMonate = new DateTime();
            int count = 0;
            // Ganze Monate
            DateTime abrunden = getMonat(DateTime.Now);
            

            MySqlCommand cmd = new MySqlCommand("SELECT Einstellungsdatum FROM Mitarbeiter WHERE idMitarbeiter = " + ID + ";", Program.conn2);
            MySqlDataReader rdr;
            try
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {   
                    // vor dem 15 gibts 2, danach 1 Tag Urlaub.
                    if (rdr.GetDateTime(0).Day < 15 || rdr.GetDateTime(0).Day >= 27)
                    {
                        count++;
                        vollMonate = new DateTime(rdr.GetDateTime(0).Year, rdr.GetDateTime(0).Month + 1, 1);
                    }
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {

            }

            return count;
        }

        public static Boolean KMCheck(int IDTour, int km) {

            MySqlCommand cmdTour = new MySqlCommand("SELECT LaengeKM FROM Tour WHERE idTour = " + IDTour + ";", Program.conn2);
            MySqlDataReader rdrTour;
            Boolean res = false;
            int kmTour = 0;
            try
            {
                rdrTour = cmdTour.ExecuteReader();
                while (rdrTour.Read())
                {
                    kmTour = rdrTour.GetInt32(0);
                }
                rdrTour.Close();
            }
            catch (Exception sqlEx)
            {

            }

            if (kmTour != 0 && (Math.Abs(kmTour - km)) > kmTour / 10)
            {
                return true;
            }
            else {
                return false;
            } 
        }

        // Berechne Wochentage in einer Zeitspanne, benötigt legitime Start / Enddaten (End > Start)
        public static int getWochentage(DateTime start, DateTime end) {
            int count = 0;
            TimeSpan laenge = end - start;

            //Identität
            if (start.Date == end.Date) {
                if (((int)start.DayOfWeek) != 0) { return 1; } else { return 0;}
            }

            for (int i = 0; i <= laenge.TotalDays; i++)
            {
                if (((int)start.Date.AddDays(i).DayOfWeek) != 0) {
                    count++;
                }
            }
            return count;

        }

        public static String FahrzeugUpdate(int ID, int Stand) {

            int StandAlt = 0;
            // Datenbank-Stand abholen
            MySqlCommand cmdFahrzeug = new MySqlCommand("SELECT Zaehlerstand FROM Fahrzeug WHERE idFahrzeug = " + ID + ";", Program.conn2);
            MySqlDataReader rdrFahrzeug;
            try
            {
                rdrFahrzeug = cmdFahrzeug.ExecuteReader();
                while (rdrFahrzeug.Read())
                {
                    StandAlt = rdrFahrzeug.GetInt32(0);
                }
                rdrFahrzeug.Close();
            }
            catch (Exception sqlEx)
            {
                return sqlEx.ToString();
            }

            //Update auf höheren Zählerstand
            if (Stand > StandAlt) {
                String up = "UPDATE Fahrzeug SET Zaehlerstand = " + Stand + " WHERE idFahrzeug= " + ID + ";";
                // String fertig, absenden
                MySqlCommand cmdAdd = new MySqlCommand(up, Program.conn2);
                try
                {
                    cmdAdd.ExecuteNonQuery();
                }
                catch (Exception sqlEx)
                {
                    return sqlEx.ToString();
                }
            }
            return "";
        }

        public static void StundenkontoUpdate() {

            // Iteration über aktive Mitarbeiter
            MySqlDataReader rdrStundenkonto;
            String insert = "";
            MySqlCommand cmdupdate = new MySqlCommand(insert, Program.conn2);          
            MySqlCommand cmdStundenkonto = new MySqlCommand("';", Program.conn2);
            List<int> updates = new List<int>();

            foreach (var pair in Sollminuten) {
                

                // Finden des letzten Stundenkonto-Objekts für diesen Mitarbeiter
                cmdStundenkonto = new MySqlCommand("Select * FROM Stundenkonto WHERE Mitarbeiter_idMitarbeiter = "+pair.Key+" ORDER BY Monat DESC Limit = 1;", Program.conn2);
                try
                {
                    rdrStundenkonto = cmdStundenkonto.ExecuteReader();
                    while (rdrStundenkonto.Read())
                    {
                        // Ziehe (normalisierten) Monat aus der DB, vergleich mit (normalisiertem) Datum von Heute
                        if ((rdrStundenkonto.GetDateTime(3).Date.CompareTo(getMonat(DateTime.Now.Date))) != 0) {
                            updates.Add(pair.Key);
                        }
                    }
                    rdrStundenkonto.Close();
                }
                catch (Exception sqlEx)
                {

                }
            }

            // Liste mit nötigen updates Abarbeiten
            foreach (var obj in updates) {
                int min = 0;
                Sollminuten.TryGetValue(obj, out min);
                // Anweisung vorbereiten
                insert = "INSERT INTO Stundenkonto (Mitarbeiter_idMitarbeiter, Monat, SollMinuten) VALUES(" + obj + ", '" + DateMachine(getMonat(DateTime.Now.Date)) + "', " + min + ");";
                cmdupdate = new MySqlCommand(insert, Program.conn2);
                // Einfügen
                try
                {
                    cmdupdate.ExecuteNonQuery();
                }
                catch (Exception sqlEx)
                {
                    
                }

            }
        }

        public static String BearbeitendAufloesen(int ID) {

            switch (ID)
            {
                case 0:
                    return "Rita";

                case 1:
                    return "Jonas";

                case 2:
                    return "Eva";

                case 3:
                    return "Jan";

                case 4:
                    return "Nora";

                default:
                    break;
            }

            return "";

        }

        // Schubst daten in die DB, mit begründung falls fehler
        public static void absender(String befehl, string Aufgabe)
        {
            MySqlCommand cmdAdd = new MySqlCommand(befehl, Program.conn2);
            try
            {
                cmdAdd.ExecuteNonQuery();
            }
            catch (Exception sqlEx)
            {
                //Program.FehlerLog(sqlEx.ToString(), "Fehler beim Speichern in die DB \r\n " + Aufgabe + " \r\n Bereits dokumentiert.");
                var bestätigung = MessageBox.Show(sqlEx.ToString(), "Erinnerung", MessageBoxButtons.YesNo); //TEST
            }
        }

        // Finde alle Einträge zu einem Tag
        public static Events kalenderKundenFinder(DateTime Date)
        {

            // Define parameters of request.
            EventsResource.ListRequest request = dienst.Events.List("primary");
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 2500;
            request.TimeMin = new DateTime(Date.Year,Date.Month,Date.Day,1,0,0);
            request.TimeMax = new DateTime(Date.Year, Date.Month, Date.Day, 23, 59, 59);
            // List events.
            Events events = request.Execute();
            Events temp = new Events();

            return events;
        }

        // Umzug nicht zulässig
        // Push einer Fahrt (über Namen), return "" Erfolgreich, sonst Fehlermeldung
        public static String pushFahrt(String Mitarbeiter, String Tour, String Fahrzeug, DateTime Datum, DateTime Start, DateTime End, int Pause, int KMStart, int KMEnde, int Kunden, int Stueck, int Handbeilagen, String Bemerkung, int idBearbeitend) {

            int Typ = -1;
            int TourNr = -1;
            int MitarbeiterNr = -1;
            int FahrzeugNr = -1;

            TourNr = getTour(Tour);

            
            FahrzeugNr = getFahrzeug(Fahrzeug);         

            MitarbeiterNr = getMitarbeiter(Mitarbeiter);
            Typ = getTourCode(Tour);

            DateTime Anfang = DateTime.Now;
            DateTime Ende = DateTime.Now;

            // Daten vorbereiten
            if (Start.TimeOfDay.CompareTo(End.TimeOfDay) > 0)
            {
                // Über Mitternacht
                Anfang = new DateTime(Datum.Year, Datum.Month, Datum.Day, Start.Hour, Start.Minute, 0);
                Ende = new DateTime(Datum.Year, Datum.Month, Datum.Day + 1, End.Hour, End.Minute, 0);
            }
            else if (Start.TimeOfDay.CompareTo(End.TimeOfDay) < 0)
            {
                // Selber Tag
                Anfang = new DateTime(Datum.Year, Datum.Month, Datum.Day, Start.Hour, Start.Minute, 0);
                Ende = new DateTime(Datum.Year, Datum.Month, Datum.Day, End.Hour, End.Minute, 0);
            }
            else { return "Start und Endzeit dürfen nicht identisch sein"; }


            // Legitimitätschecks
            if (TourNr == -1) { return "Tour ist ungültig"; }
            if (FahrzeugNr == -1 && Fahrzeug != "") { return "Fahrzeug ist ungültig"; }
            if (MitarbeiterNr == -1) { return "Mitarbeiter ist ungültig"; } 
            if (Typ == -1) { return "Typ der Tour kann nicht gefunden werden"; }
            if (Typ == 0) { return "Umzüge sind nicht zulässig"; }
            if (Fahrzeug == "" && (KMStart != 0 || KMEnde != 0)) {return "Wenn Beifahrer dann dürfen keine Kilometer gegeben sein, wenn kein Beifahrer fehlt das Fahrzeug"; }
            if (KMStart > KMEnde) { return "Endkilometer müssen größer sein als Startkilometer"; }
            if ((Ende-Anfang).TotalMinutes < Pause) { return "Die Pause darf nicht länger sein als die Arbeitszeit"; }

            // Stringbau
            String insert = "";

            if (Fahrzeug == "")
            {
                insert += "INSERT INTO Fahrt (Mitarbeiter_idMitarbeiter, Start, Ende, Pause, AnfangsKM, EndKM, Bemerkung, UserChanged, Kunden, Stückzahl, Beilagen, Tour_idTour) VALUES (";

                insert += MitarbeiterNr + ", ";
                insert += "'" + DateTimeMachine(Anfang, Anfang) + "', ";
                insert += "'" + DateTimeMachine(Ende, Ende) + "', ";
                insert += Pause + ", ";
                insert += KMStart + ", ";
                insert += KMEnde + ", ";
                insert += "'" + Bemerkung + "', ";
                insert += idBearbeitend + ", ";
                insert += Kunden + ", ";
                insert += Stueck + ", ";
                insert += Handbeilagen + ", ";
                insert += TourNr + ");";
            }
            else
            {
                insert += "INSERT INTO Fahrt (Mitarbeiter_idMitarbeiter, Start, Ende, Pause, AnfangsKM, EndKM, Bemerkung, UserChanged, Kunden, Stückzahl, Beilagen, Tour_idTour, Fahrzeug_idFahrzeug) VALUES (";

                insert += MitarbeiterNr + ", ";
                insert += "'" + DateTimeMachine(Anfang, Anfang) + "', ";
                insert += "'" + DateTimeMachine(Ende, Ende) + "', ";
                insert += Pause + ", ";
                insert += KMStart + ", ";
                insert += KMEnde + ", ";
                insert += "'" + Bemerkung + "', ";
                insert += idBearbeitend + ", ";
                insert += Kunden + ", ";
                insert += Stueck + ", ";
                insert += Handbeilagen + ", ";
                insert += TourNr + ", ";
                insert += FahrzeugNr + ");";
            }
            // String fertig, absenden
            MySqlCommand cmdAdd = new MySqlCommand(insert, Program.conn2);
            try
            {
                cmdAdd.ExecuteNonQuery();
                return "";

            }
            catch (Exception sqlEx)
            {
                return sqlEx.ToString(); 
            }
            
        }
    }

}
