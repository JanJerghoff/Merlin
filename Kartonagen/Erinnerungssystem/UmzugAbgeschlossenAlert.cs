using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kartonagen.Erinnerungssystem
{
    class UmzugAbgeschlossenAlert : AbstractAlert
    {

        int idUmzugsfortschritt;
        int idBearbeitend;
        String UserChanged;
        String Umzugsdatum;
        String KundenName;
        String Einzugsadresse;
        String Auszugsadresse;
        String FortschrittsBemerkung;

        public UmzugAbgeschlossenAlert(int idUmzugsfortschritt, int idBearbeitend, String UserChanged, String Umzugsdatum, String KundenName, String Einzugsadresse, String Auszugsadresse, String FortschrittsBemerkung)
        {
            this.idUmzugsfortschritt = idUmzugsfortschritt;
            this.idBearbeitend = idBearbeitend;
            this.UserChanged = UserChanged;
            this.Umzugsdatum = Umzugsdatum;
            this.KundenName = KundenName;
            this.Einzugsadresse = Einzugsadresse;
            this.Auszugsadresse = Auszugsadresse;
            this.FortschrittsBemerkung = FortschrittsBemerkung;
        }


        public void showAlert()
        {
            string Nachricht = "Der Umzug von " + KundenName + " von " + Auszugsadresse + " \r\n nach " + Einzugsadresse + " vom " + Umzugsdatum + " \r\n liegt in der Vergangenheit. \r\n Als abgeschlossen markieren?";

            var test = MessageBox.Show(Nachricht,"",MessageBoxButtons.YesNo);

            if (test == DialogResult.Yes)
            {
                var folgefrage = MessageBox.Show("Soll die Einzugsadresse als neue Kundenadresse übernommen werden?", "Adressübernahme", MessageBoxButtons.YesNo);

                if (folgefrage == DialogResult.Yes) {

                }
            }
        }
    }
}
