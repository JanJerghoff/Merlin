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

            int tempHVZ = -1;

            fields.TryGetValue("HVZBJa", out toSet);
            if (toSet.GetValueAsString().Length == 0) { tempHVZ = 1; }
            
            fields.TryGetValue("HVZBNein", out toSet);
            if (toSet.GetValueAsString().Length == 0) { tempHVZ = 0; }

            fields.TryGetValue("HVZBVllt", out toSet);
            if (toSet.GetValueAsString().Length == 0) { tempHVZ = 2; }

            if (tempHVZ != -1)
            {
                lesObj.einzug.HVZ1 = tempHVZ;
            }
            else { Program.FehlerLog("HVZ einlesen gescheitert, alle Felder leer", "HVZ einlesen gescheitert, alle Felder leer"); }

            //

            int tempAufzug = -1;

            fields.TryGetValue("AufzugBJa", out toSet);
            if (toSet.GetValueAsString().Length == 0) { tempAufzug = 1; }

            fields.TryGetValue("AufzugBNein", out toSet);
            if (toSet.GetValueAsString().Length == 0) { tempAufzug = 0; }

            if (tempHVZ != -1)
            {
                lesObj.einzug.Aufzug1 = tempAufzug;
            }
            else { Program.FehlerLog("tempAufzug einlesen gescheitert, alle Felder leer", "Aufzug einlesen gescheitert, alle Felder leer"); }




            if (radioAufzugBJa.Checked)
            {
                fields.TryGetValue("AufzugBJa", out toSet);
                toSet.SetValue("X");
            }
            if (radioAufzugBNein.Checked)
            {
                fields.TryGetValue("AufzugBNein", out toSet);
                toSet.SetValue("X");
            }
            //
            if (radioAussenAufzugBJa.Checked)
            {
                fields.TryGetValue("AussenAufzugBJa", out toSet);
                toSet.SetValue("X");
            }
            if (radioAussenAufzugBNein.Checked)
            {
                fields.TryGetValue("AussenAufzugBNein", out toSet);
                toSet.SetValue("X");
            }

            // Packen

            //
            if (radioEinpackenJa.Checked)
            {
                fields.TryGetValue("EinJa", out toSet);
                toSet.SetValue("X");
            }
            //if (radioEinpackenV.Checked)
            //{
            //    fields.TryGetValue("EinVllt", out toSet);
            //    toSet.SetValue("Yes");
            //}
            if (radioEinpackenNein.Checked)
            {
                fields.TryGetValue("EinNein", out toSet);
                toSet.SetValue("X");
            }
            //
            if (radioAuspackenJa.Checked)
            {
                fields.TryGetValue("AusJa", out toSet);
                toSet.SetValue("X");
            }
            //if (radioAuspackenV.Checked)
            //{
            //    fields.TryGetValue("AusVllt", out toSet);
            //    toSet.SetValue("Yes");
            //}
            if (radioAuspackenNein.Checked)
            {
                fields.TryGetValue("AusNein", out toSet);
                toSet.SetValue("X");
            }

            //Küche
            if (radioKuecheAbJa.Checked)
            {
                fields.TryGetValue("KuecheAbJa", out toSet);
                toSet.SetValue("X");
            }
            //if (radioKuecheAbV.Checked)
            //{
            //    fields.TryGetValue("KuecheAbVllt", out toSet);
            //    toSet.SetValue("Yes");
            //}
            if (radioKuecheAbNein.Checked)
            {
                fields.TryGetValue("KuecheAbNein", out toSet);
                toSet.SetValue("X");
            }
            //
            if (radioKuecheAufJa.Checked)
            {
                fields.TryGetValue("KuecheAufJa", out toSet);
                toSet.SetValue("X");
            }
            //if (radioKuecheAufV.Checked)
            //{
            //    fields.TryGetValue("KuecheAufVllt", out toSet);
            //    toSet.SetValue("Yes");
            //}
            if (radioKuecheAufNein.Checked)
            {
                fields.TryGetValue("KuecheAufNein", out toSet);
                toSet.SetValue("X");
            }
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
