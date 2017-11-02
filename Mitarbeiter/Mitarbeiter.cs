using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mitarbeiter
{
    class Mitarbeiter
    {
        // Attribute WiP

        int id;
        Dictionary<DateTime, int> SollMinuten = new Dictionary<DateTime, int>();
        bool angestellt;
        int MonatsMinuten;
        

        //Konstruktor TODO
        public Mitarbeiter(int id)
        {
            this.id = id;

            MySqlCommand cmdReadMitarbeiter = new MySqlCommand("SELECT * FROM Mitarbeiter WHERE idMitarbeiter = " + id + ";", Program.conn2);
            MySqlDataReader rdrMitarbeiter;

            DateTime temp;

            try
            {
                rdrMitarbeiter = cmdReadMitarbeiter.ExecuteReader();
                while (rdrMitarbeiter.Read())
                {
                    temp = rdrMitarbeiter.GetDateTime(29);
                    MonatsMinuten = rdrMitarbeiter.GetInt32(21);
                    if (temp == new DateTime(2017, 1, 1) || temp > DateTime.Now) {
                        angestellt = true;
                    }
                    else {
                        angestellt = false;
                        }
                }
                rdrMitarbeiter.Close();
            }
            catch (Exception sqlEx)
            {
                //Program.FehlerLog(sqlEx.ToString(), "Abrufen des Kunden zur Objekterzeugung");
            }

            

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

        private void StundenkontoAdd(DateTime monat, int tage) {
            
            int minuten;
            Program.Sollminuten.TryGetValue(id, out minuten);

            string befehl = "INSERT INTO Stundenkonto (SollMinuten, Monat, Mitarbeiter_IdMitarbeiter) VALUES (" + minuten*tage + ", '" + Program.DateMachine(Program.getMonat(monat)) + "', " + id + ");";

            Program.absender(befehl,"Eintragen eines neuen Stundenkontos");
            
        }
        
        //Abfragen
        public void StundenkontoAktualisieren(int MonatsTage) {


            SollMinutenAbruf();
            DateTime Programmstart = new DateTime(2017, 11, 1);            

            if ((SollMinuten.ContainsKey(Program.getMonat(DateTime.Now)) == false) && angestellt == true) {    //Monat ist nicht aktuell, Kollege noch angestellt?
                              

                DateTime letzter = new DateTime (2000,1,1);     //Silly Default

                foreach (var item in SollMinuten.Keys)              //letzten Verbuchten Monat finden
                {
                    if (letzter.Year == 2000 || letzter < item)
                    {
                        letzter = item;
                    }                         
                }
                
                if (letzter >= new DateTime(2017, 10, 1))
                {      // Aussortieren von Daten vor dem Programmstart              

                    while (letzter != Program.getMonat(DateTime.Now))   // Für jeden fehlenden Monat Stundenkonto hinzufügen
                    {
                        letzter = Program.getMonat(letzter.AddMonths(1));
                        StundenkontoAdd(letzter, MonatsTage);
                    }
                }
            }

        }             

    }
}
