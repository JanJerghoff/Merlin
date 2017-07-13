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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addForm hinzufügen = new addForm();
            hinzufügen.Show();
            //this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ViewForm suche = new ViewForm();
            suche.Show();
            //this.Hide();
        }
    }
}
