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

namespace Kartonagen
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
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

            convertNotation();
            
        }

        private void convertNotation()
        {

            MySqlCommand cmdReadUmzug = new MySqlCommand("SELECT idUmzuege FROM Umzuege;", Program.conn);
            MySqlDataReader rdrNummer;

            ArrayList nummern = new ArrayList();
            String tempStringA = "";
            String tempStringB = "";
            String ReturnStringA = "";
            String ReturnstringB = "";

            try
            {
                rdrNummer = cmdReadUmzug.ExecuteReader();
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

            foreach (var item in nummern)
            {

                MySqlCommand cmdReadString = new MySqlCommand("SELECT StockwerkeA,StockwerkeB FROM Umzuege WHERE idUmzuege = "+item+";", Program.conn);
                MySqlDataReader rdrStrings;

                try
                {
                    rdrStrings = cmdReadString.ExecuteReader();
                    while (rdrStrings.Read())
                    {
                        tempStringA = rdrStrings.GetString(0);
                        tempStringB = rdrStrings.GetString(1);
                    }
                    rdrStrings.Close();
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
            //List<int> test = new List<int>();

            //MySqlCommand cmdReadKunde = new MySqlCommand("SELECT idUmzuege FROM Umzuege WHERE datBesichtigung = '2016-10-12';", Program.conn);
            //MySqlDataReader rdrKunde;
            
            //try
            //{
            //    rdrKunde = cmdReadKunde.ExecuteReader();
            //    while (rdrKunde.Read())
            //    {
            //        test.Add(rdrKunde.GetInt32(0));
            //    }
            //    rdrKunde.Close();
            //}
            //catch (Exception sqlEx)
            //{
            //    Program.FehlerLog(sqlEx.ToString(), "Abrufen der Umzugsnummern zum Besichtigungsdatum (für das Ausliefern der PDFs)");
            //}

            //// Drucken für die einzelnen Besichtigungen
            //string sum = "";
                      

            // Testsektion einlesen d. Umzuege
            string[] pdfs = Directory.GetFiles(Program.getMitnehmPfad());

            foreach (var item in pdfs)
            {
                PDFInput.readUmzug(item);
            }
        
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.getUtil().KalenderDBCheck();
        }
    }
}
