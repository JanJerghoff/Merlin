using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartonagen
{
    class TransaktionAlert : AbstractAlert
    {

        int id;
        int kartons;
        int Flaschenkartons;
        int Glaeserkartons;
        int Kleiderkartons;
        string time;
        string name;
        string bemerkung;
        string Adresse;
        string UserChanged;
        string RechnungsNummer;
        int idBearbeitend;

        public TransaktionAlert(String Rechnungsnummer, int id, int kartons, int Flaschenkartons, int Glaeserkartons, int Kleiderkartons, string time, string UserChanged,string adresse, string name, string bemerkung, int idBearbeitend) {
            this.RechnungsNummer = Rechnungsnummer;
            this.id = id;
            this.kartons = kartons;
            this.Flaschenkartons = Flaschenkartons;
            this.Glaeserkartons = Glaeserkartons;
            this.Kleiderkartons = Kleiderkartons;
            this.time = time;
            this.name = name;
            this.bemerkung = bemerkung;
            Adresse = adresse;
            this.UserChanged = UserChanged;
            this.idBearbeitend = idBearbeitend;
        }

        public void showAlert()

        {
            TransaktionErinnerung win = new TransaktionErinnerung();
            win.set(UserChanged, RechnungsNummer, time, name, Adresse, bemerkung, id, kartons, Flaschenkartons, Glaeserkartons, Kleiderkartons, idBearbeitend);
            win.Show();
                       
        }
    }
}
