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

namespace Mitarbeiter
{
    public partial class Stammdaten_Check : Form
    {
        public Stammdaten_Check()
        {
            InitializeComponent();
        }


        int idBearbeitend;
        int MitarbeiterCheck;

        public void setMitarbeiterCheck(int code) {
            MitarbeiterCheck = code;
        }

        public void setBearbeitend(int code)
        {
            idBearbeitend = code;
        }


        internal void fuellen(int iD)
        {
            textMitarbeiterID.AppendText(iD+"");

            String select = "SELECT * FROM MitarbeiterCheck c, Mitarbeiter m WHERE c.Mitarbeiter_idMitarbeiter = m.idMitarbeiter AND c.Mitarbeiter_idMitarbeiter = " + iD + ";";

            MySqlCommand cmdRead = new MySqlCommand( select ,Program.conn2);
            MySqlDataReader rdr;
            try
            {

                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    // Teil 1
                    MitarbeiterCheck = rdr.GetInt32(0);
                    Abrufen(NPerso, BPerso, Perso, rdr.GetInt32(1));
                    Abrufen(NFuehrerschein, BFuehrerschein, Fuehrerschein, rdr.GetInt32(2));
                    Abrufen(NSozAusweis, BSozAusweis, SozAusweis, rdr.GetInt32(3));
                    Abrufen(NSteuerID, BSteuerID, SteuerID, rdr.GetInt32(4));
                    Abrufen(NFuehrungszeugnis, BFuehrungszeugnis, Fuehrungszeugnis, rdr.GetInt32(5));
                    Abrufen(NSchuhe, BSchuhe, Schuhe, rdr.GetInt32(6));
                    Abrufen(N450, B450, T450, rdr.GetInt32(7));
                    Abrufen(NArbeitsvertrag, BArbeitsvertrag, Arbeitsvertrag, rdr.GetInt32(8));
                    Abrufen(NVerbandbuch, BVerbandbuch, Verbandbuch, rdr.GetInt32(9));
                    Abrufen(NFahrerkarte, BFahrerkarte, Fahrerkarte, rdr.GetInt32(10));
                    Abrufen(NEinweisung, BEinweisung, Einweisung, rdr.GetInt32(11));
                    Abrufen(NEinweisungHueGA, BEinweisungHueGA, EinweisungHueGA, rdr.GetInt32(12));
                    Abrufen(NNaviAnleitung, BNaviAnleitung, NaviAnleitung, rdr.GetInt32(13));
                    Abrufen(NRuhezeiten, BRuhezeiten, Ruhezeiten, rdr.GetInt32(14));
                    Abrufen(NUnfallbericht, BUnfallbericht, Unfallbericht, rdr.GetInt32(15));
                    Abrufen(NTelefonliste, BTelefonliste, Telefonliste, rdr.GetInt32(16));
                    Abrufen(NBueroschluessel, BBueroschluessel, Bueroschluessel, rdr.GetInt32(17));
                    Abrufen(Nkladde, BKladde, Kladde, rdr.GetInt32(18));
                    Abrufen(NFach, BFach, Fach, rdr.GetInt32(19));
                    Abrufen(NBueroEinfuehrung, BBueroEinfuehrung, BueroEinfuehrung, rdr.GetInt32(20));

                    //Teil 2 - Büro
                    Abrufen(BBueroTelefonliste, NBueroTelefonliste, BueroTelefonliste, rdr.GetInt32(21));
                    Abrufen(BHuesgenMelden, NHuesgenMelden, HuesgenMelden, rdr.GetInt32(22));
                    Abrufen(BOnlineKonto, NOnlineKonto, OnlineKonto, rdr.GetInt32(23));
                    Abrufen(BWunderProbezeit, NWunderProbezeit, WunderProbezeit, rdr.GetInt32(24));
                    Abrufen(BVertragUnterschrift, NVertragUnterschrift, VertragUnterschrift, rdr.GetInt32(25));
                    Abrufen(BSteuerbuero, NSteuerbuero, Steuerbuero, rdr.GetInt32(26));
                    Abrufen(BOrdner, NOrdner, Ordner, rdr.GetInt32(27));

                    // Teil 3 - Kündigung
                    Abrufen(BSchluesselRueck, NSchluesselRueck, SchluesselRueck, rdr.GetInt32(28));
                    Abrufen(BKladdeRueck, NKladdeRueck, KladdeRueck, rdr.GetInt32(29));
                    Abrufen(BSteuerbueroAb, NSteuerbueroAb, SteuerbueroAb, rdr.GetInt32(30));
                    Abrufen(BArbeitszeugnis, NArbeitszeugnis, Arbeitszeugnis, rdr.GetInt32(31));
                    Abrufen(BPapiereSenden, NPapiereSenden, PapiereSenden, rdr.GetInt32(32));

                    // Namensfeld
                    textName.Text = rdr[35].ToString() + ", " + rdr[36].ToString();
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textStammdatenLog.AppendText(sqlEx.ToString());
                return;
            }

        }

