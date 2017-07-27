using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using MySql.Data.MySqlClient;
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
    public partial class Laufzetteldrucker : Form
    {
        public Laufzetteldrucker()
        {
            InitializeComponent();
        }

        private void buttonDrucken_Click(object sender, EventArgs e)
        {

            //PdfDocument pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Laufzettel Besichtigung.pdf")), new PdfWriter(Program.druckPfad));
            //PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            //IDictionary<String, PdfFormField> fields = form.GetFormFields();
            //PdfFormField toSet;

            // Alle betreffenden Umzüge greifen
            //Abfrage aller Namen
            MySqlCommand cmdRead = new MySqlCommand("SELECT k.Anrede, k.Vorname, k.Nachname, k.Telefonnummer, k.Handynummer, u.StraßeA, u.HausnummerA, u.OrtA, u.PLZA, u.umzugsZeit FROM Kunden k, Umzuege u WHERE (u.Kunden_idKunden = k.idKunden AND u.datBesichtigung = '" + Program.DateMachine(dateBesichtigung.Value) + "') ORDER BY u.umzugsZeit ASc", Program.conn);
            MySqlDataReader rdrPre;
            
            String[] Namen = new String [13];
            String[] Anschriften = new String[13];
            String[] Uhrzeiten = new String[13];
            String[] Handynummern = new String[13];
            int rowCount = 0;

            try
            {

                rdrPre = cmdRead.ExecuteReader();
                while (rdrPre.Read())
                {
                    Namen[rowCount] = rdrPre[0] + " " + rdrPre[1] + " " + rdrPre[2];

                    Uhrzeiten[rowCount] = rdrPre[9].ToString().Substring(0, 5);

                    Anschriften[rowCount] = rdrPre[5] + " " + rdrPre[6] + " " + rdrPre[8] + " " + rdrPre[7];

                    if (rdrPre[4].ToString() != "0")
                    {
                        Handynummern[rowCount] = rdrPre[4].ToString();
                    }
                    else
                    {
                        Handynummern[rowCount] = rdrPre[3].ToString();
                    }

                    rowCount++;
                }
                rdrPre.Close();
                
            }
            catch (Exception sqlEx)
            {

                textLog.Text += sqlEx.ToString();
            }

            textAnschrift1.Text = Anschriften[0];
            textAnschrift2.Text = Anschriften[1];
            textAnschrift3.Text = Anschriften[2];
            textAnschrift4.Text = Anschriften[3];
            textAnschrift5.Text = Anschriften[4];
            textAnschrift6.Text = Anschriften[5];
            textAnschrift7.Text = Anschriften[6];
            textAnschrift8.Text = Anschriften[7];
            textAnschrift9.Text = Anschriften[8];
            textAnschrift10.Text = Anschriften[9];
            textAnschrift11.Text = Anschriften[10];
            textAnschrift12.Text = Anschriften[11];
            textAnschrift13.Text = Anschriften[12];

            textName1.Text = Namen[0];
            textName2.Text = Namen[1];
            textName3.Text = Namen[2];
            textName4.Text = Namen[3];
            textName5.Text = Namen[4];
            textName6.Text = Namen[5];
            textName7.Text = Namen[6];
            textName8.Text = Namen[7];
            textName9.Text = Namen[8];
            textName10.Text = Namen[9];
            textName11.Text = Namen[10];
            textName12.Text = Namen[11];
            textName13.Text = Namen[12];

            textUhrzeit1.Text = Uhrzeiten[0];
            textUhrzeit2.Text = Uhrzeiten[1];
            textUhrzeit3.Text = Uhrzeiten[2];
            textUhrzeit4.Text = Uhrzeiten[3];
            textUhrzeit5.Text = Uhrzeiten[4];
            textUhrzeit6.Text = Uhrzeiten[5];
            textUhrzeit7.Text = Uhrzeiten[6];
            textUhrzeit8.Text = Uhrzeiten[7];
            textUhrzeit9.Text = Uhrzeiten[8];
            textUhrzeit10.Text = Uhrzeiten[9];
            textUhrzeit11.Text = Uhrzeiten[10];
            textUhrzeit12.Text = Uhrzeiten[11];
            textUhrzeit13.Text = Uhrzeiten[12];

            textKontakt1.Text = Handynummern[0];
            textKontakt2.Text = Handynummern[1];
            textKontakt3.Text = Handynummern[2];
            textKontakt4.Text = Handynummern[3];
            textKontakt5.Text = Handynummern[4];
            textKontakt6.Text = Handynummern[5];
            textKontakt7.Text = Handynummern[6];
            textKontakt8.Text = Handynummern[7];
            textKontakt9.Text = Handynummern[8];
            textKontakt10.Text = Handynummern[9];
            textKontakt11.Text = Handynummern[10];
            textKontakt12.Text = Handynummern[11];
            textKontakt13.Text = Handynummern[12];


        }

        private void buttonDrucker_Click(object sender, EventArgs e)
        {
            PdfDocument pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Laufzettel Besichtigung.pdf")), new PdfWriter(Program.druckPfad));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;
            
            MySqlCommand cmdRead = new MySqlCommand("SELECT k.Anrede, k.Vorname, k.Nachname, k.Telefonnummer, k.Handynummer, u.StraßeA, u.HausnummerA, u.OrtA, u.PLZA, u.umzugsZeit FROM Kunden k, Umzuege u WHERE (u.Kunden_idKunden = k.idKunden AND u.datBesichtigung = '" + Program.DateMachine(dateBesichtigung.Value) + "') ORDER BY u.umzugsZeit ASc", Program.conn);
            MySqlDataReader rdr;

            int zaehler = 1;
            String Uhrzeit = "";
            String temp = "";


            try
            {

                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    fields.TryGetValue("Name " + zaehler, out toSet);
                    toSet.SetValue(rdr[0] + " " + rdr[1] + " " + rdr[2]);

                    fields.TryGetValue("Uhrzeit " + zaehler, out toSet);
                    temp = rdr[9].ToString();
                    Uhrzeit = temp.Substring(0, 5);
                    toSet.SetValue(Uhrzeit);

                    fields.TryGetValue("Strasse " + zaehler, out toSet);
                    toSet.SetValue(rdr[5] + " " + rdr[6]);

                    fields.TryGetValue("Ort " + zaehler, out toSet);
                    toSet.SetValue(rdr[8] + " " + rdr[7]);

                    if (rdr[4].ToString() != "0")
                    {
                        fields.TryGetValue("Telefon " + zaehler, out toSet);
                        toSet.SetValue(rdr[4] + "");
                    }
                    else
                    {
                        fields.TryGetValue("Telefon " + zaehler, out toSet);
                        toSet.SetValue(rdr[3] + "");
                    }
                    zaehler++;
                }
                rdr.Close();

                fields.TryGetValue("Mitarbeiter", out toSet);
                toSet.SetValue(textMitarbeiter.Text);

                fields.TryGetValue("Datum", out toSet);
                toSet.SetValue(dateBesichtigung.Value.ToShortDateString());

                // Belegen aller Bemerkungsfelder

                fields.TryGetValue("Besonderheit 1", out toSet);
                toSet.SetValue(textBemerkung1.Text);

                fields.TryGetValue("Besonderheit 2", out toSet);
                toSet.SetValue(textBemerkung2.Text);

                fields.TryGetValue("Besonderheit 3", out toSet);
                toSet.SetValue(textBemerkung3.Text);

                fields.TryGetValue("Besonderheit 4", out toSet);
                toSet.SetValue(textBemerkung4.Text);

                fields.TryGetValue("Besonderheit 5", out toSet);
                toSet.SetValue(textBemerkung5.Text);

                fields.TryGetValue("Besonderheit 6", out toSet);
                toSet.SetValue(textBemerkung6.Text);

                fields.TryGetValue("Besonderheit 7", out toSet);
                toSet.SetValue(textBemerkung7.Text);

                fields.TryGetValue("Besonderheit 8", out toSet);
                toSet.SetValue(textBemerkung8.Text);

                fields.TryGetValue("Besonderheit 9", out toSet);
                toSet.SetValue(textBemerkung9.Text);

                fields.TryGetValue("Besonderheit 10", out toSet);
                toSet.SetValue(textBemerkung10.Text);

                fields.TryGetValue("Besonderheit 11", out toSet);
                toSet.SetValue(textBemerkung11.Text);

                fields.TryGetValue("Besonderheit 12", out toSet);
                toSet.SetValue(textBemerkung12.Text);

                fields.TryGetValue("Besonderheit 13", out toSet);
                toSet.SetValue(textBemerkung13.Text);
            }
            catch (Exception ex) {
                textLog.Text += ex.ToString();
            }

            form.FlattenFields();
            try { pdf.Close(); }
            catch (Exception ex)
            { textLog.Text += "why?" + ex.ToString(); }

            Program.SendToPrinter();

            textLog.AppendText("PDF Erfolgreich erzeugt");
            
        }
    }
}
