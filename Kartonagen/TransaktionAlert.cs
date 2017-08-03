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
            throw new NotImplementedException();
        }
    }
}