        internal void Absenden(Button Druck, Button NichtDruck, TextBox T, String dbName ) {

            String update;

            Druck.Enabled = false;
            NichtDruck.Enabled = false;

            // Unterscheidet welche Sorte Button gedrückt wurde
            if (Druck.Name.ElementAt(0) == 'B')

            {

                T.AppendText("" + Program.BearbeitendAufloesen(idBearbeitend));
                T.Enabled = false;

                update = "UPDATE MitarbeiterCheck SET " + dbName + " = " + idBearbeitend + " WHERE idMitarbeitercheck = " + MitarbeiterCheck + ";";

            }

            else {

                T.AppendText("n. Benötigt " + (Program.BearbeitendAufloesen(idBearbeitend)));
                T.Enabled = false;

                update = "UPDATE MitarbeiterCheck SET " + dbName + " = " + (idBearbeitend+10) + " WHERE idMitarbeitercheck = " + MitarbeiterCheck + ";";
                
            }

            MySqlCommand cmdAdd = new MySqlCommand(update, Program.conn2);
            try
            {
                cmdAdd.ExecuteNonQuery();
            }
            catch (Exception sqlEx)
            {
                textStammdatenLog.AppendText(sqlEx.ToString());
                return;
            }
        }

        internal void Abrufen(Button eins, Button zwei, TextBox T, int value) {

            // Alles sperren und Textfeld besetzen, wenn DB daten enthält.

            String prep = "";

            if (value == 8) {
                return;
            }

            eins.Enabled = false;
            zwei.Enabled = false;

            if (value >= 10) {
                value = value - 10;
                prep += "n. Benötigt ";
            }

            prep += Program.BearbeitendAufloesen(value);
            T.AppendText(prep);
            T.Enabled = false;

        }

        // Temporäre Abkürzung

        private void button2_Click(object sender, EventArgs e)
        {
            Absenden(BPerso, NPerso, Perso, "Perso");
            Absenden(BFuehrerschein, NFuehrerschein, Fuehrerschein, "Fuehrerschein");
            Absenden(BSozAusweis, NSozAusweis, SozAusweis, "SozVersAusweis");
            Absenden(BSteuerID, NSteuerID, SteuerID, "SteuerID");
            Absenden(BFuehrungszeugnis, NFuehrungszeugnis, Fuehrungszeugnis, "FuehrungsZeugnis");
            Absenden(BSchuhe, NSchuhe, Schuhe, "Schuhe");
            Absenden(B450, N450, T450, "GeringfuegigSchein");
            Absenden(BArbeitsvertrag, NArbeitsvertrag, Arbeitsvertrag, "Arbeitsvertrag");
            Absenden(BVerbandbuch, NVerbandbuch, Verbandbuch, "Verbandbuch");
            Absenden(BFahrerkarte, NFahrerkarte, Fahrerkarte, "Fahrerkarte");
            Absenden(BEinweisung, NEinweisung, Einweisung, "Einweisung");
            Absenden(BEinweisungHueGA, NEinweisungHueGA, EinweisungHueGA, "EinarbeitungHueGA");
            Absenden(BNaviAnleitung, NNaviAnleitung, NaviAnleitung, "NaviAnleitung");
            Absenden(BRuhezeiten, NRuhezeiten, Ruhezeiten, "LenkRuhezeit");
            Absenden(BUnfallbericht, NUnfallbericht, Unfallbericht, "Unfallbericht");
            Absenden(BTelefonliste, NTelefonliste, Telefonliste, "Telefonliste");
            Absenden(BBueroschluessel, NBueroschluessel, Bueroschluessel, "Bueroschluessel");
            Absenden(BKladde, Nkladde, Kladde, "Kladde");
            Absenden(BFach, NFach, Fach, "Fach");
            Absenden(BBueroEinfuehrung, NBueroEinfuehrung, BueroEinfuehrung, "EinfuehrungBuero");

        }


