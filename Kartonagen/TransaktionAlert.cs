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

        public TransaktionAlert(int id, int kartons, int Flaschenkartons, int Glaeserkartons, int Kleiderkartons, string time, string UserChanged,string adresse, string name, string bemerkung) {
            this.id = id;
            this.kartons = kartons;
            this.Flaschenkartons = Flaschenkartons;
            this.Glaeserkartons = Glaeserkartons;
            this.Kleiderkartons = Kleiderkartons;
            this.time = time;
            this.name = name;
            this.bemerkung = bemerkung;
            this.Adresse = adresse;
            this.UserChanged = UserChanged;
        }

        public void showAlert()

        {
            TransaktionErinnerung win = new TransaktionErinnerung();
            win.set(UserChanged, time, name, Adresse, bemerkung, id, kartons, Flaschenkartons, Glaeserkartons, Kleiderkartons);
            win.Show();
        }
    }
}
