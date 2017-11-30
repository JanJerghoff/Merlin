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

namespace Mitarbeiter
{
    public partial class Stundenübersicht : Form
    {

        // Felderlisten

        List <TextBox> Namensfeld = new List<TextBox>();
        List <TextBox> Stundenfeld = new List<TextBox>();
        List <TextBox> Urlaubsfeld = new List<TextBox>();
        List <TextBox> Statusfeld = new List<TextBox>();
        List<int> Id = new List<int>();

        // Seitenzähler

        int Seite = 0;


        public Stundenübersicht()
        {
            InitializeComponent();

            FelderFuellen();

            Fuellen();
        }

        private void FelderFuellen() {

            Namensfeld.Add(textName1);
            Namensfeld.Add(textName2);
            Namensfeld.Add(textName3);
            Namensfeld.Add(textName4);
            Namensfeld.Add(textName5);
            Namensfeld.Add(textName6);
            Namensfeld.Add(textName7);
            Namensfeld.Add(textName8);
            Namensfeld.Add(textName9);
            Namensfeld.Add(textName10);
            Namensfeld.Add(textName11);
            Namensfeld.Add(textName12);
            Namensfeld.Add(textName13);
            Namensfeld.Add(textName14);
            Namensfeld.Add(textName15);
            Namensfeld.Add(textName16);
            Namensfeld.Add(textName17);
            Namensfeld.Add(textName18);
            Namensfeld.Add(textName19);
            Namensfeld.Add(textName20);
            Namensfeld.Add(textName21);
            Namensfeld.Add(textName22);
            Namensfeld.Add(textName23);
            Namensfeld.Add(textName24);
            Namensfeld.Add(textName25);

            Stundenfeld.Add(textStunden1);
            Stundenfeld.Add(textStunden2);
            Stundenfeld.Add(textStunden3);
            Stundenfeld.Add(textStunden4);
            Stundenfeld.Add(textStunden5);
            Stundenfeld.Add(textStunden6);
            Stundenfeld.Add(textStunden7);
            Stundenfeld.Add(textStunden8);
            Stundenfeld.Add(textStunden9);
            Stundenfeld.Add(textStunden10);
            Stundenfeld.Add(textStunden11);
            Stundenfeld.Add(textStunden12);
            Stundenfeld.Add(textStunden13);
            Stundenfeld.Add(textStunden14);
            Stundenfeld.Add(textStunden15);
            Stundenfeld.Add(textStunden16);
            Stundenfeld.Add(textStunden17);
            Stundenfeld.Add(textStunden18);
            Stundenfeld.Add(textStunden19);
            Stundenfeld.Add(textStunden20);
            Stundenfeld.Add(textStunden21);
            Stundenfeld.Add(textStunden22);
            Stundenfeld.Add(textStunden23);
            Stundenfeld.Add(textStunden24);
            Stundenfeld.Add(textStunden25);

            Urlaubsfeld.Add(textUrlaub1);
            Urlaubsfeld.Add(textUrlaub2);
            Urlaubsfeld.Add(textUrlaub3);
            Urlaubsfeld.Add(textUrlaub4);
            Urlaubsfeld.Add(textUrlaub5);
            Urlaubsfeld.Add(textUrlaub6);
            Urlaubsfeld.Add(textUrlaub7);
            Urlaubsfeld.Add(textUrlaub8);
            Urlaubsfeld.Add(textUrlaub9);
            Urlaubsfeld.Add(textUrlaub10);
            Urlaubsfeld.Add(textUrlaub11);
            Urlaubsfeld.Add(textUrlaub12);
            Urlaubsfeld.Add(textUrlaub13);
            Urlaubsfeld.Add(textUrlaub14);
            Urlaubsfeld.Add(textUrlaub15);
            Urlaubsfeld.Add(textUrlaub16);
            Urlaubsfeld.Add(textUrlaub17);
            Urlaubsfeld.Add(textUrlaub18);
            Urlaubsfeld.Add(textUrlaub19);
            Urlaubsfeld.Add(textUrlaub20);
            Urlaubsfeld.Add(textUrlaub21);
            Urlaubsfeld.Add(textUrlaub22);
            Urlaubsfeld.Add(textUrlaub23);
            Urlaubsfeld.Add(textUrlaub24);
            Urlaubsfeld.Add(textUrlaub25);

            Statusfeld.Add(textStatus1);
            Statusfeld.Add(textStatus2);
            Statusfeld.Add(textStatus3);
            Statusfeld.Add(textStatus4);
            Statusfeld.Add(textStatus5);
            Statusfeld.Add(textStatus6); 
            Statusfeld.Add(textStatus7);
            Statusfeld.Add(textStatus8);
            Statusfeld.Add(textStatus9);
            Statusfeld.Add(textStatus10);
            Statusfeld.Add(textStatus11);
            Statusfeld.Add(textStatus12);
            Statusfeld.Add(textStatus13);
            Statusfeld.Add(textStatus14);
            Statusfeld.Add(textStatus15);
            Statusfeld.Add(textStatus16);
            Statusfeld.Add(textStatus17);
            Statusfeld.Add(textStatus18);
            Statusfeld.Add(textStatus19);
            Statusfeld.Add(textStatus20);
            Statusfeld.Add(textStatus21);
            Statusfeld.Add(textStatus22);
            Statusfeld.Add(textStatus23);
            Statusfeld.Add(textStatus24);
            Statusfeld.Add(textStatus25);

        }