        // Temporärer Bürobeschleuniger

        private void buttonTempBuero_Click(object sender, EventArgs e)
        {
            Absenden(BBueroTelefonliste, NBueroTelefonliste, BueroTelefonliste, "TelefonlisteEintrag");
            Absenden(BHuesgenMelden, NHuesgenMelden, HuesgenMelden, "HuesgenMelden");
            Absenden(BOnlineKonto, NOnlineKonto, OnlineKonto, "KontoAnlegen");
            Absenden(BWunderProbezeit, NWunderProbezeit, WunderProbezeit, "ProbezeitWunderlist");
            Absenden(BVertragUnterschrift, NVertragUnterschrift, VertragUnterschrift, "ArbeitsvertragUnterschreiben");
            Absenden(BSteuerbuero, NSteuerbuero, Steuerbuero, "Steuerbuero");
            Absenden(BOrdner, NOrdner, Ordner, "OrdnerAnlegen");
        }

        // Buttonspam
        // Teil 1

        private void NPerso_Click(object sender, EventArgs e)
        {
            Absenden(NPerso, BPerso, Perso, "Perso");
        }

        private void BPerso_Click(object sender, EventArgs e)
        {
            Absenden(BPerso, NPerso, Perso, "Perso");
        }

        private void NFuehrerschein_Click(object sender, EventArgs e)
        {
            Absenden(NFuehrerschein, BFuehrerschein, Fuehrerschein, "Fuehrerschein");
        }

        private void BFuehrerschein_Click(object sender, EventArgs e)
        {
            Absenden(BFuehrerschein, NFuehrerschein, Fuehrerschein, "Fuehrerschein");
        }

        private void NSozAusweis_Click(object sender, EventArgs e)
        {
            Absenden(NSozAusweis, BSozAusweis, SozAusweis, "SozVersAusweis");
        }

        private void BSozAusweis_Click(object sender, EventArgs e)
        {
            Absenden(BSozAusweis, NSozAusweis, SozAusweis, "SozVersAusweis");
        }

        private void NSteuerID_Click(object sender, EventArgs e)
        {
            Absenden(NSteuerID, BSteuerID, SteuerID, "SteuerID");
        }

        private void BSteuerID_Click(object sender, EventArgs e)
        {
            Absenden(BSteuerID, NSteuerID, SteuerID, "SteuerID");
        }

        private void NFuehrungszeugnis_Click(object sender, EventArgs e)
        {
            Absenden(NFuehrungszeugnis, BFuehrungszeugnis, Fuehrungszeugnis, "FuehrungsZeugnis");
        }

        private void BFuehrungszeugnis_Click(object sender, EventArgs e)
        {
            Absenden(BFuehrungszeugnis, NFuehrungszeugnis, Fuehrungszeugnis, "FuehrungsZeugnis");
        }

        private void NSchuhe_Click(object sender, EventArgs e)
        {
            Absenden(NSchuhe, BSchuhe, Schuhe, "Schuhe");
        }

        private void BSchuhe_Click(object sender, EventArgs e)
        {
            Absenden(BSchuhe, NSchuhe, Schuhe, "Schuhe");
        }

        private void N450_Click(object sender, EventArgs e)
        {
            Absenden(N450, B450, T450, "GeringfuegigSchein");
        }

        private void B450_Click(object sender, EventArgs e)
        {
            Absenden(B450, N450, T450, "GeringfuegigSchein");
        }

        private void NArbeitsvertrag_Click(object sender, EventArgs e)
        {
            Absenden(NArbeitsvertrag, BArbeitsvertrag, Arbeitsvertrag, "Arbeitsvertrag");
        }

        private void BArbeitsvertrag_Click(object sender, EventArgs e)
        {
            Absenden(BArbeitsvertrag, NArbeitsvertrag, Arbeitsvertrag, "Arbeitsvertrag");
        }

        private void NVerbandbuch_Click(object sender, EventArgs e)
        {
            Absenden(NVerbandbuch, BVerbandbuch, Verbandbuch, "Verbandbuch");
        }

        private void BVerbandbuch_Click(object sender, EventArgs e)
        {
            Absenden(BVerbandbuch, NVerbandbuch, Verbandbuch, "Verbandbuch");
        }

        private void NFahrerkarte_Click(object sender, EventArgs e)
        {
            Absenden(NFahrerkarte, BFahrerkarte, Fahrerkarte, "Fahrerkarte");
        }

