﻿using Google.Apis.Calendar.v3.Data;
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
        List<TextBox> Uhrzeit = new List<TextBox>();
        List<TextBox> KundenName = new List<TextBox>();
        List<TextBox> Anschrift = new List<TextBox>();
        List<TextBox> Kontakt = new List<TextBox>();
        List<TextBox> Transaktion = new List<TextBox>();
        List<TextBox> Bemerkung = new List<TextBox>();


        public LaufzettelKartons()
        {
            InitializeComponent();
            zuweisen();
        }

        private void zuweisen() {

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
        }

        private void buttonDrucken_Click(object sender, EventArgs e)
        {
            MySqlCommand cmdRead = new MySqlCommand("SELECT t.Kartons, t.Flaschenkartons, t.Glaeserkartons, t.Kleiderkartons, t.timeTransaktion, k.Anrede, k.Vorname, k.Nachname, k.Telefonnummer, k.Handynummer, k.Straße, k.Hausnummer, k.Ort, k.PLZ, t.Bemerkungen FROM Transaktionen t, Kunden k WHERE t.Umzuege_Kunden_idKunden = k.idKunden AND t.datTransaktion = " + Program.DateMachine(dateTransaktion.Value) + "ORDER BY timeTransaktion ASC;",Program.conn);
            MySqlDataReader rdr;

            int count = 0;
            TimeSpan comparison = new TimeSpan(0);

            try
            {

                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    // Sortiert Zeiten um genau Mitternacht aus
                    if (rdr.GetDateTime(4).TimeOfDay.CompareTo(comparison) != 0) {
                        // Name
                        KundenName[count].AppendText(rdr.GetString(5) + " " + rdr.GetString(6) + " " + rdr.GetString(7));
                        
                        //Kontakt (Handy vor Festnetz)
                        if (rdr.GetString(9) != "0")
                        {
                            Kontakt[count].AppendText(rdr.GetString(9));
                        }
                        else
                        {
                            Kontakt[count].AppendText(rdr.GetString(8));
                        }

                        //Anschrift
                        Anschrift[count].AppendText(rdr.GetString(10) + " " + rdr.GetString(11) + " " + rdr.GetString(13) + " " + rdr.GetString(12));

                        //Uhrzeit
                        Uhrzeit[count].AppendText(rdr.GetDateTime(4).ToShortTimeString());

                        //Bemerkung
                        Bemerkung[count].AppendText(rdr.GetString(14));

                        //Transaktion
                        if (rdr.GetInt32(0) < 0 || rdr.GetInt32(1) < 0 || rdr.GetInt32(2) < 0 || rdr.GetInt32(3) < 0)
                        {
                            Transaktion[count].AppendText("Ausliefern ");
                        }
                        else { Transaktion[count].AppendText("Abholen ca. "); }

                        if (rdr.GetInt32(0) != 0)
                        {
                            Transaktion[count].AppendText(Math.Abs(rdr.GetInt32(0)) + " Kartons, ");
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

            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
            }











            //Events eve = Program.kalenderDatumFinder(dateTransaktion.Value);
            //if (eve.Items.Count == 0)
            //{
            //    textLog.AppendText("Anzeige durch " + eve.Items.Count.ToString());
            //    return;
            //}

            //foreach (var item in eve.Items)
            //{
            //    if (item.ColorId == "8")
            //    {
            //        textLog.AppendText(item.Summary.ToString() + " " + item.ColorId.ToString() + "\r\n");
            //    }
            //}

            //textLog.AppendText("Anzeige durch " + eve.Items.Count.ToString());
        }
    }
}
