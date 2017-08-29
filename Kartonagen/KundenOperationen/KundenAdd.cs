using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kartonagen
{
    public partial class KundenAdd : Form
    {

        private int idBearbeitend = 0; // 0= Rita, 1=Jonas, 2=Eva, 3=Jan, 4, Sonst.

        public void setBearbeiter(int wer) {
            idBearbeitend = wer;
        }

        public KundenAdd()
        {
            InitializeComponent();
            
            // Autocomplete vorlegen
            textKundenAddNachname.AutoCompleteCustomSource = Program.getAutocompleteKunden();
            textKundenAddNachname.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        private void buttonKundenHinzufügen_Click(object sender, EventArgs e)
        {
            // Temporäre Deklarationen
            String Telefonnummer = "0";
            String Handynummer = "0";
            String Email = textKundenAddMail.Text;
            int neuKundennr = 0;

            // Verifizierungen Textfelder leer?

            if (textKundenAddAnrede.Text.Length == 0)
            {
                textKundenAddLog.AppendText( "Kunde hinzufügen gescheitert, Anrede ist leer \r\n");
                return;
            }

            if (textKundenAddNachname.Text.Length == 0)
            {
                textKundenAddLog.AppendText("Kunde hinzufügen gescheitert, Nachname ist leer \r\n");
                return;
            }

            if (textKundenAddTelnr.Text.Length == 0 && textKundenAddHandy.Text.Length == 0 && textKundenAddMail.Text.Length == 0)
            {
                textKundenAddLog.AppendText("Kunde hinzufügen gescheitert, Mindestens eine Kontaktmöglichkeit (Tel/Handy/Mail) notwendig \r\n");
                return;
            }

            if (textKundenAddStraße.Text.Length == 0)
            {
                textKundenAddLog.AppendText("Kunde hinzufügen gescheitert, Straße ist leer \r\n");
                return;
            }

            if (textKundenAddHausnummer.Text.Length == 0)
            {
                textKundenAddLog.AppendText("Kunde hinzufügen gescheitert, Hausnummer ist leer \r\n");
                return;
            }

            if (textKundenAddPLZ.Text.Length == 0)
            {
                textKundenAddLog.AppendText("Kunde hinzufügen gescheitert, PLZ ist leer \r\n");
                return;
            }

            if (textKundenAddOrt.Text.Length == 0)
            {
                textKundenAddLog.AppendText("Kunde hinzufügen gescheitert, Ort ist leer \r\n");
                return;
            }

            if (textKundenAddLand.Text.Length == 0)
            {
                textKundenAddLog.AppendText("Kunde hinzufügen gescheitert, Land ist leer \r\n");
                return;
            }
            // Alle Felder gefüllt!
            //Verifizierungen Kontaktdaten / Anrede gültig

            if (Regex.IsMatch(textKundenAddTelnr.Text, @"^\d+$") == false)
            {
                textKundenAddLog.AppendText("Kunde hinzufügen gescheitert, Telefonnummer enthält nicht-Zahlen \r\n");
                return;
            }
            else {
                Telefonnummer = textKundenAddTelnr.Text;
            }

            if (Regex.IsMatch(textKundenAddHandy.Text, @"^\d+$") == false)
            {
                textKundenAddLog.AppendText("Kunde hinzufügen gescheitert, Handynummer enthält nicht-Zahlen \r\n");
                return;
            }
            else {
                Handynummer = textKundenAddHandy.Text;
            }

            if (textKundenAddAnrede.Text != "Herr" && textKundenAddAnrede.Text != "Frau" && textKundenAddAnrede.Text != "Familie" && textKundenAddAnrede.Text != "Firma")
            {
                textKundenAddLog.AppendText("Kunde hinzufügen gescheitert, Anrede muss Herr, Frau, Familie oder Firma sein \r\n");
                return;
            }

            if (Program.kontaktCheck(textKundenAddTelnr.Text, textKundenAddHandy.Text, textKundenAddMail.Text)) {
                textKundenAddLog.AppendText("Kunde hinzufügen gescheitert, Mindestens eine Kontaktmöglichkeit (Tel/Handy/Mail) notwendig \r\n");
                return;
            }
            
            //String bauen
            //Vorlage zum Anhängen der Werte
            String insert = "INSERT INTO Kunden (Anrede, Vorname, Nachname, Telefonnummer, Handynummer, Email, Straße, Hausnummer, PLZ, Ort, Land, UserChanged, Erstelldatum, Bemerkung) VALUES (";
            insert += "'" + textKundenAddAnrede.Text + "', ";
            insert += "'" + textKundenAddVorname.Text + "', ";
            insert += "'" + textKundenAddNachname.Text + "', ";
            insert += "'" + Telefonnummer + "', ";
            insert += "'" + Handynummer + "', ";
            insert += "'" + Email + "', ";
            insert += "'" + textKundenAddStraße.Text+ "', ";
            insert += "'" + textKundenAddHausnummer.Text + "', ";
            insert += textKundenAddPLZ.Text + ", ";                 // Ohne '' da Int in der DB, Plz ist immer Int
            insert += "'" + textKundenAddOrt.Text + "', ";
            insert += "'" + textKundenAddLand.Text + "', ";
            insert += "'" + idBearbeitend + "', ";
            insert += "'" + Program.DateMachine(DateTime.Now) + "', ";
            insert += "'" + textKundenAddBemerkung.Text + "'); ";

            // String fertig, absenden
            Program.absender(insert, "Einfügen des Kunden in die DB");

            //Abfrage und Bestädtigungsmeldung.
            MySqlCommand cmdShow = new MySqlCommand("SELECT * FROM Kunden ORDER BY idKunden DESC LIMIT 1;", Program.conn);
            MySqlDataReader rdr;
            try
            {
                rdr = cmdShow.ExecuteReader();
                textKundenAddLog.AppendText("Erfolgreich hinzugefügter Kunde: ");
                while (rdr.Read())
                {
                    textKundenAddLog.Text += rdr[0] + ", " + rdr[2] + ", " + rdr[3] + ", Mit Kontaktdaten " + rdr[4] + ", " + rdr[5] + ", \r\n "
                        + rdr[6] + ", und Adresse " + rdr[7] + ", " + rdr[8] + ", " + rdr[9] + ", " + rdr[10] + ", " + rdr[11] + ", \r\n";
                    }
                neuKundennr += rdr.GetInt32(0);
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                Program.FehlerLog(sqlEx.ToString(), "Hinzugefügten Kunden nicht gefunden \r\n Bereits dokumentiert.");
            }

            // Reset aller Datenfelder

            textKundenAddVorname.Text = String.Empty;
            textKundenAddNachname.Text = String.Empty;
            textKundenAddTelnr.Text = "0";
            textKundenAddHandy.Text = "0";
            textKundenAddMail.Text = String.Empty;
            textKundenAddHausnummer.Text = String.Empty;
            textKundenAddStraße.Text = String.Empty;
            textKundenAddPLZ.Text = String.Empty;
            textKundenAddOrt.Text = String.Empty;
            textKundenAddLand.Text = "Deutschland";

            // Anzeige erstellte Kundennummer
            
            numericKundenNewKundennummer.Value = neuKundennr;

            // Wegen dem neuen Kunden muss die Autocomplete-Kundenliste (Singleton im Program) erneuert werden

            Program.refreshAutocompleteKunden();
        }

        private void buttonUmzug_Click(object sender, EventArgs e)
        {
            if (numericKundenNewKundennummer.Value == 1000) {
                textKundenAddLog.AppendText("Kundennummer 1000 ist ungültig. 'Kunde Hinzufügen' vergessen? \r\n");
                return;
            }
            UmzugAdd umzHinzufuegen = new UmzugAdd();
            umzHinzufuegen.setBearbeiter(idBearbeitend);
            umzHinzufuegen.umzugFuellen(decimal.ToInt32(numericKundenNewKundennummer.Value));
            umzHinzufuegen.Show();
        }
    }
}
