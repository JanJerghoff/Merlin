using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mitarbeiter.LEA_Einträge
{
    public partial class LeaAenderung : Form
    {
        // Speicher des Fahrtobjekts
        Objekte.Fahrt obj;

        public LeaAenderung()
        {
            InitializeComponent();

            //Nullen aus den Kilometern entfernen
            numericKMAnfang.Text = "";
            numericKMEnde.Text = "";
            numericKunden.Text = "";
            numericStueck.Text = "";
            numericHandbeilagen.Text = "";
            numericPause.Text = "";

            textSucheName.AutoCompleteCustomSource = Program.getAutocompleteMitarbeiter();
            textSucheName.AutoCompleteMode = AutoCompleteMode.Suggest;

            textFahrzeug.AutoCompleteCustomSource = Program.getAutocompleteFahrzeug();
            textFahrzeug.AutoCompleteMode = AutoCompleteMode.Suggest;

            textTourNeu.AutoCompleteCustomSource = Program.getAutocompleteTour();
            textTourNeu.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        public void fuellen (int Nummer) {

            // Objekt erzeugt
            obj = new Objekte.Fahrt(Nummer);

            //Pflichtfelder Füllen
            timeStart.Value = obj.Start;
            timeEnd.Value = obj.Ende;
            numericPause.Value = obj.Pause1;

            monthFahrtDatum.SelectionStart = obj.Start.Date;
            monthFahrtDatum.SelectionEnd = obj.Ende.Date;
            monthFahrtDatum.SetDate(obj.Ende.Date);

            // Sichtbarkeit der Groupboxen und Felder anpassen
            switch (Program.getTourCode(obj.Tour1))
            {
                case 0:
                    SchaltenUmzug();
                    break;

                case 1:
                    SchaltenKunden();
                    break;

                case 2:
                    SchaltenStueck();
                    break;

                case 3:
                    SchaltenBuero();
                    break;

                default:
                    break;
            }

            // Fahrzeugdaten / KM auflösen
            if (obj.Fahrzeug1 == 0) {
                checkBeifahrer.Checked = true;
                // Ausschalten Fahrzeug
                textFahrzeug.Enabled = false;
                textFahrzeug.Text = "";
            }
            else {
                numericKMAnfang.Value = obj.StartKM1;
                numericKMEnde.Value = obj.EndKM1;
                textFahrzeug.Text = Program.getFahrzeugName(obj.Fahrzeug1);
            }
            //Stückzahl / Handbeilagen
            if (obj.Stückzahl1 != 0)
            {
                numericStueck.Value = obj.Stückzahl1;
                
            }
            if (obj.Kunden1 != 0)
            {
                numericKunden.Value = obj.Kunden1;

            }
            if (obj.Beilagen1 != 0)
            {
                numericHandbeilagen.Value = obj.Beilagen1;
            }

            //Bemerkung
            textBemerkung.Text = obj.Bemerkung1;

            //Tour setzen
            textTourAlt.AppendText(Program.getTourName(obj.Tour1));

        }

        // TODO
        private void SchaltenUmzug()
        {
            numericKunden.Visible = false;
            numericStueck.Visible = false;
            numericHandbeilagen.Visible = false;
            
        }

        private void SchaltenKunden()
        {
            groupUmzug.Visible = false;
            numericStueck.Visible = false;
            numericHandbeilagen.Visible = false;
        }

        private void SchaltenStueck()
        {
            groupUmzug.Visible = false;
            numericKunden.Visible = false;
        }

        private void SchaltenBuero()
        {
            groupUmzug.Visible = false;
        }

        private void checkBeifahrer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBeifahrer.Checked) {
                numericKMAnfang.Value = 0;
                numericKMEnde.Value = 0;
                textFahrzeug.Clear();
            }
        }
    }
}
