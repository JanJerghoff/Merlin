using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        // Methode um Kalendereinträge vorzunehmen
        public static String kalenderEintrag(String titel, String text, int Farbe, DateTime Start, DateTime Ende)
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

        // Methode um *GANZTÄGIGE* Kalendereinträge vorzunehmen
        public String kalenderEintragGanz(String titel, String text, String location, int Farbe, DateTime Start, DateTime Ende)
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
                Location = location,
                Id = nextID
            };

            String calendarId = "primary";
            EventsResource.InsertRequest request = dienst.Events.Insert(test, calendarId);
            Event createdEvent = request.Execute();
            cycleID();

            return "Erfolg";
        }
    }
}
