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
    public partial class TempStartZeitstand : Form
    {
        public TempStartZeitstand()
        {
            InitializeComponent();
        }

        private void buttonNeu_Click(object sender, EventArgs e)
        {
            

            if (numericID.Value == 0)
            {
                textLog.Text = "Gültigen Mitarbeiter wählen";
                reset();
            }
            else
            {                
                string com = "INSERT INTO Stundenkonto (SollMinuten, Monat, Mitarbeiter_IdMitarbeiter) VALUES (" + decimal.ToInt32(Math.Round(numericSollstunden.Value*60)) + ", '" + Program.DateMachine(Program.getMonat(dateZeitpunkt.Value)) + "', " + decimal.ToInt32(numericID.Value) + ");";
                Program.absender(com, "Speichern des händischen Zeitkontos");
                textLog.Text = "Mitarbeiter ID "+numericID.Value.ToString()+" hinzugefügt";
                reset();
            }

        }

        private void reset() {
            numericID.Value = 0;
            numericSollstunden.Value = 0;            
        }
    }
}
