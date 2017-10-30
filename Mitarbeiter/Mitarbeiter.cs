using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiter
{
    class Mitarbeiter
    {
        // Attribute WiP

        int id;
        Dictionary<DateTime, int> SollMinuten;
        bool angestellt;
        int MonatsMinuten;
        

        //Konstruktor TODO
        public Mitarbeiter(int item)
        {
            
        }
        

        //Hilfsmethoden
        private void SollMinutenAbruf() {

            MySqlCommand cmdReadStundenkonto = new MySqlCommand("SELECT * FROM Stundenkonto WHERE Mitarbeiter_idMitarbeiter = " + id + " ORDER BY Monat DESC;", Program.conn2);
            MySqlDataReader rdrStundenkonto;
            
            try
            {
                rdrStundenkonto = cmdReadStundenkonto.ExecuteReader();
                while (rdrStundenkonto.Read())
                {
                    SollMinuten.Add(Program.getMonat(rdrStundenkonto.GetDateTime(2)),rdrStundenkonto.GetInt32(1));
                }
                rdrStundenkonto.Close();
            }
            catch (Exception sqlEx)
            {
                //Program.FehlerLog(sqlEx.ToString(), "Abrufen des Kunden zur Objekterzeugung");
            }

        }

        private void StundenkontoAdd(DateTime monat, int min) {

            int minuten;
            Program.Sollminuten.TryGetValue(id, out minuten);

            string befehl = "INSERT INTO Stundenkonto (SollMinuten, Monat, Mitarbeiter_IdMitarbeiter) VALUES ("+minuten+", "+ Program.getMonat(DateTime.Now) +")"

        }
        
        //Abfragen
        public void StundenkontoAktualisieren() {

            DateTime Programmstart = new DateTime(2017, 11, 1);

            if ((!SollMinuten.ContainsKey(Program.getMonat(DateTime.Now))) && angestellt) {    //Monat ist nicht aktuell, Kollege noch angestellt?
                 
                DateTime letzter = new DateTime (2000,1,1);     //Silly Default

                foreach (var item in SollMinuten.Keys)              //letzten Verbuchten Monat finden
                {
                    if (letzter.Year == 2000 || letzter < item)
                    {
                        letzter = item;
                    }                   
                }

                if (letzter > new DateTime(2017, 10, 1))
                {      // Aussortieren von Daten vor dem Programmstart
                    
                    while (letzter != Program.getMonat(DateTime.Now))   // Für jeden fehlenden Monat Stundenkonto hinzufügen
                    {
                        letzter = Program.getMonat(letzter.AddMonths(1));
                        StundenkontoAdd(letzter, MonatsMinuten);
                    }
                }
            }

        }             

    }
}
