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
        public static void readUmzug()
        {
            PdfDocument pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Temp3.pdf")));

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;

            fields.TryGetValue("StrasseA", out toSet);            

            var bestxätigung = MessageBox.Show("PDF geladen, " + toSet.GetValue(), "Erinnerung", MessageBoxButtons.YesNo);

            pdf.Close();
        }
    }
}
