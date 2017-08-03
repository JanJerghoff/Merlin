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
    }
}
