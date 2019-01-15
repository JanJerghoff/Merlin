using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Google.Apis.Calendar.v3.Data;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Kartonagen.CalendarAPIUtil;
using System.Data;

namespace Kartonagen
{
    static class Program
    {
        //Flag für angemeldeter Mitarbeiter
        //private int angemeldeterMitarbeiter = -1;

        //Singleton Calendar-Handler
        private static CalendarAPIUtil.Util ut;

        // Singleton-Autocomplete-Listen
        private static AutoCompleteStringCollection autocompleteKunden;
        private static AutoCompleteStringCollection autocompleteMitarbeiter;

        // Singleton Lookup-Dictionaries

        private static Dictionary<int, String> dictionaryKunden;
        private static Dictionary<String, int> dictionaryMitarbeiter;

        // PDF-Druckvorbereitung / Datenspeicher
        public static string druckPfad = System.IO.Path.Combine(Environment.CurrentDirectory, "temp2.pdf");
        public static string fehlerPfad = System.IO.Path.Combine(Environment.CurrentDirectory, "fehler"+(DateTime.Now.ToShortDateString().Replace("/","_"))+".txt");
        public static string kalenderLog = System.IO.Path.Combine(Environment.CurrentDirectory, "kalenderLog.txt");
        public static string QueryPfad = System.IO.Path.Combine(Environment.CurrentDirectory, "query" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt");
        public static string mitnehmPfad = System.IO.Path.Combine(Environment.CurrentDirectory, "Mitnehmordner");
        // Buero-geänderte-version

        // Deployment
        //internal static MySqlConnection conn = new MySqlConnection("server = 192.168.2.102;user=root;database=UmzuegeNeu;port=3306;password=he62okv;");
        //internal static MySqlConnection conn2 = new MySqlConnection("server = 192.168.2.102;user=root;database=Mitarbeiter;port=3306;password=he62okv;");

        //internal static MySqlConnection connRita = new MySqlConnection("server = 192.168.2.102;user=Rita;database=UmzuegeNeu;port=3306;password=RitaLucy!;");
        //internal static MySqlConnection connJonas = new MySqlConnection("server = 192.168.2.102;user=Jonas;database=UmzuegeNeu;port=3306;password=JonasLucy!;");
        //internal static MySqlConnection connVorne = new MySqlConnection("server = 192.168.2.102;user=Vorne;database=UmzuegeNeu;port=3306;password=VorneLucy!;");

        //Test Home
        //// private static String connUmzug = "server = 10.0.0.0;user=test;database=Umzuege;port=3306;password=he62okv;";
        //internal static MySqlConnection conn = new MySqlConnection("server = 10.0.0.0;user=test;database=Umzuege;port=3306;password=he62okv;");
        //internal static MySqlConnection conn2 = new MySqlConnection("server = 10.0.0.0;user=test;database=Mitarbeiter;port=3306;password=he62okv;");

        //internal static MySqlConnection conn = new MySqlConnection("server = 10.0.0.0;user=test;database=UmzuegeNeu;port=3306;password=he62okv;Convert Zero Datetime=True;");
        //internal static MySqlConnection conn2 = new MySqlConnection("server = 10.0.0.0;user=test;database=Mitarbeiter;port=3306;password=he62okv;");

        //Test Arbeit
        internal static MySqlConnection conn = new MySqlConnection("server = 192.168.2.102;user=root;database=DB_test;port=3306;password=he62okv;");
        internal static MySqlConnection conn2 = new MySqlConnection("server = 192.168.2.102;user=root;database=Mitarbeiter;port=3306;password=he62okv;");


        // -------------- Methoden ---------------

        //Bestimmen der Pfadwurzel und finden des Ablageverzeichnisses
        public static string getMitnehmPfad() {

            string Temp = mitnehmPfad;

            Temp = System.IO.Path.GetPathRoot(mitnehmPfad);
            Temp = System.IO.Path.Combine(Temp, "TabletPDF");

            return Temp;
        }

        public static Util getUtil () {
            if (ut == null)
            {
                ut = new CalendarAPIUtil.Util();                
            }
            return ut;
        } 

        public static AutoCompleteStringCollection getAutocompleteKunden() {

            if (autocompleteKunden == null) {
                refreshAutocompleteKunden();
            }

            return autocompleteKunden;
        }

        public static void refreshAutocompleteKunden() {

            autocompleteKunden = new AutoCompleteStringCollection();
            dictionaryKunden = new Dictionary<int,string>();
                       

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdRead = new MySqlCommand("SELECT Nachname, idKunden FROM Kunden", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    autocompleteKunden.Add(rdr[0].ToString());
                    // dictionaryKunden.Add(rdr.GetInt32(1),rdr.GetString(0));
                }
                rdr.Close();
                cmdRead.Dispose();
                conn.Close();
            }
            catch (Exception sqlEx)
            {
                
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Kundennamen \r\n Bereits dokumentiert.");
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
            dictionaryMitarbeiter = new Dictionary<string, int>();

            //Abfrage aller Mitarbeiternamen
            MySqlCommand cmdMitarbeiter = new MySqlCommand("SELECT Nachname, Vorname, idMitarbeiter FROM Mitarbeiter", Program.conn2);
            MySqlDataReader rdrMitarbeiter;
            try
            {
                rdrMitarbeiter = cmdMitarbeiter.ExecuteReader();
                while (rdrMitarbeiter.Read())
                {
                    autocompleteMitarbeiter.Add(rdrMitarbeiter[0].ToString() + ", " + rdrMitarbeiter[1].ToString());
                    dictionaryMitarbeiter.Add((rdrMitarbeiter[0].ToString() + ", " + rdrMitarbeiter[1].ToString()), rdrMitarbeiter.GetInt32(2));
                }
                rdrMitarbeiter.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Mitarbeiternamen \r\n Bereits dokumentiert.");
            }
        }

        public static void ordnerLeeren() {

            string [] temp = Directory.GetFiles(getMitnehmPfad());
            foreach (var item in temp)
            {
                File.Delete(item);
            }

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


        //Killt alle Controls, rekursiv runter
        public static void DisableControls(Control con)
        {
            foreach (Control c in con.Controls)
            {
                DisableControls(c);
            }
            con.Enabled = false;
        }

        // Erlaubt alle Controls, von der jetzigen aufwärts
        public static void EnableSingleControl(Control con)
        {
            if (con != null)
            {
                con.Enabled = true;
                EnableSingleControl(con.Parent);
            }
        }

        //Erlaubt alle, Rekursiv runter
        public static void EnableControls(Control con)
        {
            foreach (Control c in con.Controls)
            {
                EnableControls(c);
            }
            con.Enabled = true;
        }

        // Löscht rekursiv alle Text, Numeric, Checkbox und Radiobuttons
        public static void clearControl(Control con) {

            foreach (Control c in con.Controls)
            {
                clearControl(c);
            }

            if (con is TextBox) {
                con.ResetText();
            }
            else if (con is RadioButton)
            {
                RadioButton cont = (RadioButton) con;
                cont.Checked = false;
            }
            else if (con is CheckBox)
            {
                CheckBox cont = (CheckBox)con;
                cont.Checked = false;
            }
            else if (con is DateTimePicker)
            {
                DateTimePicker cont = (DateTimePicker)con;
                cont.Value = DateTime.Now;
            }

        }

        public static String getBearbeitender(int ID)
        {
            if (ID == 0)
            {
                return "Rita";
            }
            if (ID == 1)
            {
                return "Jonas";
            }
            if (ID == 2)
            {
                return "Eva";
            }
            if (ID == 3)
            {
                return "Jan";
            }
            if (ID == 4)
            {
                return "Nora";
            }
            return "fehler";
        }

        // Schubst daten in die DB, mit begründung falls fehler
        public static void absender(String befehl, string Aufgabe)
        {
            Program.QueryLog(befehl);

            if (Program.conn.State != System.Data.ConnectionState.Open)
            {
                Program.reconnect();
            }


            MySqlCommand cmdAdd = new MySqlCommand(befehl, Program.conn);
            try
            {
                cmdAdd.ExecuteNonQuery();
                cmdAdd.Dispose();
                conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Speichern in die DB \r\n "+Aufgabe+" \r\n Bereits dokumentiert.");
            }


        }

        private static void reconnect()
        {
            conn.Close();
            conn.Open();

            //conn2.Close();
            //conn2.Open();
        }

        public static int getMitarbeiterNr(String Mitarbeitername)
        {

            int ret = 0;

            if (dictionaryMitarbeiter == null)
            {
                refreshAutocompleteMitarbeiter();
            }

            dictionaryMitarbeiter.TryGetValue(Mitarbeitername, out ret);

            return ret;

        }


        // Stellt einen check zum Überprüfen von Kundendaten zur Verfügung
        public static Boolean kontaktCheck(String tele, String handy, string email) {

            if (tele != "0" || handy != "0" || email != "x@y.z") {
                return false;
            }

            return true;
        }

        public static int intparser (string toParse) {

            string temp = null;

            try
            {
                Regex regexObj = new Regex(@"[^\d]");
                temp = regexObj.Replace(toParse, "");
            }
            catch (ArgumentException ex) {
                FehlerLog(ex.ToString(), "Regex zum auslesen von Zahlen aus PDFs");
            }

            int ret = int.Parse(temp);
            return ret;

        } 

        // Stellt eine DateTime -> SQL-String Umwandlung bereit
        // C# gibt ohne führende Null, Sql will yyyy-mm-dd mit führenden nullen
        public static String DateMachine(DateTime input) {
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

        // Datetime -> String der vergleichbar ist mit Google DateTime

        public static String DateMachineGoogle(DateTime input)
        {
            String x = String.Empty;

            String dd = input.Day.ToString();
            if (dd.Length == 1)
            {
                dd = "0" + dd;
            }
            x += dd;

            String mm = input.Month.ToString();
            if (mm.Length == 1)
            {
                mm = "0" + mm;
            }
            x += "-" + mm;

            x += "-"+input.Year.ToString();

            return x;
        }

        // Umwandlung für Zeitwerte in Stunden und Minuten
        public static String ZeitMachine(DateTime input)
        {
            String Uhrzeit = "";
            if (input.Hour <= 9)
            {
                Uhrzeit += "0" + input.Hour + ":";
            }
            else { Uhrzeit += input.Hour+":"; }

            if (input.Minute <= 9) {
                Uhrzeit += "0" + input.Minute + ":";
            }
            else { Uhrzeit += input.Minute + ":"; }

            if (input.Second <= 9)
            {
                Uhrzeit += "0" + input.Second;
            }
            else { Uhrzeit += input.Second; }

            return Uhrzeit;
        }
        
        // Umwandlung eines SQL-Datums-Strings in Datetime

        public static DateTime MachineDate(String sql) {
            String yyyy = sql[0] + sql[1] + sql[2] + sql[3]+"";
            String mm = sql[5] + sql[6]+ "" ;
            String dd = sql[8] + sql[9] + "";
            int x;
            int y;
            int z;
            bool year = int.TryParse(yyyy, out x);
            bool month = int.TryParse(yyyy, out y);
            bool day = int.TryParse(yyyy, out z);

            DateTime temp = new DateTime(x,y,z);
            return temp;
        }

        public static DateTime MachineTime(String sql) {
            
            string[] spli = sql.Split(':');   
            
            int a = int.Parse(spli[0]);
            int b = int.Parse(spli[1]);
            int c = int.Parse(spli[2]);

            DateTime ret = new DateTime(2000, 1, 1, a, b, c);
            return ret;
        }

        public static DateTime MachineDateTime(String sql) {

            String yyyy = sql[0] + sql[1] + sql[2] + sql[3] + "";
            String mm = sql[5] + sql[6] + "";
            String dd = sql[8] + sql[9] + "";
            int x;
            int y;
            int z;
            bool year = int.TryParse(yyyy, out x);
            bool month = int.TryParse(yyyy, out y);
            bool day = int.TryParse(yyyy, out z);

            string[] split = sql.Split(' ');
            string[] spli = split[1].Split(':');

            int a = int.Parse(spli[0]);
            int b = int.Parse(spli[1]);
            int c = int.Parse(spli[2]);

            DateTime temp = new DateTime(x, y, z,a,b,c);
            return temp;
        }




        // Return Match und ID für das löschen von Events
        public static String EventListMatch(Events e, DateTime date, String color) {                     

            String ID = "";
            String datum = DateMachine(date);
            String datumG = DateMachineGoogle(date);
            
            // Unterscheidung zwischen Besichtigungen und Ganztägigen

            if (color=="9")
            {
                foreach (var EventItem in e.Items)
                {
                   if (EventItem.ColorId == "9") {
                        //if (EventItem.Start.DateTime.ToString().Remove(10).Replace('.', '-') == datumG){
                            ID = EventItem.Id;
                        //}
                    }
                }
            }
            else
            {
                foreach (var EventItem in e.Items)
                {
                    if (EventItem.ColorId == color && EventItem.Start.Date == datum)
                    {
                        ID = EventItem.Id;
                    }
                }
            }
            return ID;
        }
        // Überladung für Schilder stellen
        public static String EventListMatch(Events e, DateTime date, String color, String tag)
        {


            String ID = "";
            String datum = DateMachine(date);
            String datumG = DateMachineGoogle(date);
            
            foreach (var EventItem in e.Items)
            {
                if (EventItem.ColorId == color && EventItem.Start.Date == datum && EventItem.Location == tag)
                {
                    ID = EventItem.Id;
                }
            }            
            return ID;
        }
        

        public static String AdresseErsaetzen(int IDKunde, String Strasse, String Hausnummer, String Ort, int PLZ, String Land) {

            // String basteln
            String Update = "UPDATE Kunden SET ";
            Update += "Straße = '" + Strasse + "', ";
            Update += "Hausnummer = '" + Hausnummer + "', ";
            Update += "Ort = '" + Ort + "', ";
            Update += "Land = '" + Land + "', ";
            Update += "PLZ = " + PLZ.ToString() + " ";

            Update += "WHERE idKunden = " + IDKunde.ToString() + ";";

            // String fertig, absenden
            MySqlCommand cmdAdd = new MySqlCommand(Update, Program.conn);
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

        //Debug-Methode
        public static void showPDF() {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = druckPfad;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();
        }

        public static void SendToPrinter()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = druckPfad;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();
            Console.WriteLine("Start des wartens auf Drucker");
            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(5000);
            if (false == p.CloseMainWindow())
                p.Kill();
        }

        public static void FehlerLog(string Fehlermeldung, string Kurzbeschreibung) {
            
            if (!File.Exists(fehlerPfad))
            {                
                // Datei erstellen
                using (StreamWriter sw = File.CreateText(fehlerPfad))
                {
                    sw.WriteLine("");
                    sw.WriteLine("Start");
                    sw.WriteLine("");
                }
            }
            

            // Eintrag machen
            using (StreamWriter sw = File.AppendText(fehlerPfad))
            {
                sw.WriteLine(Kurzbeschreibung +" "+ DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                sw.WriteLine("");
                sw.WriteLine(Fehlermeldung);
                sw.WriteLine("");
            }
                
            var box2 = MessageBox.Show(Kurzbeschreibung, "Fehler");
            
        }

        public static void KalenderLog(string Kalendereintrag)
        {

            if (!File.Exists(kalenderLog))
            {
                // Datei erstellen
                using (StreamWriter sw = File.CreateText(kalenderLog))
                {
                    sw.WriteLine("");
                    sw.WriteLine("Start");
                    sw.WriteLine("");
                }
            }

            // Eintrag machen
            using (StreamWriter sw = File.AppendText(kalenderLog))
            {
                sw.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " : ");
                sw.WriteLine(Kalendereintrag);
                sw.WriteLine("");
            }            
        }

        public static void QueryLog(string Query)
        {

            if (!File.Exists(QueryPfad))
            {
                // Datei erstellen
                using (StreamWriter sw = File.CreateText(QueryPfad))
                {
                    sw.WriteLine("");
                    sw.WriteLine("Start");
                    sw.WriteLine("");
                }
            }

            // Eintrag machen
            using (StreamWriter sw = File.AppendText(QueryPfad))
            {
                sw.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                sw.WriteLine(Query);
                sw.WriteLine("");
            }
            
        }

        //public static MySqlConnection getConnection (int bearbeitender) {

        //    MySqlConnection ret = new MySqlConnection("server = 192.168.2.102;user=root;database=UmzuegeNeu;port=3306;password=he62okv;");

        //    switch (bearbeitender)
        //    {
        //        case 0:
        //            ret = connRita;
        //            break;

        //        case 1:
        //            ret = connJonas;
        //            break;

        //        case 4:
        //            ret = connVorne;
        //            break;

        //        default:
        //            break;
        //    }

        //    return ret;
        //}




        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CalendarAPIUtil.Util.googleInit();
                        
            //Öffne Verbindung
            try
            {
                conn.Open();
                conn2.Open();
            }
            catch (Exception ex)
            {
                Program.FehlerLog(ex.ToString(), "Konnte keine Datenbankverbindung aufbauen \r\n Bereits dokumentiert.");
            }
            
            // Öffnen des Fensters
            Application.Run(new mainForm());                       
        }
    }
}