        private void BFahrerkarte_Click(object sender, EventArgs e)
        {
            Absenden(BFahrerkarte, NFahrerkarte, Fahrerkarte, "Fahrerkarte");
        }

        private void NEinweisung_Click(object sender, EventArgs e)
        {
            Absenden(NEinweisung, BEinweisung, Einweisung, "Einweisung");
        }

        private void BEinweisung_Click(object sender, EventArgs e)
        {
            Absenden(BEinweisung, NEinweisung, Einweisung, "Einweisung");
        }

        private void NEinweisungHueGA_Click(object sender, EventArgs e)
        {
            Absenden(NEinweisungHueGA, BEinweisungHueGA, EinweisungHueGA, "EinarbeitungHueGA");
        }

        private void BEinweisungHueGA_Click(object sender, EventArgs e)
        {
            Absenden(BEinweisungHueGA, NEinweisungHueGA, EinweisungHueGA, "EinarbeitungHueGA");
        }

        private void NNaviAnleitung_Click(object sender, EventArgs e)
        {
            Absenden(NNaviAnleitung, BNaviAnleitung, NaviAnleitung, "NaviAnleitung");
        }

        private void BNaviAnleitung_Click(object sender, EventArgs e)
        {
            Absenden(BNaviAnleitung, NNaviAnleitung, NaviAnleitung, "NaviAnleitung");
        }

        private void NRuhezeiten_Click(object sender, EventArgs e)
        {
            Absenden(NRuhezeiten, BRuhezeiten, Ruhezeiten, "LenkRuhezeit");
        }

        private void BRuhezeiten_Click(object sender, EventArgs e)
        {
            Absenden(BRuhezeiten, NRuhezeiten, Ruhezeiten, "LenkRuhezeit");
        }

        private void NUnfallbericht_Click(object sender, EventArgs e)
        {
            Absenden(NUnfallbericht, BUnfallbericht, Unfallbericht, "Unfallbericht");
        }

        private void BUnfallbericht_Click(object sender, EventArgs e)
        {
            Absenden(BUnfallbericht, NUnfallbericht, Unfallbericht, "Unfallbericht");
        }

        private void NTelefonliste_Click(object sender, EventArgs e)
        {
            Absenden(NTelefonliste, BTelefonliste, Telefonliste, "Telefonliste");
        }

        private void BTelefonliste_Click(object sender, EventArgs e)
        {
            Absenden(BTelefonliste, NTelefonliste, Telefonliste, "Telefonliste");
        }

        private void NBueroschluessel_Click(object sender, EventArgs e)
        {
            Absenden(NBueroschluessel, BBueroschluessel, Bueroschluessel, "Bueroschluessel");
        }

        private void BBueroschluessel_Click(object sender, EventArgs e)
        {
            Absenden(BBueroschluessel, NBueroschluessel, Bueroschluessel, "Bueroschluessel");
        }

        private void Nkladde_Click(object sender, EventArgs e)
        {
            Absenden(Nkladde, BKladde, Kladde, "Kladde");
        }

        private void BKladde_Click(object sender, EventArgs e)
        {
            Absenden(BKladde, Nkladde, Kladde, "Kladde");
        }

        private void NFach_Click(object sender, EventArgs e)
        {
            Absenden(NFach, BFach, Fach, "Fach");
        }

        private void BFach_Click(object sender, EventArgs e)
        {
            Absenden(BFach, NFach, Fach, "Fach");
        }

        private void NBueroEinfuehrung_Click(object sender, EventArgs e)
        {
            Absenden(NBueroEinfuehrung, BBueroEinfuehrung, BueroEinfuehrung, "EinfuehrungBuero");
        }

        private void BBueroEinfuehrung_Click(object sender, EventArgs e)
        {
            Absenden(BBueroEinfuehrung, NBueroEinfuehrung, BueroEinfuehrung, "EinfuehrungBuero");
        }

        // Teil 2

        private void NBueroTelefonliste_Click(object sender, EventArgs e)
        {
            Absenden(NBueroTelefonliste, BBueroTelefonliste, BueroTelefonliste, "TelefonlisteEintrag");
        }

        private void BBueroTelefonliste_Click(object sender, EventArgs e)
        {
            Absenden(BBueroTelefonliste, NBueroTelefonliste, BueroTelefonliste, "TelefonlisteEintrag");
        }

