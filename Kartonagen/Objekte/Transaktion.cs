using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartonagen.Objekte
{
    class Transaktion
    {

        int id;
        DateTime datTransaktion;   //Datum passiert, Zeit ignorieren
        String UserChanged;
        int lfd_nr;

        //
        int Kartons;
        int Flaschenkartons;
        int Glaeserkartons;
        int Kleiderkartons;
        String Bemerkung;
        int unbenutztKaufkarton;
        String Rechnungsnummer;
        DateTime datKalender = new DateTime(2017, 1, 1); //Kalenderdatum incl Zeit, default 1.1.17


        public Transaktion(int nr) {

            MySqlCommand cmdReadTrans = new MySqlCommand("Select * from Transaktionen WHERE idTransaktionen ="+nr, Program.conn);
            MySqlDataReader rdrTrans;
            
            try
            {
                rdrTrans = cmdReadTrans.ExecuteReader();

                while (rdrTrans.Read())
                {
                    id = rdrTrans.GetInt32(0);
                    datTransaktion = rdrTrans.GetDateTime(1).Date;
                    Kartons = rdrTrans.GetInt32(2);
                    Flaschenkartons = rdrTrans.GetInt32(3);
                    Glaeserkartons = rdrTrans.GetInt32(4);
                    Kleiderkartons = rdrTrans.GetInt32(5);
                    Bemerkung = rdrTrans.GetString(8);
                    UserChanged = rdrTrans.GetString(9);
                    unbenutztKaufkarton = rdrTrans.GetInt32(11);
                    Rechnungsnummer = rdrTrans.GetString(12);
                    datKalender = rdrTrans.GetDateTime(13);
                    lfd_nr = rdrTrans.GetInt32(15);
                }
                rdrTrans.Close();
            }
            catch (Exception sqlEx)
            {
                return;
            }


        }

    }
}
