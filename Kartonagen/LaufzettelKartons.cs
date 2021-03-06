﻿
using Google.Apis.Calendar.v3.Data;
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
    public partial class LaufzettelKartons : Form
    {
        List<TextBox> TransNr = new List<TextBox>();
        List<TextBox> Uhrzeit = new List<TextBox>();
        List<TextBox> KundenName = new List<TextBox>();
        List<TextBox> Anschrift = new List<TextBox>();
        List<TextBox> Kontakt = new List<TextBox>();
        List<TextBox> Transaktion = new List<TextBox>();
        List<TextBox> Bemerkung = new List<TextBox>();
        List<int> Kartonzahl = new List<int>();
        List<RadioButton> Auszuege = new List<RadioButton>();
        List<RadioButton> Einzuege = new List<RadioButton>();
        List<int> Umzugsnummern = new List<int>();
        List<Umzug> Umzuege = new List<Umzug>();

        public LaufzettelKartons()
        {
            InitializeComponent();
            zuweisen();
        }

        private void zuweisen() {

            TransNr.Add(textTransNr1);
            TransNr.Add(textTransNr2);
            TransNr.Add(textTransNr3);
            TransNr.Add(textTransNr4);
            TransNr.Add(textTransNr5);
            TransNr.Add(textTransNr6);
            TransNr.Add(textTransNr7);
            TransNr.Add(textTransNr8);
            TransNr.Add(textTransNr9);
            TransNr.Add(textTransNr10);
            TransNr.Add(textTransNr11);
            TransNr.Add(textTransNr12);
            TransNr.Add(textTransNr13);
            TransNr.Add(textTransNr14);
            TransNr.Add(textTransNr15);
            TransNr.Add(textTransNr16);

            Uhrzeit.Add(textUhrzeit1);
            Uhrzeit.Add(textUhrzeit2);
            Uhrzeit.Add(textUhrzeit3);
            Uhrzeit.Add(textUhrzeit4);
            Uhrzeit.Add(textUhrzeit5);
            Uhrzeit.Add(textUhrzeit6);
            Uhrzeit.Add(textUhrzeit7);
            Uhrzeit.Add(textUhrzeit8);
            Uhrzeit.Add(textUhrzeit9);
            Uhrzeit.Add(textUhrzeit10);
            Uhrzeit.Add(textUhrzeit11);
            Uhrzeit.Add(textUhrzeit12);
            Uhrzeit.Add(textUhrzeit13);
            Uhrzeit.Add(textUhrzeit14);
            Uhrzeit.Add(textUhrzeit15);
            Uhrzeit.Add(textUhrzeit16);

            KundenName.Add(textName1);
            KundenName.Add(textName2);
            KundenName.Add(textName3);
            KundenName.Add(textName4);
            KundenName.Add(textName5);
            KundenName.Add(textName6);
            KundenName.Add(textName7);
            KundenName.Add(textName8);
            KundenName.Add(textName9);
            KundenName.Add(textName10);
            KundenName.Add(textName11);
            KundenName.Add(textName12);
            KundenName.Add(textName13);
            KundenName.Add(textName14);
            KundenName.Add(textName15);
            KundenName.Add(textName16);

            Anschrift.Add(textAnschrift1);
            Anschrift.Add(textAnschrift2);
            Anschrift.Add(textAnschrift3);
            Anschrift.Add(textAnschrift4);
            Anschrift.Add(textAnschrift5);
            Anschrift.Add(textAnschrift6);
            Anschrift.Add(textAnschrift7);
            Anschrift.Add(textAnschrift8);
            Anschrift.Add(textAnschrift9);
            Anschrift.Add(textAnschrift10);
            Anschrift.Add(textAnschrift11);
            Anschrift.Add(textAnschrift12);
            Anschrift.Add(textAnschrift13);
            Anschrift.Add(textAnschrift14);
            Anschrift.Add(textAnschrift15);
            Anschrift.Add(textAnschrift16);

            Kontakt.Add(textKontakt1);
            Kontakt.Add(textKontakt2);
            Kontakt.Add(textKontakt3);
            Kontakt.Add(textKontakt4);
            Kontakt.Add(textKontakt5);
            Kontakt.Add(textKontakt6);
            Kontakt.Add(textKontakt7);
            Kontakt.Add(textKontakt8);
            Kontakt.Add(textKontakt9);
            Kontakt.Add(textKontakt10);
            Kontakt.Add(textKontakt11);
            Kontakt.Add(textKontakt12);
            Kontakt.Add(textKontakt13);
            Kontakt.Add(textKontakt14);
            Kontakt.Add(textKontakt15);
            Kontakt.Add(textKontakt16);

            Transaktion.Add(textAnAbZahl1);
            Transaktion.Add(textAnAbZahl2);
            Transaktion.Add(textAnAbZahl3);
            Transaktion.Add(textAnAbZahl4);
            Transaktion.Add(textAnAbZahl5);
            Transaktion.Add(textAnAbZahl6);
            Transaktion.Add(textAnAbZahl7);
            Transaktion.Add(textAnAbZahl8);
            Transaktion.Add(textAnAbZahl9);
            Transaktion.Add(textAnAbZahl10);
            Transaktion.Add(textAnAbZahl11);
            Transaktion.Add(textAnAbZahl12);
            Transaktion.Add(textAnAbZahl13);
            Transaktion.Add(textAnAbZahl14);
            Transaktion.Add(textAnAbZahl15);
            Transaktion.Add(textAnAbZahl16);

            Bemerkung.Add(textBemerkung1);
            Bemerkung.Add(textBemerkung2);
            Bemerkung.Add(textBemerkung3);
            Bemerkung.Add(textBemerkung4);
            Bemerkung.Add(textBemerkung5);
            Bemerkung.Add(textBemerkung6);
            Bemerkung.Add(textBemerkung7);
            Bemerkung.Add(textBemerkung8);
            Bemerkung.Add(textBemerkung9);
            Bemerkung.Add(textBemerkung10);
            Bemerkung.Add(textBemerkung11);
            Bemerkung.Add(textBemerkung12);
            Bemerkung.Add(textBemerkung13);
            Bemerkung.Add(textBemerkung14);
            Bemerkung.Add(textBemerkung15);
            Bemerkung.Add(textBemerkung16);

            Auszuege.Add(radio1A);
            Auszuege.Add(radio2A);
            Auszuege.Add(radio3A);
            Auszuege.Add(radio4A);
            Auszuege.Add(radio5A);
            Auszuege.Add(radio6A);
            Auszuege.Add(radio7A);
            Auszuege.Add(radio8A);
            Auszuege.Add(radio9A);
            Auszuege.Add(radio10A);
            Auszuege.Add(radio11A);
            Auszuege.Add(radio12A);
            Auszuege.Add(radio13A);
            Auszuege.Add(radio14A);
            Auszuege.Add(radio15A);
            Auszuege.Add(radio16A);

            Einzuege.Add(radio1B);
            Einzuege.Add(radio2B);
            Einzuege.Add(radio3B);
            Einzuege.Add(radio4B);
            Einzuege.Add(radio5B);
            Einzuege.Add(radio6B);
            Einzuege.Add(radio7B);
            Einzuege.Add(radio8B);
            Einzuege.Add(radio9B);
            Einzuege.Add(radio10B);
            Einzuege.Add(radio11B);
            Einzuege.Add(radio12B);
            Einzuege.Add(radio13B);
            Einzuege.Add(radio14B);
            Einzuege.Add(radio15B);
            Einzuege.Add(radio16B);
        }

        private void buttonDrucken_Click(object sender, EventArgs e)
        {
            Umzugsnummern = new List<int>();
            int count = 0;
            TimeSpan comparison = new TimeSpan(0);

            try
            {
                if (Program.conn.State != ConnectionState.Open)
                {
                    Program.conn.Open();
                }
                MySqlCommand cmdRead = new MySqlCommand("SELECT t.Kartons, t.Flaschenkartons, t.Glaeserkartons, t.Kleiderkartons, t.timeTransaktion, k.Anrede, k.Vorname, k.Nachname, k.Telefonnummer, k.Handynummer, t.Bemerkungen, u.idUmzuege, k.idKunden, t.idTransaktionen FROM Transaktionen t, Kunden k, Umzuege u WHERE t.Umzuege_Kunden_idKunden = k.idKunden AND u.idUmzuege = t.Umzuege_idUmzuege AND t.datTransaktion = '" + Program.DateMachine(dateTransaktion.Value) + "' ORDER BY timeTransaktion ASC;", Program.conn);
                MySqlDataReader rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    // Sortiert Zeiten um genau Mitternacht aus
                    if (rdr.GetDateTime(4).TimeOfDay.CompareTo(comparison) != 0 || true) { //Test, remove!

                        //Transaktionsnummer
                        TransNr[count].AppendText(rdr.GetInt32(12)+"");

                        // Name + Kundennummer + Transnr 
                        KundenName[count].AppendText(rdr.GetInt32(13) + " " + rdr.GetString(5) + " " + rdr.GetString(7)); // Auslassung des Vornamens auf rdr[6]
                        
                        //Kontakt (Handy vor Festnetz)
                        if (rdr.GetString(9) != "0")
                        {
                            Kontakt[count].AppendText(rdr.GetString(9));
                        }
                        else
                        {
                            Kontakt[count].AppendText(rdr.GetString(8));
                        }

                        //Uhrzeit
                        Uhrzeit[count].AppendText(rdr.GetDateTime(4).ToShortTimeString());

                        //Bemerkung
                        Bemerkung[count].AppendText(rdr.GetString(10));

                        //Umzugsnummer für Adressen Zwischenspeichern
                        Umzugsnummern.Add(rdr.GetInt32(11));

                        //Transaktion
                        if (rdr.GetInt32(0) < 0 || rdr.GetInt32(1) < 0 || rdr.GetInt32(2) < 0 || rdr.GetInt32(3) < 0)
                        {
                            Transaktion[count].AppendText("Abholen ca. ");
                        }
                        else { Transaktion[count].AppendText("Ausliefern "); }

                        if (rdr.GetInt32(0) != 0)
                        {
                            Transaktion[count].AppendText(Math.Abs(rdr.GetInt32(0)) + " Kartons, ");
                            Kartonzahl.Add(Math.Abs(rdr.GetInt32(0)));
                        }
                        else {
                            Kartonzahl.Add(0);
                        }


                        if (rdr.GetInt32(1) != 0)
                        {
                            Transaktion[count].AppendText(Math.Abs(rdr.GetInt32(1)) + " Flaschenkartons, ");
                        }

                        if (rdr.GetInt32(2) != 0)
                        {
                            Transaktion[count].AppendText(Math.Abs(rdr.GetInt32(2)) + " Glaeserkartons, ");
                        }

                        if (rdr.GetInt32(3) != 0)
                        {
                            Transaktion[count].AppendText(Math.Abs(rdr.GetInt32(3)) + " Kleiderkartons, ");
                        }
                        count++;
                    }
                    
                }
                rdr.Close();
                Program.conn.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Fehler beim Auslesen der Kartondaten \r\n Bereits dokumentiert.");
            }

            // Umzüge auffüllen
            foreach (var item in Umzugsnummern)
            {
                Umzuege.Add(new Umzug(item));
            }

            // Warscheinliche Adressen bestimmen und einfüllen
            int counter = 0;
            foreach (var item in Umzuege)
            {
                if (item.DatUmzug <= DateTime.Now)
                {    //Umzug passiert
                    Anschrift[counter].Text = item.einzug.Straße1 + " " + item.einzug.Hausnummer1 + ", " + item.einzug.PLZ1 + " " + item.einzug.Ort1;
                    Einzuege[counter].Checked = true;
                }
                else {
                    Anschrift[counter].Text = item.auszug.Straße1 + " " + item.auszug.Hausnummer1 + ", " + item.auszug.PLZ1 + " " + item.auszug.Ort1;
                    Auszuege[counter].Checked = true;
                }

                counter++;
            }

            // Gegenprüfen mittels Kalender
            //Events eve = Program.getUtil().kalenderDatumFinder(dateTransaktion.Value);            
            //foreach (var item in eve.Items)
            //{
            //    if (item.ColorId == "8")
            //    {
            //        count--;
            //    }
            //}

            //if (count < 0)
            //{
            //    var Meldung = MessageBox.Show("Im Kalender sind mehr Termine als Transaktionen in der Datenbank \r\n Bitte überprüfen" , "Warnung");
            //}
            //else if (count > 0)
            //{
            //    var Meldung = MessageBox.Show("Im Kalender sind weniger Termine als Transaktionen in der Datenbank \r\n Bitte überprüfen", "Warnung");
            //}            
        }

        private void changeAdresse(RadioButton activated) {
                       

            if (Auszuege.Contains(activated))   // Ist ein Auszugsbutton
            {
                int temp = Auszuege.IndexOf(activated);

                Einzuege[temp].Checked = false; //korrespondierenden Button ausschalten

                Anschrift[temp].Text = Umzuege[temp].auszug.Straße1 + " " + Umzuege[temp].auszug.Hausnummer1 + ", " + Umzuege[temp].auszug.PLZ1 + " " + Umzuege[temp].auszug.Ort1;                     

            }
            else if (Einzuege.Contains(activated))
            {
                int temp = Einzuege.IndexOf(activated);

                Auszuege[temp].Checked = false; //korrespondierenden Button ausschalten

                Anschrift[temp].Text = Umzuege[temp].einzug.Straße1 + " " + Umzuege[temp].einzug.Hausnummer1 + ", " + Umzuege[temp].einzug.PLZ1 + " " + Umzuege[temp].einzug.Ort1;                
            }

            activated.Checked = true;

        }


        private void buttonDrucker_Click(object sender, EventArgs e)
        {

            PdfDocument pdf = new PdfDocument(new PdfReader(System.IO.Path.Combine(Environment.CurrentDirectory, "Laufzettel Kartonagen+Schilder.pdf")), new PdfWriter(Program.druckPfad));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;

            int count = 1;

            textLog.AppendText(Uhrzeit.Count.ToString());

            foreach (var item in Uhrzeit)
            {
                if (item.Text != "")
                {
                    fields.TryGetValue("Name " + count, out toSet);
                    toSet.SetValue(KundenName[count - 1].Text);

                    fields.TryGetValue("Uhrzeit " + count, out toSet);
                    toSet.SetValue(Uhrzeit[count - 1].Text);

                    if (Transaktion[count-1].Text.Contains("Ausliefern"))
                    {
                        fields.TryGetValue("AnAb " + count, out toSet);
                        toSet.SetValue("Anliefern");
                    }
                    else
                    {
                        fields.TryGetValue("AnAb " + count, out toSet);
                        toSet.SetValue("Abholen");
                    }

                    fields.TryGetValue("Anzahl " + count, out toSet);
                    toSet.SetValue(Kartonzahl[count - 1].ToString());

                    fields.TryGetValue("Strasse " + count, out toSet);
                    toSet.SetValue(Anschrift[count - 1].Text);

                    fields.TryGetValue("Telefon " + count, out toSet);
                    toSet.SetValue(Kontakt[count - 1].Text);

                    fields.TryGetValue("Besonderheit " + count, out toSet);
                    toSet.SetValue(Bemerkung[count - 1].Text);

                    count++;
                }
            }

            fields.TryGetValue("Datum", out toSet);
            toSet.SetValue(dateTransaktion.Value.ToShortDateString());

            fields.TryGetValue("Mitarbeiter", out toSet);
            toSet.SetValue(textMitarbeiter.Text);

            //fields.TryGetValue("Fahrzeug", out toSet);
            //toSet.SetValue(textFahrzeug.Text);


            form.FlattenFields();
            try { pdf.Close(); }
            catch (Exception ex)
            { Program.FehlerLog(ex.ToString(), "Fehler beim schließen des PDF \r\n Bereits dokumentiert."); }

            //Program.showPDF();
            Program.SendToPrinter();

            textLog.AppendText("PDF Erfolgreich erzeugt");
        }

        private void radio1A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio1A);
        }

        private void radio1B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio1B);
        }

        private void radio2A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio2A);
        }

        private void radio2B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio2B);
        }

        private void radio3A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio3A);
        }

        private void radio3B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio3B);
        }

        private void radio4A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio4A);
        }

        private void radio4B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio4B);
        }

        private void radio5A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio5A);
        }

        private void radio5B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio5B);
        }

        private void radio6A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio6A);
        }

        private void radio6B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio6B);
        }

        private void radio7A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio7A);
        }

        private void radio7B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio7B);
        }

        private void radio8A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio8A);
        }

        private void radio8B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio8B);
        }

        private void radio9A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio9A);
        }

        private void radio9B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio9B);
        }

        private void radio10A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio10A);
        }

        private void radio10B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio10B);
        }

        private void radio11A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio11A);
        }

        private void radio11B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio11B);
        }

        private void radio12A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio12A);
        }

        private void radio12B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio12B);
        }

        private void radio13A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio13A);
        }

        private void radio13B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio13B);
        }

        private void radio14A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio14A);
        }

        private void radio14B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio14B);
        }

        private void radio15A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio15A);
        }

        private void radio15B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio15B);
        }

        private void radio16A_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio16A);
        }

        private void radio16B_CheckedChanged(object sender, EventArgs e)
        {
            changeAdresse(radio16B);
        }
    }
}
