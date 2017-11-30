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
    public partial class LEA_UrlaubFeiertag : Form
    {
        public LEA_UrlaubFeiertag()
        {
            InitializeComponent();

            // Autocomplete vorlegen
            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteMitarbeiter();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        int idBearbeitend;

        public void setBearbeitend(int code)
        {
            idBearbeitend = code;
        }

        private void KomponentenSteuerung() {       //Aktiviert / Deaktiviert Elemente je nach Radiobuttons

            if (radioUrlaub.Checked || radioKrankheit.Checked)
            {
                textSucheName.Enabled = true;
            }
            else if (radioFeiertag.Enabled)
            {
                textSucheName.Text = "";
                textSucheName.Enabled = false;
            }
        }

        private void buttonAbsenden_Click(object sender, EventArgs e)
        {

            Dictionary<int, int> Sollminuten = Program.Sollminute();
            DateTime morgen = new DateTime(2017, 1, 1, 1, 0, 0);

            if (radioFeiertag.Checked)
            {
                foreach (var item in Sollminuten.Keys)
                {
                    string insert = "";

                    insert += "INSERT INTO Fahrt (Mitarbeiter_idMitarbeiter, Start, Ende, Pause, AnfangsKM, EndKM, Bemerkung, UserChanged, Tour_idTour) VALUES (";
                    
                    int minuten = 0;
                    Sollminuten.TryGetValue(item, out minuten);

                    insert += item + ", ";
                    insert += "'" + Program.DateTimeMachine(morgen, monthFahrtDatum.SelectionStart) + "', ";
                    insert += "'" + Program.DateTimeMachine(morgen.AddMinutes(minuten), monthFahrtDatum.SelectionStart) + "', ";
                    insert += 0 + ", ";
                    insert += 0 + ", ";
                    insert += 0 + ", ";
                    insert += "'Feiertag', ";
                    insert += idBearbeitend + ", ";
                    insert += "64); ";

                    Program.absender(insert, "Einfügen Feiertags-Fahrt für Mitarbeiter " + item);

                    textStartLog.AppendText("Feiertag erfolgreich eingetragen! \r\n ");
                }
            }

            else if (radioKrankheit.Checked || radioUrlaub.Checked || radioFeiertagEinzel.Checked) {

                string insert = "";
                int mitarbeiter = Program.getMitarbeiter(textSucheName.Text);
                int sollminuten;
                Sollminuten.TryGetValue(mitarbeiter, out sollminuten);

                //Bestimmung Krankheit/Urlaub
                int tournummer;
                string bemerk = "";
                if (radioKrankheit.Checked)
                {
                    tournummer = 53;
                    bemerk = "Krankheit";
                }
                else if (radioUrlaub.Checked)
                {
                    tournummer = 54;
                    bemerk = "Urlaub";
                }
                else if (radioFeiertagEinzel.Checked)
                {
                    tournummer = 64;
                    bemerk = "Feiertag";
                }

                insert += "INSERT INTO Fahrt (Mitarbeiter_idMitarbeiter, Start, Ende, Pause, AnfangsKM, EndKM, Bemerkung, UserChanged, Tour_idTour) VALUES (";
                
                insert += mitarbeiter + ", ";
                insert += "'" + Program.DateTimeMachine(morgen, monthFahrtDatum.SelectionStart) + "', ";
                insert += "'" + Program.DateTimeMachine(morgen.AddMinutes(sollminuten), monthFahrtDatum.SelectionStart) + "', ";
                insert += 0 + ", ";
                insert += 0 + ", ";
                insert += 0 + ", ";
                insert += "'', ";
                insert += idBearbeitend + ", ";
                insert += tournummer+"); ";

                Program.absender(insert, "Einfügen Urlaub / Krankheit für Mitarbeiter " + mitarbeiter);

                textStartLog.AppendText("Eintrag für Mitarbeiter "+mitarbeiter+" erfolgreich! \r\n ");
            }
            
            

        }

        private void radioUrlaub_CheckedChanged(object sender, EventArgs e)
        {
            KomponentenSteuerung();
        }

        private void radioKrankheit_CheckedChanged(object sender, EventArgs e)
        {
            KomponentenSteuerung();
        }

        private void radioFeiertag_CheckedChanged(object sender, EventArgs e)
        {
            KomponentenSteuerung();
        }
    }
}
