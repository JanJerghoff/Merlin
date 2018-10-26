using Google.Apis.Calendar.v3.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        Boolean unbenutzt;
        Boolean Kaufkarton;
        String Rechnungsnummer;
        DateTime datKalender = new DateTime(2017, 1, 1); //Kalenderdatum incl Zeit, default 1.1.17
        int IDAdresse;
        int idUmzuege;
        int idKunden;

        //Objekte
        Kunde kunde;

        public Transaktion(int nr)
        {
            
            int tempint = 0; // zwischenspeicher für auflösung in unbenutzt / kaufkartons
            Kaufkarton = false;
            unbenutzt = false;

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdReadTrans = new MySqlCommand("Select * from Transaktionen WHERE idTransaktionen =" + nr, Program.conn);
                MySqlDataReader rdrTrans = cmdReadTrans.ExecuteReader();

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
                    tempint = rdrTrans.GetInt32(11);
                    Rechnungsnummer = rdrTrans.GetString(12);
                    datKalender = rdrTrans.GetDateTime(13);
                    lfd_nr = rdrTrans.GetInt32(15);
                }
                rdrTrans.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                return;
            }

            if (tempint == 1)
            {
                unbenutzt = true;
            }
            else if (tempint == 2) {
                Kaufkarton = true;
            }

        }

        public Transaktion(int Kartons, int Glaeserkartons, int Flaschenkartons, int Kleiderkartons, Boolean Kaufkartons, Boolean unbenutzt, String Bemerkung, String Rechnungsnummer, DateTime datTransaktion, int idAdresse, int idUmzuege, int idKunden, String User)
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
            

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }

                MySqlCommand cmdRead = new MySqlCommand(select, Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();

                while (rdr.Read())
                {
                    id = rdr.GetInt32(0);
                }
                rdr.Close();
                Program.conn.Close();

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
        

        //Selbst entfernen in Vorbereitung eines Updates, dann erneutes Hinzufügen
        public Boolean KalenderRemove() {

            if (Program.getUtil().targetedDelete(datTransaktion, "8", "Transaktion_" + id)) {
                return true;
            }

            return false;
        }

        public Boolean KalenderAddDefault() {

            String Adresse = getKunde().Anschrift.Straße1 + getKunde().Anschrift.Hausnummer1 + "/r/n" + getKunde().Anschrift.PLZ1 + getKunde().Anschrift.Ort1 + "/r/n";

            Program.getUtil().kalenderEventEintrag(KalenderHeader(), KalenderString(Adresse), 8, datKalender, datKalender.AddHours(1));
            return true;
        }

        public Boolean KalenderAddAdresse(String Adresse)
        {

            Program.getUtil().kalenderEventEintrag(KalenderHeader(), KalenderString(Adresse), 8, datKalender, datKalender.AddHours(1));
            return true;
        }

        private String KalenderHeader() {

            String Header = "" + getKunde().getVollerName();


            if (Kartons < 0 || Kleiderkartons < 0 || Glaeserkartons < 0 || Flaschenkartons < 0)
            {
                Header += " Kartonlieferung";
            }
            else
            {
                Header += " Kartonabholung";
            }

            return Header;
        }


        private String KalenderString(String Adresse) {
            
            String Body = "";

            //TransaktionsID in den Body
            Body += "Transaktion_" + id + " /r/n";

            //Adresse in den Body
            //Body += getKunde().Anschrift.Straße1 + getKunde().Anschrift.Hausnummer1 + "/r/n" + getKunde().Anschrift.PLZ1 + getKunde().Anschrift.Ort1 + "/r/n";
            Body += Adresse + " /r/n ";

            // Kontaktdaten
            if (getKunde().Handy.Length > 2)
            {
                Body += "Handy: " + getKunde().Handy + "/r/n";
            }
            else if (getKunde().Telefon.Length > 2)
            {
                Body += "Telefon: " + getKunde().Telefon + "/r/n";
            }
            else {
                Body += "E-Mail: " + getKunde().Email + "/r/n";
            }

            if (Kartons<0||Kleiderkartons<0||Glaeserkartons<0||Flaschenkartons<0)
            {
                Body += "Kartonlieferung";
            }
            else
            {
                Body += "Kartonabholung";
            }

            //if (radioAusgang.Checked && labelLieferung.Visible)
            //{
            //    Body += " Kostenpflichtig!";
            //}
            //else if (radioEingang.Checked && labelAbholung.Visible)
            //{
            //    Body += " Kostenpflichtig!";
            //}

            Body += " über ";

            //Jeweils das Minus rausnehmen, weil über Auslieferung / abholung erklärt
            if (Kartons != 0)
            {
                Body += Kartons.ToString().Replace("-","") + " Kartons ";
            }
            if (Glaeserkartons != 0)
            {
                Body += Glaeserkartons.ToString().Replace("-", "") + " Gläserkartons ";
            }
            if (Flaschenkartons != 0)
            {
                Body += Flaschenkartons.ToString().Replace("-", "") + " Flaschenkartons ";
            }
            if (Kleiderkartons != 0)
            {
                Body += Kleiderkartons.ToString().Replace("-", "") + " Kleiderkartons ";
            }
            

           // Body += ", Zeichen =" + Program.getBearbeitender();

            //if (radioEingang.Checked) { Body += " tatsächliche Kartonzahl nachkorrigieren"; }
            
            return Body;
        }

        private Kunde getKunde() {

            if (kunde == null) {
                kunde = new Kunde(idKunden);
            }
            return kunde;
        }

        //Getter

        public int getId() {
            return id;
        }
    }
}