        private void NHuesgenMelden_Click(object sender, EventArgs e)
        {
            Absenden(NHuesgenMelden, BHuesgenMelden, HuesgenMelden, "HuesgenMelden");
        }

        private void BHuesgenMelden_Click(object sender, EventArgs e)
        {
            Absenden(BHuesgenMelden, NHuesgenMelden, HuesgenMelden, "HuesgenMelden");
        }

        private void NOnlineKonto_Click(object sender, EventArgs e)
        {
            Absenden(NOnlineKonto, BOnlineKonto, OnlineKonto, "KontoAnlegen");
        }

        private void BOnlineKonto_Click(object sender, EventArgs e)
        {
            Absenden(BOnlineKonto, NOnlineKonto, OnlineKonto, "KontoAnlegen");
        }

        private void NWunderProbezeit_Click(object sender, EventArgs e)
        {
            Absenden(NWunderProbezeit, BWunderProbezeit, WunderProbezeit, "ProbezeitWunderlist");
        }

        private void BWunderProbezeit_Click(object sender, EventArgs e)
        {
            Absenden(BWunderProbezeit, NWunderProbezeit, WunderProbezeit, "ProbezeitWunderlist");
        }

        private void NVertragUnterschrift_Click(object sender, EventArgs e)
        {
            Absenden(NVertragUnterschrift, BVertragUnterschrift, VertragUnterschrift, "ArbeitsvertragUnterschreiben");
        }

        private void BVertragUnterschrift_Click(object sender, EventArgs e)
        {
            Absenden(BVertragUnterschrift, NVertragUnterschrift, VertragUnterschrift, "ArbeitsvertragUnterschreiben");
        }

        private void NSteuerbuero_Click(object sender, EventArgs e)
        {
            Absenden(NSteuerbuero, BSteuerbuero, Steuerbuero, "Steuerbuero");
        }

        private void BSteuerbuero_Click(object sender, EventArgs e)
        {
            Absenden(BSteuerbuero, NSteuerbuero, Steuerbuero, "Steuerbuero");
        }

        private void NOrdner_Click(object sender, EventArgs e)
        {
            Absenden(NOrdner, BOrdner, Ordner, "OrdnerAnlegen");
        }

        private void BOrdner_Click(object sender, EventArgs e)
        {
            Absenden(BOrdner, NOrdner, Ordner, "OrdnerAnlegen");
        }

        // Teil 3 - Entlassung

        private void NSchluesselRueck_Click(object sender, EventArgs e)
        {
            Absenden(NSchluesselRueck, BSchluesselRueck, SchluesselRueck, "SchluesselRueck");
        }

        private void BSchluesselRueck_Click(object sender, EventArgs e)
        {
            Absenden(BSchluesselRueck, NSchluesselRueck, SchluesselRueck, "SchluesselRueck");
        }

        private void NKladdeRueck_Click(object sender, EventArgs e)
        {
            Absenden(NKladdeRueck, BKladdeRueck, KladdeRueck, "KladdeRueck");
        }

        private void BKladdeRueck_Click(object sender, EventArgs e)
        {
            Absenden(BKladdeRueck, NKladdeRueck, KladdeRueck, "KladdeRueck");
        }

        private void NSteuerbueroAb_Click(object sender, EventArgs e)
        {
            Absenden(NSteuerbueroAb, BSteuerbueroAb, SteuerbueroAb, "AbmeldungSteuer");
        }

        private void BSteuerbueroAb_Click(object sender, EventArgs e)
        {
            Absenden(BSteuerbueroAb, NSteuerbueroAb, SteuerbueroAb, "AbmeldungSteuer");
        }

        private void NArbeitszeugnis_Click(object sender, EventArgs e)
        {
            Absenden(NArbeitszeugnis, BArbeitszeugnis, Arbeitszeugnis, "Arbeitszeugnis");
        }

        private void BArbeitszeugnis_Click(object sender, EventArgs e)
        {
            Absenden(BArbeitszeugnis, NArbeitszeugnis, Arbeitszeugnis, "Arbeitszeugnis");
        }

        private void NPapiereSenden_Click(object sender, EventArgs e)
        {
            Absenden(NPapiereSenden, BPapiereSenden, PapiereSenden, "PapiereZusenden");
        }

        private void BPapiereSenden_Click(object sender, EventArgs e)
        {
            Absenden(BPapiereSenden, NPapiereSenden, PapiereSenden, "PapiereZusenden");
        }
    }
}
