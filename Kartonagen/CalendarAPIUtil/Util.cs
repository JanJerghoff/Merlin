using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Kartonagen.Objekte;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        // Finde alle Einträge zu einem Datum (DEPRECATED)
        public Events kalenderDatumFinder(DateTime datum)
        {

            // Define parameters of request.
            EventsResource.ListRequest request = dienst.Events.List("primary");

            DateTime keks = DateTime.Now;

            String x = XmlConvert.ToString(keks, XmlDateTimeSerializationMode.Utc);

            request.TimeMin = DateTime.Now.Date;
            request.TimeMax = DateTime.Now.Date.AddDays(1);
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 2500;
            // List events.
            Events events = request.Execute();

            return events;
        }

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
    }
}
