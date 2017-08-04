using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Google.Apis.Calendar.v3.Data;

namespace Kartonagen
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        public int getBearbeitender() {
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
                return  2;
            }
            if (radioMainBenutzerJan.Checked)
            {
                return  3;
            }
            
            return  4;
            
        }

        private void buttonMainKundenAdd_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            KundenAdd kundeHinzufuegen = new KundenAdd();
            kundeHinzufuegen.setBearbeiter(getBearbeitender());
            kundeHinzufuegen.Show();
        }

        private void buttonMainKundenChange_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            KundenSearch kundeSuchen = new KundenSearch();
            kundeSuchen.setBearbeiter(getBearbeitender());
            kundeSuchen.Show();
        }

        private void buttonMainUmzuegeAdd_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            UmzugAdd umzHinzufuegen = new UmzugAdd();
            umzHinzufuegen.setBearbeiter(getBearbeitender());
            umzHinzufuegen.Show();
        }

        private void buttonMainKartonagenAdd_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            TransaktionAdd TranHinzufuegen = new TransaktionAdd();
            TranHinzufuegen.setBearbeiter(getBearbeitender());
            TranHinzufuegen.Show();
        }

        private void buttonMainUmzuegeChange_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            UmzuegeSearch umzAendern = new UmzuegeSearch();
            umzAendern.setBearbeiter(getBearbeitender());
            umzAendern.Show();
        }

        private void buttonMainKartonagenChange_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            TransaktionenSearch transaktionenSuche = new TransaktionenSearch();
            transaktionenSuche.setBearbeiter(getBearbeitender());
            transaktionenSuche.Show();
        }

        private void buttonMainKundenShow_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            KundenUebersicht kundenShow = new KundenUebersicht();
            kundenShow.setBearbeiter(getBearbeitender());
            kundenShow.letztenX();
            kundenShow.Show();
        }

        private void buttonMainUmzuegeShow_Click(object sender, EventArgs e)
        {
            if (radioMainBenutzerSonst.Checked)
            {
                textMainLog.AppendText("Bitte Angeben, wer bearbeiten möchte \r\n");
                return;
            }
            UmzuegeUebersicht UmzuegeShow = new UmzuegeUebersicht();
            UmzuegeShow.setBearbeiter(getBearbeitender());
            UmzuegeShow.letztenX("u.idUmzuege DESC LIMIT 50;");
            UmzuegeShow.Show();
        }

        private void buttonSonderabfragen_Click(object sender, EventArgs e)
        {
            Sonderabfragen Sonderabfragen = new Sonderabfragen();
            Sonderabfragen.setBearbeiter(getBearbeitender());
            Sonderabfragen.Show();
            
        }

        private void buttonLaufzettel_Click(object sender, EventArgs e)
        {
            Laufzetteldrucker laufzettel = new Laufzetteldrucker();
            laufzettel.Show();
        }

        private void buttonUebersichtKartons_Click(object sender, EventArgs e)
        {
            TransaktionenUebersicht uber = new TransaktionenUebersicht();
            uber.Show();
        }

        private void buttonLaufKarton_Click(object sender, EventArgs e)
        {
            LaufzettelKartons lk = new LaufzettelKartons();
            lk.Show();
        }

        private void buttonErinnerungen_Click(object sender, EventArgs e)
        {
            Erinnerungen er = new Erinnerungen();
            er.setIdBearbeitend(getBearbeitender());
            er.Show();
        }
    }
}
