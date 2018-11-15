using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Kartonagen.Objekte;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

// Namenskonvention für Termin-ID´s: 
// 1) merlin
// 2) "umz" (zu einem Umzug gehörend) / "tran" (zu einer Transaktion gehörend)
// 3) Nummer der Transaktion / des Umzugs
// 4) Endung falls Umzug "umz/bes/ein/aus/ent" je nach Art des Termins
//
// Damit ist ein Termin ein-eindeutig definiert, wenn Kollision auftritt ist eine Löschung versäumt worden

namespace Kartonagen.CalendarAPIUtil
{
    class Util
    {
        // Singleton für Zwischenspeichern
        private Events events = null;
        private string nextID = null;
        private int nID;

        // Google vorbereitungen
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API";
        static CalendarService dienst;

        //Google Setup
        public static void googleInit() {

            // Google-Auth

            UserCredential credential;

            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
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

        }

        //Singleton ID-String zugriff
        public string getNextID ()
        {
            if (nextID == null)
            {
                //neue ID erzeugen die nicht in der Liste ist
                Events current = getEvents();

                int count = current.Items.Count;

                foreach (var item in current.Items)
                {
                    if (item.Id.Contains("merlin" + count))
                    {
                        Random rand = new Random();
                        int ran = rand.Next();
                        count += ran;
                    }
                }
                nID = count;
                nextID = "merlin" + count;
            }
            return nextID;            
        }

        private void cycleID()
        {
            nID++;
            foreach (var item in getEvents().Items)
            {
                if (item.Id.Contains(nID.ToString()))
                {
                    cycleID();
                }
            }
            nextID = "merlin" + nID;
        }

        //Singleton Eventliste zugriff
        public Events getEvents() {
            if (events == null) {
                events = kalenderIDListe();
                return events;
            }
            return events;
        }

        // Eventliste ziehen
        public static Events kalenderIDListe()
        {

            // Define parameters of request.
            EventsResource.ListRequest request = dienst.Events.List("primary");
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 2500;
            // List events.
            Events events = request.Execute();

            return events;
        }

        // Neuen Umzug in die DB anlegen
        public  void UmzugInsert(Kunde kunde, DateTime datBesichtigung, DateTime datUmzug, DateTime datEinraeumen, DateTime datAusraeumen, DateTime datRuempeln, DateTime zeitUmzug, int statBesichtigung, int statUmzug, int statAus, int statEin, int statRuempeln,
            int umzugsdauer, string autos, int mann, int stunden, int versicherung, int einpacken, int einpacker, int einStunden, int karton, int auspacken, int auspacker, int ausStunden, int kleiderkartons, int kuecheAuf, int kuecheAb, int kuecheBau,
            int kuechePausch, Adresse auszug, Adresse einzug, int schilder, DateTime schilderZeit, string notizTitel, string notizBuero, string notizFahrer, string userChanged, DateTime erstelldatum, Adresse ruempeladresse, int RuempelMann, int RuempelStunden)

        {

            try
            {
                String longInsert = "INSERT INTO Umzuege (Kunden_idKunden, datBesichtigung, datUmzug, datRuempelung, datEinpacken, datAuspacken, umzugsZeit, " +
                "StatBes, StatUmz, StatAus, StatEin, StatEnt, Umzugsdauer, Autos, Mann, Stunden, Versicherung, " +
                "Einpacken, EinPackerZahl, EinPackStunden, Kartons, Auspacken, AusPackerZahl, AusPackStunden, Kleiderkisten, SchilderZurueck, " +
                "KuecheAb, KuecheAuf, KuecheBau, KuechePausch, " +
                "StraßeA, HausnummerA, PLZA, OrtA, LandA, AufzugA, StockwerkeA, HausTypA, HVZA, LaufmeterA, AussenAufzugA, " +
                "StraßeB, HausnummerB, PLZB, OrtB, LandB, AufzugB, StockwerkeB, HausTypB, HVZB, LaufmeterB, AussenAufzugB, " +
                "NotizBuero, NotizFahrer, BemerkungTitel, SchilderZeit, UserChanged, Erstelldatum, Adresse_id, entruempelMann, entruempelStunden) VALUES (";

                longInsert += kunde.Id + ", ";
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
                longInsert += ruempeladresse.IDAdresse1 + ", ";
                longInsert += RuempelMann + ", ";
                longInsert += RuempelStunden + ");";

                // Merkt den Query
                Program.QueryLog(longInsert);

                Program.absender(longInsert, "Einfügen des neuen Umzuges zum erstellen des Umzugsobjekts");
                return;
            }
            catch (Exception e)
            {
                Program.FehlerLog(e.ToString(), "Ich bin ein Error");
                return;
            }
        }

