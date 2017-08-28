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
    public partial class TransaktionErinnerung : Form
    {

        string UserChanged;
        int id;
        int idBearbeitend;

        public TransaktionErinnerung()
        {
            InitializeComponent();
        }

        public void set(string UserChanged, string Rechnungsnummer, string zeit, string name, string adresse, string bemerkung, int id, int kartons, int Flaschenkartons, int Glaeserkartons, int Kleiderkartons, int idBearbeitend) {

            this.UserChanged = UserChanged;
            this.id = id;

            textZeit.AppendText(zeit);
            textKunde.AppendText(name);
            textAdresse.AppendText(adresse);
            textBemerkung.AppendText(bemerkung);
            textRechnungsnummer.AppendText(Rechnungsnummer);

            numericKarton.Value = kartons;
            numericGlaeserkarton.Value = Glaeserkartons;
            numericKleiderKarton.Value = Kleiderkartons;
            numericFlaschenKarton.Value = Flaschenkartons;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSpeichern_Click(object sender, EventArgs e)
        {
            if (textRechnungsnummer.Text == "") {
                var box = MessageBox.Show("Es muss eine Rechnungsnummer gesetzt sein, um eine Transaktion abzuschließen \r\n Bitte erneut versuchen", "Abgebrochen");
                return;
            }

            String update = "UPDATE Transaktionen SET Kartons = " + numericKarton.Value + ", " +
                "FlaschenKartons = " + numericFlaschenKarton.Value + ", " +
                "GlaeserKartons = " + numericGlaeserkarton.Value + ", " +
                "KleiderKartons = " + numericKleiderKarton.Value + ", " +
                "Bemerkungen = '" + textBemerkung.Text + "', " +
                "GlaeserKartons = " + numericGlaeserkarton.Value + ", " +
                "RechnungsNr = '" + textRechnungsnummer.Text + "', " +
                "UserChanged = '" + UserChanged + idBearbeitend + "', " +
                "final = 1 WHERE idTransaktionen = " + id + ";";

            Program.absender(update);
        }
    }
}
