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
    public partial class TransaktionenUebersicht : Form
    {
        public TransaktionenUebersicht()
        {
            InitializeComponent();
            abfrage(0);
        }

        private void abfrage (int view) {
            
        }

    }
}
