﻿using System;
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
    public partial class TransaktionErinnerung : Form
    {

        string UserChanged;
        int id;
        Boolean changed = false;
        int idBearbeitend;

        public TransaktionErinnerung()
        {
            InitializeComponent();
        }

        public void set(string UserChanged, string zeit, string name, string adresse, string bemerkung, int id, int kartons, int Flaschenkartons, int Glaeserkartons, int Kleiderkartons) {

            this.UserChanged = UserChanged;
            this.id = id;

            textZeit.AppendText(zeit);
            textKunde.AppendText(name);
            textAdresse.AppendText(adresse);
            textBemerkung.AppendText(bemerkung);

            numericKarton.Value = kartons;
            numericGlaeserkarton.Value = Glaeserkartons;
            numericKleiderKarton.Value = Kleiderkartons;
            numericFlaschenKarton.Value = Flaschenkartons;
        }

        private void numericKarton_ValueChanged(object sender, EventArgs e)
        {
            changed = true;
        }

        private void numericFlaschenKarton_ValueChanged(object sender, EventArgs e)
        {
            changed = true;
        }

        private void numericGlaeserkarton_ValueChanged(object sender, EventArgs e)
        {
            changed = true;
        }

        private void numericKleiderKarton_ValueChanged(object sender, EventArgs e)
        {
            changed = true;
        }

        private void textBemerkung_TextChanged(object sender, EventArgs e)
        {
            changed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSpeichern_Click(object sender, EventArgs e)
        {
            String update = "UPDATE Transaktionen SET Kartons = " + numericKarton.Value + ", " +
                "FlaschenKartons = " + numericFlaschenKarton.Value + ", " +
                "GlaeserKartons = " + numericGlaeserkarton.Value + ", " +
                "KleiderKartons = " + numericKleiderKarton.Value + ", " +
                "Bemerkungen = '" + textBemerkung.Text + "', " +
                "GlaeserKartons = " + numericGlaeserkarton.Value + ", " +
                "UserChanged = '" + UserChanged + idBearbeitend + "', " +
                "final = 1 WHERE idTransaktionen = " + id + ";";
        }
    }
}
