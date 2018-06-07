using Google.Apis.Calendar.v3.Data;
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
        int unbenutzt;
        int Kaufkarton;
        String Rechnungsnummer;
        DateTime datKalender = new DateTime(2017, 1, 1); //Kalenderdatum incl Zeit, default 1.1.17
        int IDAdresse;
        int idUmzuege;
        int idKunden;

        //Objekte
        Kunde kunde;

        public Transaktion(int nr)
        {

            MySqlCommand cmdReadTrans = new MySqlCommand("Select * from Transaktionen WHERE idTransaktionen =" + nr, Program.conn);
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
                    unbenutzt = rdrTrans.GetInt32(11);
                    Kaufkarton = rdrTrans.GetInt32(11);
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

        public Transaktion(int Kartons, int Glaeserkartons, int Flaschenkartons, int Kleiderkartons, int Kaufkartons, int unbenutzt, String Bemerkung, String Rechnungsnummer, DateTime datTransaktion, int idAdresse, int idUmzuege, int idKunden, String User)
        {

            this.Kartons = Kartons;
            this.Glaeserkartons = Glaeserkartons;
            this.Flaschenkartons = Flaschenkartons;
            this.Kleiderkartons = Kleiderkartons;
            this.unbenutzt = unbenutzt;
            this.Bemerkung = Bemerkung;
            this.Kaufkarton = Kaufkartons;
            this.Rechnungsnummer = Rechnungsnummer;
            this.datKalender = datTransaktion;
            this.IDAdresse = idAdresse;     //Adresse 0 = Büro
            this.idUmzuege = idUmzuege;
            this.idKunden = idKunden;
            this.UserChanged = User;

            //Userobjekt
            Kunde kunde = new Kunde(idKunden);

            String insert = "INSERT INTO Transaktionen (datTransaktion,Kartons,FlaschenKartons,GlaeserKartons,KleiderKartons,Umzuege_idUmzuege,Umzuege_Kunden_idKunden,Bemerkungen, UserChanged, Erstelldatum, unbenutzt, Rechnungsnummer, timeTransaktion, final) values (";
            insert += "'" + Program.DateMachine(datKalender) + "',";
            insert += this.Kartons + ",";
            insert += this.Flaschenkartons + ",";
            insert += this.Glaeserkartons + ",";
            insert += this.Kleiderkartons + ",";
            insert += this.idUmzuege + ",";
            insert += this.idKunden + ",";
            insert += "'" + this.Bemerkung + "',";
            insert += "'" + this.UserChanged + ",";
            insert += "'" + Program.DateMachine(DateTime.Now) + "',";
            insert += unbenutzt + ",";
            insert += "'" + Rechnungsnummer + "',";
            insert += "'" + Program.ZeitMachine(datKalender) + "',";
            insert += "0);";

            Program.QueryLog(insert);

            Program.absender(insert, "Einfügen der neuen Transaktion in die Datenank");


            // Abholen der fertigen Transaktionsnummer
            String select = "SELECT idTransaktionen FROM Transaktionen ORDER BY idTransaktionen DESC LIMIT 1;";
            MySqlCommand cmdRead = new MySqlCommand(select, Program.conn);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    id = rdr.GetInt32(0);
                }
                rdr.Close();

            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der neuen Transaktions-ID");
                throw sqlEx;
            }

            refreshKalender();
        }

        public void updateDB (String idBearbeitend) {

            String longInsert = "UPDATE Transaktionen SET ";

            longInsert += "datTransaktionen = '" + Program.DateMachine(datKalender) + "', ";
            longInsert += "Kartons = " + Kartons + ", ";
            longInsert += "FlaschenKartons = " + Flaschenkartons + ", ";
            longInsert += "GlaeserKartons = " + Glaeserkartons + ", ";
            longInsert += "KleiderKartons = " + Kleiderkartons + ", ";
            longInsert += "Umzuege_idUmzuege = " + idUmzuege + ", ";
            longInsert += "Umzuege_Kunden_idKunden = " + idKunden + ", ";
            longInsert += "Bemerkungen = '" + Bemerkung + "', ";
            longInsert += "UserChanged = '" + UserChanged+idBearbeitend + "', ";
            longInsert += "unbenutzt = " + unbenutzt + ", ";
            longInsert += "Rechnungsnummer = '" + Rechnungsnummer + "', ";
            longInsert += "timeTransaktion = '" + Program.ZeitMachine(datKalender) + "', ";
            longInsert += "final = " + 0 + ", ";

            Program.QueryLog(longInsert);

            Program.absender(longInsert, "Absenden der Änderung an der Transaktion");

        }

        public void refreshKalender()
        {
            Events ev = Program.getUtil().kalenderUmzugFinder("Transaktion_"+id);
            Console.WriteLine(ev.Items.Count + "gefunden");

            foreach (var item in ev.Items)
            {
                Program.getUtil().kalenderEventRemove(item.Id);
            }

            //Nur in Kalender wenn ausser Haus und in der Zukunft
            if (IDAdresse != 0 && datKalender > DateTime.Now) {
                Program.getUtil().kalenderEventEintrag(idKunden + " "+ getKunde().Anrede + " " + getKunde().Vorname + " " + getKunde().Nachname, KalenderString(), 8, datKalender, datKalender.AddHours(1));
            }

        }

        private String KalenderString() {

            String ret="TESTSTRING";

            ret += "/r/n Transaktion_" + id; 
            return ret;
        }

        private Kunde getKunde() {

            if (kunde == null) {
                kunde = new Kunde(idKunden);
            }
            return kunde;
        }
    }
}