        // Methode um Kalendereinträge vorzunehmen (Autogenerierter ID-String)
        public String kalenderEventEintrag(String titel, String text, int Farbe, DateTime Start, DateTime Ende)
        {

            Event test = new Event()
            {
                Summary = titel,
                Description = text,
                Start = new EventDateTime()
                {
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
            string ret = nextID;
            cycleID();

            return ret;
        }

        // Methode um Kalendereinträge vorzunehmen (Von AktionsObjekt vorgegebener ID-String)
        public String kalenderEventEintrag(String titel, String text, int Farbe, DateTime Start, DateTime Ende, String ID)
        {

            Event test = new Event()
            {
                Summary = titel,
                Description = text,
                Start = new EventDateTime()
                {
                    DateTime = Start
                },
                End = new EventDateTime()
                {
                    DateTime = Ende
                },
                ColorId = Farbe.ToString(),
                Id = ID
            };

            String calendarId = "primary";
            EventsResource.InsertRequest request = dienst.Events.Insert(test, calendarId);
            Event createdEvent = request.Execute();

            return ID;
        }

        // Methode um *GANZTÄGIGE* Kalendereinträge vorzunehmen (Autogenerierter ID-String)
        public String kalenderEventEintragGanz(String titel, String text, String location, int Farbe, DateTime Start, DateTime Ende)
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
            cycleID();

            return "";
        }

        // Methode um *GANZTÄGIGE* Kalendereinträge vorzunehmen (Von AktionsObjekt vorgegebener ID-String)
        public String kalenderEventEintragGanz(String titel, String text, String location, int Farbe, DateTime Start, DateTime Ende, String ID)
        {

            Event toAdd = new Event()
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
                Location = location,
                Id = ID
            };

            String calendarId = "primary";
            EventsResource.InsertRequest request = dienst.Events.Insert(toAdd, calendarId);
            Event createdEvent = request.Execute();

            return ID;
        }

        // Finde spezifischen Eintrag
        public Event kalenderEventFinder(String ID)
        {
            //Define parameters of request.
            EventsResource.GetRequest request = dienst.Events.Get("primary",ID);
            // List events.
            Event ret = request.Execute();
            return ret;
        }

        // Verifikation freie KalenderID
        public Boolean verifyIDAvailability(String ID) {

            Events current = getEvents();

            foreach (var item in current.Items)
            {
                if (item.Id == ID)
                {
                    return false;
                }
            }
            return true;
        }

        // Kill spezifischen Eintrag, return Erfolg oder Misserfolg
        public Boolean kalenderEventRemove(String ID)
        {
            try
            {                
                EventsResource.DeleteRequest request = dienst.Events.Delete("primary", ID);                
                request.Execute();
            }
            catch (Exception)
            {
                return false;                
            }
            return true;
        }

        //
        // Deprecated
        // Altmethoden für Auslaufmodell (Terminsuche ohne ID, über Farbe / Datum / Nummern als String)
        //

