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
            numericKundenStueck.Text = "";
            numericHandbeilagen.Text = "";
            numericPause.Text = "";
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
                numericKundenStueck.Value = obj.Stückzahl1;
                
            }

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
        }

        private void SchaltenUmzug()
        {

        }

        private void SchaltenKunden()
        {

        }

        private void SchaltenStueck()
        {

        }

        private void SchaltenBuero()
        {

        }

    }
}
