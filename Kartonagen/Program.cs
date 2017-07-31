﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Geom;
using System.Globalization;
using iText.Forms;
using iText.Forms.Fields;
using System.Diagnostics;

namespace Kartonagen
{
    static class Program
    {

        // Singleton-Autocomplete-Listen
        private static AutoCompleteStringCollection autocompleteKunden;
        private static AutoCompleteStringCollection autocompleteMitarbeiter;

        // Singleton Lookup-Dictionaries

        private static Dictionary<String, int> dictionaryKunden;
        private static Dictionary<String, int> dictionaryMitarbeiter;

        // Google vorbereitungen
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API";
        static CalendarService dienst;

        // PDF-Druckvorbereitung
        public static string druckPfad = System.IO.Path.Combine(Environment.CurrentDirectory, "temp2.pdf");


        // Buero-geänderte-version

        // Deployment
        //internal static MySqlConnection conn = new MySqlConnection("server = 192.168.2.102;user=root;database=Umzuege;port=3306;password=he62okv;");
        //internal static MySqlConnection conn2 = new MySqlConnection("server = 192.168.2.102;user=root;database=Mitarbeiter;port=3306;password=he62okv;");

        //Test
        internal static MySqlConnection conn = new MySqlConnection("server = 10.0.0.0;user=test;database=Umzuege;port=3306;password=he62okv;");
        internal static MySqlConnection conn2 = new MySqlConnection("server = 10.0.0.0;user=test;database=Mitarbeiter;port=3306;password=he62okv;");

        // -------------- Methoden ---------------

        public static AutoCompleteStringCollection getAutocompleteKunden() {

            if (autocompleteKunden == null) {
                refreshAutocompleteKunden();
            }

            return autocompleteKunden;
        }

        public static void refreshAutocompleteKunden() {

            autocompleteKunden = new AutoCompleteStringCollection();
            dictionaryKunden = new Dictionary<string, int>();

            //Abfrage aller Namen
            MySqlCommand cmdRead = new MySqlCommand("SELECT Nachname, idKunden FROM Kunden", Program.conn);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    autocompleteKunden.Add(rdr[0].ToString());
                    dictionaryKunden.Add(rdr.GetString(0), rdr.GetInt32(1));
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
                return;
            }
        }

        public static int getKundenNr(String Kundenname) {

            int ret = 0;

            if (dictionaryKunden == null) {
                refreshAutocompleteKunden();
            }

            dictionaryKunden.TryGetValue(Kundenname, out ret);

            return ret;
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

            DateTime temp = new DateTime(x, 02,02);
            return temp;
        }

        // Methode um Kalendereinträge vorzunehmen

        public static String kalenderEintrag(String titel, String text, int Farbe, DateTime Start, DateTime Ende) {

            Event test = new Event()
            {
                Summary = titel,
                Description = text,
                Start = new EventDateTime() {
                    DateTime = Start
                },
                End = new EventDateTime()
                {
                    DateTime = Ende
                },
                ColorId = Farbe.ToString()
            
            };
            
            String calendarId = "primary";
            EventsResource.InsertRequest request = dienst.Events.Insert(test, calendarId);
            Event createdEvent = request.Execute();

            return "Erfolg";
        }

        // Methode um *GANZTÄGIGE* Kalendereinträge vorzunehmen

        public static String kalenderEintragGanz(String titel, String text, String location, int Farbe, DateTime Start, DateTime Ende)
        {

            Event test = new Event()
            {
                Summary = titel,
                Description = text,
                Start = new EventDateTime()
                {
                    Date = Start.Year + "-" + Start.Month + "-" + Start.Day,
                },
                End = new EventDateTime()
                {
                    Date = Ende.Year + "-" + Ende.Month + "-" + Ende.Day,
                },
                ColorId = Farbe.ToString(),
                Location = location

            };

            String calendarId = "primary";
            EventsResource.InsertRequest request = dienst.Events.Insert(test, calendarId);
            Event createdEvent = request.Execute();

            return "Erfolg";
        }

        // Finde alle Einträge zu einem Kunden
        public static Events kalenderKundenFinder(String Kundennummer) {

            // Define parameters of request.
            EventsResource.ListRequest request = dienst.Events.List("primary");
            request.Q = Kundennummer;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 2500;
            // List events.
            Events events = request.Execute();
                      
            return events;
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

        // Lösche Eintrag aus Kalender mittels ID
        public static void EventDeleteId(String ID) {
            try
            {
                dienst.Events.Delete("primary", ID).Execute();
            }
            catch (Exception ex)
            {
                // Popup wenn zu löschendes Datum nicht gefunden wird
                String what = "Ein zu löschender Kalendereintrag konnte nicht gefunden werden. \r\n Bitte den Kalender überprüfen und manuell Löschungen vornehmen, \r\n sodass das System und der Kalender nach dieser änderung wieder übereinstimmen.";
                what += " \r\n Speziell darauf achten das der Termin, den die Änderung betrifft, korrekt (und nicht doppelt) ist. \r\n";
                what += ex.ToString();
                what += "/r/n"+ID;

                var warnung = MessageBox.Show(what, "Fehlermeldung");
            }

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

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(5000);
            if (false == p.CloseMainWindow())
                p.Kill();
        }

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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

            //// TEST TEST TEST TESTEVENT
            

            //Event test = new Event()
            //{
            //    Summary = "X Test",
            //    Location = "800 Howard St., San Francisco, CA 94103",
            //    Description = "A chance to hear more about Google's developer products.",
            //    Start = new EventDateTime()
            //    {
            //        DateTime = DateTime.Parse("2017-02-28T09:00:00-07:00"),
            //        TimeZone = "America/Los_Angeles"
            //    },
            //    End = new EventDateTime()
            //    {
            //        DateTime = DateTime.Parse("2017-02-28T17:00:00-07:00"),
            //        TimeZone = "America/Los_Angeles"
            //    }
            //};
            //String calendarId = "primary";
            //EventsResource.InsertRequest request = service.Events.Insert(test, calendarId);
            //Event createdEvent = request.Execute();


            // TEST TEST TESTPDF ITEXT 5

            //FileStream fs = new FileStream("Test.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            //Document dox = new Document();
            //var writer = PdfWriter.GetInstance(dox,fs);

            //dox.Open();
            //dox.Add(new Paragraph("Test"));
            //dox.Close();

            //TEST TEST TESTPDF ITEXT7

            //var writer = new PdfWriter("test.pdf");
            //var pdf = new PdfDocument(writer);
            //var dox = new Document(pdf, PageSize.A4.Rotate());
            //dox.SetMargins(20, 20, 20, 20);
            //dox.Add(new Paragraph("Hello World!"));
            //dox.Close();

            //Table ganz = new Table(new float[] { 1, 1, 1 });
            //ganz.SetWidthPercent(100);

            //ganz.AddCell("Test");
            //ganz.AddCell("Test2");
            //ganz.AddCell("Test3");

            //dox.Add(ganz);



            //Öffne Verbindung
            try
            {
                conn.Open();
                conn2.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.Application.Exit();
            }
            // Öffnen des Fensters
            Application.Run(new mainForm());
                       
        }
    }
}