        private void Fuellen() {

            //Abfrage aller Namen
            MySqlCommand cmdRead = new MySqlCommand("SELECT Nachname, Vorname, idMitarbeiter FROM Mitarbeiter", Program.conn2);
            MySqlDataReader rdr;
            int counter = 0;
            int Min = 0;
            int counter2 = 0; // Zählt Berechnungsschleifen
            String Stunden = "";

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    if (counter < 25 * (Seite + 1) && counter >= 25*Seite)
                    {
                        Namensfeld[counter].AppendText(rdr[0].ToString() + ", " + rdr[1].ToString());
                        Id.Add(rdr.GetInt32(2));
                    }
                    counter++;
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }

            //Schleife pro Mitarbeiterfeld

            foreach (var item in Id)
            {

                Min = 0;

                // Sollstunden pro ID sammeln
                MySqlCommand cmdMin = new MySqlCommand("SELECT * FROM Stundenkonto WHERE Mitarbeiter_idMitarbeiter = "+item+";", Program.conn2);
                MySqlDataReader rdrSoll;
                
                try
                {
                    rdrSoll = cmdMin.ExecuteReader();
                    while (rdrSoll.Read())
                    {
                        Min = Min + rdrSoll.GetInt32(1);
                    }
                    rdrSoll.Close();
                }
                catch (Exception sqlEx)
                {
                    textLog.Text += sqlEx.ToString();
                    return;
                }

                textLog.AppendText(Min.ToString() + " Soll,");

                // Hier sind alle Sollminuten seid Einstellung akkumuliert.

                MySqlCommand cmdFahrt = new MySqlCommand("SELECT Start, Ende, Pause FROM Fahrt WHERE Mitarbeiter_idMitarbeiter = " + item + ";", Program.conn2);
                MySqlDataReader rdrHaben;

                try
                {
                    rdrHaben = cmdFahrt.ExecuteReader();
                    while (rdrHaben.Read())
                    {
                        Min = Min - Program.ArbeitsZeitBlock(rdrHaben.GetDateTime(0), rdrHaben.GetDateTime(1), rdrHaben.GetInt32(2));
                    }
                    rdrHaben.Close();
                }
                catch (Exception sqlEx)
                {
                    textLog.Text += sqlEx.ToString();
                    return;
                }

                // Soll - Haben gerechnet, hier sind jetzt noch fehlende Minuten positiv.

                Stundenfeld[counter2].AppendText(Min.ToString());

                //Stunden = (Math.Round((Convert.ToDouble(Min) / 60.0), 2)).ToString();
                //Stundenfeld[counter2].AppendText(Stunden);

                // Weiterzählen
                counter2++;


            }
        }
    }
}
