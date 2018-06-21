using Kartonagen.Objekte;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kartonagen.CalendarAPIUtil
{
    public partial class AdressSelect3 : Form
    {
        public AdressSelect3()
        {
            InitializeComponent();

        }

        public AdressSelect3(Adresse aus, Adresse ein)
        {
            // Adressen auffüllen

            //Auszug
            textStraßeA.Text = aus.Straße1;
            textHausnummerA.Text = aus.Hausnummer1;
            textPLZA.Text = aus.PLZ1;
            textLandA.Text = aus.Land1;
            textOrtA.Text = aus.Ort1;
            textBeschreibungA.Text = aus.beschreibungsText();
            buttonA.DialogResult = DialogResult.Yes;

            //Einzug
            textStrasseB.Text = ein.Straße1;
            textHausnummerB.Text = ein.Hausnummer1;
            textPLZB.Text = ein.PLZ1;
            textLandB.Text = ein.Land1;
            textOrtB.Text = ein.Ort1;
            textBeschreibungB.Text = ein.beschreibungsText();
            buttonB.DialogResult = DialogResult.No;

            buttonBuero.DialogResult = DialogResult.Cancel;

            InitializeComponent();
        }

    }
}
