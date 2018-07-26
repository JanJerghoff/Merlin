using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kartonagen
{
    public partial class ausstehendeKartonagen : Form
    {
        public ausstehendeKartonagen()
        {
            InitializeComponent();

            LinkedList<int> Kundenkanidaten = new LinkedList<int>();


            // Wer kommt in Frage
            MySqlCommand cmdReadKunde = new MySqlCommand("Select Umzuege_Kunden_idKunden from Transaktionen where datTransaktion < '"+ Program.DateMachine(DateTime.Now.AddMonths(-11)) +"' group by Umzuege_Kunden_idKunden Order By datTransaktion ASC;", Program.conn);
            MySqlDataReader rdrKunde;

            try
            {
                rdrKunde = cmdReadKunde.ExecuteReader();
                while (rdrKunde.Read())
                {
                    Kundenkanidaten.AddLast(rdrKunde.GetInt32(0));
                }
                rdrKunde.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Personendaten \r\n Bereits dokumentiert.");
                return;
            }

            textMainLog.AppendText("Sql durch "+cmdReadKunde.CommandText);

            // Wer ist noch aktiv / kommt nichtmehr in Frage
            MySqlCommand cmdReadKundeneu = new MySqlCommand("Select Umzuege_Kunden_idKunden from Transaktionen where datTransaktion > '" + Program.DateMachine(DateTime.Now.AddMonths(-11)) + "' group by Umzuege_Kunden_idKunden", Program.conn);
            MySqlDataReader rdrKundeneu;

            try
            {
                rdrKundeneu = cmdReadKundeneu.ExecuteReader();
                while (rdrKundeneu.Read())
                {
                    Kundenkanidaten.Remove(rdrKundeneu.GetInt32(0));
                }
                rdrKundeneu.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Personendaten \r\n Bereits dokumentiert.");
                return;
            }

            // Datenbeschaffung zu denen, die in Frage kommen

            foreach (var item in Kundenkanidaten)
            {

                int kartonsTemp = 0;
                int flaschenTemp = 0;
                int glaeserTemp = 0;
                int kleiderTemp = 0;
                DateTime dateTemp = DateTime.Now;

                String Kundenname = "";
                String Telefonnummer = "";
                String Email = "";

                // Abfrage Stückzahlen und Datum

                MySqlCommand cmdReadKundespez = new MySqlCommand("Select * from Transaktionen where Umzuege_Kunden_idKunden = "+item+ " order by datTransaktion ASC;", Program.conn);
                MySqlDataReader rdrKundespez;

                try
                {
                    rdrKundespez = cmdReadKundespez.ExecuteReader();
                    while (rdrKundespez.Read())
                    {
                        dateTemp = rdrKundespez.GetDateTime(1);

                        //Ignorieren von Kaufkartons
                        if (rdrKundespez.GetInt32(11) != 2)
                        {
                            kartonsTemp += rdrKundespez.GetInt32(2);
                            flaschenTemp += rdrKundespez.GetInt32(3);
                            glaeserTemp += rdrKundespez.GetInt32(4);
                            kleiderTemp += rdrKundespez.GetInt32(5);
                        }
                    }
                    rdrKundespez.Close();
                }
                catch (Exception sqlEx)
                {
                    Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Transaktionsdaten zum Kunden "+item+" \r\n Bereits dokumentiert.");
                    return;
                }

                //Abfrage restliche Kundendaten

                MySqlCommand cmdReadKundespez2 = new MySqlCommand("Select * from Kunden where idKunden = " + item + ";", Program.conn);
                MySqlDataReader rdrKundespez2;

                try
                {
                    rdrKundespez2 = cmdReadKundespez2.ExecuteReader();
                    while (rdrKundespez2.Read())
                    {
                        Kundenname += rdrKundespez2.GetString(1);
                        Kundenname += " "+rdrKundespez2.GetString(2);
                        Kundenname += " " + rdrKundespez2.GetString(3);

                        if (rdrKundespez2.GetString(5).Length > 1)
                        {
                            Telefonnummer = rdrKundespez2.GetString(5);
                        }
                        else {
                            Telefonnummer = rdrKundespez2.GetString(4);
                        }
                        Email = rdrKundespez2.GetString(6);

                    }
                    rdrKundespez2.Close();
                }
                catch (Exception sqlEx)
                {
                    Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Transaktionsdaten zum Kunden " + item + " \r\n Bereits dokumentiert.");
                    return;
                }

                // Eintrag der Zeile nur wenn Kartons vorhanden

                if (kartonsTemp==0 && kleiderTemp == 0 && flaschenTemp == 0 && glaeserTemp == 0) {
                    continue;
                }

                //DataGridViewRow temp = (DataGridViewRow) dataGridausstehendeKartonagen.RowTemplate.Clone();

                String[] rowtemp = {item.ToString(),Kundenname,Email, Telefonnummer,kartonsTemp.ToString(), flaschenTemp.ToString(), glaeserTemp.ToString(),kleiderTemp.ToString(),dateTemp.ToShortDateString()};

                dataGridausstehendeKartonagen.Rows.Add(rowtemp);
            }

            // Ordnen
        }
    }
}
