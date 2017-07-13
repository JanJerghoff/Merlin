using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Kartonagen
{
    public partial class ViewForm : Form
    {
        public ViewForm()
        {
            InitializeComponent();
        }

        private void buttonViewKundennr_Click(object sender, EventArgs e)
        {
            //Abruf und Druck

            MySqlCommand cmdRead = new MySqlCommand("SELECT * FROM Kunde WHERE idKunde=" + numericViewKundennr.Value + ";", Program.conn);
            MySqlDataReader rdr;

            string tempName = "fail";
            string tempDate = "fail"; //Dies ist das Umzugsdatum des Kunden, Teil des Kundendatensatzes!
            string tempTelefon = "fail";

            try
            {
                rdr = cmdRead.ExecuteReader();
                while (rdr.Read())
                {
                    tempName = rdr[1].ToString();
                    tempDate = rdr.GetDateTime(2).ToShortDateString().ToString();
                    tempTelefon = rdr.GetInt32(3).ToString();
                }
                rdr.Close();
            }
            catch (Exception sqlEx)
            {
                textLog.Text += sqlEx.ToString();
                return;
            }


        }
    }
}
