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

        public static void readUmzug()
        {
            PdfDocument pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Temp3.pdf")));

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;

            fields.TryGetValue("Umzugsnummer", out toSet);
            lesObj = new Umzug(int.Parse(toSet.GetValueAsString()));

            //Auslesen + in Umzug ändern
            
            fields.TryGetValue("TragwegA", out toSet);
            lesObj.auszug.Laufmeter1 = (int.Parse(toSet.GetValueAsString()));

            fields.TryGetValue("TragwegB", out toSet);
            lesObj.einzug.Laufmeter1 = (int.Parse(toSet.GetValueAsString()));

            lesObj.einzug.HVZ1 = DreiFelderCheck("HVZBJa", "HVZBNein", "HVZBVllt", "HVZ Einzugsadresse", fields);

            //

            int tempAufzug = -1;

            fields.TryGetValue("AufzugBJa", out toSet);
            if (toSet.GetValueAsString().Length != 0) { tempAufzug = 1; }

            fields.TryGetValue("AufzugBNein", out toSet);
            if (toSet.GetValueAsString().Length != 0) { tempAufzug = 0; }

            if (tempAufzug != -1)
            {
                lesObj.einzug.Aufzug1 = tempAufzug;
            }
            else { Program.FehlerLog("tempAufzug einlesen gescheitert, alle Felder leer" + lesObj.Id, "Aufzug einlesen gescheitert, alle Felder leer"); }

            //
            int tempAussenaufzug = -1;

            fields.TryGetValue("AussenAufzugBJa", out toSet);
            if (toSet.GetValueAsString().Length != 0) { tempAussenaufzug = 1; }

            fields.TryGetValue("AussenAufzugBNein", out toSet);
            if (toSet.GetValueAsString().Length != 0) { tempAussenaufzug = 0; }

            if (tempAussenaufzug != -1)
            {
                lesObj.einzug.AussenAufzug1 = tempAussenaufzug;
            }
            else { Program.FehlerLog("Aussenaufzug einlesen gescheitert, alle Felder leer" + lesObj.Id, "Aussenaufzug einlesen gescheitert, alle Felder leer"); }

            // Packen
            lesObj.Einpacken1 = DreiFelderCheck("EinJa", "EinNein", "EinVllt", "Einpacken", fields);
            lesObj.Auspacken1 = DreiFelderCheck("AusJa", "AusNein", "AusVllt", "Auspacken", fields);
            lesObj.KuecheAuf1 = DreiFelderCheck("KuecheAufJa", "KuecheAufNein", "KuecheAufVllt", "Kueche aufbauen", fields);
            lesObj.KuecheAuf1 = DreiFelderCheck("KuecheAbJa", "KuecheAbNein", "KuecheAbVllt", "Kueche abbauen", fields);
            
            //
            //if (radioKuecheExtern.Checked)
            //{
            //    fields.TryGetValue("KuecheExtern", out toSet);
            //    toSet.SetValue("Yes");
            //}
            if (radioKuecheIntern.Checked)
            {
                fields.TryGetValue("KuecheIntern", out toSet);
                toSet.SetValue("X");
            }
            //
            if (textKuechenPreis.Text != "0")
            {
                fields.TryGetValue("KuechePreis", out toSet);
                toSet.SetValue(textKuechenPreis.Text);
            }

            // Restdaten
            if (numericMannZahl.Value != 0)
            {
                fields.TryGetValue("Mann", out toSet);
                toSet.SetValue(numericMannZahl.Value.ToString());
            }

            if (numericArbeitszeit.Value != 0)
            {
                fields.TryGetValue("Stunden", out toSet);
                toSet.SetValue(numericArbeitszeit.Value.ToString());
            }

            fields.TryGetValue("Autos", out toSet);
            toSet.SetValue(AutoString());

            if (numericKleiderkisten.Value != 0)
            {
                fields.TryGetValue("Kleiderkisten", out toSet);
                toSet.SetValue(numericKleiderkisten.Value.ToString());
            }

            //Bemerkungen
            fields.TryGetValue("NoteBuero", out toSet);
            toSet.SetValue(textNoteBuero.Text);

            fields.TryGetValue("NoteFahrer", out toSet);
            toSet.SetValue(textNoteFahrer.Text);



            pdf.Close();
        }
    }
}
