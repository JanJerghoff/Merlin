using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Google.Apis.Calendar.v3.Data;
using System.IO;
using System.Collections;
using Kartonagen.Objekte;

namespace Kartonagen
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            this.Icon = Properties.Resources.icon_Fnb_icon;
            InitializeComponent();
        }

        public int getBearbeitender() {
            if (radioMainBenutzerRita.Checked)
            {
                return 0;
            }
            if (radioMainBenutzerJonas.Checked)
            {
                return 1;
            }
            if (radioMainBenutzerEva.Checked)
            {
                return  2;
            }
            if (radioMainBenutzerJan.Checked)
            {
                return  3;
            }
            if (radioMainBenutzerJan.Checked)
            {
                return 3;
            }
            if (radioMainBenutzerNora.Checked)
            {
                return 4;
            }

            return  8;
            
        }

        private void buttonMainKundenAdd_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            KundenAdd kundeHinzufuegen = new KundenAdd();
            kundeHinzufuegen.setBearbeiter(getBearbeitender());
            kundeHinzufuegen.Show();
        }

        private void buttonMainKundenChange_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            KundenSearch kundeSuchen = new KundenSearch();
            kundeSuchen.setBearbeiter(getBearbeitender());
            kundeSuchen.Show();
        }

        private void buttonMainUmzuegeAdd_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            UmzugAdd umzHinzufuegen = new UmzugAdd();
            umzHinzufuegen.setBearbeiter(getBearbeitender());
            umzHinzufuegen.Show();
        }

        private void buttonMainKartonagenAdd_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            TransaktionAdd TranHinzufuegen = new TransaktionAdd();
            TranHinzufuegen.setBearbeiter(getBearbeitender());
            TranHinzufuegen.Show();
        }

        private void buttonMainUmzuegeChange_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            UmzuegeSearch umzAendern = new UmzuegeSearch();
            umzAendern.setBearbeiter(getBearbeitender());
            umzAendern.Show();
        }

        private void buttonMainKartonagenChange_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            TransaktionenSearch transaktionenSuche = new TransaktionenSearch();
            transaktionenSuche.setBearbeiter(getBearbeitender());
            transaktionenSuche.Show();
        }

        private void buttonMainKundenShow_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            KundenUebersicht kundenShow = new KundenUebersicht();
            kundenShow.setBearbeiter(getBearbeitender());
            kundenShow.letztenX();
            kundenShow.Show();
        }

        private void buttonMainUmzuegeShow_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            UmzuegeUebersicht UmzuegeShow = new UmzuegeUebersicht();
            UmzuegeShow.setBearbeiter(getBearbeitender());
            UmzuegeShow.letztenX("u.idUmzuege DESC LIMIT 50;");
            UmzuegeShow.Show();
        }

        private void buttonSonderabfragen_Click(object sender, EventArgs e)
        {
            //Sonderabfragen Sonderabfragen = new Sonderabfragen();
            //Sonderabfragen.setBearbeiter(getBearbeitender());
            //Sonderabfragen.Show();

            if (radioMainBenutzerJan.Checked)
            {
                //convertNotation();
                convertAdressen();
            }
            else {
                textMainLog.AppendText("Bitte nicht Benutzen, nur für Adminfunktionen vorgesehen! "+Environment.NewLine);
            }
            
        }

        private void convertAdressen()
        {
            
            ArrayList nummern = new ArrayList();
            
            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }
                MySqlCommand cmdReadUmzug = new MySqlCommand("SELECT idUmzuege FROM Umzuege WHERE AdresseAuszug = 0 OR AdresseEinzug = 0;", Program.conn);
                MySqlDataReader rdrNummer = cmdReadUmzug.ExecuteReader();
                while (rdrNummer.Read())
                {
                    nummern.Add(rdrNummer.GetInt32(0));
                }
                rdrNummer.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Nummern \r\n Bereits dokumentiert.");
                return;
            }

            foreach (var item in nummern) {
                
                int tempAuszug = 0;
                int tempEinzug = 0;
                Adresse Auszug;
                Adresse Einzug;

                string straße;
                string hausnummer;
                string ort;
                string pLZ;
                string land;
                int aufzug;
                string stockwerke;
                string haustyp;
                int hVZ;
                int laufmeter;
                int aussenAufzug;

                // Welche Adressen unbelegt?
                try
                {
                    if (Program.conn.State != ConnectionState.Open)
                    {
                        Program.conn.Open();
                    }

                    MySqlCommand cmdReadString = new MySqlCommand("SELECT AdresseAuszug, AdresseEinzug FROM Umzuege WHERE idUmzuege = " + item + ";", Program.conn);
                    MySqlDataReader rdrStrings = cmdReadString.ExecuteReader();
                    while (rdrStrings.Read())
                    {
                        tempAuszug = rdrStrings.GetInt32(0);
                        tempEinzug = rdrStrings.GetInt32(1);
                    }
                    rdrStrings.Close();
                    Program.conn.Close();
                }
                catch (Exception sqlEx)
                {
                    Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Nummern \r\n Bereits dokumentiert.");
                    return;
                }

                //Adressen belegen

                if (tempAuszug == 0) {

                    try
                    {
                        if (Program.conn.State != ConnectionState.Open)
                        {
                            Program.conn.Open();
                        }

                        straße = String.Empty;
                        hausnummer = String.Empty;
                        pLZ = String.Empty;
                        ort = String.Empty;
                        land = String.Empty;
                        aufzug = 0;
                        land = String.Empty;
                        stockwerke = String.Empty;
                        haustyp = String.Empty;
                        hVZ = 0;
                        laufmeter = 0;
                        aussenAufzug = 0;

                        MySqlCommand cmdReadString = new MySqlCommand("SELECT StraßeA, HausnummerA, PLZA, OrtA, LandA, AufzugA, StockwerkeA, HausTypA, HVZA, LaufmeterA, AussenAufzugA FROM Umzuege WHERE idUmzuege = " + item + ";", Program.conn);
                        MySqlDataReader rdrStrings = cmdReadString.ExecuteReader();
                        while (rdrStrings.Read())
                        {
                            straße = rdrStrings.GetString(0);
                            hausnummer = rdrStrings.GetString(1);
                            pLZ = rdrStrings.GetString(2);
                            ort = rdrStrings.GetString(3);
                            land = rdrStrings.GetString(4);
                            aufzug = rdrStrings.GetInt32(5);
                            stockwerke = rdrStrings.GetString(6);
                            haustyp = rdrStrings.GetString(7);
                            hVZ = rdrStrings.GetInt32(8);
                            laufmeter = rdrStrings.GetInt32(9);
                            aussenAufzug = rdrStrings.GetInt32(10);
                        }
                        rdrStrings.Close();
                        Program.conn.Close();
                    }
                    catch (Exception sqlEx)
                    {
                        Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Nummern \r\n Bereits dokumentiert.");
                        return;
                    }

                    Console.WriteLine("Auszug wird angelegt mit der Straße "+straße);

                    Auszug = new Adresse(straße,hausnummer,ort,pLZ,land,aufzug,stockwerke,haustyp, hVZ,laufmeter, aussenAufzug);

                    String update = "UPDATE Umzuege SET AdresseAuszug = " + Auszug.IDAdresse1 + " WHERE idUmzuege = " + item + ";";

                    Program.absender(update, "Datenbankänderung, einfügen von AdressId " + Auszug.IDAdresse1 + " als Auszugsadresse in Umzug " + item);

                }

                if (tempEinzug == 0)
                {

                    try
                    {
                        if (Program.conn.State != ConnectionState.Open)
                        {
                            Program.conn.Open();
                        }

                        straße = String.Empty;
                        hausnummer = String.Empty;
                        pLZ = String.Empty;
                        ort = String.Empty;
                        land = String.Empty;
                        aufzug = 0;
                        land = String.Empty;
                        stockwerke = String.Empty;
                        haustyp = String.Empty;
                        hVZ = 0;
                        laufmeter = 0;
                        aussenAufzug = 0;

                        MySqlCommand cmdReadString = new MySqlCommand("SELECT StraßeB, HausnummerB, PLZB, OrtB, LandB, AufzugB, StockwerkeB, HausTypB, HVZB, LaufmeterB, AussenAufzugB FROM Umzuege WHERE idUmzuege = " + item + ";", Program.conn);
                        MySqlDataReader rdrStrings = cmdReadString.ExecuteReader();
                        while (rdrStrings.Read())
                        {
                            straße = rdrStrings.GetString(0);
                            hausnummer = rdrStrings.GetString(1);
                            pLZ = rdrStrings.GetString(2);
                            ort = rdrStrings.GetString(3);
                            land = rdrStrings.GetString(4);
                            aufzug = rdrStrings.GetInt32(5);
                            stockwerke = rdrStrings.GetString(6);
                            haustyp = rdrStrings.GetString(7);
                            hVZ = rdrStrings.GetInt32(8);
                            laufmeter = rdrStrings.GetInt32(9);
                            aussenAufzug = rdrStrings.GetInt32(10);
                        }
                        rdrStrings.Close();
                        Program.conn.Close();
                    }
                    catch (Exception sqlEx)
                    {
                        Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Nummern \r\n Bereits dokumentiert.");
                        return;
                    }

                    Einzug = new Adresse(straße, hausnummer, ort, pLZ, land, aufzug, stockwerke, haustyp, hVZ, laufmeter, aussenAufzug);

                    String update = "UPDATE Umzuege SET AdresseEinzug = " + Einzug.IDAdresse1 + " WHERE idUmzuege = " + item + ";";

                    Program.absender(update, "Datenbankänderung, einfügen von AdressId " + Einzug.IDAdresse1 + " als Einzugsadresse in Umzug " + item);

                }

            }
        }

        private void convertNotation()
        {
            

            ArrayList nummern = new ArrayList();
            String tempStringA = "";
            String tempStringB = "";
            String ReturnStringA = "";
            String ReturnstringB = "";

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }
                MySqlCommand cmdReadUmzug = new MySqlCommand("SELECT idUmzuege FROM Umzuege;", Program.conn);
                MySqlDataReader rdrNummer = cmdReadUmzug.ExecuteReader();
                while (rdrNummer.Read())
                {
                    nummern.Add(rdrNummer.GetInt32(0));
                }
                rdrNummer.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Nummern \r\n Bereits dokumentiert.");
                return;
            }

            foreach (var item in nummern)
            {
                
                try
                {
                    if (Program.conn.State != ConnectionState.Open)
                    {
                        Program.conn.Open();
                    }
                    MySqlCommand cmdReadString = new MySqlCommand("SELECT StockwerkeA,StockwerkeB FROM Umzuege WHERE idUmzuege = " + item + ";", Program.conn);
                    MySqlDataReader rdrStrings = cmdReadString.ExecuteReader();
                    while (rdrStrings.Read())
                    {
                        tempStringA = rdrStrings.GetString(0);
                        tempStringB = rdrStrings.GetString(1);
                    }
                    rdrStrings.Close();
                    Program.conn.Close();
                }
                catch (Exception sqlEx)
                {
                    Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Nummern \r\n Bereits dokumentiert.");
                    return;
                }

                string[] tempA = tempStringA.Split(',');
                string[] concatA = new String[] {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", ""};
                string[] tempB = tempStringB.Split(',');
                string[] concatB = new String[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0","" };

                foreach (var A in tempA)
                {
                    if (A.Contains("K"))
                    {
                        concatA[0] = "1";
                    }
                    else if (A.Contains("EG"))
                    {
                        concatA[1] = "1";
                    }
                    else if (A.Contains("DB"))
                    {
                        concatA[10] = "1";
                    }
                    else if (A.Contains("MA"))
                    {
                        concatA[4] = "1";
                    }
                    else if (A.Contains("ST"))
                    {
                        concatA[3] = "1";
                    }
                    else if (A.Contains("HP"))
                    {
                        concatA[2] = "1";
                    }
                    else if (A.Contains("1"))
                    {
                        concatA[5] = "1";
                    }
                    else if (A.Contains("2"))
                    {
                        concatA[6] = "1";
                    }
                    else if (A.Contains("3"))
                    {
                        concatA[7] = "1";
                    }
                    else if (A.Contains("4"))
                    {
                        concatA[8] = "1";
                    }
                    else if (A.Contains("5"))
                    {
                        concatA[9] = "1";
                    }
                    else
                    {
                        concatA[11] = "-"+A;
                    }
                }

                foreach (var A in tempB)
                {
                    if (A.Contains("K"))
                    {
                        concatB[0] = "1";
                    }
                    else if (A.Contains("EG"))
                    {
                        concatB[1] = "1";
                    }
                    else if (A.Contains("DB"))
                    {
                        concatB[10] = "1";
                    }
                    else if (A.Contains("MA"))
                    {
                        concatB[4] = "1";
                    }
                    else if (A.Contains("ST"))
                    {
                        concatB[3] = "1";
                    }
                    else if (A.Contains("HP"))
                    {
                        concatB[2] = "1";
                    }
                    else if (A.Contains("1"))
                    {
                        concatB[5] = "1";
                    }
                    else if (A.Contains("2"))
                    {
                        concatB[6] = "1";
                    }
                    else if (A.Contains("3"))
                    {
                        concatB[7] = "1";
                    }
                    else if (A.Contains("4"))
                    {
                        concatB[8] = "1";
                    }
                    else if (A.Contains("5"))
                    {
                        concatB[9] = "1";
                    }
                    else
                    {
                        concatB[11] = "-" + A;
                    }
                }

                Console.WriteLine("Umzugsnummer "+item+" A vorher = "+tempStringA+" A Nachher = "+String.Join("",concatA)+"/r/n B Vorher = "+tempStringB+" B nacher = "+ String.Join("", concatA) + " /r/n");

                String up = "UPDATE Umzuege SET StockwerkeA = '" + String.Join("", concatA) + "', StockwerkeB = '" + String.Join("", concatA) + "' WHERE idUmzuege = " + item + ";";
                Program.absender(up, "update Bitstring");

            } // End Foreach

        }

        private void buttonLaufzettel_Click(object sender, EventArgs e)
        {
            Laufzetteldrucker laufzettel = new Laufzetteldrucker();
            laufzettel.Show();
        }

        private void buttonUebersichtKartons_Click(object sender, EventArgs e)
        {
            TransaktionenUebersicht uber = new TransaktionenUebersicht();
            uber.Show();
        }

        private void buttonLaufKarton_Click(object sender, EventArgs e)
        {
            LaufzettelKartons lk = new LaufzettelKartons();
            lk.Show();
        }

        private void buttonErinnerungen_Click(object sender, EventArgs e)
        {
            ausstehendeKartonagen er = new ausstehendeKartonagen();
            er.Show();

        }

        private void PDFRead_Click(object sender, EventArgs e)
        {

            // Testsektion einlesen d. Umzuege
            string[] pdfs = Directory.GetFiles(Program.getMitnehmPfad());

            foreach (var item in pdfs)
            {
                PDFInput.readUmzug(item);
            }
        
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.getUtil().KalenderDBCheck(textMainLog);
        }

        private void radioMainBenutzerRita_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
