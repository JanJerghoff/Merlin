using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace Kartonagen
{
    class Class1
    {
        

        public static String refreshEntireCalendar() {

            //String Error = String.Empty;

            //MySqlCommand cmdRead = new MySqlCommand("Select * from Umzuege", Program.conn);
            //MySqlDataReader rdr;

            //List<int> Nummern = new List<int>();
            //Events events;
            //Umzug umz;

            //try
            //{
            //    rdr = cmdRead.ExecuteReader();

            //    while (rdr.Read())
            //    {
            //        Nummern.Add(rdr.GetInt32(0));
            //    }
            //    rdr.Close();
            //}
            //catch (Exception sqlEx)
            //{
            //    return "";
            //}

            //foreach (var item in Nummern)
            //{
            //    umz = new Umzug(item);

            //   // events = Program.getUtil().kalenderKundenFinder(umz.IdKunden.ToString());

            //    foreach (var Termin in events.Items)
            //    {
            //        if (Termin.ColorId != "8")
            //        {
            //            Program.getUtil().kalenderEventRemove(Termin.Id);
            //            Console.WriteLine("Kill Termin " + Termin.Id + " von Kunde" + umz.IdKunden.ToString());
            //        }
            //        else
            //        {

            //        }
            //    }

            //    //if (!umz.addAll())
            //    //{
            //    //    Console.WriteLine("Abbruch beim Hinzufügen von " + umz.Id.ToString());
            //    //    return "";
            //    //}
            //    umz.addAll();
            //    Console.WriteLine("Termine drin für " + umz.Id.ToString());
            //}

            //return "";

            //Alle Kartonagentransaktionen reinhauen

            //MySqlCommand cmdReadTrans = new MySqlCommand("Select * from Transaktionen WHERE timeTransaktion != '2017-01-01 00:00:00' ", Program.conn);
            //MySqlDataReader rdrTrans;

            //List<int> TransNummern = new List<int>();


            //try
            //{
            //    rdrTrans = cmdReadTrans.ExecuteReader();

            //    while (rdrTrans.Read())
            //    {
            //        TransNummern.Add(rdrTrans.GetInt32(0));
            //    }
            //    rdrTrans.Close();
            //}
            //catch (Exception sqlEx)
            //{
            //    return "";
            //}

            //foreach (var item in TransNummern)
            //{

            //}


            //return Error;
            return "";
        }



    }
}
