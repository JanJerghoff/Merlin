using Google.Apis.Calendar.v3.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
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
        int IDAdresse = 0;
        int idUmzuege;
        int idKunden;

        //Objekte
        Kunde kunde;
        Adresse adresse;

        public Kunde Kunde { get => kunde; set => kunde = value; }
        public Adresse Adresse { get => adresse; set => adresse = value; }
        public int Kartons1 { get => Kartons; set => Kartons = value; }
        public int Flaschenkartons1 { get => Flaschenkartons; set => Flaschenkartons = value; }
        public int Glaeserkartons1 { get => Glaeserkartons; set => Glaeserkartons = value; }
        public int Kleiderkartons1 { get => Kleiderkartons; set => Kleiderkartons = value; }
        public string Bemerkung1 { get => Bemerkung; set => Bemerkung = value; }
        public bool Unbenutzt { get => unbenutzt; set => unbenutzt = value; }
        public bool Kaufkarton1 { get => Kaufkarton; set => Kaufkarton = value; }
        public string Rechnungsnummer1 { get => Rechnungsnummer; set => Rechnungsnummer = value; }
        public DateTime DatKalender { get => datKalender; set => datKalender = value; }
        public int IDAdresse1 { get => IDAdresse; set => IDAdresse = value; }
        public int IdUmzuege { get => idUmzuege; set => idUmzuege = value; }
        public int IdKunden { get => idKunden; set => idKunden = value; }
        public int Id { get => id; set => id = value; }
        public DateTime DatTransaktion { get => datTransaktion; set => datTransaktion = value; }
        public string UserChanged1 { get => UserChanged; set => UserChanged = value; }
        public int Lfd_nr { get => lfd_nr; set => lfd_nr = value; }

        public Transaktion(int nr)
        {
            
            int tempint = 0; // zwischenspeicher für auflösung in unbenutzt / kaufkartons
            Kaufkarton1 = false;
            Unbenutzt = false;

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
                    Id = rdrTrans.GetInt32(0);
                    DatTransaktion = rdrTrans.GetDateTime(1).Date;
                    Kartons1 = rdrTrans.GetInt32(2);
                    Flaschenkartons1 = rdrTrans.GetInt32(3);
                    Glaeserkartons1 = rdrTrans.GetInt32(4);
                    Kleiderkartons1 = rdrTrans.GetInt32(5);
                    IdUmzuege = rdrTrans.GetInt32(6);
                    IdKunden = rdrTrans.GetInt32(7);
                    Bemerkung1 = rdrTrans.GetString(8);
                    UserChanged1 = rdrTrans.GetString(9);
                    tempint = rdrTrans.GetInt32(11);
                    Rechnungsnummer1 = rdrTrans.GetString(12);
                    datKalender = rdrTrans.GetDateTime(13);
                    Lfd_nr = rdrTrans.GetInt32(15);
                    IDAdresse = rdrTrans.GetInt32(16);
                }
                rdrTrans.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der Transaktionsdaten zur Objekterstellung");
                return;
            }

            if (tempint == 1)
            {
                Unbenutzt = true;
            }
            else if (tempint == 2) {
                Kaufkarton1 = true;
            }
            

            kunde = new Kunde(IdKunden);

            if (IDAdresse != 0) {
                adresse = new Adresse(IDAdresse);
            }

        }

        public Transaktion(int Kartons, int Glaeserkartons, int Flaschenkartons, int Kleiderkartons, Boolean Kaufkartons, Boolean unbenutzt, String Bemerkung, String Rechnungsnummer, DateTime datTransaktion, int idAdresse, Adresse Adresse, int idUmzuege, int idKunden, String User)
        {

            this.Kartons1 = Kartons;
            this.Glaeserkartons1 = Glaeserkartons;
            this.Flaschenkartons1 = Flaschenkartons;
            this.Kleiderkartons1 = Kleiderkartons;
            this.Unbenutzt = unbenutzt;
            this.Bemerkung1 = Bemerkung;
            this.Kaufkarton1 = Kaufkartons;
            this.Rechnungsnummer1 = Rechnungsnummer;
            this.DatKalender = datTransaktion;
            this.IDAdresse1 = idAdresse;     //Adresse 0 = Büro
            this.IdUmzuege = idUmzuege;
            this.IdKunden = idKunden;
            this.UserChanged1 = User;
            if (Adresse != null) {
                this.Adresse = Adresse;
            }

            //Userobjekt
            Kunde kunde = new Kunde(idKunden);

            String insert = "INSERT INTO Transaktionen (datTransaktion,Kartons,FlaschenKartons,GlaeserKartons,KleiderKartons,Umzuege_idUmzuege,Umzuege_Kunden_idKunden,Bemerkungen, UserChanged, Erstelldatum, unbenutzt, RechnungsNr, timeTransaktion, Adresse, final) values (";
            insert += "'" + Program.DateMachine(DatKalender) + "',";
            insert += this.Kartons1 + ",";
            insert += this.Flaschenkartons1 + ",";
            insert += this.Glaeserkartons1 + ",";
            insert += this.Kleiderkartons1 + ",";
            insert += this.IdUmzuege + ",";
            insert += this.IdKunden + ",";
            insert += "'" + this.Bemerkung1 + "',";
            insert += "'" + this.UserChanged1 + "',";
            insert += "'" + Program.DateMachine(DateTime.Now) + "',";
            insert += unbenutzt + ",";
            insert += "'" + Rechnungsnummer + "',";
            insert += "'" + Program.DateMachine(datTransaktion) + " " + Program.ZeitMachine(datTransaktion) + "', ";
            insert += idAdresse+",";
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
                    Id = rdr.GetInt32(0);
                }
                rdr.Close();
                Program.conn.Close();

            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Abrufen der neuen Transaktions-ID");
                throw sqlEx;
            }

            //refreshKalender();
        }

        public void updateDB (String idBearbeitend) {

            int unbenutzTemp = 0;
            if (unbenutzt) { unbenutzTemp = 1; }


            String longInsert = "UPDATE Transaktionen SET ";

            longInsert += "datTransaktion = '" + Program.DateMachine(DatKalender) + "', ";
            longInsert += "Kartons = " + Kartons1 + ", ";
            longInsert += "FlaschenKartons = " + Flaschenkartons1 + ", ";
            longInsert += "GlaeserKartons = " + Glaeserkartons1 + ", ";
            longInsert += "KleiderKartons = " + Kleiderkartons1 + ", ";
            longInsert += "Umzuege_idUmzuege = " + IdUmzuege + ", ";
            longInsert += "Umzuege_Kunden_idKunden = " + IdKunden + ", ";
            longInsert += "Bemerkungen = '" + Bemerkung1 + "', ";
            longInsert += "UserChanged = '" + UserChanged1+idBearbeitend + "', ";
            longInsert += "unbenutzt = " + unbenutzTemp + ", ";
            longInsert += "RechnungsNr = '" + Rechnungsnummer1 + "', ";
            longInsert += "timeTransaktion = '" + Program.DateMachine(datTransaktion) + " " + Program.ZeitMachine(datTransaktion) + "', ";
            longInsert += "final = " + 0 + " WHERE idTransaktionen = "+id+";";
            
            Program.absender(longInsert, "Absenden der Änderung an der Transaktion");

        }
        

        //Selbst entfernen in Vorbereitung eines Updates, dann erneutes Hinzufügen
        public Boolean KalenderRemove() {

            if (Program.getUtil().targetedDelete(DatTransaktion, "8", "Transaktion_" + Id,false)) {
                return true;
            }

            return false;
        }

        public Boolean KalenderAddDefault() {

            String Adresse = getKunde().Anschrift.Straße1 + getKunde().Anschrift.Hausnummer1 + "/r/n" + getKunde().Anschrift.PLZ1 + getKunde().Anschrift.Ort1 + "/r/n";

            Program.getUtil().kalenderEventEintrag(KalenderHeader(), KalenderString(), 8, DatKalender, DatKalender.AddHours(1));
            return true;
        }

        public Boolean KalenderAdd()
        {
            Program.getUtil().kalenderEventEintrag(KalenderHeader(), KalenderString(), 8, DatKalender, DatKalender.AddHours(1));
            return true;
        }

        private String KalenderHeader() {

            String Header = IdKunden+" " + getKunde().getVollerName();


            if (Kartons1 > 0 || Kleiderkartons1 > 0 || Glaeserkartons1 > 0 || Flaschenkartons1 > 0)
            {
                Header += " Kartonlieferung";
            }
            else
            {
                Header += " Kartonabholung";
            }

            return Header;
        }


        private String KalenderString() {
            
            String Body = "";

            //TransaktionsID in den Body
            Body += "Transaktion_" + Id + " "+Environment.NewLine;

            //Adresse in den Body
            if (IDAdresse1 != 0)
            {
                Body += Adresse.Straße1 + Adresse.Hausnummer1 + " " + Environment.NewLine + " " + Adresse.PLZ1 + Adresse.Ort1 + " " + Environment.NewLine;
            }
            else {
                Body += "Büro " + Environment.NewLine ;
            }

            // Kontaktdaten
            if (getKunde().Handy.Length > 2)
            {
                Body += "Handy: " + getKunde().Handy + " " + Environment.NewLine;
            }
            else if (getKunde().Telefon.Length > 2)
            {
                Body += "Telefon: " + getKunde().Telefon + " " + Environment.NewLine;
            }
            else {
                Body += "E-Mail: " + getKunde().Email + " " + Environment.NewLine;
            }

            if (Kartons1>0||Kleiderkartons1>0||Glaeserkartons1>0||Flaschenkartons1>0)
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
            if (Kartons1 != 0)
            {
                Body += Kartons1.ToString().Replace("-","") + " Kartons " + Environment.NewLine;
            }
            if (Glaeserkartons1 != 0)
            {
                Body += Glaeserkartons1.ToString().Replace("-", "") + " Gläserkartons " + Environment.NewLine;
            }
            if (Flaschenkartons1 != 0)
            {
                Body += Flaschenkartons1.ToString().Replace("-", "") + " Flaschenkartons " + Environment.NewLine;
            }
            if (Kleiderkartons1 != 0)
            {
                Body += Kleiderkartons1.ToString().Replace("-", "") + " Kleiderkartons " + Environment.NewLine;
            }
            

           // Body += ", Zeichen =" + Program.getBearbeitender();

            //if (radioEingang.Checked) { Body += " tatsächliche Kartonzahl nachkorrigieren"; }
            
            return Body;
        }

        private Kunde getKunde() {

            if (Kunde == null) {
                Kunde = new Kunde(IdKunden);
            }
            return Kunde;
        }

        //Getter

        public int getId() {
            return Id;
        }
    }
}
