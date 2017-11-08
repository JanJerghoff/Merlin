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
    }
}
