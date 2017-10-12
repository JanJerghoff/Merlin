using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kartonagen
{
    class PDFInput
    {
        static Umzug lesObj;

        private static int DreiFelderCheck(string ja, string nein, string vllt, string thema, IDictionary<String, PdfFormField> fields) {

            int tempX = -1;
            PdfFormField toSet;

            fields.TryGetValue(ja, out toSet);
            if (toSet.GetValueAsString().Length != 0) { tempX = 1; }

            fields.TryGetValue(nein, out toSet);
            if (toSet.GetValueAsString().Length != 0) { tempX = 0; }

            fields.TryGetValue(vllt, out toSet);
            if (toSet.GetValueAsString().Length != 0) { tempX = 2; }

            if (tempX == -1)
            {
                Program.FehlerLog(thema+" einlesen gescheitert, alle Felder leer" + lesObj.Id, thema + " einlesen gescheitert, alle Felder leer");
                return 0;
            }
            return tempX;
        }

        private static int ZweiFelderCheck(string ja, string nein, string thema, IDictionary<String, PdfFormField> fields) {


            int tempX = -1;
            PdfFormField toSet;

            fields.TryGetValue(ja, out toSet);
            if (toSet.GetValueAsString().Length != 0) { tempX = 1; }

            fields.TryGetValue(nein, out toSet);
            if (toSet.GetValueAsString().Length != 0) { tempX = 0; }
            
            if (tempX == -1)
            {
                Program.FehlerLog(thema + " einlesen gescheitert, alle Felder leer" + lesObj.Id, thema + " einlesen gescheitert, alle Felder leer");
                return 0;
            }
            return tempX;

        }

        public static void readUmzug(string pfad)
        {
            PdfDocument pdf = new PdfDocument(new PdfReader(pfad));

            //TEST
            var bestätigung = MessageBox.Show(pfad, "Erinnerung", MessageBoxButtons.YesNo);

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;

            fields.TryGetValue("Umzugsnummer", out toSet);            
            lesObj = new Umzug(Program.intparser(toSet.GetValueAsString()));

            //TEST
            var bestätigusng = MessageBox.Show("gebaut!", "Erinnerung", MessageBoxButtons.YesNo);

            //Auslesen + in Umzug ändern

            fields.TryGetValue("TragwegA", out toSet);
            lesObj.auszug.Laufmeter1 = (Program.intparser(toSet.GetValueAsString()));

            fields.TryGetValue("TragwegB", out toSet);
            lesObj.einzug.Laufmeter1 = (Program.intparser(toSet.GetValueAsString()));

            lesObj.einzug.HVZ1 = DreiFelderCheck("HVZBJa", "HVZBNein", "HVZBVllt", "HVZ Einzugsadresse", fields);

            //  AUfzug + AussenAufzug f. Einzugsadresse

            lesObj.einzug.Aufzug1 = ZweiFelderCheck("AufzugBJa", "AufzugBNein", "Aufzug Einzugsadresse", fields);

            int tempAufzug = -1;



            fields.TryGetValue("AufzugBJa", out toSet);
            if (toSet.GetValueAsString().Length != 0) { tempAufzug = 1; }

            fields.TryGetValue("AufzugBNein", out toSet);
            if (toSet.GetValueAsString().Length != 0) { tempAufzug = 0; }

            //if (tempAufzug != -1)
            //{
            //    lesObj.einzug.Aufzug1 = tempAufzug;
            //}
            //else { Program.FehlerLog("tempAufzug einlesen gescheitert, alle Felder leer" + lesObj.Id, "Aufzug einlesen gescheitert, alle Felder leer"); }

            ////
            //int tempAussenaufzug = -1;

            //fields.TryGetValue("AussenAufzugBJa", out toSet);
            //if (toSet.GetValueAsString().Length != 0) { tempAussenaufzug = 1; }

            //fields.TryGetValue("AussenAufzugBNein", out toSet);
            //if (toSet.GetValueAsString().Length != 0) { tempAussenaufzug = 0; }

            //if (tempAussenaufzug != -1)
            //{
            //    lesObj.einzug.AussenAufzug1 = tempAussenaufzug;
            //}
            //else { Program.FehlerLog("Aussenaufzug einlesen gescheitert, alle Felder leer" + lesObj.Id, "Aussenaufzug einlesen gescheitert, alle Felder leer"); }

            //// Packen
            //lesObj.Einpacken1 = DreiFelderCheck("EinJa", "EinNein", "EinVllt", "Einpacken", fields);
            //lesObj.Auspacken1 = DreiFelderCheck("AusJa", "AusNein", "AusVllt", "Auspacken", fields);
            //lesObj.KuecheAuf1 = DreiFelderCheck("KuecheAufJa", "KuecheAufNein", "KuecheAufVllt", "Kueche aufbauen", fields);
            //lesObj.KuecheAuf1 = DreiFelderCheck("KuecheAbJa", "KuecheAbNein", "KuecheAbVllt", "Kueche abbauen", fields);

            //// Küche
            //lesObj.KuecheBau1 = ZweiFelderCheck("KuecheIntern", "KuecheExtern", "Küchenbau (intern/extern)", fields);

            //fields.TryGetValue("KuechePreis", out toSet);
            //if (toSet.GetValueAsString().Length != 0)
            //{
            //    lesObj.KuechePausch1 = int.Parse(toSet.GetValueAsString());
            //}

            //// Restdaten

            //fields.TryGetValue("Mann", out toSet);
            //if (toSet.GetValueAsString().Length != 0)
            //{
            //    lesObj.Mann = int.Parse(toSet.GetValueAsString());
            //}

            //fields.TryGetValue("Stunden", out toSet);
            //if (toSet.GetValueAsString().Length != 0)
            //{
            //    lesObj.Stunden = int.Parse(toSet.GetValueAsString());
            //}

            //lesObj.Versicherung = ZweiFelderCheck("VersicherungJa", "VersicherungNein", "Zusatzversicherung", fields);


            ////fields.TryGetValue("Autos", out toSet);
            ////toSet.SetValue(AutoString());                 Autoparser!!

            //fields.TryGetValue("Kleiderkisten", out toSet);
            //if (toSet.GetValueAsString().Length != 0)
            //{
            //    lesObj.Kleiderkartons1 = int.Parse(toSet.GetValueAsString());
            //}

            //Bemerkungen
            fields.TryGetValue("NoteBuero", out toSet);
            lesObj.NotizBuero1 = toSet.GetValueAsString();

            fields.TryGetValue("NoteFahrer", out toSet);
            lesObj.NotizFahrer1 = toSet.GetValueAsString();


            pdf.Close();

            lesObj.UpdateDB("3");
        }
    }
}
