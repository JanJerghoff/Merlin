using Google.Apis.Calendar.v3.Data;
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
        public LaufzettelKartons()
        {
            InitializeComponent();
        }

        private void buttonDrucken_Click(object sender, EventArgs e)
        {
            Events eve = Program.kalenderDatumFinder(dateTransaktion.Value);
            if (eve.Items.Count == 0)
            {
                textLog.AppendText("Anzeige durch " + eve.Items.Count.ToString());
                return;
            }

            foreach (var item in eve.Items)
            {
                if (item.ColorId == "8")
                {
                    textLog.AppendText(item.Summary.ToString() + " " + item.ColorId.ToString() + "\r\n");
                }
            }

            textLog.AppendText("Anzeige durch " + eve.Items.Count.ToString());
        }
    }
}
