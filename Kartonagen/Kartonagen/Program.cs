using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Kartonagen
{
    static class Program
    {
        internal static MySqlConnection conn = new MySqlConnection ("server = 10.0.0.0; user=test;database=kartonagen;port=3306;password=he62okv;");

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Öffne Verbindung
            try
            {
                conn.Open();
            }
            catch (Exception ex) {
                //donothing
            }

            // Öffnen des Fensters
            Application.Run(new mainForm());
        }
    }
}