        // Finde alle Einträge zu einem Kunden (DEPRECATED)
        public Events kalenderKundenFinder(String Kundennummer)
        {

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

        // Finde alle Einträge zu einem Umzug
        public Events kalenderUmzugFinder(String partialID)
        {

            // Define parameters of request.
            EventsResource.ListRequest request = dienst.Events.List("primary");
            request.Q = partialID;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 2500;
            // List events.
            Events events = request.Execute();

            return events;
        }

        //// Finde alle Einträge zu einem Datum (DEPRECATED)
        //public Events kalenderDatumFinder(DateTime datum)
        //{

        //   // Define parameters of request.
        //    EventsResource.ListRequest request = dienst.Events.List("primary");

        //    DateTime keks = DateTime.Now;

        //    String x = XmlConvert.ToString(keks, XmlDateTimeSerializationMode.Utc);

        //    request.TimeMin = DateTime.Now.Date;
        //    request.TimeMax = DateTime.Now.Date.AddDays(1);
        //    request.ShowDeleted = false;
        //    request.SingleEvents = true;
        //    request.MaxResults = 2500;
        //    // List events.
        //    Events events = request.Execute();

        //    return events;
        //} 

        // Methode um Kalendereinträge vorzunehmen (DEPRECATED)
        public String kalenderEintrag(String titel, String text, int Farbe, DateTime Start, DateTime Ende)
        {

            Event test = new Event()
            {
                Summary = titel,
                Description = text,
                Start = new EventDateTime()
                {
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

        //UTILITY METHODEN

        //Merge von Date und Time into DateTime
        public DateTime mergeDatetime(DateTime Tag, DateTime Uhrzeit) {

            DateTime ret = new DateTime(Tag.Year, Tag.Month, Tag.Day, Uhrzeit.Hour, Uhrzeit.Minute, Uhrzeit.Second);
            return ret;

        }

        public Boolean targetedDelete (DateTime Datum, String Color, String DescriptionQ, Boolean isGanztaegig)
        {



            // Define parameters of request.
            EventsResource.ListRequest request = dienst.Events.List("primary");
            request.SingleEvents = true;
            request.MaxResults = 2500;
            request.TimeMin = Datum.AddDays(-1);
            request.TimeMax = Datum.AddDays(1);
            request.ShowDeleted = false;
            // List events.
            Events events = request.Execute();
            LinkedList<Event> echtEvent = new LinkedList<Event>();



            foreach (var item in events.Items)
            {

                if (isGanztaegig)
                { //Rausfiltern von Terminen die nicht ganztätgig sind

                    if (item.Start != null)
                    {
                        if (item.Start.Date != null)
                        {
                            if (item.Start.Date.Length > 0)
                            {
                                echtEvent.AddLast(item);
                            }
                        }
                    }

                }

                else { // Alle Termine übernehmen
                    echtEvent.AddLast(item);
                }

            }

            //Vorbereitung des korrekt geformten Vergleichsdatums
            String date = "" + Datum.Date.Year;

            if (Datum.Date.Month < 10)
            {
                date += "-0" + Datum.Date.Month;
            }
            else
            {
                date += "-" + Datum.Date.Month;
            }

            if (Datum.Date.Day < 10)
            {
                date += "-0" + Datum.Date.Day;
            }
            else
            {
                date += "-" + Datum.Date.Day;
            }

            if (isGanztaegig)
            {
                //Durchsuche die in Frage kommenden Events mit den gegebenen Parametern samt Datum
                foreach (var item in echtEvent)
                {
                    if (item.ColorId.Equals(Color) && item.Start.Date.Equals(date) && item.Description.Contains(DescriptionQ))
                    {
                        if (kalenderEventRemove(item.Id))
                        {
                            // Löschen versucht und erfolgreich -> zurückmelden dass erfolgreich ein passendes Event gelöscht wurde
                            return true;
                        }
                    }
                }
            }
            else {
                foreach (var item in echtEvent)
                {
                    if (item.ColorId.Equals(Color) && item.Description.Contains(DescriptionQ))
                    {
                        if (kalenderEventRemove(item.Id))
                        {
                            // Löschen versucht und erfolgreich -> zurückmelden dass erfolgreich ein passendes Event gelöscht wurde
                            return true;
                        }
                    }
                }
            }


            //Wenn nicht vorher erfolgreich, dann negative Rückmeldung
            return false;
        }


        public void KalenderDBCheck(TextBox Log)
        {

            // Define parameters of request.
            EventsResource.ListRequest request = dienst.Events.List("primary");
            request.SingleEvents = true;
            request.MaxResults = 2500;
            request.TimeMin = DateTime.Now.AddMonths(-1);
            request.TimeMax = DateTime.Now.AddYears(1);
            request.ShowDeleted = false;
            // List events.
            Events events = request.Execute();
            LinkedList<Event> echtEvent = new LinkedList<Event>();
            
            int withDate = 0;

            foreach (var item in events.Items)
            {
                if (item.Start != null)
                {
                    if (item.Start.Date != null)
                    {
                        if (item.Start.Date.Length > 0)
                        {
                            echtEvent.AddLast(item);
                        }
                    }
                }
            }

            Console.WriteLine("Events.Items ist so lang: " + events.Items.LongCount());
            Console.WriteLine("Soviele haben ein Datum => "+echtEvent.Count);
            // 1) Alle Umzugstermine mit Status != 0

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdRead = new MySqlCommand("SELECT idUmzuege, datUmzug, StatUmz, Kunden_idKunden FROM Umzuege WHERE (StatUmz != 0) AND (datUmzug > '" + Program.DateTimeMachine(DateTime.Now, DateTime.Now) + "') ORDER BY datUmzug asc;", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();
                int countUmzug = 0;
                Boolean success = false;

                while (rdr.Read())
                {
                    //Check auf vorhanden -> wenn ja add zur kill-list
                    //Console.WriteLine("Umzug nummer "+rdr.GetInt32(0)+" Kunde "+rdr.GetInt32(3)+" vom "+rdr.GetDateTime(1).ToShortDateString());
                    countUmzug++;
                    success = false;
                    //Bei jeder Iteration überschreiben

                    String date =""+ rdr.GetDateTime(1).Date.Year;

                    if (rdr.GetDateTime(1).Date.Month < 10)
                    {
                        date += "-0" + rdr.GetDateTime(1).Date.Month;
                    }
                    else
                    {
                        date += "-" + rdr.GetDateTime(1).Date.Month;
                    }

                    if (rdr.GetDateTime(1).Date.Day<10)
                    {
                        date += "-0" + rdr.GetDateTime(1).Date.Day;
                    }
                    else {
                        date += "-" + rdr.GetDateTime(1).Date.Day;
                    }

                    String id = rdr.GetInt32(0)+"";                    

                    if (rdr.GetInt32(2) == 1)
                    { // Festgelegter Umzug, Farbe 11

                        foreach (var item in echtEvent)
                        {
                            //Console.WriteLine(item.Start.Date + "+" + date);

                            if (item.ColorId == "11" && item.Start.Date.Equals(date)  && !success && item.Summary.Contains(rdr.GetInt32(3) + "")) //&& item.Description.Contains(id)
                            {                                                             //
                                Console.WriteLine("Hit" + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                                success = true;
                                break;
                            }
                        }
                    }

                    else if (rdr.GetInt32(2) == 3)
                    { // vorl Festgelegt, Farbe 2

                        foreach (var item in echtEvent)
                        {
                            if (item.ColorId == "2" && item.Start.Date.Equals(date) && !success && item.Summary.Contains(rdr.GetInt32(3) + ""))
                            {
                                Console.WriteLine("Hit" + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                                success = true;
                                break;
                            }
                        }
                    }

                    else if (rdr.GetInt32(2) == 2)
                    { // Vorläufig, Farbe 11

                        foreach (var item in echtEvent)
                        {
                            if (item.ColorId == "10" && item.Start.Date.Equals(date) && !success && item.Summary.Contains(rdr.GetInt32(3) + ""))
                            {
                                Console.WriteLine("Hit" + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                                success = true;
                                break;
                            }
                        }
                    }

                    if (success == false)
                    {
                        Console.WriteLine("Fail! " + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                        Log.AppendText("Umzug Nr."+id+" vom "+rdr.GetDateTime(1).ToShortDateString()+ " fehlt im Kalender!"+Environment.NewLine);
                    }

                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Bla");
                throw sqlEx;
            }

            // 2) Alle Besichtigungstermine mit Status != 0

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdRead = new MySqlCommand("SELECT idUmzuege, datBesichtigung, StatBes, Kunden_idKunden, umzugsZeit FROM Umzuege WHERE (StatBes != 0) AND (datBesichtigung > '" + Program.DateTimeMachine(DateTime.Now, DateTime.Now) + "') ORDER BY datBesichtigung asc;", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();
                int countUmzug = 0;

                while (rdr.Read())
                {
                    //Check auf vorhanden -> wenn ja add zur kill-list
                    Console.WriteLine("Besichtigung " + rdr.GetInt32(0) + ", Kunde " + rdr.GetInt32(3) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                    countUmzug++;

                    //Bei jeder Iteration überschreiben
                    Boolean success = false;
                    DateTime date = new DateTime(rdr.GetDateTime(1).Date.Year , rdr.GetDateTime(1).Date.Month , rdr.GetDateTime(1).Date.Day,0,0,0);

                    //Tatsächlicher Abgleich
                    foreach (var item in echtEvent)
                    {
                        //Console.WriteLine(item.Start.Date + "+" + date);

                        if (item.ColorId == "9" && (item.Start.DateTime > date && item.Start.DateTime < date.AddDays(1)) && item.Summary.Contains(rdr.GetInt32(3) + "")) //&& item.Description.Contains(id)
                        {                                                             //
                            Console.WriteLine("Hit" + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                            success = true;
                            break;
                        }
                    }

                    if (success == false)
                    {
                        Console.WriteLine("Fail! " + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                        Log.AppendText("Besichtigung zum Umzug Nr." + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString() + " fehlt im Kalender!" + Environment.NewLine);
                    }

                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Bla");
                throw sqlEx;
            }

            // 3) Alle Einpacken

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdRead = new MySqlCommand("SELECT idUmzuege, datEinpacken, StatEin, Kunden_idKunden, umzugsZeit FROM Umzuege WHERE (StatEin != 0) AND (datEinpacken > '" + Program.DateTimeMachine(DateTime.Now, DateTime.Now) + "') ORDER BY datEinpacken asc;", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();
                int countUmzug = 0;

                while (rdr.Read())
                {
                    //Check auf vorhanden -> wenn ja add zur kill-list
                    Console.WriteLine("Einpacken " + rdr.GetInt32(0) + ", Kunde " + rdr.GetInt32(3) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                    countUmzug++;

                    //Bei jeder Iteration überschreiben
                    Boolean success = false;
                    String date = "" + rdr.GetDateTime(1).Date.Year;

                    if (rdr.GetDateTime(1).Date.Month < 10)
                    {
                        date += "-0" + rdr.GetDateTime(1).Date.Month;
                    }
                    else
                    {
                        date += "-" + rdr.GetDateTime(1).Date.Month;
                    }

                    if (rdr.GetDateTime(1).Date.Day < 10)
                    {
                        date += "-0" + rdr.GetDateTime(1).Date.Day;
                    }
                    else
                    {
                        date += "-" + rdr.GetDateTime(1).Date.Day;
                    }

                    if (rdr.GetInt32(2) == 1)
                    {
                        foreach (var item in echtEvent)
                        {
                            //Console.WriteLine(item.Start.Date + "+" + date);

                            if (item.ColorId == "5" && item.Start.Date.Equals(date) && !success && item.Summary.Contains(rdr.GetInt32(3) + "")) 
                            {                                                             //
                                Console.WriteLine("Hit" + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                                success = true;
                                break;
                            }
                        }
                    }
                    else {
                        foreach (var item in echtEvent)
                        {
                            //Console.WriteLine(item.Start.Date + "+" + date);

                            if (item.ColorId == "6" && item.Start.Date.Equals(date) && !success && item.Summary.Contains(rdr.GetInt32(3) + "")) 
                            {                                                             //
                                Console.WriteLine("Hit" + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                                success = true;
                                break;
                            }
                        }
                    }
                                      

                    if (success == false)
                    {
                        Console.WriteLine("Fail! " + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                        Log.AppendText("Einpacken zum Umzug Nr." + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString() + " fehlt im Kalender!" + Environment.NewLine);
                    }

                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Bla");
                throw sqlEx;
            }

            // 4) Alle Einpacken / Auspacken
            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdRead = new MySqlCommand("SELECT idUmzuege, datAuspacken, StatAus, Kunden_idKunden, umzugsZeit FROM Umzuege WHERE (StatAus != 0) AND (datAuspacken > '" + Program.DateTimeMachine(DateTime.Now, DateTime.Now) + "') ORDER BY datAuspacken asc;", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();
                int countUmzug = 0;

                while (rdr.Read())
                {
                    //Check auf vorhanden -> wenn ja add zur kill-list
                    Console.WriteLine("Auspacken " + rdr.GetInt32(0) + ", Kunde " + rdr.GetInt32(3) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                    countUmzug++;

                    //Bei jeder Iteration überschreiben
                    Boolean success = false;
                    String date = "" + rdr.GetDateTime(1).Date.Year;

                    if (rdr.GetDateTime(1).Date.Month < 10)
                    {
                        date += "-0" + rdr.GetDateTime(1).Date.Month;
                    }
                    else
                    {
                        date += "-" + rdr.GetDateTime(1).Date.Month;
                    }

                    if (rdr.GetDateTime(1).Date.Day < 10)
                    {
                        date += "-0" + rdr.GetDateTime(1).Date.Day;
                    }
                    else
                    {
                        date += "-" + rdr.GetDateTime(1).Date.Day;
                    }

                    if (rdr.GetInt32(2) == 1)
                    {
                        foreach (var item in echtEvent)
                        {
                            //Console.WriteLine(item.Start.Date + "+" + date);

                            if (item.ColorId == "5" && item.Start.Date.Equals(date) && !success && item.Summary.Contains(rdr.GetInt32(3) + ""))
                            {                                                             //
                                Console.WriteLine("Hit" + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                                success = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in echtEvent)
                        {
                            //Console.WriteLine(item.Start.Date + "+" + date);

                            if (item.ColorId == "6" && item.Start.Date.Equals(date) && !success && item.Summary.Contains(rdr.GetInt32(3) + ""))
                            {                                                             //
                                Console.WriteLine("Hit" + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                                success = true;
                                break;
                            }
                        }
                    }


                    if (success == false)
                    {
                        Console.WriteLine("Fail! " + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                        Log.AppendText("Auspacken zum Umzug Nr." + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString() + " fehlt im Kalender!" + Environment.NewLine);
                    }

                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Bla");
                throw sqlEx;
            }

            // 5) Alle Entrümpeln

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdRead = new MySqlCommand("SELECT idUmzuege, datRuempelung, StatEnt, Kunden_idKunden, umzugsZeit FROM Umzuege WHERE (StatEnt != 0) AND (datRuempelung > '" + Program.DateTimeMachine(DateTime.Now, DateTime.Now) + "') ORDER BY datRuempelung asc;", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();
                int countUmzug = 0;

                while (rdr.Read())
                {
                    //Check auf vorhanden -> wenn ja add zur kill-list
                    Console.WriteLine("Ruempeln " + rdr.GetInt32(0) + ", Kunde " + rdr.GetInt32(3) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                    countUmzug++;

                    //Bei jeder Iteration überschreiben
                    Boolean success = false;
                    String date = "" + rdr.GetDateTime(1).Date.Year;

                    if (rdr.GetDateTime(1).Date.Month < 10)
                    {
                        date += "-0" + rdr.GetDateTime(1).Date.Month;
                    }
                    else
                    {
                        date += "-" + rdr.GetDateTime(1).Date.Month;
                    }

                    if (rdr.GetDateTime(1).Date.Day < 10)
                    {
                        date += "-0" + rdr.GetDateTime(1).Date.Day;
                    }
                    else
                    {
                        date += "-" + rdr.GetDateTime(1).Date.Day;
                    }

                    if (rdr.GetInt32(2) == 1)
                    {
                        foreach (var item in echtEvent)
                        {
                            //Console.WriteLine(item.Start.Date + "+" + date);

                            if (item.ColorId == "11" && item.Start.Date.Equals(date) && !success && item.Summary.Contains(rdr.GetInt32(3) + ""))
                            {                                                             //
                                Console.WriteLine("Hit" + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                                success = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in echtEvent)
                        {
                            //Console.WriteLine(item.Start.Date + "+" + date);

                            if (item.ColorId == "10" && item.Start.Date.Equals(date) && !success && item.Summary.Contains(rdr.GetInt32(3) + ""))
                            {                                                             //
                                Console.WriteLine("Hit" + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                                success = true;
                                break;
                            }
                        }
                    }


                    if (success == false)
                    {
                        Console.WriteLine("Fail! " + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString());
                        Log.AppendText("Entrümpeln zum Umzug Nr." + rdr.GetInt32(0) + " vom " + rdr.GetDateTime(1).ToShortDateString() + " fehlt im Kalender!" + Environment.NewLine);
                    }

                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Bla");
                throw sqlEx;
            }


        }


    }
}
