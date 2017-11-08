﻿using MySql.Data.MySqlClient;
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
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        public int getBearbeitender()
        {
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
                return 2;
            }
            if (radioMainBenutzerJan.Checked)
            {
                return 3;
            }

            return 4;

        }

        private void buttonStammdaten_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                //textStartLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            Stammdaten stamm = new Stammdaten();
            stamm.setBearbeitend(getBearbeitender());
            stamm.Show();
        }

        private void mitarbeiterUpdate() {

            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Stundenkonto s, Mitarbeiter m WHERE s.Mitarbeiter_idMitarbeiter = m.idMitarbeiter AND m.Ausgeschieden != '2017-01-01';", Program.conn2);
            MySqlDataReader rdr;

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    // Heute schon geupdated
                    if (rdr.GetDateTime(4).Date == DateTime.Now.Date)
                    {
                        break;
                    }
                    // Update, checken ob neuer Monat
                    else {
                        int diff = Program.MonatsDifferenz(rdr.GetDateTime(4), DateTime.Now.Date);
                        if (diff >= 1)
                        {
                            int Stunden;
                        }
                        // Letztes Update noch keinen Monat her
                        else {  
                            break;
                        }
                    }

                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                
                return;
            }

        }

        private void buttonEintragUmzug_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textStartLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            LEA_Umzug umzug = new LEA_Umzug();
            umzug.setBearbeitend(getBearbeitender());
            umzug.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textStartLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            LEA_Kundenzahl switcher = new LEA_Kundenzahl();
            switcher.setBearbeitend(getBearbeitender());
            switcher.Show();
        }

        private void buttonTourenFahrzeuge_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textStartLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            Tour_Fahrzeug TF = new Tour_Fahrzeug();
            TF.setBearbeitend(getBearbeitender());
            TF.Show();
        }

        private void buttonMehrfachUmzug_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textStartLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            LEA_Mehrfach_Umzug MU = new LEA_Mehrfach_Umzug();
            MU.setBearbeitend(getBearbeitender());
            MU.Show();
        }

        private void buttonStundenübersicht_Click(object sender, EventArgs e)
        {            
            Stundenübersicht SU = new Stundenübersicht();
            SU.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LEA_Mitarbeiter_Details Det = new LEA_Mitarbeiter_Details();
            Det.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Repetoire Rep = new Repetoire();
            Rep.Show();
        }

        private void buttonEintragTabelle_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textStartLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            LEA_Fahrtentabelle FT = new LEA_Fahrtentabelle();
            FT.setBearbeitend(getBearbeitender());
            FT.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TempStartZeitstand x = new TempStartZeitstand();
            x.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            UebersichtFahrzeuge ue = new UebersichtFahrzeuge();
            ue.Show();
        }
    }
}
