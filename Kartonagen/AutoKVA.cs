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
    public partial class AutoKVA : Form
    {
        public AutoKVA()
        {
            InitializeComponent();
        }



        private String Einleitung (String Anrede, String Nachname, String Datum, String OrtA, String OrtB, String AnschriftA, String AnschriftB) {




            String result = "";

            if (Anrede == "Herr")
            {
                result += "Sehr geehrter Herr " + Nachname+", \r\n \r\n";
            }
            else if (Anrede == "Frau") {
                result += "Sehr geehrte Frau " + Nachname+ ", \r\n \r\n";
            }

            result += "wie bereits bei der Besichtigung besprochen, unterbreiten wir ihnen folgendes Angebot für ihren Umzug \r\n";
            result += "am " + Datum + " \r\n";

            if (OrtA == OrtB)
            {
                result += "aus der " + AnschriftA + " in die " + AnschriftB + ", beides in " + OrtA + ". \r\n";
            }
            else {
                result += "von " + OrtA + " nach " + OrtB + ". \r\n";
            }
            return result;
        }

        private String Pauschal(int Betrag, String Datum) {
            String result = "";
            double mwst = Betrag * 0.19;
            double gesamt = Betrag + mwst;

            result += "Angebot                                                                          " + Betrag + " € \r\n";
            result += "- Umzug am " + Datum + "\r\n";
            result += Autos() + " \r\n";
            result += Packer() + " \r\n";
            //result += Montage() + " \r\n";
            result += "- Versicherung nach HBG siehe Anhang";            
            result += "Verpackungspauschale";
            result += "                                                         zzgl. gesetzl. MwSt. 19%   " + mwst + " € \r\n";
            result += "                                                               ´       Gesamtpreis  " + gesamt + " € \r\n \r\n";
            result += HVZInfo() + "\r\n";
            result += Bohr();
            return result;
        }

        private String Autos() {
            return "";
        }

        private String Packer() {
            return "";
        }

        private String Bar() {
            return "Bezahlung in bar nach getaner Arbeit";
        }

        private String Bohr() {
            return "Es werden von uns keine Bohr- oder Elektromontagen vorgenommen.";
        }

        private String Kartons() {
            return "Kartonagen können bei uns bestellt werden. \r\n Kaufkartons gebraucht 1,50€ per Stück, zahlbar bei Abgabe. Eine Anlieferung ist kostenfrei";
        }

        private String HVZInfo () {
            String result = "";
            result += "Für Haltemöglichkeiten, 12m, direkt vor den jeweiligen Adressen sorgt der Kunde. \r\n ";
            result += "Wenn von uns Halteverbotszonen eingerichtet werden sollen kosten diese pro Stelle 70,00 € zzgl Mwst. \r\n ";
            result += "Falls gewünscht bitte um baldige Rückmeldung wegen der Beantragungsfrist. \r\n";

            return result;
        }

        private String Kuechenbau() {
            return "Der Küchenbauer wird seperat abgerechnet. Karte liegt bei. Bitte setzen sie sich ggf. \r\n zwecks Terminvereinbarung für einen Kostenvoranschlag mit Herrn Schuldt in Verbindung.";
        }

        private String Einpacken(String Einpackdatum) {
            String result = "";
            result += "Falls Einpackarbeiten durch uns gewünscht, können diese am " + Einpackdatum + " erledigt werden. \r\n ";
            result += "Sie werden seperat mit 25,00€ pro Mitarbeiter und Stunde abgerechnet, zzgl. ges. MwSt 19% \r\n ";
            result += "Packmaterial wird seperat mit 40,00 € zzgl 19% MwSt. berechnet.";

            return result;
        }

        private String Klavier() {
            return "Das Klavier wird von uns nicht transportiert. Bitte setzen sie sich mit der Firma Klavierhaus \r\n Klavins unter Tel: 0228-461515 in Verbindung (www.klavierhaus-klavins.de)";
        }

        private String Wandregal() {
            return "Für die Montage der Wandregale wird der Schreiner mit 35,00€ die Stunde zzgl. 19% MwSt. \r\n auf Zeit berechnet.";
        }

        private void buttonPauschal_Click(object sender, EventArgs e)
        {
            textBlock.Text = Pauschal(1337, DateTime.Now.ToShortDateString());
        }
    }
}
